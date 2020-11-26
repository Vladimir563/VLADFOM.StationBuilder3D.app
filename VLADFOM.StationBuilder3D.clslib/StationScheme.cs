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

        /// <summary>
        /// StationComponentsTypeEnum - components type
        /// int[0,0] - there are two component diameters you need in the array
        /// the first value - smaller DN 
        /// the second value - bigger DN (or Main DN for component from form)
        /// </summary>
        public Dictionary<StationComponentsTypeEnum, int[]> stationComponents = new Dictionary<StationComponentsTypeEnum, int[]>();

        public StationTypeEnum StationType
        {
            get { return stationType; }
            set { stationType = value; }
        }

    }
}
