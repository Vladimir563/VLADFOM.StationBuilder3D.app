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

        public List<StationComponentsTypeEnum> stationComponents = new List<StationComponentsTypeEnum>();

        public StationTypeEnum StationType
        {
            get { return stationType; }
            set { stationType = value; }
        }

        public void AddStationComponents(StationComponentsTypeEnum stationComponent)
        {
            stationComponents.Add(stationComponent);
        }
        public void RemoveStationComponents(StationComponentsTypeEnum stationComponent)
        {
            stationComponents.Remove(stationComponent);
        }


    }
}
