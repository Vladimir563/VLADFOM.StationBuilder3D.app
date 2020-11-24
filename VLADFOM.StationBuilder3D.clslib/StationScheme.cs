using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class StationScheme
    {
        private StationTypeEnum stationType;
        public List<PumpStationComponent> stationComponents;

        public StationTypeEnum StationType
        {
            get { return stationType; }
            set { stationType = value; }
        }


    }
}
