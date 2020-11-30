using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class Frame : PumpStationComponent
    {
        private FrameTypesEnum frameType;

        public FrameTypesEnum FrameType
        {
            get { return frameType; }
            set { frameType = value; }
        }

        public Frame(PumpStation _pumpStation, StationComponentsTypeEnum _stationComponentsType, string _componentsName, string _pathToTheComponent,
            double _componentsWeight, int _rotationByX, int _rotationByY, int _rotationByZ) 
            : base(_pumpStation,_stationComponentsType, _componentsName, _componentsWeight, _rotationByX, 
                  _rotationByY, _rotationByZ)
        {
            FrameType = ComponentsValCalculator.GetFrameTypeByPumpsWeight(PumpStation.Pump.ComponentsWeight);
            PathToTheComponent = ComponentsValCalculator.GetFullPathToTheComponent(PumpStation, this);
        }
    }
}
