using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VLADFOM.StationBuilder3D.clslib;

namespace VLADFOM.StationBuilder3D.clsbllib
{
    public class BusinessLogic
    {
        public PumpStation StartAssembly(string pumpStationType, string pumpsName, string jockeyPumpsName, string pumpsType, 
            int pumpsCount, double waterConsumption, string controlCabinetsName, int dnSuctionCollector, int dnPressureCollector, 
            int pressureValueForStation, string collectorsMaterialTypeString)
        {
            //gets pumps connection from DB (when pump was added in data base successfully)
            int dnPumpsPressureConnection = 40; //gets it from DB
            int dnPumpsSuctionConnection = 50; //gets it from DB

            StationScheme currentScheme = GetStationSchemeByTypeValue(pumpStationType, pumpsCount, pumpsType, 
                dnPumpsPressureConnection, dnPumpsSuctionConnection);
            currentScheme.StationType = GetStationTypeEnumByTypesValue(pumpStationType);

            PumpStation pumpStation = new PumpStation(pumpsName, jockeyPumpsName, controlCabinetsName, true, pumpsCount, 
                waterConsumption, dnSuctionCollector, dnPressureCollector, pressureValueForStation,
                GetCollectorsMaterialEnumByTypesValue(collectorsMaterialTypeString), currentScheme);

            pumpStation.Pump.RightSidePlaneDistance = 56;
            pumpStation.Pump.ComponentsWeight = 156;

            Frame frame = (Frame)pumpStation.stationComponents[StationComponentsTypeEnum.Рама_];
            frame.ComponentsName = ComponentsValCalculator.GetFramesFullName(pumpStation);
            frame.PathToTheComponent = ComponentsValCalculator.GetFullPathToTheComponent(pumpStation, frame);

            return pumpStation;
        }

        public StationTypeEnum GetStationTypeEnumByTypesValue(string typeValueString) 
        {
            if (typeValueString.Equals(StationTypeEnum.Пожаротушения.ToString()))
            {
                return StationTypeEnum.Пожаротушения;
            }
            else if (typeValueString.Equals(StationTypeEnum.Повышения_давления.ToString()))
            {
                return StationTypeEnum.Повышения_давления;
            }
            else if (typeValueString.Equals(StationTypeEnum.Совмещённая.ToString()))
            {
                return StationTypeEnum.Совмещённая;
            }
            else if (typeValueString.Equals(StationTypeEnum.Мультидрайв.ToString()))
            {
                return StationTypeEnum.Мультидрайв;
            }
            else if (typeValueString.Equals(StationTypeEnum.Ф_Драйв.ToString())) 
            {
                return StationTypeEnum.Ф_Драйв;
            }

            throw new Exception();        
        }

        public CollectorsMaterialEnum GetCollectorsMaterialEnumByTypesValue(string typeValueString) 
        {
            if (typeValueString.Equals(CollectorsMaterialEnum.Нержавеющая_сталь.ToString()))
            {
                return CollectorsMaterialEnum.Нержавеющая_сталь;
            }
            else if (typeValueString.Equals(CollectorsMaterialEnum.Чёрная_сталь.ToString()))
            {
                return CollectorsMaterialEnum.Чёрная_сталь;
            }
            throw new Exception();
        }

        public StationScheme GetStationSchemeByTypeValue(string pumpStationType, 
            int pumpsCount, string pumpsType, int dnPumpsPressureConnection, int dnPumpsSuctionConnection) 
        {
            if (pumpStationType.Equals(StationTypeEnum.Пожаротушения.ToString()) && pumpsCount == 2 && pumpsType.Equals(PumpsType.Горизонтальный.ToString())) 
            {
                return StationScheme.GetSimpleFireProtectionScheme2HorizontalPumps(dnPumpsPressureConnection, dnPumpsSuctionConnection);
            }
            else if (pumpStationType.Equals(StationTypeEnum.Пожаротушения.ToString()) && pumpsCount > 2 && pumpsType.Equals(PumpsType.Горизонтальный.ToString()))
            {
                return StationScheme.GetSimpleFireProtectionSchemeMoreThan2HorizontalPumps(dnPumpsPressureConnection, dnPumpsSuctionConnection);
            }
            else if (pumpStationType.Equals(StationTypeEnum.Пожаротушения.ToString()) && pumpsCount == 2 && pumpsType.Equals(PumpsType.Вертикальный.ToString()))
            {
                return StationScheme.GetSimpleFireProtectionScheme2VerticalPumps(dnPumpsPressureConnection, dnPumpsSuctionConnection);
            }
            else if (pumpStationType.Equals(StationTypeEnum.Пожаротушения.ToString()) && pumpsCount > 2 && pumpsType.Equals(PumpsType.Вертикальный.ToString()))
            {
                return StationScheme.GetSimpleFireProtectionSchemeMoreThan2VerticalPumps(dnPumpsPressureConnection, dnPumpsSuctionConnection);
            }

            throw new Exception();
        }
    }
}
