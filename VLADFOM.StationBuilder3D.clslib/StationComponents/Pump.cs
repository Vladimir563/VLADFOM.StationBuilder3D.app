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


        public Pump(PumpStation _pumpStation, int _suctionSideDn, int _pressureSideDn, double _backPlaneDistance,
        double _frontPlaneDistance, double _rightSidePlaneDistance, double _topPlaneDistance, double _bottomPlaneDistance,
            StationComponentsTypeEnum _stationComponentsType, string _componentsName, string _pathToTheComponent,
            double _componentsWeight, int _rotationByX, int _rotationByY, int _rotationByZ) 
            : base(_pumpStation, _stationComponentsType, _componentsName, _pathToTheComponent, _componentsWeight, _rotationByX,
                  _rotationByY, _rotationByZ)
        {
            SuctionSideDn = _suctionSideDn;
            PressureSideDn = _pressureSideDn;
            BackPlaneDistance = _backPlaneDistance;
            FrontPlaneDistance = _frontPlaneDistance;
            RightSidePlaneDistance = _rightSidePlaneDistance;
            TopPlaneDistance = _topPlaneDistance;
            BottomPlaneDistance = _bottomPlaneDistance;
            PumpStation.Pump = this;
        }
    }
}
