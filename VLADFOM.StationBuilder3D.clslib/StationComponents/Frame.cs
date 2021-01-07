using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class Frame : StationComponent
    {
        private FrameTypesEnum frameType;
        private int frameLength;

        public int FrameLength
        {
            get { return frameLength; }
            set { frameLength = value; }
        }
        public FrameTypesEnum FrameType
        {
            get { return frameType; }
            set { frameType = value; }
        }

        public Frame(PumpStation _pumpStation, StationComponentTypeEnum _stationComponentsType) 
            : base(_pumpStation, _stationComponentsType)
        {
            FrameType = ComponentsValCalculator.GetFrameTypeByPumpsWeight(_pumpStation.mainPump.ComponentWeight);
            ComponentName = ComponentsValCalculator.GetFramesFullName(_pumpStation);
            ComponentLocationPath = ComponentsValCalculator.GetFullPathToTheComponent(_pumpStation.componentsLocation, this);
        }
    }
}
