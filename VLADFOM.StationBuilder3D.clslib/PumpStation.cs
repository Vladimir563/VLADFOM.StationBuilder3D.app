using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class PumpStation
    {
        private int pumpsCount;
        private double waterConsumption;
        private int dnSuctionCollector;
        private int dnPressureCollector;
        private int secondaryLineDn;
        private double pressureValueForStation;
        private CollectorsMaterialEnum collectorsMaterial;
        private StationScheme stationScheme;
        private Pump pump;
        private bool isAutoCalculationDiameterConnection;

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
        public Pump Pump
        {
            get { return pump; }
            set { pump = value; }
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

        public PumpStation(bool _isAutoCalculationDiameterConnection, int _pumpsCount, double _waterConsumption, int _dnSuctionCollector, int _dnPressureCollector,
            double _pressureValueForStation, CollectorsMaterialEnum _collectorsMaterial, StationScheme _stationScheme)
        {
            IsAutoCalculationDiameterConnection = _isAutoCalculationDiameterConnection;
            PumpsCount = _pumpsCount;
            WaterConsumption = _waterConsumption;
            DnSuctionCollector = _dnSuctionCollector;
            DnPressureCollector = _dnPressureCollector;
            PressureValueForStation = _pressureValueForStation;
            CollectorsMaterial = _collectorsMaterial;
            StationScheme = _stationScheme;
            SecondaryLineDn = GetSecondaryLineDn(DnSuctionCollector);
        }

        public int GetSecondaryLineDn(int mainDn) 
        {
            int[] tubesDn = { 32, 40, 50, 65, 80, 100, 125, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600 };
            int currentDnIndex = Array.IndexOf(tubesDn, DnSuctionCollector);

            return tubesDn[currentDnIndex - 2];
        }
    }
}
