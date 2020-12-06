using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public abstract class PumpStationComponent
    {
        private string componentsName;
        private StationComponentsTypeEnum stationComponentsType;
        private string pathToTheComponent;
        private double componentsWeight;
        private int rotationByX1;
        private int rotationByX2;
        private int rotationByX3;
        private int rotationByY1;
        private int rotationByY2;
        private int rotationByY3;
        private int rotationByZ1;
        private int rotationByZ2;
        private int rotationByZ3;
        private int isComponentForNewLine;


        private PumpStation pumpStation;
        public string ComponentsName
        {
            get { return componentsName; }
            set { componentsName = value; }
        }
        public StationComponentsTypeEnum StationComponentsType
        {
            get { return stationComponentsType; }
            set { stationComponentsType = value; }
        }
        public string PathToTheComponent
        {
            get { return pathToTheComponent; }
            set { pathToTheComponent = value; }
        }
        public double ComponentsWeight
        {
            get { return componentsWeight; }
            set { componentsWeight = value; }
        }
        public int RotationByX1
        {
            get { return rotationByX1; }
            set { rotationByX1 = value; }
        }
        public int RotationByX2
        {
            get { return rotationByX2; }
            set { rotationByX2 = value; }
        }
        public int RotationByX3
        {
            get { return rotationByX3; }
            set { rotationByX3 = value; }
        }
        public int RotationByY1
        {
            get { return rotationByY1; }
            set { rotationByY1 = value; }
        }
        public int RotationByY2
        {
            get { return rotationByY2; }
            set { rotationByY2 = value; }
        }
        public int RotationByY3
        {
            get { return rotationByY3; }
            set { rotationByY3 = value; }
        }
        public int RotationByZ1
        {
            get { return rotationByZ1; }
            set { rotationByZ1 = value; }
        }
        public int RotationByZ2
        {
            get { return rotationByZ2; }
            set { rotationByZ2 = value; }
        }
        public int RotationByZ3
        {
            get { return rotationByZ3; }
            set { rotationByZ3 = value; }
        }
        public int IsComponentForNewLine
        {
            get { return isComponentForNewLine; }
            set { isComponentForNewLine = value; }
        }

        public PumpStation PumpStation
        {
            get { return pumpStation; }
            set { pumpStation = value; }
        }

        public PumpStationComponent(PumpStation _pumpStation, StationComponentsTypeEnum _stationComponentsType)
        {
            PumpStation = _pumpStation;
            StationComponentsType = _stationComponentsType;
        }
    }
}
