using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class UnequalFittings : Fittings
    {
        public UnequalFittings(PumpStation _pumpStation, StationComponentTypeEnum _stationComponentsType) 
            : base(_pumpStation, _stationComponentsType)
        {
            if (PumpStation.IsAutoCalculationDiameterConnection)
            {
                this.ComponentName = this.ComponentsNameAutoGenerate(PumpStation, PumpStation.mainPump.SuctionSideDn,
                    PumpStation.mainPump.PressureSideDn);
            }
            else this.ComponentName = ComponentsNameGenerate(PumpStation);

            ComponentLocationPath = ComponentsValCalculator.GetFullPathToTheComponent(_pumpStation.componentsLocation, this);
        }

        public override string ComponentsNameAutoGenerate(PumpStation pumpStation, double pumpsSuctionLineConnectionDn, 
            double pumpsPressureLineConnectionDn)
        {
            string[] s1 = this.ComponentType.ToString().Split('_');

            if (s1[0].Equals("КЭ") || s1[0].Equals("КЭР"))
            {
                return $"{s1[0]}_DN{pumpsSuctionLineConnectionDn}-{pumpStation.SecondaryLineDn}";
            }
            else if (s1[0].Equals("КК") || s1[0].Equals("ККР"))
            {
                return $"{s1[0]}_DN{pumpsPressureLineConnectionDn}-{pumpStation.SecondaryLineDn}";
            }
            else if (s1[0].Equals("ТВ") )
            {
                return $"{s1[0]}_{pumpStation.DnSuctionCollector}-{pumpStation.SecondaryLineDn}-{pumpStation.DnSuctionCollector}" +
                    $"-{pumpStation.DistanceBetweenAxis}";
            }
            else if (s1[0].Equals("КВ"))
            {
                return $"{s1[0]}_{pumpStation.PumpsCount}х{pumpStation.SecondaryLineDn}-{pumpStation.DnSuctionCollector}" +
                    $"-{pumpStation.DistanceBetweenAxis}";
            }

            else if (s1[0].Equals("ТН")) 
            {
                return $"{s1[0]}_{pumpStation.DnPressureCollector}-{pumpStation.SecondaryLineDn}-{pumpStation.DnPressureCollector}" +
                    $"-{pumpStation.DistanceBetweenAxis}";
            }
            else if (s1[0].Equals("КН"))
            {
                return $"{s1[0]}_{pumpStation.PumpsCount}х{pumpStation.SecondaryLineDn}-{pumpStation.DnPressureCollector}" +
                    $"-{pumpStation.DistanceBetweenAxis}_{s1[3]}_{s1[4]}";
            }
            return string.Empty;
        }

        public override string ComponentsNameGenerate(PumpStation pumpStation)
        {
            string[] s1 = this.ComponentType.ToString().Split('_');

            if (s1.Equals("ТВ") || s1.Equals("ТН"))
            {
                return $"{s1[0]}_{pumpStation.StationScheme.stationChemeComponents[this.ComponentType].Item2}-" +
                    $"{pumpStation.StationScheme.stationChemeComponents[this.ComponentType].Item1}-" +
                    $"{pumpStation.StationScheme.stationChemeComponents[this.ComponentType].Item2}-" +
                    $"{pumpStation.DistanceBetweenAxis}";
            }
            else if (s1.Equals("КВ") || s1.Equals("КН")) 
            {
                return $"{s1[0]}_{pumpStation.PumpsCount}х{pumpStation.StationScheme.stationChemeComponents[this.ComponentType].Item1}" +
                    $"-{pumpStation.StationScheme.stationChemeComponents[this.ComponentType].Item2}-{pumpStation.DistanceBetweenAxis}";
            }

            return $"{s1[0]}_DN{pumpStation.StationScheme.stationChemeComponents[this.ComponentType].Item1}-" +
                $"{pumpStation.StationScheme.stationChemeComponents[this.ComponentType].Item2}";
        }

    }
}
