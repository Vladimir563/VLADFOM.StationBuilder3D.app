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
        /// StationComponentsTypeEnum - components type
        /// int[0,0] - there are two component diameters you need in the array
        /// the first value is smaller DN 
        /// the second value is bigger DN (or the main DN for component from current form instance)
        /// the third value is rotation by X axes
        /// the fourth value is rotation by Y axes
        /// the fifth value is rotation by Z axes
        /// </summary>
        public Dictionary<StationComponentsTypeEnum, int[]> stationComponents = new Dictionary<StationComponentsTypeEnum, int[]>();

        public StationTypeEnum StationType
        {
            get { return stationType; }
            set { stationType = value; }
        }

        #region FireProtectionStationsSchemes
        public static StationScheme GetSimpleFireProtectionScheme2HorizontalPumps(int pumpPressureConnection, int pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationComponents = new Dictionary<StationComponentsTypeEnum, int[]>
            {
                { StationComponentsTypeEnum.Насос_основной, new int[]{ pumpPressureConnection,pumpSuctionConnection, 0, 0, 0, 0, 0, 0, 0, 0, 0 }},
                { StationComponentsTypeEnum.КЭ_катушка_эксцентрическая, new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
                { StationComponentsTypeEnum.ТВ_тройник_всасывающий,new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, new int[]{} },
                { StationComponentsTypeEnum.КК_катушка_концентрическая, new int []{} },
                { StationComponentsTypeEnum.ОКФ_обратный_клапан_фланцевый, new int[]{} },
                { StationComponentsTypeEnum.КР_катушка_резьбовая,new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, new int[]{} },
                { StationComponentsTypeEnum.ТН_тройник_напорный, new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, new int[]{} },
                { StationComponentsTypeEnum.Рама_, new int[]{} },
                { StationComponentsTypeEnum.ШУ_, new int[]{} }
            };
            return scheme;
        }

        public static StationScheme GetSimpleFireProtectionSchemeMoreThan2HorizontalPumps(int pumpPressureConnection, int pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationComponents = new Dictionary<StationComponentsTypeEnum, int[]>
            {
                { StationComponentsTypeEnum.Насос_основной, new int[]{pumpPressureConnection,pumpSuctionConnection,0,0,0}},
                { StationComponentsTypeEnum.КЭ_катушка_эксцентрическая, new int[]{}},
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, new int[]{} },
                { StationComponentsTypeEnum.ТВ_тройник_всасывающий,new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, new int[]{} },
                { StationComponentsTypeEnum.ККР_катушка_концентрическая_резьбовая, new int []{} },
                { StationComponentsTypeEnum.ОКФ_обратный_клапан_фланцевый, new int[]{} },
                { StationComponentsTypeEnum.КР_катушка_резьбовая,new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, new int[]{} },
                { StationComponentsTypeEnum.ТН_тройник_напорный, new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, new int[]{} },
                { StationComponentsTypeEnum.Рама_, new int[]{} },
                { StationComponentsTypeEnum.ШУ_, new int[]{} }
            };
            return scheme;
        }

        public static StationScheme GetSimpleFireProtectionScheme2VerticalPumps(int pumpPressureConnection, int pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationComponents = new Dictionary<StationComponentsTypeEnum, int[]>
            {
                { StationComponentsTypeEnum.Насос_основной, new int[]{pumpPressureConnection,pumpSuctionConnection,0,0,0}},
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, new int[]{} },
                { StationComponentsTypeEnum.ТВ_тройник_всасывающий,new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, new int[]{} },
                { StationComponentsTypeEnum.ОКФ_обратный_клапан_фланцевый, new int[]{} },
                { StationComponentsTypeEnum.К_катушка, new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, new int[]{} },
                { StationComponentsTypeEnum.ТН_тройник_напорный, new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, new int[]{} },
                { StationComponentsTypeEnum.Рама_, new int[]{} },
                { StationComponentsTypeEnum.ШУ_, new int[]{} }
            };
            return scheme;
        }

        public static StationScheme GetSimpleFireProtectionSchemeMoreThan2VerticalPumps(int pumpPressureConnection, int pumpSuctionConnection)
        {
            StationScheme scheme = new StationScheme();
            scheme.stationComponents = new Dictionary<StationComponentsTypeEnum, int[]>
            {
                { StationComponentsTypeEnum.Насос_основной, new int[]{pumpPressureConnection,pumpSuctionConnection,0,0,0}},
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, new int[]{} },
                { StationComponentsTypeEnum.ТВ_тройник_всасывающий,new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, new int[]{} },
                { StationComponentsTypeEnum.ФланецСРеле_, new int[]{} },
                { StationComponentsTypeEnum.ОКФ_обратный_клапан_фланцевый, new int[]{} },
                { StationComponentsTypeEnum.К_катушка, new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, new int[]{} },
                { StationComponentsTypeEnum.ТН_тройник_напорный, new int[]{} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, new int[]{} },
                { StationComponentsTypeEnum.Рама_, new int[]{} },
                { StationComponentsTypeEnum.ШУ_, new int[]{} }
            };
            return scheme;
        }
        #endregion


        #region PressureIncreaseStationsSchemes

        #endregion
    }
}
