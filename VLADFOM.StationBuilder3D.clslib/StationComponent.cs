
namespace VLADFOM.StationBuilder3D.clslib
{
    public abstract class StationComponent
    {
        private string componentName;
        private StationComponentTypeEnum сomponentType;
        private string componentLocationPath;
        private double componentWeight;
        private  int[] rotationComponentArray;
        private bool isComponentForPressureLine;
        private string componentNameInAssembly;
        private PumpStation pumpStation;

        public string ComponentName
        {
            get { return componentName; }
            set { componentName = value; }
        }
        public StationComponentTypeEnum ComponentType
        {
            get { return сomponentType; }
            set { сomponentType = value; }
        }
        public string ComponentLocationPath
        {
            get { return componentLocationPath; }
            set { componentLocationPath = value; }
        }
        public double ComponentWeight
        {
            get { return componentWeight; }
            set { componentWeight = value; }
        }
        public bool IsComponentForPressureLine
        {
            get { return isComponentForPressureLine; }
            set { isComponentForPressureLine = value; }
        }
        public string ComponentNameInAssembly
        {
            get { return componentNameInAssembly; }
            set { componentNameInAssembly = value; }
        }
        public PumpStation PumpStation
        {
            get { return pumpStation; }
            set { pumpStation = value; }
        }
        public int[] RotationComponentArray
        {
            get { return rotationComponentArray; }
            set { rotationComponentArray = value; }
        }

        public StationComponent(StationComponentTypeEnum _stationComponentType) 
        {
            ComponentType = _stationComponentType;
        }

        public StationComponent(PumpStation _pumpStation, StationComponentTypeEnum _stationComponentType)
        {
            ComponentType = _stationComponentType; 
            PumpStation = _pumpStation;
        }
    }
}
