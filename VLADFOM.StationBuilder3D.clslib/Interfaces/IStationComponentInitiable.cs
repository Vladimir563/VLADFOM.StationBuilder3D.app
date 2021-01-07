using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    /// <summary>
    /// Represents station components initialization interface
    /// </summary>
    interface IStationComponentInitiable
    {
        string ComponentsNameGenerate(PumpStation pumpStation);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pumpStation"></param>
        /// <param name="pumpSuctionLineConnectionDn"></param>
        /// <param name="pumpPressureLineConnectionDn"></param>
        /// <returns></returns>
        string ComponentsNameAutoGenerate(PumpStation pumpStation, double pumpSuctionLineConnectionDn, double pumpPressureLineConnectionDn);
    }
}
