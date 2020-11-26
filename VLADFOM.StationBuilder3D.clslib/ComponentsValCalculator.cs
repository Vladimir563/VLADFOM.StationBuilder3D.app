using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{

    #region Enums
        public enum StationComponentsTypeEnum
        {
            #region Pumps
                Насос_основной = 100,
                Насос_жокей = 101,
            #endregion

            #region Coils
                К_катушка = 200,
                КР_катушка_резьбовая = 201,
                КК_катушка_концентрическая = 202,
                ККР_катушка_концентрическая_резьбовая =  203,
                КЭ_катушка_эксцентрическая = 204,
                КЭ_катушка_эксцентрическая_резьбовая = 205,
            #endregion

            #region Collectors
                КВ_коллектор_всасывающий = 300,
                КН_коллектор_напорный = 301,
            #endregion

            #region Tees
                ТВ_тройник_всасывающий = 401,
                ТН_тройник_напорный = 402,
            #endregion

            #region Shutters
                ЗД_затвор_дисковый_всасывающего_коллектора = 501,
                ЗД_затвор_дисковый_напорного_коллектора = 502,
                ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора = 503,
                ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора = 504,
            #endregion

            #region CarveValves
                ОКР_обратный_клапан_резьбовой = 601,
            #endregion

            #region FlangeCheckValves
                ОКФ_обратный_клапан_фланцевый = 701,
            #endregion

            #region CarveCheckValves
                РК_резьбовой_кран = 801,
            #endregion

            #region Frame
                Рама_ = 901,
            #endregion

            #region ControlCabinets
                ШУ_ = 1001,
            #endregion

            #region ScrewFitting
                Американка_ = 1101,
            #endregion

            #region Nippels
                НиппельВнВн_ = 1201,
                НиппельВнН_ = 1202,
                НиппельНН_ = 1203,
            #endregion
        }
        public enum FrameTypesEnum
            {
                StandartRottenFrame = 0,
                WeldedFrame10 = 10,
                WeldedFrame12 = 12,
                WeldedFrame14 = 14,
                WeldedFrame16 = 16,
                WeldedFrame18 = 18
            }
        public enum CollectorsMaterialEnum 
        {
            Нержавеющая_сталь = 0,
            Чёрная_сталь = 1
        }
        public enum StationTypeEnum 
        {
            Пожаротушения = 0,
            Повышения_давления = 1
        }
    #endregion

    class ComponentsValCalculator
    {
        public static int[] GetControlCabinetPositionProp(string controlCabinetName)
        {
            throw new NotImplementedException();
        }

        public static FrameTypesEnum GetFrameTypeByPumpsWeight(double pumpWeight)
        {
            if (pumpWeight == 0) throw new Exception("Pumps weight can not be equals zero (0). Please initialize this field");
            if (pumpWeight < 200) { return FrameTypesEnum.StandartRottenFrame; }
            else if (pumpWeight < 400) { return FrameTypesEnum.WeldedFrame12; }
            else if (pumpWeight < 600) { return FrameTypesEnum.WeldedFrame14; }
            else if (pumpWeight < 800) { return FrameTypesEnum.WeldedFrame16; }
            else { return FrameTypesEnum.WeldedFrame18; }
        }

        public static int GetStationConnectionDnByConsumption(double consumption )
        {
            Dictionary<int, double> tubesDN = new Dictionary<int, double>()
            {
                { 50, 60.3 },
                { 65, 76.1},
                { 80, 88.9},
                { 100, 104},
                { 125, 129},
                { 150, 154},
                { 200, 204},
                { 250, 254},
                { 300, 304},
                { 350, 358},
                { 400, 408},
                { 450, 458},
                { 500, 508}
        };
            int DN = 0;
            double D = 0, insideD = 0, S = 0, Q = 0, VFact = 0;

            foreach(var dn in tubesDN)
            {
                DN = dn.Key;
                D = dn.Value;
                insideD = D - 4;
                S = (Math.PI * insideD * (insideD / 4)) / 1000000;
                Q = S * 3600;
                VFact = consumption / S / 3600;

                if (D < 254 && VFact >= 0.6 && VFact < 1.3)
                {
                    return DN;
                }
                else if (D >= 250 && D <= 800 && VFact >= 0.8 && VFact < 1.7)
                {
                    return DN;
                }
                else if (D > 800 && VFact >= 1.2 && VFact < 2.1)
                {
                    return DN;
                }
            }
            return 0;
        }

        public static string GetFullPathToTheComponent(PumpStation station, PumpStationComponent component) 
        {
            //доделать выбор дирректории от выбора компонента
            return String.Format(station.componentsLocationPaths["mainDirPath"] +
            (station.componentsLocationPaths[station.StationScheme.StationType.Equals(StationTypeEnum.Пожаротушения) ? "fireFightingStationCompPath" : "pressureIncreaseStationCompPath"]) +
            component.ComponentsName + ".SLDPRT");
        }
    }
}
