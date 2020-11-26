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
        /// <summary>
        /// Allows to generate not automatically components name  
        /// </summary>
        /// <param name="mainDn">the inside fitting's diameter from the secondary line</param>
        /// <param name="pumpsConnectionDn">the pump's suction/pressure inside diameter</param>
        /// <returns></returns>
        string ComponentsNameGenerate(PumpStation pumpStation);
        string ComponentsNameAutoGenerate(PumpStation pumpStation, int pumpsSuctionLineConnectionDn, int pumpsPressureLineConnectionDn);
    }
}
