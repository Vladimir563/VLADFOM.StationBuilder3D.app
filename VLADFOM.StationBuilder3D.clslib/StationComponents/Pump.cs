using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class Pump
    {
        private double backPlaneDistance;
        private double frontPlaneDistance;
        private double rightSidePlaneDistance;
        private double topPlaneDistance;
        private double bottomPlaneDistance;
        private int suctionSideDn;
        private int pressureSideDn;
        private PumpsPositionTypeEnum pumpsPositionType;

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
        public PumpsPositionTypeEnum PumpsPositionType
        {
            get { return pumpsPositionType; }
            set { pumpsPositionType = value; }
        }


        public Pump(PumpStation station, int _suctionSideDn, int _pressureSideDn, PumpsPositionTypeEnum _pumpsPositionType, double _backPlaneDistance,
        double _frontPlaneDistance, double _rightSidePlaneDistance, double _topPlaneDistance, double _bottomPlaneDistance)
        {
            SuctionSideDn = _suctionSideDn;
            PressureSideDn = _pressureSideDn;
            PumpsPositionType = _pumpsPositionType;
            BackPlaneDistance = _backPlaneDistance;
            FrontPlaneDistance = _frontPlaneDistance;
            RightSidePlaneDistance = _rightSidePlaneDistance;
            TopPlaneDistance = _topPlaneDistance;
            BottomPlaneDistance = _bottomPlaneDistance;
            station.Pump = this;
        }
    }
}
