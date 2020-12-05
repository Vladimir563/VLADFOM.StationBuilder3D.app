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
        public SldWorks swApp;
        ModelDoc2 swModel = default(ModelDoc2);
        AssemblyDoc swAssembly = default(AssemblyDoc);
        Component2 swInsertedComponent = default(Component2);
        MathUtility swMathUtil = default(MathUtility);
        MathTransform swTransform = default(MathTransform);
        ModelDocExtension swModelDocExt = default(ModelDocExtension);
        Mate2 swMate = default(Mate2);
        Mate2 swLinkedMate = default(Mate2);
        Component2 swComp = default(Component2);
        SelectionMgr swSelectionManager = default(SelectionMgr);
        Feature swFeature = default(Feature);
        bool status = false;
        int errors = 0;
        int warnings = 0;
        string fileName = null;
        double swSheetWidth = 0;
        double swSheetHeight = 0;
        string AssemblyTitle = null;
        double[] TransformData = new double[16];
        object TransformDataObj = null;
        CustomPropertyManager swCustProp = default(CustomPropertyManager);
        Measure swMeasure = default(Measure);
        string firstSelection;
        string secondSelection;
        bool isContinueAssemblyBuilding = true;

        public void AddComponentInStationAssembly() 
        {
            ////Add the part to the assembly
            //swInsertedComponent = (Component2)swAssembly.AddComponent5(fileName, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -0.000181145833835217, 0.000107469465717713, 2.25750183631135E-05);
            //swApp.CloseDoc(fileName);
            //TransformData[0] = 1;
            //TransformData[1] = 0;
            //TransformData[2] = 0;
            //TransformData[3] = 0;
            //TransformData[4] = 1;
            //TransformData[5] = 0;
            //TransformData[6] = 0;
            //TransformData[7] = 0;
            //TransformData[8] = 1;
            //TransformData[9] = 0;
            //TransformData[10] = 0;
            //TransformData[11] = 0;
            //TransformData[12] = 1;
            //TransformData[13] = 0;
            //TransformData[14] = 0;
            //TransformData[15] = 0;
            //TransformDataObj = (object)TransformData;
            //swMathUtil = (MathUtility)swApp.GetMathUtility();
            //swTransform = (MathTransform)swMathUtil.CreateTransform((TransformDataObj));
            //status = swInsertedComponent.SetTransformAndSolve2(swTransform);

            ////Open and add another part to the assembly
            //fileName = "C:\\Users\\Public\\Documents\\SOLIDWORKS\\SOLIDWORKS 2018\\samples\\tutorial\\api\\beam with holes.sldprt";
            //swApp.OpenDoc6(fileName, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_AutoMissingConfig, "", ref errors, ref warnings);
            //swModel = (ModelDoc2)swApp.ActivateDoc3(AssemblyTitle, true, 0, ref errors);
            //swAssembly = (AssemblyDoc)swModel;
            //swInsertedComponent = (Component2)swAssembly.AddComponent5(fileName, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -0.0543800538871437, -0.10948954091873, 0.0944189997389913);
            //swApp.CloseDoc(fileName);
            //TransformData[0] = 1;
            //TransformData[1] = 0;
            //TransformData[2] = 0;
            //TransformData[3] = 0;
            //TransformData[4] = 1;
            //TransformData[5] = 0;
            //TransformData[6] = 0;
            //TransformData[7] = 0;
            //TransformData[8] = 1;
            //TransformData[9] = -0.179380053887144;
            //TransformData[10] = -0.0894895409187302;
            //TransformData[11] = 0.0744189997389913;
            //TransformData[12] = 1;
            //TransformData[13] = 0;
            //TransformData[14] = 0;
            //TransformData[15] = 0;
            //TransformDataObj = (object)TransformData;
            //swMathUtil = (MathUtility)swApp.GetMathUtility();
            //swTransform = (MathTransform)swMathUtil.CreateTransform((TransformDataObj));
        }

        public void StartAssembly(string pumpStationType, string pumpsName, string jockeyPumpsName, string pumpsType, 
            int pumpsCount, double waterConsumption, string controlCabinetsName, int dnSuctionCollector, int dnPressureCollector, 
            int pressureValueForStation, string collectorsMaterialTypeString)
        {
            //gets pumps connection from DB (when pump was added in data base successfully)
            int dnPumpsPressureConnection = 40; //gets it from DB
            int dnPumpsSuctionConnection = 50; //gets it from DB

            StationScheme currentScheme = GetStationSchemeByTypeValue(pumpStationType, pumpsCount, pumpsType, 
                dnPumpsPressureConnection, dnPumpsSuctionConnection);
            currentScheme.StationType = GetStationTypeEnumByTypesValue(pumpStationType);

            PumpStation pumpStation = new PumpStation(pumpsName, jockeyPumpsName, controlCabinetsName, true, pumpsCount, 
                waterConsumption, dnSuctionCollector, dnPressureCollector, pressureValueForStation,
                GetCollectorsMaterialEnumByTypesValue(collectorsMaterialTypeString), currentScheme);

            //getting the SldWorks current instance
            swApp = (SldWorks)Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application"));

            //Create an assembly
            swSheetWidth = 0;
            swSheetHeight = 0;
            swAssembly = (AssemblyDoc)swApp.NewDocument("C:\\ProgramData\\SolidWorks\\SOLIDWORKS 2020\\templates\\Сборка.asmdot", 0, swSheetWidth, swSheetHeight);
            swModel = (ModelDoc2)swAssembly;
            AssemblyTitle = swModel.GetTitle();

            foreach (var component in pumpStation.stationComponents)
            {
                if (isContinueAssemblyBuilding) 
                {
                    InsertComponent(component.Value);
                }
            }
        }

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

        public void InsertComponent(PumpStationComponent stationComponent) 
        {
            #region Add the component to the assembly
            //Open the component
            fileName = stationComponent.PathToTheComponent;
            swModel = (ModelDoc2)swApp.OpenDoc6(fileName, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            if (swModel == null) 
            {
                MessageBox.Show($"Компонент {stationComponent.ComponentsName} отсутствует в базе (приложение будет закрыто)", "Ошибка доступа к компоненту", MessageBoxButtons.OK, MessageBoxIcon.Error);
                swApp.CloseDoc(AssemblyTitle);
                isContinueAssemblyBuilding = false;
                return;
            }

            // Get the custom property data from opened component
            swModel = (ModelDoc2)swApp.ActiveDoc;
            swModelDocExt = swModel.Extension;
            swCustProp = swModelDocExt.get_CustomPropertyManager("");
            string componentsWeight = swCustProp.Get("weight");
            //stationComponent.ComponentsWeight = double.Parse(componentsWeight);

            //Insert the component to the assembly
            swInsertedComponent = (Component2)swAssembly.AddComponent5(fileName, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", 0, 0, 0);
            firstSelection = swInsertedComponent.Name;
            swApp.CloseDoc(fileName);
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

            #endregion
        }
    }
}

////measuring distance from opened component (if we need)
//double exampleDistVal = 0;
//status = swModelDocExt.SelectByID2("Спереди", "PLANE", -0.00382117299216134, -0.0032246917626253, -0.00153854043344381, false, 0, null, 0);
//status = swModelDocExt.SelectByID2("Plane1", "PLANE", 0.00547600669648318, 0.00252191841298099, 0.0050000000000523, true, 0, null, 0);
//swMeasure = (Measure)swModelDocExt.CreateMeasure();
////Can set this to 0
//// 0 = center to center
//// 1 = minimum distance
//// 2 = maximum distance
//swMeasure.ArcOption = 0;

//status = swMeasure.Calculate(null);

//if ((status))
//{
//    if ((!(swMeasure.Distance == -1)))
//    {
//        Debug.Print("Distance: " + swMeasure.Distance * 1000);
//        exampleDistVal = swMeasure.Distance * 1000;
//    }
//}
//else
//{
//    Debug.Print("Invalid combination of selected entities.");
//}
//swModel.ClearSelection2(true);




//pumpStation.Pump.RightSidePlaneDistance = 56;
//pumpStation.Pump.ComponentsWeight = 156;

//clslib.Frame frame = (clslib.Frame)pumpStation.stationComponents[StationComponentsTypeEnum.Рама_];
//frame.ComponentsName = ComponentsValCalculator.GetFramesFullName(pumpStation);
//frame.PathToTheComponent = ComponentsValCalculator.GetFullPathToTheComponent(pumpStation, frame);