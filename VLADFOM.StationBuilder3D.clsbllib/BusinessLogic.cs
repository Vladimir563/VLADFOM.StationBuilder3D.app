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
        bool isAssemblyBuildingContinue = true;
        int indexOfPumpsStationComponentsArray = 0;
        #endregion

        public void StartAssembly(string pumpStationType, string pumpsName, string jockeyPumpsName, string pumpsType, 
            int pumpsCount, double waterConsumption, string controlCabinetsName, int dnSuctionCollector, int dnPressureCollector, 
            int pressureValueForStation, string collectorsMaterialTypeString)
        {
            #region Itialization
            //gets pumps connection from DB (when pump was added in data base successfully)
            int dnPumpsPressureConnection = 40; //gets it from DB
            int dnPumpsSuctionConnection = 50; //gets it from DB

            StationScheme currentScheme = GetStationSchemeByTypeValue(pumpStationType, pumpsCount, pumpsType,
                dnPumpsPressureConnection, dnPumpsSuctionConnection);
            currentScheme.StationType = GetStationTypeEnumByTypesValue(pumpStationType);

            PumpStation pumpStation = new PumpStation(pumpsName, jockeyPumpsName, controlCabinetsName, true, pumpsCount,
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

            for (int i = 0; i < stationComponents.Length; i++)
            {
                if (isAssemblyBuildingContinue) 
                {
                    InsertStationComponentInAssemly(stationComponents[i]);
                    try
                    {
                        //сделать тернарным оператором проверку идет идет ли построение напорной линии для isPressureLineBuilding
                        CreateCoincidentMateForComponentsPlanes(stationComponents[i - 1], stationComponents[i], isPressureLineBuilding);
                        CreateCoincidentMateForComponentsAxis(stationComponents[i - 1], stationComponents[i], isPressureLineBuilding);
                    }
                    catch (Exception e)
                    { Debug.Print(e.Message); }
                }  
            }
        }

        #region UsefulMethods
        public StationTypeEnum GetStationTypeEnumByTypesValue(string typeValueString)
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
            ////open part component
            swModel = (ModelDoc2)swApp.OpenDoc6(stationComponent.PathToTheComponent, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            swApp.ActivateDoc2(stationComponent.ComponentsName + ".sldprt", false, ref errors);

            //if current component not exist in components base
            if (swModel == null) 
            {
                MessageBox.Show($"Компонент {stationComponent.ComponentsName} отсутствует в базе (приложение будет закрыто)", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isAssemblyBuildingContinue = false;
                return;
            }

            //Add the part to the assembly
            swInsertedComponent = (Component2)swAssembly.AddComponent5(stationComponent.PathToTheComponent, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -0.000181145833835217, 0.000107469465717713, 2.25750183631135E-05);
            swApp.CloseDoc(stationComponent.PathToTheComponent);
            TransformData[0] = 1;
            TransformData[1] = 0;
            TransformData[2] = 0;
            TransformData[3] = 0;
            TransformData[4] = 1;
            TransformData[5] = 0;
            TransformData[6] = 0;
            TransformData[7] = 0;
            TransformData[8] = 1;
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

            MateName = "coinc_matePlains_" + stationComponent1.ComponentsName + "_" + stationComponent2.ComponentsName;
            FirstSelection = _isPressureLineBuilding ? "Пл_Вых@" : "Пл_Вх@" + stationComponent1.ComponentsName + "-1" + "@" + AssemblyTitle;
            SecondSelection = _isPressureLineBuilding ? "Пл_Вх@" : "Пл_Вых@" + stationComponent2.ComponentsName + "-1" + "@" + AssemblyTitle;

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

            MateName = "coinc_mateAxis_" + stationComponent1.ComponentsName + "_" + stationComponent2.ComponentsName;
            FirstSelection = _isPressureLineBuilding ? "Ось_Вых@" : "Ось_Вх@" + stationComponent1.ComponentsName + "-1" + "@" + AssemblyTitle;
            SecondSelection = _isPressureLineBuilding ? "Ось_Вх@" : "Ось_Вых@" + stationComponent2.ComponentsName + "-1" + "@" + AssemblyTitle;

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
