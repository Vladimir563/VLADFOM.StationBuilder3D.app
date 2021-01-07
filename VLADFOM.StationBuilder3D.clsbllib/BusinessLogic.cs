using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VLADFOM.StationBuilder3D.clslib;
using Frame = VLADFOM.StationBuilder3D.clslib.Frame;

namespace VLADFOM.StationBuilder3D.clsbllib
{
    public class BusinessLogic
    {
        #region Variables
        private Pump mainPump = null;
        private Pump jockeyPump = null;
        private ComponentsLocationPaths locationPaths = null;
        private StationScheme currentScheme = null;
        private PumpStation pumpStation = null;
        private StationComponent[] stationComponentsArray = null;
        public static Frame frame = null;
        public static ControlCabinet controlCabinet = null;
        public bool isControlCabStandAlone = false;
        public string controlCabinetStAlonePosition = string.Empty;
        private string componentConfigName = string.Empty;
        public static double pumpStationWeight = 0;
        private string controlCabinetName = string.Empty;

        //SOLIDWORKS variables
        private SldWorks swApp;
        private ModelDoc2 swModel = default;
        private AssemblyDoc swAssembly = default;
        private string AssemblyTitle;
        private int swSheetWidth;
        private int swSheetHeight;
        private int errors;
        private int warnings;
        private readonly double[] TransformData = new double[16];
        private Component2 swInsertedComponent = default;
        private MathUtility swMathUtil;
        private MathTransform swTransform;
        private bool status;
        private ModelDocExtension swDocExt;
        public bool boolstat;
        private Feature matefeature;
        public int mateError;
        public object TransformDataObj { get; private set; }
        public static bool isAssemblyBuildingContinue = true;
        private FeatureManager swFeatureManager;
        public Feature swFeature;
        private CustomPropertyManager swCustProp = default;
        private Measure swMeasure = default;
        #endregion

        public void StartAssembly(string pumpStationType, string pumpsName, string jockeyPumpsName, string pumpsType, 
            int pumpsCount, double waterConsumption, string controlCabinetsName, bool isControlCabinetStandAlone, 
            string controlCabStAlonePosition, int dnSuctionCollector, int dnPressureCollector, int pressureValueForStation, 
            string collectorsMaterialTypeString)
        {
            locationPaths = new ComponentsLocationPaths();
            mainPump = new Pump(locationPaths, pumpsName, 0, 0, 0, StationComponentTypeEnum.Насос_основной);
            mainPump.PumpsType = GetPumpsTypeEnumByValue(pumpsType);
            jockeyPump = new Pump(locationPaths, jockeyPumpsName, 0, 0, 0, StationComponentTypeEnum.Насос_жокей);
            isControlCabStandAlone = isControlCabinetStandAlone;
            controlCabinetStAlonePosition = controlCabStAlonePosition;
            controlCabinetName = controlCabinetsName;

            #region Assembly creation
            //getting the SldWorks current instance
            swApp = (SldWorks)Activator.CreateInstance(Type.GetTypeFromProgID("SldWorks.Application"));

            //Create an assembly
            swSheetWidth = 0;
            swSheetHeight = 0;
            swAssembly = (AssemblyDoc)swApp.NewDocument(locationPaths.componentsLocationPaths["assemblyPatternLocationPath"], 0, swSheetWidth, swSheetHeight);
            swModel = (ModelDoc2)swAssembly;
            AssemblyTitle = swModel.GetTitle();
            #endregion

            #region MainPumpInitialization
            swModel = (ModelDoc2)swApp.OpenDoc6(mainPump.ComponentLocationPath, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
            GetComponentPropertyData(mainPump);
            #endregion

            Task InitializationTask = new Task(delegate () 
                {
                    //taking main pump's and jockey pump's parameters from database (connection to database method needs) and initialize properties we need

                    //need to know DN suction connection by station
                    currentScheme = GetStationSchemeByTypeValue(pumpStationType, pumpsCount, pumpsType, mainPump.PressureSideDn, 
                        mainPump.SuctionSideDn, ComponentsValCalculator.GetStationConnectionDnByConsumption(waterConsumption));

                    currentScheme.StationType = GetStationTypeEnumByTypeValue(pumpStationType);

                    pumpStation = new PumpStation(locationPaths, mainPump, jockeyPump, controlCabinetsName, true, pumpsCount,
                        waterConsumption, dnSuctionCollector, dnPressureCollector, pressureValueForStation,
                        GetCollectorsMaterialEnumByTypesValue(collectorsMaterialTypeString), currentScheme);

                    stationComponentsArray = pumpStation.stationComponents.ToArray();

                    Console.WriteLine(pumpStation.StationScheme.StationType);

                    foreach (var e in stationComponentsArray) 
                    {
                        Console.WriteLine(e.ComponentName);
                        Console.WriteLine(e.ComponentLocationPath);
                        Console.WriteLine("---------------------");
                    }
                }
            );
            //start the Initialization task
            InitializationTask.Start();

            //adding the MainPump to the assembly
            InsertStationComponentInAssemly(mainPump);

            #region Getting components arrays for different lines
            StationComponent[] suctionLineComponents = pumpStation.stationComponents.Where(component => component.IsComponentForPressureLine.Equals(false)
                && !component.ComponentType.Equals(StationComponentTypeEnum.Рама_) && !component.ComponentType.Equals(StationComponentTypeEnum.ШУ_)).ToArray();

            StationComponent[] pressureLineComponents = pumpStation.stationComponents.Where(component => component.IsComponentForPressureLine.Equals(true)).ToArray();
            #endregion


            #region AddSuctionComponents
            Task addSuctionComponentsTask = new Task(delegate ()
            {
                for (int i = 1; i < suctionLineComponents.Length; i++)
                {
                    if (!isAssemblyBuildingContinue) return;

                    //open part component
                    swModel = (ModelDoc2)swApp.OpenDoc6(suctionLineComponents[i].ComponentLocationPath, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
                    swApp.ActivateDoc2(suctionLineComponents[i].ComponentName + ".sldprt", false, ref errors);

                    //if current component not exist in components base
                    if (swModel == null)
                    {
                        isAssemblyBuildingContinue = false;
                        MessageBox.Show($"Компонент {suctionLineComponents[i].ComponentName} отсутствует в базе (приложение будет закрыто)", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        swApp.CloseDoc(AssemblyTitle);
                        return;
                    }

                    //Add the part to the assembly
                    swInsertedComponent = (Component2)swAssembly.AddComponent5(suctionLineComponents[i].ComponentLocationPath, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -0.000181145833835217, 0.000107469465717713, 2.25750183631135E-05);
                    //getting the current component property data
                    GetComponentPropertyData(suctionLineComponents[i]);
                    suctionLineComponents[i].ComponentNameInAssembly = swInsertedComponent.Name2;
                    swApp.CloseDoc(suctionLineComponents[i].ComponentLocationPath);
                    TransformData[0] = suctionLineComponents[i].RotationComponentArray[0];
                    TransformData[1] = suctionLineComponents[i].RotationComponentArray[1];
                    TransformData[2] = suctionLineComponents[i].RotationComponentArray[2];
                    TransformData[3] = suctionLineComponents[i].RotationComponentArray[3];
                    TransformData[4] = suctionLineComponents[i].RotationComponentArray[4];
                    TransformData[5] = suctionLineComponents[i].RotationComponentArray[5];
                    TransformData[6] = suctionLineComponents[i].RotationComponentArray[6];
                    TransformData[7] = suctionLineComponents[i].RotationComponentArray[7];
                    TransformData[8] = suctionLineComponents[i].RotationComponentArray[8];
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
            });
            #endregion


            #region AddPressureComponents
            Task addPressureComponentsTask = new Task(delegate ()
            {
                for (int i = 0; i < pressureLineComponents.Length; i++)
                {
                    if (!isAssemblyBuildingContinue) return;
                    //open part component
                    swModel = (ModelDoc2)swApp.OpenDoc6(pressureLineComponents[i].ComponentLocationPath, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
                    swApp.ActivateDoc2(pressureLineComponents[i].ComponentName + ".sldprt", false, ref errors);
                    //if current component not exist in components base
                    if (swModel == null)
                    {
                        isAssemblyBuildingContinue = false;
                        MessageBox.Show($"Компонент {pressureLineComponents[i].ComponentName} отсутствует в базе (приложение будет закрыто)", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        swApp.CloseDoc(AssemblyTitle);
                        return;
                    }

                    //Add the part to the assembly
                    swInsertedComponent = (Component2)swAssembly.AddComponent5(pressureLineComponents[i].ComponentLocationPath, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -0.000181145833835217, 0.000107469465717713, 2.25750183631135E-05);
                    //getting the current component property data
                    GetComponentPropertyData(pressureLineComponents[i]);
                    pressureLineComponents[i].ComponentNameInAssembly = swInsertedComponent.Name2;
                    swApp.CloseDoc(pressureLineComponents[i].ComponentLocationPath);
                    TransformData[0] = pressureLineComponents[i].RotationComponentArray[0];
                    TransformData[1] = pressureLineComponents[i].RotationComponentArray[1];
                    TransformData[2] = pressureLineComponents[i].RotationComponentArray[2];
                    TransformData[3] = pressureLineComponents[i].RotationComponentArray[3];
                    TransformData[4] = pressureLineComponents[i].RotationComponentArray[4];
                    TransformData[5] = pressureLineComponents[i].RotationComponentArray[5];
                    TransformData[6] = pressureLineComponents[i].RotationComponentArray[6];
                    TransformData[7] = pressureLineComponents[i].RotationComponentArray[7];
                    TransformData[8] = pressureLineComponents[i].RotationComponentArray[8];
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
            });
            #endregion

            addSuctionComponentsTask.Start();
            addSuctionComponentsTask.Wait();

            addPressureComponentsTask.Start();

            //getting the frame and the control cabinet components from station components array
            frame = (Frame)Array.Find(stationComponentsArray, comp => comp.ComponentType.Equals(StationComponentTypeEnum.Рама_));
            controlCabinet = (ControlCabinet)Array.Find(stationComponentsArray, comp => comp.ComponentType.Equals(StationComponentTypeEnum.ШУ_));

            //insert control cabinet and frame components
            InsertStationComponentInAssemly(frame);
            InsertStationComponentInAssemly(controlCabinet);

            addPressureComponentsTask.Wait();

            //control cabinet, frame and pump mates creation
            CreateCoincidentMateForComponentPlanes(mainPump, frame);
            CreateCoincidentMateForComponentAxis(mainPump, frame);
            CreateCoincidentMateForComponentPlanes(controlCabinet, frame);
            CreateCoincidentMateForComponentAxis(controlCabinet, frame);

            #region CreateMatesForTheSuctionLine
            Task createMatesForTheSuctionLine = new Task(delegate
            {
                for (int i = 0; i < suctionLineComponents.Length; i++)
                {
                    try
                    {
                        if (suctionLineComponents[i - 1] == null || suctionLineComponents[i] == null || !isAssemblyBuildingContinue) return;

                        #region CoincidentPlanesMate
                        // Create the name of the mate and the names of the planes to use for the mate
                        swModel = (ModelDoc2)swApp.ActivateDoc3(AssemblyTitle, true, (int)swRebuildOnActivation_e.swUserDecision, ref errors);

                        StringBuilder planesMateName = new StringBuilder("coinc_matePlains_");
                        StringBuilder firstSelectionByPlanes = new StringBuilder("Пл_Вх@");
                        StringBuilder secondSelectionByPlanes = new StringBuilder("Пл_Вых@");

                        if (pumpStation.StationScheme.StationType.Equals(StationTypeEnum.Пожаротушения) &&
                            suctionLineComponents[i].ComponentType.Equals(StationComponentTypeEnum.ФланцевыйПереход_1))
                        {
                            firstSelectionByPlanes = new StringBuilder("Пл_Вх@");
                            secondSelectionByPlanes = new StringBuilder("Пл_Вх@");
                        }
                        else if (suctionLineComponents[i - 1].ComponentType.Equals(StationComponentTypeEnum.ФланцевыйПереход_1) &&
                            pumpStation.StationScheme.StationType.Equals(StationTypeEnum.Пожаротушения))
                        {
                            firstSelectionByPlanes = new StringBuilder("Пл_Вых@");
                            secondSelectionByPlanes = new StringBuilder("Пл_Вых@");
                        }

                        planesMateName.AppendFormat("{0}_{1}", suctionLineComponents[i - 1].ComponentNameInAssembly, suctionLineComponents[i].ComponentNameInAssembly);
                        firstSelectionByPlanes.AppendFormat("{0}@{1}", suctionLineComponents[i - 1].ComponentNameInAssembly, AssemblyTitle);
                        secondSelectionByPlanes.AppendFormat("{0}@{1}", suctionLineComponents[i].ComponentNameInAssembly, AssemblyTitle);

                        swModel.ClearSelection2(true);
                        swDocExt = (ModelDocExtension)swModel.Extension;

                        // Select the planes for the mate
                        boolstat = swDocExt.SelectByID2(firstSelectionByPlanes.ToString(), "PLANE", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);
                        boolstat = swDocExt.SelectByID2(secondSelectionByPlanes.ToString(), "PLANE", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);

                        // Add the mate
                        matefeature = (Feature)swAssembly.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignCLOSEST, false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);
                        matefeature.Name = planesMateName.ToString();
                        #endregion

                        #region CoincidentAxesMate
                        // Create the name of the mate and the names of the planes to use for the mate
                        swModel = (ModelDoc2)swApp.ActivateDoc3(AssemblyTitle, true, (int)swRebuildOnActivation_e.swUserDecision, ref errors);

                        StringBuilder axesMateName = new StringBuilder("coinc_mateAxis_");
                        StringBuilder firstSelectionByAxes = new StringBuilder("Ось_Вх@");
                        StringBuilder secondSelectionByAxes = new StringBuilder("Ось_Вых@");

                        axesMateName.AppendFormat("{0}_{1}", suctionLineComponents[i - 1].ComponentNameInAssembly, suctionLineComponents[i].ComponentName);
                        firstSelectionByAxes.AppendFormat("{0}@{1}", suctionLineComponents[i - 1].ComponentNameInAssembly, AssemblyTitle);
                        secondSelectionByAxes.AppendFormat("{0}@{1}", suctionLineComponents[i].ComponentNameInAssembly, AssemblyTitle);

                        swModel.ClearSelection2(true);
                        swDocExt = (ModelDocExtension)swModel.Extension;

                        // Select the planes for the mate
                        boolstat = swDocExt.SelectByID2(firstSelectionByAxes.ToString(), "AXIS", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);
                        boolstat = swDocExt.SelectByID2(secondSelectionByAxes.ToString(), "AXIS", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);

                        // Add the mate
                        matefeature = (Feature)swAssembly.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignCLOSEST, false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);
                        matefeature.Name = axesMateName.ToString();
                        #endregion
                    }
                    catch (Exception e)
                    {
                        Debug.Print(e.Message);
                    }
                }

            });
            #endregion

            #region CreateMatesForThePressureLine
            Task createMatesForThePressureLine = new Task(delegate
            {
                for (int i = 0; i < pressureLineComponents.Length; i++)
                {
                    try
                    {
                        #region CoincidentPlanesMate

                        if (pressureLineComponents[i] == null || !isAssemblyBuildingContinue) return;
                        // Create the name of the mate and the names of the planes to use for the mate
                        swModel = (ModelDoc2)swApp.ActivateDoc3(AssemblyTitle, true, (int)swRebuildOnActivation_e.swUserDecision, ref errors);

                        StringBuilder planesMateName = new StringBuilder("coinc_matePlains_");
                        StringBuilder firstSelectionByPlanes = new StringBuilder("Пл_Вых@");
                        StringBuilder secondSelectionByPlanes = new StringBuilder("Пл_Вх@");

                        if (i == 0)
                        {
                            if ((pressureLineComponents[i].ComponentType.Equals(StationComponentTypeEnum.ФланцевыйПереход_2) ||
                                pressureLineComponents[i].ComponentType.Equals(StationComponentTypeEnum.Американка_2)) &&
                                !pumpStation.StationScheme.StationType.Equals(StationTypeEnum.Пожаротушения))
                            {
                                secondSelectionByPlanes = new StringBuilder("Пл_Вых@");
                            }
                            planesMateName.AppendFormat("{0}_{1}", mainPump.ComponentNameInAssembly, pressureLineComponents[i].ComponentNameInAssembly);
                            firstSelectionByPlanes.AppendFormat("{0}@{1}", mainPump.ComponentNameInAssembly, AssemblyTitle);
                            secondSelectionByPlanes.AppendFormat("{0}@{1}", pressureLineComponents[i].ComponentNameInAssembly, AssemblyTitle);
                        }
                        else if ((pressureLineComponents[i - 1].ComponentType.Equals(StationComponentTypeEnum.ФланцевыйПереход_2) ||
                                pressureLineComponents[i - 1].ComponentType.Equals(StationComponentTypeEnum.Американка_2)) &&
                                !pumpStation.StationScheme.StationType.Equals(StationTypeEnum.Пожаротушения))
                        {
                            firstSelectionByPlanes = new StringBuilder("Пл_Вх@");
                            planesMateName.AppendFormat("{0}_{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, pressureLineComponents[i].ComponentNameInAssembly);
                            firstSelectionByPlanes.AppendFormat("{0}@{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, AssemblyTitle);
                            secondSelectionByPlanes.AppendFormat("{0}@{1}", pressureLineComponents[i].ComponentNameInAssembly, AssemblyTitle);
                        }
                        else if (pressureLineComponents[i].ComponentType.Equals(StationComponentTypeEnum.ТВ_тройник_всасывающий_2))
                        {
                            secondSelectionByPlanes = new StringBuilder("Пл_Вых@");
                            planesMateName.AppendFormat("{0}_{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, pressureLineComponents[i].ComponentNameInAssembly);
                            firstSelectionByPlanes.AppendFormat("{0}@{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, AssemblyTitle);
                            secondSelectionByPlanes.AppendFormat("{0}@{1}", pressureLineComponents[i].ComponentNameInAssembly, AssemblyTitle);
                        }
                        else if (pressureLineComponents[i].ComponentType.Equals(StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора)
                            && Array.Exists(pressureLineComponents, comp => comp.ComponentType.Equals(StationComponentTypeEnum.ТВ_тройник_всасывающий_2)))
                        {
                            firstSelectionByPlanes = new StringBuilder("Пл_Вх1@");
                            planesMateName.AppendFormat("{0}_{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, pressureLineComponents[i].ComponentNameInAssembly);
                            firstSelectionByPlanes.AppendFormat("{0}@{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, AssemblyTitle);
                            secondSelectionByPlanes.AppendFormat("{0}@{1}", pressureLineComponents[i].ComponentNameInAssembly, AssemblyTitle);
                        }
                        else
                        {
                            planesMateName.AppendFormat("{0}_{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, pressureLineComponents[i].ComponentNameInAssembly);
                            firstSelectionByPlanes.AppendFormat("{0}@{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, AssemblyTitle);
                            secondSelectionByPlanes.AppendFormat("{0}@{1}", pressureLineComponents[i].ComponentNameInAssembly, AssemblyTitle);
                        }

                        swModel.ClearSelection2(true);
                        swDocExt = (ModelDocExtension)swModel.Extension;

                        // Select the planes for the mate
                        boolstat = swDocExt.SelectByID2(firstSelectionByPlanes.ToString(), "PLANE", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);
                        boolstat = swDocExt.SelectByID2(secondSelectionByPlanes.ToString(), "PLANE", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);

                        // Add the mate
                        matefeature = (Feature)swAssembly.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignCLOSEST, false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);
                        matefeature.Name = planesMateName.ToString();
                        #endregion

                        #region CoincidentAxesMate
                        // Create the name of the mate and the names of the planes to use for the mate
                        swModel = (ModelDoc2)swApp.ActivateDoc3(AssemblyTitle, true, (int)swRebuildOnActivation_e.swUserDecision, ref errors);

                        StringBuilder axesMateName = new StringBuilder("coinc_mateAxis_");
                        StringBuilder firstSelectionByAxes = new StringBuilder("Ось_Вых@");
                        StringBuilder secondSelectionByAxes = new StringBuilder("Ось_Вх@");

                        if (i == 0)
                        {
                            axesMateName.AppendFormat("{0}_{1}", mainPump.ComponentNameInAssembly, pressureLineComponents[i].ComponentName);
                            firstSelectionByAxes.AppendFormat("{0}@{1}", mainPump.ComponentNameInAssembly, AssemblyTitle);
                            secondSelectionByAxes.AppendFormat("{0}@{1}", pressureLineComponents[i].ComponentNameInAssembly, AssemblyTitle);
                        }
                        else if (pressureLineComponents[i].ComponentType.Equals(StationComponentTypeEnum.ТВ_тройник_всасывающий_2))
                        {
                            StringBuilder secondSelectionByAxes1 = new StringBuilder("Ось_Вых@");
                            axesMateName.AppendFormat("{0}_{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, pressureLineComponents[i].ComponentNameInAssembly);
                            firstSelectionByAxes.AppendFormat("{0}@{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, AssemblyTitle);
                            secondSelectionByAxes1.AppendFormat("{0}@{1}", pressureLineComponents[i].ComponentNameInAssembly, AssemblyTitle);
                            secondSelectionByAxes = secondSelectionByAxes1;
                        }
                        else if (pressureLineComponents[i].ComponentType.Equals(StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора)
                            && Array.Exists(pressureLineComponents, comp => comp.ComponentType.Equals(StationComponentTypeEnum.ТВ_тройник_всасывающий_2)))
                        {
                            StringBuilder firstSelectionByAxes1 = new StringBuilder("Ось_Вх@");
                            axesMateName.AppendFormat("{0}_{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, pressureLineComponents[i].ComponentNameInAssembly);
                            firstSelectionByAxes1.AppendFormat("{0}@{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, AssemblyTitle);
                            secondSelectionByAxes.AppendFormat("{0}@{1}", pressureLineComponents[i].ComponentNameInAssembly, AssemblyTitle);
                            firstSelectionByAxes = firstSelectionByAxes1;
                        }
                        else
                        {
                            axesMateName.AppendFormat("{0}_{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, pressureLineComponents[i].ComponentName);
                            firstSelectionByAxes.AppendFormat("{0}@{1}", pressureLineComponents[i - 1].ComponentNameInAssembly, AssemblyTitle);
                            secondSelectionByAxes.AppendFormat("{0}@{1}", pressureLineComponents[i].ComponentNameInAssembly, AssemblyTitle);
                        }

                        swModel.ClearSelection2(true);
                        swDocExt = (ModelDocExtension)swModel.Extension;

                        // Select the planes for the mate
                        boolstat = swDocExt.SelectByID2(firstSelectionByAxes.ToString(), "AXIS", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);
                        boolstat = swDocExt.SelectByID2(secondSelectionByAxes.ToString(), "AXIS", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);

                        // Add the mate
                        matefeature = (Feature)swAssembly.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignCLOSEST, false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);
                        matefeature.Name = axesMateName.ToString();
                        #endregion
                    }
                    catch (Exception e)
                    {
                        Debug.Print(e.Message);
                    }
                }

            });
            #endregion

            createMatesForTheSuctionLine.Start();
            createMatesForTheSuctionLine.Wait();

            createMatesForThePressureLine.Start();
            createMatesForThePressureLine.Wait();

            CreateLocalLinearPattern(pumpsCount, pumpStation.DistanceBetweenAxis, stationComponentsArray);

            #region PrintStationInfo
            foreach (var component in pumpStation.stationComponents)
            {
                if (component.ComponentType.Equals(StationComponentTypeEnum.Рама_) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.ШУ_) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.КВЖ_катушка_для_жокей_насоса_всасывающая) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.КНЖ_катушка_для_жокей_насоса_напорная) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.Насос_жокей) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.Насос_основной) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.КВ_коллектор_всасывающий) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.КН_коллектор_напорный_вертик_насос) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.КН_коллектор_напорный_горизонт_насос))
                {
                    pumpStationWeight += component.ComponentWeight;
                    //Console.WriteLine($"component name: {component.ComponentName}, weight: {component.ComponentWeight}");
                }
                else if (component.ComponentType.Equals(StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора))
                {
                    pumpStationWeight += component.ComponentWeight * (pumpStation.PumpsCount - 1);
                    //Console.WriteLine($"component name: {component.ComponentName}, weight: {component.ComponentWeight * (pumpStation.PumpsCount - 1)}");
                }
                else
                {
                    pumpStationWeight += component.ComponentWeight * pumpStation.PumpsCount;
                    //Console.WriteLine($"component name: {component.ComponentName}, weight: {component.ComponentWeight * pumpStation.PumpsCount}");
                }
            }
            //Console.WriteLine("-------------------------------------");
            //Console.WriteLine($"Station weight: {pumpStationWeight}");
            #endregion

            //Console.ReadKey();
            swModel.ShowNamedView2("Изометрия", 7);
            swModel.ViewZoomtofit2();
            MessageBox.Show($"Установка \"{pumpStation.StationScheme.StationType}\" \n\nВес установки: {pumpStationWeight}кг.", "Сборка завершена", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public PumpsType GetPumpsTypeEnumByValue(string pumpsType) 
        {
            if (pumpsType.Equals("Горизонтальный")) 
            {
                return PumpsType.Горизонтальный;
            }
            return PumpsType.Вертикальный;
        }

        public StationScheme GetStationSchemeByTypeValue(string pumpStationType, int pumpsCount, string pumpsType, 
            double dnPumpsPressureConnection, double dnPumpsSuctionConnection, int pumpStationConnectionDN)
        {
            #region FireProtectionStationScheme

            if (pumpStationType.Equals(StationTypeEnum.Пожаротушения.ToString()))
            {
                if (mainPump.PumpsType.Equals(PumpsType.Горизонтальный))
                {
                    if (pumpsCount == 2)
                    {
                        if (mainPump.SuctionSideDn > 2) //means that it's a pump with flange connection
                        {
                            return StationScheme.GetSimpleFireProtectionScheme2HorizontalPumpsFlangePumpConnection(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                        else //means that it's a pump with carve connection
                        {
                            return StationScheme.GetSimpleFireProtectionScheme2HorizontalPumpsCarvePumpConnection(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                    }
                    else //means that count of pumps current station more than 2
                    {
                        if (mainPump.SuctionSideDn > 2) //means that it's a pump with flange connection
                        {
                            return StationScheme.GetSimpleFireProtectionSchemeMoreThan2HorizontalPumpsFlangePumpConnection(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                        else //means that it's a pump with carve connection
                        {
                            return StationScheme.GetSimpleFireProtectionSchemeMoreThan2HorizontalPumpsCarvePumpConnection(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                    }
                }
                else 
                {
                    if (pumpsCount == 2)
                    {
                        if (mainPump.SuctionSideDn > 2) //means that it's a pump with flange connection
                        {
                            return StationScheme.GetSimpleFireProtectionScheme2VerticalPumpsFlangePumpConnection(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                        else //means that it's a pump with carve connection
                        {
                            return StationScheme.GetSimpleFireProtectionScheme2VerticalPumpsCarvePumpConnection(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                    }
                    else //means that count of pumps current station more than 2
                    {
                        if (mainPump.SuctionSideDn > 2) //means that it's a pump with flange connection
                        {
                            return StationScheme.GetSimpleFireProtectionSchemeMoreThan2VerticalPumpsFlangePumpConnection(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                        else //means that it's a pump with carve connection
                        {
                            return StationScheme.GetSimpleFireProtectionSchemeMoreThan2VerticalPumpsCarvePumpConnection(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                    }
                }
            }

            #endregion

            #region PressureIncreaseStationScheme
            else if (pumpStationType.Equals(StationTypeEnum.Повышения_давления.ToString()))
            {
                if (mainPump.PumpsType.Equals(PumpsType.Горизонтальный))
                {
                    if (mainPump.SuctionSideDn > 2) //means that the pump has suction flange connection
                    {
                        if (mainPump.SuctionSideDn > 50)
                        {
                            return StationScheme.GetSimplePressureIncreaseSchemeHorizontalPumpsFlangePumpConnectionBiggerThan50(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                        return StationScheme.GetSimplePressureIncreaseSchemeHorizontalPumpsFlangePumpConnectionSmallerThan50(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                    }
                    else //means that the pump has suction carve connection
                    {
                        if (mainPump.SuctionSideDn > 1.25)
                        {
                            return StationScheme.GetSimplePressureIncreaseSchemeHorizontalPumpsCarvePumpConnectionBiggerThan1_1_4(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                        return StationScheme.GetSimplePressureIncreaseSchemeHorizontalPumpsCarvePumpConnectionSmallerThan1_1_2(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                    }
                }
                else 
                {
                    if (mainPump.SuctionSideDn > 2) //means that the pump has suction flange connection
                    {
                        if (mainPump.SuctionSideDn > 50)
                        {
                            return StationScheme.GetSimplePressureIncreaseSchemeVerticalPumpsFlangePumpConnectionBiggerThan50(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                        return StationScheme.GetSimplePressureIncreaseSchemeVerticalPumpsFlangePumpConnectionSmallerThan50(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                    }
                    else //means that the pump has suction carve connection
                    {
                        if (mainPump.SuctionSideDn > 1.25)
                        {
                            return StationScheme.GetSimplePressureIncreaseSchemeVerticalPumpsCarvePumpConnectionBiggerThan1_1_4(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                        }
                        return StationScheme.GetSimplePressureIncreaseSchemeVerticalPumpsCarvePumpConnectionSmallerThan1_1_2(dnPumpsPressureConnection, dnPumpsSuctionConnection);
                    }
                }
            }
            #endregion

            #region F-DriveStationScheme

            #endregion

            #region MultiDriveStationScheme

            #endregion

            #region CombinedStationScheme

            #endregion

            #region JockeyStationScheme

            #endregion

            throw new Exception();
        }
        #endregion

        public void InsertStationComponentInAssemly(StationComponent stationComponent) 
        {
            if (!isAssemblyBuildingContinue) return;
            if (stationComponent.ComponentName.Equals("Без ШУ")) return;

            //choosing the control cabinet configuration if the current component is the control cabinet
            if (stationComponent.ComponentType.Equals(StationComponentTypeEnum.ШУ_))
            {
                componentConfigName = GetControlCabinetConfigName(controlCabinet, frame);
            }

            if (!stationComponent.ComponentType.Equals(StationComponentTypeEnum.Насос_основной)) 
            {
                //open part component
                swModel = (ModelDoc2)swApp.OpenDoc6(stationComponent.ComponentLocationPath, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, componentConfigName, ref errors, ref warnings);
                //if current component not exist in components base
                if (swModel == null)
                {
                    isAssemblyBuildingContinue = false;
                    MessageBox.Show($"Компонент {stationComponent.ComponentName} отсутствует в базе (приложение будет закрыто)", "Ошибка доступа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    swApp.CloseDoc(AssemblyTitle);
                    return;
                }
            }

            //getting the current component property data
            GetComponentPropertyData(stationComponent);

            swApp.ActivateDoc2(stationComponent.ComponentName + ".sldprt", false, ref errors);

            Debug.Print(componentConfigName.Equals("СТОЙКА_1500_ОТДЕЛЬНО_СТОЯЩИЙ").ToString());

            //Add the part to the assembly
            swInsertedComponent = (Component2)swAssembly.AddComponent5(stationComponent.ComponentLocationPath, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, componentConfigName, false, "", 0, 0, 0);

            stationComponent.ComponentNameInAssembly = swInsertedComponent.Name2;
            swApp.CloseDoc(stationComponent.ComponentLocationPath);
            TransformData[0] = stationComponent.RotationComponentArray[0];
            TransformData[1] = stationComponent.RotationComponentArray[1];
            TransformData[2] = stationComponent.RotationComponentArray[2];
            TransformData[3] = stationComponent.RotationComponentArray[3];
            TransformData[4] = stationComponent.RotationComponentArray[4];
            TransformData[5] = stationComponent.RotationComponentArray[5];
            TransformData[6] = stationComponent.RotationComponentArray[6];
            TransformData[7] = stationComponent.RotationComponentArray[7];
            TransformData[8] = stationComponent.RotationComponentArray[8];
            //if the current component is the control cabinet and it has a stand alone configuration
            if (stationComponent.ComponentType.Equals(StationComponentTypeEnum.ШУ_) && !controlCabinetStAlonePosition.Equals("Не установлено") && !controlCabinetStAlonePosition.Equals(""))
            {
                SetControlCabinetStandAloneConfigPosition(controlCabinet, frame, controlCabinetStAlonePosition);
                TransformData[9] = stationComponent.RotationComponentArray[9];
                TransformData[10] = stationComponent.RotationComponentArray[10];
                TransformData[11] = stationComponent.RotationComponentArray[11];
            }
            else 
            {
                TransformData[9] = 0;
                TransformData[10] = 0;
                TransformData[11] = 0;
            }
            TransformData[12] = 1;
            TransformData[13] = 0;
            TransformData[14] = 0;
            TransformData[15] = 0;
            TransformDataObj = (object)TransformData;
            swMathUtil = (MathUtility)swApp.GetMathUtility();
            swTransform = (MathTransform)swMathUtil.CreateTransform((TransformDataObj));
            status = swInsertedComponent.SetTransformAndSolve2(swTransform);

            swModel.Rebuild(1);
        }

        public void CreateLocalLinearPattern(int instanceCount, int distanceBetweenAxis, StationComponent [] stationComponents) 
        {
            if (!isAssemblyBuildingContinue) return;

            swModel = (ModelDoc2)swApp.ActivateDoc3(AssemblyTitle, true, (int)swRebuildOnActivation_e.swUserDecision, ref errors);

            swDocExt = (ModelDocExtension)swModel.Extension;
            swFeatureManager = (FeatureManager)swModel.FeatureManager;

            status = swDocExt.SelectByID2("Справа", "PLANE", 0, 0, 0, false, 2, null, 0);

            foreach (var component in stationComponents)
            {
                if (component.ComponentType.Equals(StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.КВ_коллектор_всасывающий) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.КН_коллектор_напорный_вертик_насос) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.КН_коллектор_напорный_горизонт_насос) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.ШУ_) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.Рама_) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.Насос_жокей) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.КВЖ_катушка_для_жокей_насоса_всасывающая) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.КНЖ_катушка_для_жокей_насоса_напорная)) 
                {
                    continue;
                }

                status = swDocExt.SelectByID2(component.ComponentNameInAssembly + "@" + AssemblyTitle, "COMPONENT", 0, 0, 0, true, 1, null, 0);
            }

            double interval = (double)distanceBetweenAxis / 1000;//?

            swFeature = (Feature)swFeatureManager.FeatureLinearPattern5(instanceCount, interval, 1, 0.05, true, false, "NULL", "NULL", false, false,
            false, false, false, false, true, true, false, false, 0, 0, false, false);
            swModel.ClearSelection2(true);
        }

        public string GetControlCabinetConfigName(ControlCabinet controlCabinet, Frame frame) 
        {
            if (controlCabinetName.Equals("Не установлено") || controlCabinetName.Equals("Без ШУ")) { return string.Empty; }
            //getting the control cabinet width (for choosing the control cabinet supports size)
            StringBuilder ccConfigNameBuilder = new StringBuilder("СТОЙКА_");
            int cabinetHigh = int.Parse(controlCabinet.ComponentName.Split('_')[1].ToString().Split('x')[1]);

            //need to check the control cabinet position for choosing the configuration
            if (cabinetHigh < 1400)
            {
                if (cabinetHigh == 1200) 
                {
                    ccConfigNameBuilder.Append("1700_");
                }
                else ccConfigNameBuilder.Append("1500_");

                if (isControlCabStandAlone) 
                {
                    //need to set some value for stand alone control cabinet position
                    SetControlCabinetStandAloneConfigPosition(controlCabinet, frame, controlCabinetStAlonePosition);
                    return ccConfigNameBuilder.Append("ОТДЕЛЬНО_СТОЯЩИЙ").ToString();
                }

                if (frame.FrameType.Equals(FrameTypesEnum.StandartRottenFrame))
                {
                    if (int.Parse(frame.ComponentName.Split('_')[2].ToString().Split('х')[0]) == 500)
                    {
                        ccConfigNameBuilder.Append("ОПОРЫ_СЗАДИ_500");
                    }
                    else 
                    {
                        ccConfigNameBuilder.Append("ОПОРЫ_СЗАДИ_400");
                    }
                }
                else 
                {
                    ccConfigNameBuilder.Append("ОПОРЫ_СЗАДИ_400");
                }
            }
            else
            {
                //means that control cabinet will be stand alone
                //need to set some value for stand alone control cabinet position
                SetControlCabinetStandAloneConfigPosition(controlCabinet, frame, controlCabinetStAlonePosition);
                return string.Empty;
            }

            return ccConfigNameBuilder.ToString();
        }

        public void CreateCoincidentMateForComponentPlanes(StationComponent component1, StationComponent frame)
        {
            if (component1 == null || frame == null || !isAssemblyBuildingContinue) return;
            
            // Create the name of the mate and the names of the planes to use for the mate
            swModel = (ModelDoc2)swApp.ActivateDoc3(AssemblyTitle, true, (int)swRebuildOnActivation_e.swUserDecision, ref errors);

            string MateName;
            string FirstSelection;
            string SecondSelection;
            string line1PlaneName = "Пл_Низ_Лапы@";
            string line2PlaneName = "Пл_Верх_Рамы@";

            if (component1.ComponentType.Equals(StationComponentTypeEnum.ШУ_) && isControlCabStandAlone) 
            {
                line1PlaneName = "Сверху@";
                line2PlaneName = "Пл_Низ_Рамы@";
            }

            MateName = "coinc_matePlains_" + component1.ComponentNameInAssembly + "_" + frame.ComponentNameInAssembly;
            FirstSelection = line1PlaneName + component1.ComponentNameInAssembly + "@" + AssemblyTitle;
            SecondSelection = line2PlaneName + frame.ComponentNameInAssembly + "@" + AssemblyTitle;

            swModel.ClearSelection2(true);
            swDocExt = (ModelDocExtension)swModel.Extension;

            // Select the planes for the mate
            boolstat = swDocExt.SelectByID2(FirstSelection, "PLANE", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);
            boolstat = swDocExt.SelectByID2(SecondSelection, "PLANE", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);

            // Add the mate
            matefeature = (Feature)swAssembly.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignCLOSEST, false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);
            matefeature.Name = MateName;

        }

        public void CreateCoincidentMateForComponentAxis(StationComponent component1, StationComponent frame)
        {
            if (component1 == null || frame == null || !isAssemblyBuildingContinue) return;
            if (component1.ComponentType.Equals(StationComponentTypeEnum.ШУ_) && isControlCabStandAlone) return;

            // Create the name of the mate and the names of the planes to use for the mate
            swModel = (ModelDoc2)swApp.ActivateDoc3(AssemblyTitle, true, (int)swRebuildOnActivation_e.swUserDecision, ref errors);

            string MateName;
            string FirstSelection;
            string SecondSelection;
            string line1AxisName = "Ось_Лапы@";
            string line2AxisName = "Ось_Отв_Лапы@";

            if (component1.ComponentType.Equals(StationComponentTypeEnum.ШУ_))
            {
                line2AxisName = "Ось_ШУ@";
            }
            else if (component1.ComponentName.StartsWith("CR_15") || component1.ComponentName.StartsWith("Helix_10") ||
                component1.ComponentName.StartsWith("Helix_15") || component1.ComponentName.StartsWith("Helix_22"))
            {
                line2AxisName = "Ось_CR15@";
            }
            else if (component1.ComponentName.StartsWith("MHI")) 
            {
                line2AxisName = "Ось_MHI@";
            }

            MateName = "coinc_mateAxis_" + component1.ComponentNameInAssembly + "_" + frame.ComponentName;
            FirstSelection = line1AxisName + component1.ComponentNameInAssembly + "@" + AssemblyTitle;
            SecondSelection = line2AxisName + frame.ComponentNameInAssembly + "@" + AssemblyTitle;

            swModel.ClearSelection2(true);
            swDocExt = (ModelDocExtension)swModel.Extension;

            // Select the planes for the mate
            boolstat = swDocExt.SelectByID2(FirstSelection, "AXIS", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);
            boolstat = swDocExt.SelectByID2(SecondSelection, "AXIS", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);

            // Add the mate
            matefeature = (Feature)swAssembly.AddMate5((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignCLOSEST, false, 0, 0, 0, 0, 0, 0, 0, 0, false, false, 0, out mateError);
            matefeature.Name = MateName;
        }

        public void SetControlCabinetStandAloneConfigPosition(ControlCabinet controlCabinet, Frame frame, string position) 
        {
            //means that control cabinet will be stand alone
            //need to set some value for stand alone control cabinet position

            int[] newArr = new int[12];
            Array.Copy(controlCabinet.RotationComponentArray, newArr, 9);
            controlCabinet.RotationComponentArray = newArr;

            if (position.Equals("Слева от рамы"))
            {
                controlCabinet.RotationComponentArray[9] = (frame.FrameLength / 2 /1000 + 1);
                controlCabinet.RotationComponentArray[10] = 0;
                controlCabinet.RotationComponentArray[11] = 0;
            }
            else if (position.Equals("Справа от рамы"))
            {
                controlCabinet.RotationComponentArray[9] = -(frame.FrameLength / 2 / 1000 + 1);
                controlCabinet.RotationComponentArray[10] = 0;
                controlCabinet.RotationComponentArray[11] = 0;
            }
            else if (position.Equals("Сзади рамы"))
            {
                controlCabinet.RotationComponentArray[9] = 0;
                controlCabinet.RotationComponentArray[10] = 0;
                controlCabinet.RotationComponentArray[11] = -(frame.FrameLength / 2 / 1000 + 1);
            }
            else if (position.Equals("Спереди рамы"))
            {
                controlCabinet.RotationComponentArray[9] = 0;
                controlCabinet.RotationComponentArray[10] = 0;
                controlCabinet.RotationComponentArray[11] = (frame.FrameLength / 2 / 1000 + 1);
            }

        } 

        public void GetComponentPropertyData(StationComponent component) 
        {
            //Get the custom property data
            swDocExt = swModel.Extension;
            swCustProp = swDocExt.get_CustomPropertyManager("");

            try
            {
                if (component.ComponentType.Equals(StationComponentTypeEnum.Насос_основной) ||
                    component.ComponentType.Equals(StationComponentTypeEnum.Насос_жокей))
                {
                    Pump pump = (Pump)component;
                    Debug.Print("The pump's cast successfully finished");
                    pump.ComponentWeight = double.Parse(swCustProp.Get("weight"));
                    pump.SuctionSideDn = double.Parse(swCustProp.Get("DNsuction"));
                    pump.PressureSideDn = double.Parse(swCustProp.Get("DNpressure"));

                    Debug.Print($"weight: {pump.ComponentWeight}, DNsuction: {pump.SuctionSideDn }, DNpressure: {pump.PressureSideDn}"); swDocExt = (ModelDocExtension)swModel.Extension;
                    status = swDocExt.SelectByID2("Справа", "PLANE", 0, 0, 0, false, 0, null, 0);
                    status = swDocExt.SelectByID2("Пл_Шир", "PLANE", 0, 0, 0, true, 0, null, 0);
                    swMeasure = (Measure)swDocExt.CreateMeasure();

                    //Can set this to 0
                    // 0 = center to center
                    // 1 = minimum distance
                    // 2 = maximum distance

                    swMeasure.ArcOption = 0;
                    double width = 0;

                    status = swMeasure.Calculate(null);
                    if ((status))
                    {
                        if ((!(swMeasure.Distance == -1)))
                        {
                            width = swMeasure.Distance * 1000 * 2;
                            pump.PumpsWidth = (int)width;
                        }
                    }
                    else
                    {
                        Debug.Print("Invalid combination of selected entities.");
                    }

                    swModel.ClearSelection2(true);
                    return;
                }
                else if (component.ComponentType.Equals(StationComponentTypeEnum.Рама_)) 
                {
                    Frame frame = (Frame)component;
                    Debug.Print("The frame's cast successfully finished");
                    frame.ComponentWeight = double.Parse(swCustProp.Get("weight"));

                    swDocExt = (ModelDocExtension)swModel.Extension;
                    status = swDocExt.SelectByID2("Грань_Л", "PLANE", 0, 0, 0, false, 0, null, 0);
                    status = swDocExt.SelectByID2("Грань_П", "PLANE", 0, 0, 0, true, 0, null, 0);
                    swMeasure = (Measure)swDocExt.CreateMeasure();

                    //Can set this to 0
                    // 0 = center to center
                    // 1 = minimum distance
                    // 2 = maximum distance

                    swMeasure.ArcOption = 0;
                    double length = 0;

                    status = swMeasure.Calculate(null);
                    if ((status))
                    {
                        if ((!(swMeasure.Distance == -1)))
                        {
                            length = swMeasure.Distance * 1000;
                            frame.FrameLength = (int)length;
                            Debug.Print(frame.FrameLength.ToString());
                        }
                    }
                    else
                    {
                        Debug.Print("Invalid combination of selected entities.");
                    }

                    swModel.ClearSelection2(true);
                    return;
                }
                component.ComponentWeight = double.Parse(swCustProp.Get("weight"));
            }
            catch (Exception e)
            {

                Debug.Print(e.Message);
            }
        }
    }
}
