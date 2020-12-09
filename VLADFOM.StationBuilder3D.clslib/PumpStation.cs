﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class PumpStation
    {
        public Pump mainPump;
        public Pump jockeyPump;
        private int pumpsCount;
        private double waterConsumption;
        private int dnSuctionCollector;
        private int dnPressureCollector;
        private int secondaryLineDn;
        private double pressureValueForStation;
        private CollectorsMaterialEnum collectorsMaterial;
        private StationScheme stationScheme;
        private string controlCabinetsSize;
        private bool isAutoCalculationDiameterConnection;
        private int distanceBetweenAxis;
        public ComponentsLocationPaths componentsLocation;
        public Dictionary<StationComponentsTypeEnum, PumpStationComponent> stationComponents = new Dictionary<StationComponentsTypeEnum, PumpStationComponent>();

        public int PumpsCount
        {
            get { return pumpsCount; }
            set { pumpsCount = value; }
        }
        public double WaterConsumption
        {
            get { return waterConsumption; }
            set { waterConsumption = value; }
        }
        public int DnSuctionCollector
        {
            get { return dnSuctionCollector; }
            set 
                {
                    if (IsAutoCalculationDiameterConnection)
                    {
                        dnSuctionCollector = ComponentsValCalculator.GetStationConnectionDnByConsumption(WaterConsumption);
                    }
                    else 
                    {
                        dnSuctionCollector = value;
                    }
                }
        }
        public double PressureValueForStation
        {
            get { return pressureValueForStation; }
            set { pressureValueForStation = value; }
        }
        public int DnPressureCollector
        {
            get { return dnPressureCollector; }
            set 
            {
                if (IsAutoCalculationDiameterConnection)
                {
                    dnPressureCollector = ComponentsValCalculator.GetStationConnectionDnByConsumption(WaterConsumption);
                }
                else 
                {
                    dnPressureCollector = value;
                }
                
            }
        }
        public CollectorsMaterialEnum CollectorsMaterial
        {
            get { return collectorsMaterial; }
            set { collectorsMaterial = value; }
        }
        public StationScheme StationScheme
        {
            get { return stationScheme; }
            set { stationScheme = value; }
        }
        public string ControlCabinetsSize
        {
            get { return controlCabinetsSize; }
            set { controlCabinetsSize = value; }
        }
        public bool IsAutoCalculationDiameterConnection
        {
            get { return isAutoCalculationDiameterConnection; }
            set { isAutoCalculationDiameterConnection = value; }
        }
        public int SecondaryLineDn
        {
            get { return secondaryLineDn; }
            set { secondaryLineDn = value; }
        }
        public int DistanceBetweenAxis
        {
            get { return distanceBetweenAxis; }
            set { distanceBetweenAxis = value; }
        }

        public PumpStation( ComponentsLocationPaths _componentsLocation, Pump _mainPump, Pump _jockeyPump, string _controlCabinetsName, 
            bool _isAutoCalculationDiameterConnection, int _pumpsCount, double _waterConsumption, int _dnSuctionCollector, 
            int _dnPressureCollector, double _pressureValueForStation, CollectorsMaterialEnum _collectorsMaterial, 
            StationScheme _stationScheme)
        {
            componentsLocation = _componentsLocation;
            mainPump = _mainPump;
            jockeyPump = _jockeyPump;
            ControlCabinetsSize = _controlCabinetsName;
            IsAutoCalculationDiameterConnection = _isAutoCalculationDiameterConnection;
            PumpsCount = _pumpsCount;
            WaterConsumption = _waterConsumption;
            DnSuctionCollector = _dnSuctionCollector;
            DnPressureCollector = _dnPressureCollector;
            PressureValueForStation = _pressureValueForStation;
            CollectorsMaterial = _collectorsMaterial;
            StationScheme = _stationScheme;
            SecondaryLineDn = GetSecondaryLineDn(DnSuctionCollector);
            DistanceBetweenAxis = ComponentsValCalculator.GetDistanceBetweenPumpsAxis(mainPump.PumpsWidth);

            CreatePumpStationComponentsByScheme(StationScheme);
        }

        public int GetSecondaryLineDn(int mainDn) 
        {
            if (mainDn == 0) return 0;
            int[] tubesDn = { 25, 32, 40, 50, 65, 80, 100, 125, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600 };
            int currentDnIndex = Array.IndexOf(tubesDn, DnSuctionCollector);

            return DnSuctionCollector < 150 ? tubesDn[currentDnIndex - 1] : tubesDn[currentDnIndex - 2];
        }

        //taking the value of stationScheme list, checking type and creating instance of current type component
        public void CreatePumpStationComponentsByScheme(StationScheme stationScheme) 
        {
            foreach (var componentsType in stationScheme.stationComponents) 
            {
                string[] s1 = componentsType.Key.ToString().Split('_');

                if (componentsType.Key.Equals(StationComponentsTypeEnum.Насос_основной))
                {
                    Pump stationPump = mainPump;
                    StationComponentInitialize(stationPump, stationScheme);
                    stationComponents.Add(componentsType.Key, stationPump);

                }
                else if (componentsType.Equals(StationComponentsTypeEnum.Насос_жокей))
                {
                    Pump stationJockeyPump = jockeyPump;
                    StationComponentInitialize(stationJockeyPump, stationScheme);
                    stationComponents.Add(componentsType.Key, stationJockeyPump);
                }

                #region UnEqualsFittings
                else if (s1[0].Equals("КЭ") || s1[0].Equals("КЭР") || s1[0].Equals("КК") || s1[0].Equals("ККР")
                || s1[0].Equals("ТВ") || s1[0].Equals("ТН") || s1[0].Equals("КВ") || s1[0].Equals("КН"))
                {
                    UnequalFittings unequalFitting = new UnequalFittings(this, componentsType.Key);
                    StationComponentInitialize(unequalFitting, stationScheme);
                    stationComponents.Add(componentsType.Key, unequalFitting);
                }
                #endregion
                #region EqualsFittings
                else if (s1[0].Equals("К") || s1[0].Equals("КР") || s1[0].Equals("ЗД") || s1[0].Equals("ОКФ") ||
                    s1[0].Equals("ОКР") || s1[0].Equals("РК") || s1[0].Equals("Американка") || s1[0].Equals("НиппельВнВн")
                    || s1[0].Equals("НиппельВнН") || s1[0].Equals("НиппельНН") || s1[0].Equals("КВЖ") || s1[0].Equals("КНЖ") 
                    || s1[0].Equals("ФланецСРеле"))
                {
                    Fittings fitting = new Fittings(this, componentsType.Key);
                    StationComponentInitialize(fitting, stationScheme);
                    stationComponents.Add(componentsType.Key, fitting);
                }
                #endregion

                else if (componentsType.Key.Equals(StationComponentsTypeEnum.Рама_))
                {
                    Frame frame = new Frame(this, StationComponentsTypeEnum.Рама_);
                    StationComponentInitialize(frame, stationScheme);
                    stationComponents.Add(componentsType.Key, frame);
                }
                else if (componentsType.Key.Equals(StationComponentsTypeEnum.ШУ_))
                {
                    ControlCabinet controlCabinet = new ControlCabinet(this, StationComponentsTypeEnum.ШУ_, "ШУ_" + ControlCabinetsSize);
                    StationComponentInitialize(controlCabinet, stationScheme);
                    stationComponents.Add(componentsType.Key, controlCabinet);
                }
            }
        }

        public void StationComponentInitialize(PumpStationComponent stationComponent, StationScheme stationScheme) 
        {
            foreach (var componentsType in stationScheme.stationComponents)
            {
                if (stationComponent.StationComponentsType.Equals(componentsType.Key) && componentsType.Value.Length > 0)
                {
                    stationComponent.RotationByX1 = componentsType.Value[2];
                    stationComponent.RotationByX2 = componentsType.Value[3];
                    stationComponent.RotationByX3 = componentsType.Value[4];
                    stationComponent.RotationByY1 = componentsType.Value[5];
                    stationComponent.RotationByY2 = componentsType.Value[6];
                    stationComponent.RotationByY3 = componentsType.Value[7];
                    stationComponent.RotationByZ1 = componentsType.Value[8];
                    stationComponent.RotationByZ2 = componentsType.Value[9];
                    stationComponent.RotationByZ3 = componentsType.Value[10];
                    stationComponent.IsComponentForTheNewLine = componentsType.Value[11];
                }
                else if(stationComponent.StationComponentsType.Equals(componentsType.Key))
                {
                    stationComponent.RotationByX1 = 0;
                    stationComponent.RotationByX2 = 0;
                    stationComponent.RotationByX3 = 0;
                    stationComponent.RotationByY1 = 0;
                    stationComponent.RotationByY2 = 0;
                    stationComponent.RotationByY3 = 0;
                    stationComponent.RotationByZ1 = 0;
                    stationComponent.RotationByZ2 = 0;
                    stationComponent.RotationByZ3 = 0;
                    stationComponent.IsComponentForTheNewLine = 0;
                }
            }
        }
    }
}
