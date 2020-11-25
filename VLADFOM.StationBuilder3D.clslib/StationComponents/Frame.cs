using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class Frame : PumpStationComponent
    {
        public Frame(PumpStation _pumpStation, StationComponentsTypeEnum _stationComponentsType, string _componentsName, string _pathToTheComponent,
            double _componentsWeight, int _rotationByX, int _rotationByY, int _rotationByZ) 
            : base(_pumpStation,_stationComponentsType, _componentsName, _pathToTheComponent, _componentsWeight, _rotationByX, 
                  _rotationByY, _rotationByZ)
        {
        }
    }
}
