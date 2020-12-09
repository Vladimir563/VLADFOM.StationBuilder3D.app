using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class Pump : PumpStationComponent
    {

        private int suctionSideDn;
        private int pressureSideDn;
        private int pumpsWidth;

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
        public int PumpsWidth
        {
            get { return pumpsWidth; }
            set { pumpsWidth = value; }
        }
        public Pump( ComponentsLocationPaths _componentsLocation, string _componentsName, int _pumpsWidth, int _pressureSideDn, 
            int _suctionSideDn, StationComponentsTypeEnum _stationComponentsType) 
            : base(_stationComponentsType)
        {
            ComponentsName = _componentsName;
            PumpsWidth = _pumpsWidth;
            PressureSideDn = _pressureSideDn;
            SuctionSideDn = _suctionSideDn;

            PathToTheComponent = ComponentsValCalculator.GetFullPathToTheComponent(_componentsLocation, ComponentsName, this);
        }
    }
}
