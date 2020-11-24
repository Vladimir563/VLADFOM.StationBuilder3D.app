using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    class UnequalFittings : Fittings
    {
        public UnequalFittings(PumpStation station, StationComponentsTypeEnum _stationComponentsType, 
            string _componentsName, string _pathToTheComponent, double _componentsWeight, int _rotationByX, 
            int _rotationByY, int _rotationByZ) 
            : base(station, _stationComponentsType, _componentsName, _pathToTheComponent, _componentsWeight, 
                  _rotationByX, _rotationByY, _rotationByZ)
        {
            this.ComponentsName = ComponentsNameGenerate(station.SecondaryLineDn, station.Pump.SuctionSideDn, 
                station.Pump.PressureSideDn);
        }

        public override string ComponentsNameGenerate(int mainDn, int pumpsSuctionLineConnectionDn, 
            int pumpsPressureLineConnectionDn)
        {
            string[] s1 = this.StationComponentsType.ToString().Split('_');
            if (s1.Equals("КЭ"))
            {
                return $"{s1[0]}_DN{pumpsSuctionLineConnectionDn}-{mainDn}";
            }
            else if(s1.Equals("КК"))
            {
                return $"{s1[0]}_DN{pumpsPressureLineConnectionDn}-{mainDn}";
            }
            return string.Empty;
        }

    }
}
