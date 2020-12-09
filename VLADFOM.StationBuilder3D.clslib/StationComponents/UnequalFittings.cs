using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class UnequalFittings : Fittings
    {
        public UnequalFittings(PumpStation _pumpStation, StationComponentsTypeEnum _stationComponentsType) 
            : base(_pumpStation, _stationComponentsType)
        {
            if (PumpStation.IsAutoCalculationDiameterConnection)
            {
                this.ComponentsName = this.ComponentsNameAutoGenerate(PumpStation, PumpStation.mainPump.SuctionSideDn,
                    PumpStation.mainPump.PressureSideDn);
            }
            else this.ComponentsName = ComponentsNameGenerate(PumpStation);

            PathToTheComponent = ComponentsValCalculator.GetFullPathToTheComponent(_pumpStation.componentsLocation, this);
        }

        public override string ComponentsNameAutoGenerate(PumpStation pumpStation, int pumpsSuctionLineConnectionDn, 
            int pumpsPressureLineConnectionDn)
        {
            string[] s1 = this.StationComponentsType.ToString().Split('_');

            if (s1[0].Equals("КЭ") || s1[0].Equals("КЭР"))
            {
                return $"{s1[0]}_DN{pumpsSuctionLineConnectionDn}-{pumpStation.SecondaryLineDn}";
            }
            else if (s1[0].Equals("КК") | s1[0].Equals("ККР"))
            {
                return $"{s1[0]}_DN{pumpsPressureLineConnectionDn}-{pumpStation.SecondaryLineDn}";
            }
            else if (s1[0].Equals("ТВ") || s1[0].Equals("КВ"))
            {
                return $"{s1[0]}_DN{pumpStation.DnSuctionCollector}-{pumpStation.SecondaryLineDn}-{pumpStation.DnSuctionCollector}" +
                    $"-{pumpStation.DistanceBetweenAxis}";
            }
            else if (s1[0].Equals("ТН") || s1[0].Equals("КН"))
            {
                return $"{s1[0]}_DN{pumpStation.DnPressureCollector}-{pumpStation.SecondaryLineDn}-{pumpStation.DnPressureCollector}" +
                    $"-{pumpStation.DistanceBetweenAxis}";
            }
            return string.Empty;
        }

        public override string ComponentsNameGenerate(PumpStation pumpStation)
        {
            string[] s1 = this.StationComponentsType.ToString().Split('_');

            if (s1.Equals("ТВ") || s1.Equals("ТН") || s1.Equals("КВ") || s1.Equals("КН")) 
            {
                return $"{s1[0]}_DN{pumpStation.StationScheme.stationComponents[this.StationComponentsType][1]}-" +
                    $"{pumpStation.StationScheme.stationComponents[this.StationComponentsType][0]}-" +
                    $"{pumpStation.StationScheme.stationComponents[this.StationComponentsType][1]}-" +
                    $"{pumpStation.DistanceBetweenAxis}";
            }

            return $"{s1[0]}_DN{pumpStation.StationScheme.stationComponents[this.StationComponentsType][0]}-" +
                $"{pumpStation.StationScheme.stationComponents[this.StationComponentsType][1]}";
        }

    }
}
