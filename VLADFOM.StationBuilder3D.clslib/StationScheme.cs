using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLADFOM.StationBuilder3D.clslib
{
    public class StationScheme
    {
        private StationTypeEnum stationType;

        /// <summary>
        /// Parameters of the stationChemeComponents members: 
        /// the first value presents component's input diameter connection
        /// the second value presents component's output diameter connection
        /// int [] represents the array of 9 numbers (x1,x2,x3,y1,y2,y3,z1,z2,z3) there are the rotation values contains
        /// or it can be represents the array of 10 numbers (x1,x2,x3,y1,y2,y3,z1,z2,z3,a1)
        /// where a1 value (if exist) shows current pumps connection (is the new line of pump or not)
        /// the last value shows which line current component belongs (true - belongs pressure line, false - belongs suction line) 
        /// </summary>
        public Dictionary<StationComponentTypeEnum, (double, double, int[], bool)> stationChemeComponents;

        public StationTypeEnum StationType
        {
            get { return stationType; }
            set { stationType = value; }
        }

        #region FireProtectionStationsSchemes

        #region HorizontalPumps
        public static StationScheme GetSimpleFireProtectionScheme2HorizontalPumpsFlangePumpConnection(double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.КЭ_катушка_эксцентрическая, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, false)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.КК_катушка_концентрическая, (0, 0, new int[10], true)},
                {StationComponentTypeEnum.ОКФ_обратный_клапан_фланцевый, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.КР_катушка_резьбовая, (0, 0, new int []{0, 0, -1, 1, 0, 0, 0, -1, 0}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, (0, 0, new int[]{0, 0, -1, -1, 0, 0, 0, 1, 0}, true)},
                {StationComponentTypeEnum.ТН_тройник_напорный, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, (0, 0, new int[]{0, 0, -1, 0, -1, 0, -1, 0, 0}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }
        public static StationScheme GetSimpleFireProtectionScheme2HorizontalPumpsCarvePumpConnection(double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_1, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, false)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_2, (0, 0, new int[]{1, 0, 0, 0, 0, 1, 0, -1, 0}, true)},
                {StationComponentTypeEnum.ОКФ_обратный_клапан_фланцевый, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.КР_катушка_резьбовая, (0, 0, new int []{0, 0, -1, 1, 0, 0, 0, -1, 0}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, (0, 0, new int[]{0, 0, -1, -1, 0, 0, 0, 1, 0}, true)},
                {StationComponentTypeEnum.ТН_тройник_напорный, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, (0, 0, new int[]{0, 0, -1, 0, -1, 0, -1, 0, 0}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }
        public static StationScheme GetSimpleFireProtectionSchemeMoreThan2HorizontalPumpsFlangePumpConnection(double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.КЭ_катушка_эксцентрическая, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ККР_катушка_концентрическая_резьбовая, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ОКФ_обратный_клапан_фланцевый, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.КР_катушка_резьбовая, (0, 0, new int []{0, 0, -1, 1, 0, 0, 0, -1, 0}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, (0, 0, new int[]{0, 0, -1, 1, 0, 0, 0, -1, 0}, true)},
                {StationComponentTypeEnum.ТН_тройник_напорный, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, (0, 0, new int[]{0, 0, -1, 0, -1, 0, -1, 0, 0}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }
        public static StationScheme GetSimpleFireProtectionSchemeMoreThan2HorizontalPumpsCarvePumpConnection(double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ФланецСРеле_, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ОКФ_обратный_клапан_фланцевый, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.КР_катушка_резьбовая, (0, 0, new int []{0, 0, -1, 1, 0, 0, 0, -1, 0}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, (0, 0, new int[]{0, 0, -1, 1, 0, 0, 0, -1, 0}, true)},
                {StationComponentTypeEnum.ТН_тройник_напорный, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, (0, 0, new int[]{0, 0, -1, 0, -1, 0, -1, 0, 0}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }
        #endregion

        #region VerticalPumps
        public static StationScheme GetSimpleFireProtectionScheme2VerticalPumpsFlangePumpConnection(double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, false)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ОКФ_обратный_клапан_фланцевый, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, true)},
                {StationComponentTypeEnum.К_катушка, (0, 0, new int[]{ }, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, true)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_2, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, (0, 0, new int[]{0, 0, 1, 0, 1, 0, -1, 0, 0}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }
        public static StationScheme GetSimpleFireProtectionScheme2VerticalPumpsCarvePumpConnection(double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_1, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, false)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ОКФ_обратный_клапан_фланцевый, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.К_катушка, (0, 0, new int[]{ }, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, true)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_2, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, (0, 0, new int[]{0, 0, 1, 0, 1, 0, -1, 0, 0}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }
        public static StationScheme GetSimpleFireProtectionSchemeMoreThan2VerticalPumpsFlangePumpConnection(double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, false)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ФланецСРеле_, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ОКФ_обратный_клапан_фланцевый, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.К_катушка, (0, 0, new int[]{ }, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, (0, 0, new int[]{0, 0, -1, -1, 0, 0, 0, 1, 0}, true)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, (0, 0, new int[]{0, 0, -1, 0, -1, 0, -1, 0, 0}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }
        public static StationScheme GetSimpleFireProtectionSchemeMoreThan2VerticalPumpsCarvePumpConnection(double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, false)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ФланецСРеле_, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ОКФ_обратный_клапан_фланцевый, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.К_катушка, (0, 0, new int[]{ }, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, (0, 0, new int[]{0, 0, -1, -1, 0, 0, 0, 1, 0}, true)},
                {StationComponentTypeEnum.ТВ_тройник_всасывающий_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, (0, 0, new int[]{0, 0, -1, 0, -1, 0, -1, 0, 0}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }
        #endregion

        #endregion

        #region PressureIncreaseStationSchemes

        #region VerticalPumps
        public static StationScheme GetSimplePressureIncreaseSchemeVerticalPumpsFlangePumpConnectionBiggerThan50
            (double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, false)},
                {StationComponentTypeEnum.КВ_коллектор_всасывающий, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ОКФ_обратный_клапан_фланцевый, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.К_катушка, (0, 0, new int []{0, 0, -1, 1, 0, 0, 0, -1, 0}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, (0, 0, new int[]{0, 0, -1, -1, 0, 0, 0, 1, 0}, true)},
                {StationComponentTypeEnum.КН_коллектор_напорный_вертик_насос, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }

        public static StationScheme GetSimplePressureIncreaseSchemeVerticalPumpsFlangePumpConnectionSmallerThan50
            (double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.РК_резьбовой_кран_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.КВ_коллектор_всасывающий, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_2, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, true)},
                {StationComponentTypeEnum.ОКР_обратный_клапан_резьбовой, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, true)},
                {StationComponentTypeEnum.НиппельНН_1, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.РК_резьбовой_кран_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.КН_коллектор_напорный_вертик_насос, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }

        public static StationScheme GetSimplePressureIncreaseSchemeVerticalPumpsCarvePumpConnectionBiggerThan1_1_4
            (double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.Американка_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.НиппельНН_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.РК_резьбовой_кран_2, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.КВ_коллектор_всасывающий, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.Американка_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.НиппельНН_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ОКР_обратный_клапан_резьбовой, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.НиппельНН_3, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.РК_резьбовой_кран_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.КН_коллектор_напорный_вертик_насос, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }

        public static StationScheme GetSimplePressureIncreaseSchemeVerticalPumpsCarvePumpConnectionSmallerThan1_1_2
            (double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.Американка_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.РК_резьбовой_кран_2, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.КВ_коллектор_всасывающий, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.Американка_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.НиппельНН_1, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ОКР_обратный_клапан_резьбовой, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.РК_резьбовой_кран_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.КН_коллектор_напорный_вертик_насос, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }
        #endregion

        #region HorizontalPumps
        public static StationScheme GetSimplePressureIncreaseSchemeHorizontalPumpsFlangePumpConnectionBiggerThan50
            (double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.КЭ_катушка_эксцентрическая, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, false)},
                {StationComponentTypeEnum.КВ_коллектор_всасывающий, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.КК_катушка_концентрическая, (0, 0, new int[10], true)},
                {StationComponentTypeEnum.ОКФ_обратный_клапан_фланцевый, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.КР_катушка_резьбовая, (0, 0, new int []{0, 0, -1, 1, 0, 0, 0, -1, 0}, true)},
                {StationComponentTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, (0, 0, new int[]{0, 0, -1, -1, 0, 0, 0, 1, 0}, true)},
                {StationComponentTypeEnum.КН_коллектор_напорный_горизонт_насос, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }

        public static StationScheme GetSimplePressureIncreaseSchemeHorizontalPumpsFlangePumpConnectionSmallerThan50
            (double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.РК_резьбовой_кран_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.КВ_коллектор_всасывающий, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ФланцевыйПереход_2, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, true)},
                {StationComponentTypeEnum.ОКР_обратный_клапан_резьбовой, (0, 0, new int[]{-1, 0, 0, 0, 1, 0, 0, 0, -1}, true)},
                {StationComponentTypeEnum.НиппельНН_1, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.РК_резьбовой_кран_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.КН_коллектор_напорный_горизонт_насос, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }

        public static StationScheme GetSimplePressureIncreaseSchemeHorizontalPumpsCarvePumpConnectionBiggerThan1_1_4
            (double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.Американка_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.НиппельНН_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.РК_резьбовой_кран_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.КВ_коллектор_всасывающий, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.Американка_2, (0, 0, new int[]{1, 0, 0, 0, 0, -1, 0, 1, 0}, true)},
                {StationComponentTypeEnum.НиппельНН_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ОКР_обратный_клапан_резьбовой, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.НиппельНН_3, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.РК_резьбовой_кран_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.КН_коллектор_напорный_горизонт_насос, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }

        public static StationScheme GetSimplePressureIncreaseSchemeHorizontalPumpsCarvePumpConnectionSmallerThan1_1_2
            (double pumpPressureConnection, double pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationChemeComponents = new Dictionary<StationComponentTypeEnum, (double, double, int[], bool)>()
            {
                {StationComponentTypeEnum.Насос_основной, (pumpPressureConnection, pumpSuctionConnection, new int[]{}, false)},
                {StationComponentTypeEnum.Американка_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.РК_резьбовой_кран_1, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.КВ_коллектор_всасывающий, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.Американка_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.НиппельНН_1, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.ОКР_обратный_клапан_резьбовой, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.РК_резьбовой_кран_2, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.КН_коллектор_напорный_горизонт_насос, (0, 0, new int[]{}, true)},
                {StationComponentTypeEnum.Рама_, (0, 0, new int[]{}, false)},
                {StationComponentTypeEnum.ШУ_, (0, 0, new int[]{}, false)}
            };
            return scheme;
        }
        #endregion

        #endregion
    }
}
