using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class ControlCabinet : PumpStationComponent
    {
        public ControlCabinet(PumpStation _pumpStation, StationComponentsTypeEnum _stationComponentsType, string _componentsName) 
            : base(_pumpStation, _stationComponentsType)
        {
            ComponentsName = _componentsName;
            PathToTheComponent = ComponentsValCalculator.GetFullPathToTheComponent(_pumpStation.componentsLocation, this);
        }
    }
}
