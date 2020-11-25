﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class UnequalFittings : Fittings
    {
        public UnequalFittings(PumpStation _pumpStation, StationComponentsTypeEnum _stationComponentsType, 
            string _componentsName, string _pathToTheComponent, double _componentsWeight, int _rotationByX, 
            int _rotationByY, int _rotationByZ) 
            : base(_pumpStation, _stationComponentsType, _componentsName, _pathToTheComponent, _componentsWeight, 
                  _rotationByX, _rotationByY, _rotationByZ)
        {
            this.ComponentsName = this.ComponentsNameGenerate(PumpStation, PumpStation.Pump.SuctionSideDn,
                PumpStation.Pump.PressureSideDn);
        }

        public override string ComponentsNameGenerate(PumpStation pumpStation, int pumpsSuctionLineConnectionDn, 
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
                return $"{s1[0]}_DN{pumpStation.SecondaryLineDn}-{pumpStation.DnSuctionCollector}";
            }
            else if (s1[0].Equals("ТН") || s1[0].Equals("КН")) 
            {
                return $"{s1[0]}_DN{pumpStation.SecondaryLineDn}-{pumpStation.DnPressureCollector}";
            }
            return string.Empty;
        }

    }
}
