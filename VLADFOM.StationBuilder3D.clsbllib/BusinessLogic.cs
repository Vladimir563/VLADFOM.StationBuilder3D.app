using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using StationBuilder3DAddin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VLADFOM.StationBuilder3D.clslib;

namespace VLADFOM.StationBuilder3D.clsbllib
{
    public class BusinessLogic
    {
        #region Variables
        SldWorks swApp;
        ModelDoc2 swModel = default(ModelDoc2);
        AssemblyDoc swAssembly = default(AssemblyDoc);
        string AssemblyTitle;
        int swSheetWidth;
        int swSheetHeight;
        private int errors;
        private int warnings;
        double[] TransformData = new double[16];
        Component2 swInsertedComponent = default(Component2);
        public object TransformDataObj { get; private set; }
        private MathUtility swMathUtil;
        private MathTransform swTransform;
        private bool status;
        private ModelDocExtension swDocExt;
        private bool boolstat;
        private Feature matefeature;
        private int mateError;
        bool isPressureLineBuilding;
        bool isComponentsPlanesReverseMate;
        bool isAssemblyBuildingContinue = true;
        int indexOfPumpsStationComponentsArray = 0;
        #endregion

        public void StartAssembly(string pumpStationType, string pumpsName, string jockeyPumpsName, string pumpsType, 
            int pumpsCount, double waterConsumption, string controlCabinetsName, int dnSuctionCollector, int dnPressureCollector, 
            int pressureValueForStation, string collectorsMaterialTypeString)
        {
            #region Initialization
            //gets pumps connection from DB (when pump was added in data base successfully)

            //инициализируем пути из xml
            ComponentsLocationPaths locationPaths = new ComponentsLocationPaths();

            //создаем обьекты насосов (главный, жокей) все данные для создание обьектов берем из ДБ 
            //(в которую будут заноситься все данные из модели при валидации)
            Pump mainPump = new Pump(locationPaths, pumpsName, 0, 0, 0, StationComponentsTypeEnum.Насос_основной);
            Pump jockeyPump = new Pump(locationPaths, "", 0, 0, 0, StationComponentsTypeEnum.Насос_жокей);
            //getting all properties of pump we need from db
            #region For test
            //taking all these propeties form pumps DB (they will be available after pump's validation process and addition in database)
            mainPump.ComponentsWeight = 210;
            mainPump.PumpsWidth = 300;
            mainPump.PressureSideDn = 40;
            mainPump.SuctionSideDn = 50;

            jockeyPump.ComponentsWeight = 50;
            jockeyPump.PressureSideDn = 2;
            jockeyPump.SuctionSideDn = 2;
            #endregion


            StationScheme currentScheme = GetStationSchemeByTypeValue(pumpStationType, pumpsCount, pumpsType,
                mainPump.PressureSideDn, mainPump.SuctionSideDn);

            currentScheme.StationType = GetStationTypeEnumByTypeValue(pumpStationType);


            PumpStation pumpStation = new PumpStation( locationPaths, mainPump, jockeyPump, controlCabinetsName, true, pumpsCount,
                waterConsumption, dnSuctionCollector, dnPressureCollector, pressureValueForStation,
                GetCollectorsMaterialEnumByTypesValue(collectorsMaterialTypeString), currentScheme);
            #endregion      


            //getting the SldWorks current instance
            swApp = (SldWorks)Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application"));

            //Create an assembly
            swSheetWidth = 0;
            swSheetHeight = 0;
            swAssembly = (AssemblyDoc)swApp.NewDocument("C:\\ProgramData\\SolidWorks\\SOLIDWORKS 2020\\templates\\Сборка.asmdot", 0, swSheetWidth, swSheetHeight);
            swModel = (ModelDoc2)swAssembly;
            AssemblyTitle = swModel.GetTitle();

            PumpStationComponent[] stationComponents = new PumpStationComponent[pumpStation.stationComponents.Count];

            foreach (var component in pumpStation.stationComponents.Values)
            {
                stationComponents[indexOfPumpsStationComponentsArray++] = component;
            }


            foreach (var component in stationComponents)
            {
                Console.WriteLine(component.ComponentsName);
                Console.WriteLine(component.PathToTheComponent);
                Console.WriteLine("----------------------------");
            }


            for (int i = 0; i < stationComponents.Length; i++)
            {

                if (!isAssemblyBuildingContinue) return;

                InsertStationComponentInAssemly(stationComponents[i]);

                try
                {
                    isPressureLineBuilding = stationComponents[i].IsComponentForTheNewLine.Equals(2) ? true : false;
                    isComponentsPlanesReverseMate = stationComponents[i].IsComponentForTheNewLine.Equals(1) ? true : false;
                    if (!isAssemblyBuildingContinue) return;

                    if (isPressureLineBuilding)
                    {
                        CreateCoincidentMateForComponentsPlanes(stationComponents[0], stationComponents[i], true);
                        CreateCoincidentMateForComponentsAxis(stationComponents[0], stationComponents[i], true);
                        continue;
                    }

                    CreateCoincidentMateForComponentsPlanes(stationComponents[i - 1], stationComponents[i], isComponentsPlanesReverseMate);
                    CreateCoincidentMateForComponentsAxis(stationComponents[i - 1], stationComponents[i], isComponentsPlanesReverseMate);
                }
                catch (Exception e)
                { Debug.Print(e.Message); }
                
            }
        }

        #region UsefulMethods
        public StationTypeEnum GetStationTypeEnumByTypeValue(string typeValueString)
        {
            if (typeValueString.Equals(StationTypeEnum.Пожаротушения.ToString()))
            {
                return StationTypeEnum.Пожаротушения;
            }
            else if (typeValueString.Equals(StationTypeEnum.Повышения_давления.ToString()))
            {
                return StationTypeEnum.Повышения_давления;
            }
            else if (typeValueString.Equals(StationTypeEnum.Совмещённая.ToString()))
            {
                return StationTypeEnum.Совмещённая;
            }
            else if (typeValueString.Equals(StationTypeEnum.Мультидрайв.ToString()))
            {
                return StationTypeEnum.Мультидрайв;
            }
            else if (typeValueString.Equals(StationTypeEnum.Ф_Драйв.ToString()))
            {
                return StationTypeEnum.Ф_Драйв;
            }

            throw new Exception();
        }

        public CollectorsMaterialEnum GetCollectorsMaterialEnumByTypesValue(string typeValueString)
        {
            if (typeValueString.Equals(CollectorsMaterialEnum.Нержавеющая_сталь.ToString()))
            {
                return CollectorsMaterialEnum.Нержавеющая_сталь;
            }
            else if (typeValueString.Equals(CollectorsMaterialEnum.Чёрная_сталь.ToString()))
            {
                return CollectorsMaterialEnum.Чёрная_сталь;
            }
            throw new Exception();
        }

        public StationScheme GetStationSchemeByTypeValue(string pumpStationType,
            int pumpsCount, string pumpsType, int dnPumpsPressureConnection, int dnPumpsSuctionConnection)
        {
            if (pumpStationType.Equals(StationTypeEnum.Пожаротушения.ToString()) && pumpsCount == 2 && pumpsType.Equals(PumpsType.Горизонтальный.ToString()))
            {
                return StationScheme.GetSimpleFireProtectionScheme2HorizontalPumps(dnPumpsPressureConnection, dnPumpsSuctionConnection);
            }
            else if (pumpStationType.Equals(StationTypeEnum.Пожаротушения.ToString()) && pumpsCount > 2 && pumpsType.Equals(PumpsType.Горизонтальный.ToString()))
            {
                return StationScheme.GetSimpleFireProtectionSchemeMoreThan2HorizontalPumps(dnPumpsPressureConnection, dnPumpsSuctionConnection);
            }
            else if (pumpStationType.Equals(StationTypeEnum.Пожаротушения.ToString()) && pumpsCount == 2 && pumpsType.Equals(PumpsType.Вертикальный.ToString()))
            {
                return StationScheme.GetSimpleFireProtectionScheme2VerticalPumps(dnPumpsPressureConnection, dnPumpsSuctionConnection);
            }
            else if (pumpStationType.Equals(StationTypeEnum.Пожаротушения.ToString()) && pumpsCount > 2 && pumpsType.Equals(PumpsType.Вертикальный.ToString()))
            {
                return StationScheme.GetSimpleFireProtectionSchemeMoreThan2VerticalPumps(dnPumpsPressureConnection, dnPumpsSuctionConnection);
            }

            throw new Exception();
        }
        #endregion

        public void InsertStationComponentInAssemly(PumpStationComponent stationComponent) 
        {
            //open part component
            swModel = (ModelDoc2)swApp.OpenDoc6(stationComponent.PathToTheComponent, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            swApp.ActivateDoc2(stationComponent.ComponentsName + ".sldprt", false, ref errors);

            //if current component not exist in components base
            if (swModel == null) 
            {
                MessageBox.Show($"Компонент {stationComponent.ComponentsName} отсутствует в базе (приложение будет закрыто)", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isAssemblyBuildingContinue = false;
                swApp.CloseDoc(AssemblyTitle);
                return;
            }

            //Add the part to the assembly
            swInsertedComponent = (Component2)swAssembly.AddComponent5(stationComponent.PathToTheComponent, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -0.000181145833835217, 0.000107469465717713, 2.25750183631135E-05);
            stationComponent.NameInAssebmly = swInsertedComponent.Name2;
            swApp.CloseDoc(stationComponent.PathToTheComponent);
            TransformData[0] = stationComponent.RotationByX1;
            TransformData[1] = stationComponent.RotationByX2;
            TransformData[2] = stationComponent.RotationByX3;
            TransformData[3] = stationComponent.RotationByY1;
            TransformData[4] = stationComponent.RotationByY2;
            TransformData[5] = stationComponent.RotationByY3;
            TransformData[6] = stationComponent.RotationByZ1;
            TransformData[7] = stationComponent.RotationByZ2;
            TransformData[8] = stationComponent.RotationByZ3;
            TransformData[9] = 0;
            TransformData[10] = 0;
            TransformData[11] = 0;
            TransformData[12] = 1;
            TransformData[13] = 0;
            TransformData[14] = 0;
            TransformData[15] = 0;
            TransformDataObj = (object)TransformData;
            swMathUtil = (MathUtility)swApp.GetMathUtility();
            swTransform = (MathTransform)swMathUtil.CreateTransform((TransformDataObj));
            status = swInsertedComponent.SetTransformAndSolve2(swTransform);
        }

        public void CreateCoincidentMateForComponentsPlanes(PumpStationComponent stationComponent1, PumpStationComponent stationComponent2, 
            bool _isPressureLineBuilding) 
        {
            if (stationComponent1 == null || stationComponent2 == null) return;

            // Create the name of the mate and the names of the planes to use for the mate
            swModel = (ModelDoc2)swApp.ActivateDoc3(AssemblyTitle, true, (int)swRebuildOnActivation_e.swUserDecision, ref errors);

            string MateName;
            string FirstSelection;
            string SecondSelection;
            string line1PlaneName = _isPressureLineBuilding ? "Пл_Вых@" : "Пл_Вх@";
            string line2PlaneName = _isPressureLineBuilding ? "Пл_Вх@" : "Пл_Вых@";

            MateName = "coinc_matePlains_" + stationComponent1.NameInAssebmly + "_" + stationComponent2.NameInAssebmly;
            FirstSelection = line1PlaneName + stationComponent1.NameInAssebmly + "@" + AssemblyTitle;
            SecondSelection = line2PlaneName + stationComponent2.NameInAssebmly + "@" + AssemblyTitle;

            swModel.ClearSelection2(true);
            swDocExt = (ModelDocExtension)swModel.Extension;

            // Select the planes for the mate
            boolstat = swDocExt.SelectByID2(FirstSelection, "PLANE", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);
            boolstat = swDocExt.SelectByID2(SecondSelection, "PLANE", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);

            // Add the mate
            matefeature = (Feature)swAssembly.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignCLOSEST, false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);
            matefeature.Name = MateName;

        }

        public void CreateCoincidentMateForComponentsAxis(PumpStationComponent stationComponent1, PumpStationComponent stationComponent2,
            bool _isPressureLineBuilding) 
        {
            if (stationComponent1 == null || stationComponent2 == null) return;

            // Create the name of the mate and the names of the planes to use for the mate
            swModel = (ModelDoc2)swApp.ActivateDoc3(AssemblyTitle, true, (int)swRebuildOnActivation_e.swUserDecision, ref errors);

            string MateName;
            string FirstSelection;
            string SecondSelection;
            string line1AxisName = _isPressureLineBuilding ? "Ось_Вых@" : "Ось_Вх@";
            string line2AxisName = _isPressureLineBuilding ? "Ось_Вх@" : "Ось_Вых@";

            MateName = "coinc_mateAxis_" + stationComponent1.NameInAssebmly + "_" + stationComponent2.ComponentsName;
            FirstSelection = line1AxisName + stationComponent1.NameInAssebmly + "@" + AssemblyTitle;
            SecondSelection = line2AxisName + stationComponent2.NameInAssebmly + "@" + AssemblyTitle;

            swModel.ClearSelection2(true);
            swDocExt = (ModelDocExtension)swModel.Extension;

            // Select the planes for the mate
            boolstat = swDocExt.SelectByID2(FirstSelection, "AXIS", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);
            boolstat = swDocExt.SelectByID2(SecondSelection, "AXIS", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);

            // Add the mate
            matefeature = (Feature)swAssembly.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignCLOSEST, false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);
            matefeature.Name = MateName;
        }

    }
}
