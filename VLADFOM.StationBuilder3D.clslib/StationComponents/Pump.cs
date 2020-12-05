using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class Pump : PumpStationComponent
    {
        private double backPlaneDistance;
        private double frontPlaneDistance;
        private double rightSidePlaneDistance;
        private double topPlaneDistance;
        private double bottomPlaneDistance;
        private int suctionSideDn;
        private int pressureSideDn;

        public double BackPlaneDistance
        {
            get { return backPlaneDistance; }
            set { backPlaneDistance = value; }
        }
        public double FrontPlaneDistance
        {
            get { return frontPlaneDistance; }
            set { frontPlaneDistance = value; }
        }
        public double RightSidePlaneDistance
        {
            get { return rightSidePlaneDistance; }
            set { rightSidePlaneDistance = value; }
        }
        public double TopPlaneDistance
        {
            get { return topPlaneDistance; }
            set { topPlaneDistance = value; }
        }
        public double BottomPlaneDistance
        {
            get { return bottomPlaneDistance; }
            set { bottomPlaneDistance = value; }
        }
        public int SuctionSideDn
        {
            get { return suctionSideDn; }
            set { suctionSideDn = value; }
        }
        public int PressureSideDn
        {
            get { return pressureSideDn; }
            set { pressureSideDn = value; }
        }


        public Pump(PumpStation _pumpStation,  int _pressureSideDn, int _suctionSideDn,
            StationComponentsTypeEnum _stationComponentsType, string _componentsName) 
            : base(_pumpStation, _stationComponentsType)
        {
            PressureSideDn = _pressureSideDn;
            SuctionSideDn = _suctionSideDn;
            ComponentsName = _componentsName;
            PumpStation.Pump = this;

            PathToTheComponent = ComponentsValCalculator.GetFullPathToTheComponent(PumpStation, this);
        }
    }
}
