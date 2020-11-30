using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class Fittings : PumpStationComponent, IStationComponentInitiable
    {
        public Fittings(PumpStation _pumpStation, StationComponentsTypeEnum _stationComponentsType, string _componentsName,
            double _componentsWeight, int _rotationByX, int _rotationByY, int _rotationByZ) 
            : base(_pumpStation,_stationComponentsType, _componentsName, _componentsWeight, _rotationByX, 
                  _rotationByY, _rotationByZ)
        {
            if (PumpStation.IsAutoCalculationDiameterConnection) 
            {
                this.ComponentsName = ComponentsNameAutoGenerate(PumpStation, 0, 0);
            }
            else this.ComponentsName = ComponentsNameGenerate(PumpStation);

            PathToTheComponent = ComponentsValCalculator.GetFullPathToTheComponent(PumpStation, this);
        }

        public virtual string ComponentsNameAutoGenerate(PumpStation pumpStation, int pumpsSuctionLineConnectionDn, int pumpsPressureLineConnectionDn)
        {
            string [] s1 = this.StationComponentsType.ToString().Split('_');
            if (this.StationComponentsType.Equals(StationComponentsTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора))
            {
                return $"{s1[0]}_DN{pumpStation.DnSuctionCollector}";
            }
            else if (this.StationComponentsType.Equals(StationComponentsTypeEnum.ЗД_затвор_дисковый_напорного_коллектора)) 
            {
                return $"{s1[0]}_DN{pumpStation.DnPressureCollector}";
            }
            return $"{s1[0]}_DN{pumpStation.SecondaryLineDn}";
        }
        public virtual string ComponentsNameGenerate(PumpStation pumpStation)
        {
            string[] s1 = this.StationComponentsType.ToString().Split('_');

            return $"{s1[0]}_DN{pumpStation.StationScheme.stationComponents[this.StationComponentsType][1]}";
        }
    }
}
