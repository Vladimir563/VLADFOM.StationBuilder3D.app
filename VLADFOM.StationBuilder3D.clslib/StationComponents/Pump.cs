using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class Pump : StationComponent
    {

        private double suctionSideDn;
        private double pressureSideDn;
        private int pumpsWidth;
        private PumpsType pumpsType;

        public PumpsType PumpsType
        {
            get { return pumpsType; }
            set { pumpsType = value; }
        }
        public double SuctionSideDn
        {
            get { return suctionSideDn; }
            set { suctionSideDn = value; }
        }
        public double PressureSideDn
        {
            get { return pressureSideDn; }
            set { pressureSideDn = value; }
        }
        public int PumpsWidth
        {
            get { return pumpsWidth; }
            set { pumpsWidth = value; }
        }
        public Pump( ComponentsLocationPaths _componentsLocation, string _componentsName, int _pumpsWidth, int _pressureSideDn, 
            int _suctionSideDn, StationComponentTypeEnum _stationComponentsType) 
            : base(_stationComponentsType)
        {
            ComponentName = _componentsName;
            PumpsWidth = _pumpsWidth;
            PressureSideDn = _pressureSideDn;
            SuctionSideDn = _suctionSideDn;

            ComponentLocationPath = ComponentsValCalculator.GetFullPathToTheComponent(_componentsLocation, ComponentName, this);
        }
    }
}
