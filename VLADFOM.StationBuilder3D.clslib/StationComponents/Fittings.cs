using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class Fittings : StationComponent, IStationComponentInitiable
    {
        public Fittings(PumpStation _pumpStation, StationComponentTypeEnum _stationComponentsType) 
            : base(_pumpStation, _stationComponentsType)
        {
            if (PumpStation.IsAutoCalculationDiameterConnection) 
            {
                this.ComponentName = ComponentsNameAutoGenerate(PumpStation, 0, 0);
            }
            else this.ComponentName = ComponentsNameGenerate(PumpStation);

            ComponentLocationPath = ComponentsValCalculator.GetFullPathToTheComponent(_pumpStation.componentsLocation, this);
        }

        public virtual string ComponentsNameAutoGenerate(PumpStation pumpStation, double pumpsSuctionLineConnectionDn, double pumpsPressureLineConnectionDn)
        {
            string [] s1 = this.ComponentType.ToString().Split('_');
            if (this.ComponentType.Equals(StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора) ||
                this.ComponentType.Equals(StationComponentTypeEnum.КВЖ_катушка_для_жокей_насоса_всасывающая))
            {
                return $"{s1[0]}_DN{pumpStation.DnSuctionCollector}";
            }
            else if (this.ComponentType.Equals(StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора) ||
                this.ComponentType.Equals(StationComponentTypeEnum.КНЖ_катушка_для_жокей_насоса_напорная))
            {
                return $"{s1[0]}_DN{pumpStation.DnPressureCollector}";
            }
            else if (this.ComponentType.Equals(StationComponentTypeEnum.ФланецСРеле_))
            {
                return $"{s1[0]}_DN{pumpStation.mainPump.PressureSideDn}";
            }
            return $"{s1[0]}_DN{pumpStation.SecondaryLineDn}";
        }
        public virtual string ComponentsNameGenerate(PumpStation pumpStation)
        {
            string[] s1 = this.ComponentType.ToString().Split('_');
            
            return $"{s1[0]}_DN{pumpStation.StationScheme.stationChemeComponents[this.ComponentType].Item2}";
        }
    }
}
