using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    class Fittings : PumpStationComponent, IStationComponentInitiable
    {
        public Fittings(PumpStation station, StationComponentsTypeEnum _stationComponentsType, string _componentsName,
            string _pathToTheComponent, double _componentsWeight, int _rotationByX, int _rotationByY, int _rotationByZ) 
            : base(_stationComponentsType, _componentsName,  _pathToTheComponent, _componentsWeight, _rotationByX, 
                  _rotationByY, _rotationByZ)
        {
            this.ComponentsName = ComponentsNameGenerate(station.SecondaryLineDn, 0,0);
        }

        public virtual string ComponentsNameGenerate(int mainDn, int pumpsSuctionLineConnectionDn, int pumpsPressureLineConnectionDn)
        {
            string [] s1 = this.StationComponentsType.ToString().Split('_');
            return $"{s1[0]}_DN{mainDn}";
        }
    }
}
