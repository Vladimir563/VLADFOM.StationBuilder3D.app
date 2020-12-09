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
                КВЖ_катушка_для_жокей_насоса_всасывающая = 206,
                КНЖ_катушка_для_жокей_насоса_напорная = 207,
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

            #region FlangeWithReley
                ФланецСРеле_ = 1301
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
            Повышения_давления = 1,
            Совмещённая = 2,
            Ф_Драйв = 3,
            Мультидрайв = 4
        }
        public enum PumpsType 
        {
            Горизонтальный = 0,
            Вертикальный = 1
        }
    #endregion

    public class ComponentsValCalculator
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

        public static string GetFramesFullName(PumpStation _pumpStation)
        {
            if (GetFrameTypeByPumpsWeight(_pumpStation.mainPump.ComponentsWeight).Equals(FrameTypesEnum.StandartRottenFrame))
            {
                string rottenFrameSize = string.Empty;
                if (_pumpStation.DistanceBetweenAxis == 300)
                {
                    if (_pumpStation.PumpsCount == 2)
                    {
                        rottenFrameSize = "400х600х4";
                    }
                    else if (_pumpStation.PumpsCount == 3)
                    {
                        rottenFrameSize = "400х900х4";
                    }
                    else if (_pumpStation.PumpsCount == 4)
                    {
                        rottenFrameSize = "400х1200х4";
                    }
                }
                else
                {
                    if (_pumpStation.PumpsCount == 2)
                    {
                        rottenFrameSize = "500х990х4";
                    }
                    else if (_pumpStation.PumpsCount == 3)
                    {
                        rottenFrameSize = "500х1200х4";
                    }
                    else if (_pumpStation.PumpsCount == 4)
                    {
                        rottenFrameSize = "500х1700х4";
                    }
                }

                if (rottenFrameSize == string.Empty)
                {
                    return "Рама_швеллер_10П";
                }
                return $"Рама_гнутая_{rottenFrameSize}";
            }

            return $"Рама_швеллер_{(int)ComponentsValCalculator.GetFrameTypeByPumpsWeight(_pumpStation.mainPump.ComponentsWeight)}П";
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

        public static string GetFullPathToTheComponent(ComponentsLocationPaths componentsLocation, 
            PumpStationComponent component) 
        {
            return String.Format(componentsLocation.componentsLocationPaths["mainDirPath"] + 
            GetComponentsPathByType(componentsLocation, component) + component.ComponentsName + ".SLDPRT");
        }

        public static string GetFullPathToTheComponent(ComponentsLocationPaths componentsLocation, string pumpName,
    PumpStationComponent component)
        {
            return String.Format(componentsLocation.componentsLocationPaths["mainDirPath"] +
            GetComponentsPathByType(componentsLocation, pumpName, component) + component.ComponentsName + ".SLDPRT");
        }

        public static string GetComponentsPathByType(ComponentsLocationPaths componentsLocation, string pumpName, PumpStationComponent component) 
        {
            string[] startMainPumpsNameChars = pumpName.Split('_');
            return componentsLocation.componentsLocationPaths["pumpsPath"] + startMainPumpsNameChars[0] + @"\";

        }

        public static string GetComponentsPathByType(ComponentsLocationPaths componentsLocation, PumpStationComponent component) 
        {
            string[] s1 = component.StationComponentsType.ToString().Split('_');
            
            if (s1[0].Equals("КВ") || s1[0].Equals("КН"))
            {
                return componentsLocation.componentsLocationPaths["collectorsPath"] + (component.StationComponentsType.Equals(StationComponentsTypeEnum.КВ_коллектор_всасывающий)
                    ? componentsLocation.componentsLocationPaths["suctionCollectorsPath"] : componentsLocation.componentsLocationPaths["pressureCollectorsPath"]);
            }
            else if (s1[0].Equals("ОКФ") || s1[0].Equals("ОКР"))
            {
                return componentsLocation.componentsLocationPaths["checkValvesPath"] + (component.StationComponentsType.Equals(StationComponentsTypeEnum.ОКФ_обратный_клапан_фланцевый)
                    ? componentsLocation.componentsLocationPaths["flangeCheckValvesPath"] : componentsLocation.componentsLocationPaths["carveCheckValvesPath"]);
            }
            else if (s1[0].Equals("ЗД") || s1[0].Equals("РК"))
            {
                return componentsLocation.componentsLocationPaths["lockValvesPath"] + (component.StationComponentsType.Equals(StationComponentsTypeEnum.РК_резьбовой_кран)
                    ? componentsLocation.componentsLocationPaths["carvesValvesPath"] : componentsLocation.componentsLocationPaths["shuttersPath"]);
            }
            else if (s1[0].Equals("ТВ") || s1[0].Equals("ТН"))
            {
                return componentsLocation.componentsLocationPaths["teesPath"] + (component.StationComponentsType.Equals(StationComponentsTypeEnum.ТВ_тройник_всасывающий)
                    ? componentsLocation.componentsLocationPaths["suctionTeesPath"] : componentsLocation.componentsLocationPaths["pressureTeesPath"]);
            }
            else if (s1[0].Equals("ШУ"))
            {
                return componentsLocation.componentsLocationPaths["controlCabinetsPath"];
            }
            else if (s1[0].Equals("КЭ") || s1[0].Equals("КЭР") || s1[0].Equals("КК") || s1[0].Equals("ККР") || s1[0].Equals("К")
                || s1[0].Equals("КР") || s1[0].Equals("КВЖ") || s1[0].Equals("КНЖ"))
            {
                string localPath = string.Empty;
                if (s1[0].Equals("КЭ")) localPath = componentsLocation.componentsLocationPaths["essentricCoilsPath"];
                if (s1[0].Equals("КЭР")) localPath = componentsLocation.componentsLocationPaths["essentricCoilsPathWithNippel"];
                if (s1[0].Equals("КК")) localPath = componentsLocation.componentsLocationPaths["concentricCoilsPath"];
                if (s1[0].Equals("ККР")) localPath = componentsLocation.componentsLocationPaths["concentricCoilsWithNippelPath"];
                if (s1[0].Equals("К")) localPath = componentsLocation.componentsLocationPaths["simpleCoilsPath"];
                if (s1[0].Equals("КР")) localPath = componentsLocation.componentsLocationPaths["simpleCoilsWithNippelPath"];
                if (s1[0].Equals("КВЖ")) localPath = componentsLocation.componentsLocationPaths["jockeySuctionCoils"];
                if (s1[0].Equals("КНЖ")) localPath = componentsLocation.componentsLocationPaths["jockeyPressureCoils"];

                return componentsLocation.componentsLocationPaths["coilsPath"] + localPath;
            }
            else if (s1[0].Equals("Рама"))
            {
                Frame frame = (Frame)component;
                return componentsLocation.componentsLocationPaths["framesPath"] + (frame.FrameType.Equals(FrameTypesEnum.StandartRottenFrame)
                    ? componentsLocation.componentsLocationPaths["weldedFramesPath"] : componentsLocation.componentsLocationPaths["framesFromShvellerPath"]);
            }
            else if (s1[0].Equals("ФланецСРеле")) 
            {
                return componentsLocation.componentsLocationPaths["flangesWithReley"];
            }

            return string.Empty;
        }

        public static int GetDistanceBetweenPumpsAxis(int pumpsWidth) 
        {
            int[] distanceBetweenPumpsAxis = {300, 500, 600, 700, 800, 900, 1000};

            for (int i = 0; i < distanceBetweenPumpsAxis.Length; i++)
            {
                if (distanceBetweenPumpsAxis[i] - pumpsWidth > 200)
                {
                    return distanceBetweenPumpsAxis[i];
                }
            }

            throw new Exception("There is no right distance between axis for current pumps instance");
        }
    }
}

