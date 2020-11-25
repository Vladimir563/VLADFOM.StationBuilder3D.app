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
        private string mainPumpsName;
        private string jockeyPumpsName;
        private string controlCabinetsSize;
        private bool isAutoCalculationDiameterConnection;
        public List<PumpStationComponent> stationComponents = new List<PumpStationComponent>();
        public List<int> formParametersGetter = new List<int>(); //for getting parameters(DN) from user forms

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
        public string MainPumpsName
        {
            get { return mainPumpsName; }
            set { mainPumpsName = value; }
        }
        public string JockeyPumpsName
        {
            get { return jockeyPumpsName; }
            set { jockeyPumpsName = value; }
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


        public PumpStation(string _mainPumpsName, string _jockeyPumpsName, string _controlCabinetsName, bool _isAutoCalculationDiameterConnection, int _pumpsCount, double _waterConsumption, int _dnSuctionCollector, int _dnPressureCollector,
            double _pressureValueForStation, CollectorsMaterialEnum _collectorsMaterial, StationScheme _stationScheme)
        {
            MainPumpsName = _mainPumpsName;
            JockeyPumpsName = _jockeyPumpsName;
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

            if (IsAutoCalculationDiameterConnection) 
            {
                AutoCreatePumpStationComponentsByScheme(StationScheme);
            }
            else CreatePumpStationComponentsByScheme(StationScheme);

        }

        public int GetSecondaryLineDn(int mainDn) 
        {
            int[] tubesDn = { 32, 40, 50, 65, 80, 100, 125, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600 };
            int currentDnIndex = Array.IndexOf(tubesDn, DnSuctionCollector);

            return tubesDn[currentDnIndex - 2];
        }

        public void CreatePumpStationComponentsByScheme(StationScheme stationScheme) 
        {
            foreach (var componentsType in stationScheme.stationComponents) 
            {
                string[] s1 = componentsType.ToString().Split('_');

                if (componentsType.Equals(StationComponentsTypeEnum.Насос_основной))
                {
                    stationComponents.Add(new Pump(this, 0, 0, 0, 0, 0, 0, 0, StationComponentsTypeEnum.Насос_основной, MainPumpsName, "",
                        0, 0, 0, 0));
                }
                else if (componentsType.Equals(StationComponentsTypeEnum.Насос_жокей))
                {
                    stationComponents.Add(new Pump(this, 0, 0, 0, 0, 0, 0, 0, StationComponentsTypeEnum.Насос_жокей, JockeyPumpsName, "",
                        0, 0, 0, 0));
                }

                #region UnEqualsFittings
                else if (s1[0].Equals("КЭ") || s1[0].Equals("КЭР") || s1[0].Equals("КК") || s1[0].Equals("ККР")
                || s1[0].Equals("ТВ") || s1[0].Equals("ТН") || s1[0].Equals("КВ") || s1[0].Equals("КН"))
                {
                    stationComponents.Add(new UnequalFittings(this, componentsType, "", "", 0, 0, 0, 0));
                }
                #endregion
                #region EqualsFittings
                else if (s1[0].Equals("К") || s1[0].Equals("КР") || s1[0].Equals("ЗД") || s1[0].Equals("ОКФ"))
                {
                    stationComponents.Add(new Fittings(this, componentsType, "", "", 0, 0, 0, 0));
                }
                #endregion

                else if (componentsType.Equals(StationComponentsTypeEnum.Рама_))
                {
                    stationComponents.Add(new Frame(this,StationComponentsTypeEnum.Рама_,"","",0,0,0,0));
                }
                else if (componentsType.Equals(StationComponentsTypeEnum.ШУ_)) 
                {
                    stationComponents.Add(new ControlCabinet(this, StationComponentsTypeEnum.ШУ_, "", "", 0, 0, 0, 0));
                }
                //to add another station components type
            }
        }

        //to realize this method
        public void AutoCreatePumpStationComponentsByScheme(StationScheme stationScheme) { }

    }
}
