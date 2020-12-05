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

        public Frame(PumpStation _pumpStation, StationComponentsTypeEnum _stationComponentsType) 
            : base(_pumpStation,_stationComponentsType)
        {
            PathToTheComponent = ComponentsValCalculator.GetFullPathToTheComponent(PumpStation, this);
        }
    }
}
