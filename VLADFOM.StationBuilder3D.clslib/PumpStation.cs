using System;
using System.Collections.Generic;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class PumpStation
    {
        public Pump mainPump;
        public Pump jockeyPump;
        private int pumpsCount;
        private double waterConsumption;
        private double dnSuctionCollector;
        private double dnPressureCollector;
        private double secondaryLineDn;
        private double pressureValueForStation;
        private CollectorsMaterialEnum collectorMaterial;
        private StationScheme stationScheme;
        private string controlCabinetSize;
        private bool isAutoCalculationDiameterConnection;
        private int distanceBetweenAxis;
        public ComponentsLocationPaths componentsLocation;
        public List<StationComponent> stationComponents;

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
        public double DnSuctionCollector
        {
            get { return dnSuctionCollector; }
            set 
                {
                    if (IsAutoCalculationDiameterConnection)
                    {
                        if (!this.stationScheme.StationType.Equals(StationTypeEnum.Пожаротушения) &&
                            !this.stationScheme.StationType.Equals(StationTypeEnum.Ф_Драйв) &&
                            ComponentsValCalculator.GetStationConnectionDnByConsumption(WaterConsumption).Equals(50)) 
                            {
                                dnSuctionCollector = 2;
                            }
                        else dnSuctionCollector = ComponentsValCalculator.GetStationConnectionDnByConsumption(WaterConsumption);
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
        public double DnPressureCollector
        {
            get { return dnPressureCollector; }
            set 
            {
                if (IsAutoCalculationDiameterConnection)
                {
                    if (!this.stationScheme.StationType.Equals(StationTypeEnum.Пожаротушения) &&
                        !this.stationScheme.StationType.Equals(StationTypeEnum.Ф_Драйв) &&
                        ComponentsValCalculator.GetStationConnectionDnByConsumption(WaterConsumption).Equals(50))
                    {
                        dnPressureCollector = 2;
                    }
                    else dnPressureCollector = ComponentsValCalculator.GetStationConnectionDnByConsumption(WaterConsumption);
                }
                else
                {
                    dnPressureCollector = value;
                }

            }
        }
        public CollectorsMaterialEnum CollectorMaterial
        {
            get { return collectorMaterial; }
            set { collectorMaterial = value; }
        }
        public StationScheme StationScheme
        {
            get { return stationScheme; }
            set { stationScheme = value; }
        }
        public string ControlCabinetSize
        {
            get { return controlCabinetSize; }
            set { controlCabinetSize = value; }
        }
        public bool IsAutoCalculationDiameterConnection
        {
            get { return isAutoCalculationDiameterConnection; }
            set { isAutoCalculationDiameterConnection = value; }
        }
        public double SecondaryLineDn
        {
            get { return secondaryLineDn; }
            set { secondaryLineDn = value; }
        }
        public int DistanceBetweenAxis
        {
            get { return distanceBetweenAxis; }
            set { distanceBetweenAxis = value; }
        }

        public PumpStation( ComponentsLocationPaths _componentsLocation, Pump _mainPump, Pump _jockeyPump, string _controlCabinetName, 
            bool _isAutoCalculationDiameterConnection, int _pumpsCount, double _waterConsumption, int _dnSuctionCollector, 
            int _dnPressureCollector, double _pressureValueForStation, CollectorsMaterialEnum _collectorMaterial, 
            StationScheme _stationScheme)
        {
            stationComponents = new List<StationComponent>();
            StationScheme = _stationScheme;
            componentsLocation = _componentsLocation;
            mainPump = _mainPump;
            jockeyPump = _jockeyPump;
            ControlCabinetSize = _controlCabinetName;
            IsAutoCalculationDiameterConnection = _isAutoCalculationDiameterConnection;
            PumpsCount = _pumpsCount;
            WaterConsumption = _waterConsumption;
            DnSuctionCollector = _dnSuctionCollector;
            DnPressureCollector = _dnPressureCollector;
            PressureValueForStation = _pressureValueForStation;
            CollectorMaterial = _collectorMaterial;
            SecondaryLineDn = GetSecondaryLineDn(DnSuctionCollector);
            DistanceBetweenAxis = ComponentsValCalculator.GetDistanceBetweenPumpsAxis(mainPump.PumpsWidth);

            CreatePumpStationComponentsByScheme(StationScheme);
        }

        public double GetSecondaryLineDn(double mainDn) 
        {
            if (mainDn == 0) return 0;
            if (mainPump.PumpsType.Equals(PumpsType.Вертикальный))
            {
                if ((this.stationScheme.StationType.Equals(StationTypeEnum.Пожаротушения) ||
                    this.stationScheme.StationType.Equals(StationTypeEnum.Ф_Драйв)))
                {
                    if (mainPump.SuctionSideDn <= 2) 
                    {
                        double[] flangeConnectionArr = { 25, 32, 40, 50 };
                        double[] carveConnectionArr = { 1, 1.25, 1.5, 2 };
                        return flangeConnectionArr[Array.IndexOf(carveConnectionArr, mainPump.SuctionSideDn)];
                    }
                    return mainPump.SuctionSideDn;
                }
                else 
                {
                    if (mainPump.SuctionSideDn > 50) { return mainPump.SuctionSideDn; }

                    double[] flangeConnectionArr = { 25, 32, 40, 50 };
                    double[] carveConnectionArr = { 1, 1.25, 1.5, 2 };
                    return carveConnectionArr[Array.IndexOf(flangeConnectionArr, mainPump.SuctionSideDn)];
                }
            }

            double[] tubesDnFireProtectionStation = { 1, 1.25, 1.5, 2, 25, 32, 40, 50, 65, 80, 100, 125, 150, 200, 250, 300, 350, 400, 450, 500, 550, 600 };

            int currentDnIndex = Array.IndexOf(tubesDnFireProtectionStation, DnSuctionCollector);

            return DnSuctionCollector < 150 ? tubesDnFireProtectionStation[currentDnIndex - 1] : tubesDnFireProtectionStation[currentDnIndex - 2];
        }

        //taking the value of stationScheme list, checking type and creating instance of current type of the component
        public void CreatePumpStationComponentsByScheme(StationScheme stationScheme) 
        {
            foreach (var schemeComponent in stationScheme.stationChemeComponents) 
            {
                string[] s1 = schemeComponent.Key.ToString().Split('_');

                if (schemeComponent.Key.Equals(StationComponentTypeEnum.Насос_основной))
                {
                    Pump stationPump = mainPump;
                    StationComponentInitialize(stationPump, stationScheme);
                    stationComponents.Add(stationPump);

                }
                else if (schemeComponent.Equals(StationComponentTypeEnum.Насос_жокей))
                {
                    Pump stationJockeyPump = jockeyPump;
                    StationComponentInitialize(stationJockeyPump, stationScheme);
                    stationComponents.Add(stationJockeyPump);
                }

                #region UnEqualsFittings
                else if (s1[0].Equals("КЭ") || s1[0].Equals("КЭР") || s1[0].Equals("КК") || s1[0].Equals("ККР")
                || s1[0].Equals("ТВ") || s1[0].Equals("ТН") || s1[0].Equals("КВ") || s1[0].Equals("КН"))
                {
                    UnequalFittings unequalFitting = new UnequalFittings(this, schemeComponent.Key);
                    StationComponentInitialize(unequalFitting, stationScheme);
                    stationComponents.Add(unequalFitting);
                }
                #endregion
                #region EqualsFittings
                else if (s1[0].Equals("К") || s1[0].Equals("КР") || s1[0].Equals("ЗД") || s1[0].Equals("ОКФ") ||
                    s1[0].Equals("ОКР") || s1[0].Equals("РК") || s1[0].Equals("Американка") || s1[0].Equals("НиппельВнВн")
                    || s1[0].Equals("НиппельВнН") || s1[0].Equals("НиппельНН") || s1[0].Equals("КВЖ") || s1[0].Equals("КНЖ") 
                    || s1[0].Equals("ФланецСРеле") || s1[0].Equals("ФланцевыйПереход"))
                {
                    Fittings fitting = new Fittings(this, schemeComponent.Key);
                    StationComponentInitialize(fitting, stationScheme);
                    stationComponents.Add(fitting);
                }
                #endregion

                else if (schemeComponent.Key.Equals(StationComponentTypeEnum.Рама_))
                {
                    Frame frame = new Frame(this, StationComponentTypeEnum.Рама_);
                    StationComponentInitialize(frame, stationScheme);
                    stationComponents.Add(frame);
                }
                else if (schemeComponent.Key.Equals(StationComponentTypeEnum.ШУ_))
                {
                    ControlCabinet controlCabinet = new ControlCabinet(this, StationComponentTypeEnum.ШУ_, "ШУ_" + ControlCabinetSize);
                    StationComponentInitialize(controlCabinet, stationScheme);
                    stationComponents.Add(controlCabinet);
                }
            }
        }

        public void StationComponentInitialize(StationComponent stationComponent, StationScheme stationScheme)
        {
            foreach (var schemeComponent in stationScheme.stationChemeComponents)
            {
                if (stationComponent.ComponentType.Equals(schemeComponent.Key) && schemeComponent.Value.Item3.Length > 0)
                {
                    stationComponent.RotationComponentArray = schemeComponent.Value.Item3;
                    stationComponent.IsComponentForPressureLine = schemeComponent.Value.Item4;
                }
                else if (stationComponent.ComponentType.Equals(schemeComponent.Key))
                {
                    stationComponent.RotationComponentArray = new int[] { 1, 0, 0, 0, 1, 0, 0, 0, 1 };
                    stationComponent.IsComponentForPressureLine = schemeComponent.Value.Item4;
                }
            }
        }

    }
}
