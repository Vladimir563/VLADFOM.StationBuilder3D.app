using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class ControlCabinet : StationComponent
    {
        public ControlCabinet(PumpStation _pumpStation, StationComponentTypeEnum _stationComponentsType, string _componentsName) 
            : base(_pumpStation, _stationComponentsType)
        {
            ComponentName = _componentsName;
            ComponentLocationPath = ComponentsValCalculator.GetFullPathToTheComponent(_pumpStation.componentsLocation, this);
        }
    }
}
