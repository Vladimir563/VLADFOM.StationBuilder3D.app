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
        private int rotationByX;
        private int rotationByY;
        private int rotationByZ;
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
        public int RotationByX
        {
            get { return rotationByX; }
            set { rotationByX = value; }
        }
        public int RotationByY
        {
            get { return rotationByY; }
            set { rotationByY = value; }
        }
        public int RotationByZ
        {
            get { return rotationByZ; }
            set { rotationByZ = value; }
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
