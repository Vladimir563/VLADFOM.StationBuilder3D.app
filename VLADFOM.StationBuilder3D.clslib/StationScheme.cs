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
        /// 
        /// Dictionary<StationComponentsTypeEnum, int[]>
        /// ------------------------------------------------------------------
        /// first member is StationComponentsTypeEnum (components type)
        /// ------------------------------------------------------------------
        /// second member is int array[12]:
        /// array[0] is the smaller DN 
        /// array[1] is the bigger DN (or the main DN for component from current form instance)
        /// __________________________________________________________________
        /// array[2] is rotation by X1 axes
        /// array[3] is rotation by X2 axes
        /// array[4] is rotation by X3 axes
        /// __________________________________________________________________
        /// array[5] is rotation by Y1 axes
        /// array[6] is rotation by Y2 axes
        /// array[7] is rotation by Y3 axes
        /// __________________________________________________________________
        /// array[8] is rotation by Z1 axes
        /// array[9] is rotation by Z2 axes
        /// array[10] is rotation by Z3 axes
        /// __________________________________________________________________
        /// array[11] is the component for the new Line (0 equals false, 1 equals true (reverse planes), 2 start new line)
        /// 
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
                { StationComponentsTypeEnum.Насос_основной, new int[]{ pumpPressureConnection,pumpSuctionConnection, 1, 0, 0, 0, 1, 0, 0, 0, 1 ,0}},
                { StationComponentsTypeEnum.КЭ_катушка_эксцентрическая, new int[]{ 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, new int[]{ 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0} },
                { StationComponentsTypeEnum.ТВ_тройник_всасывающий,new int[]{ 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0} },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_всасывающего_коллектора, new int[]{ 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0 } },
                { StationComponentsTypeEnum.КК_катушка_концентрическая, new int []{ 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 2} },
                { StationComponentsTypeEnum.ОКФ_обратный_клапан_фланцевый, new int[]{ 0, 0, 1, 0, 0, 0, 0, -1, 0, 1, 0, 1} },
                { StationComponentsTypeEnum.КР_катушка_резьбовая,new int[]{ 0, 0, 0, 0, -1, 1, 0, 0, 0, -1, 0, 1 } },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_напорного_коллектора, new int[]{ 0, 0, 0, 0, -1, 1, 0, 0, 0, -1, 0 ,1} },
                { StationComponentsTypeEnum.ТН_тройник_напорный, new int[]{ 0, 0, 1, 0, 0, 0, 0, -1, 0, 1, 0, 1 } },
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_напорного_коллектора, new int[]{ 0, 0, 0, 0, -1, 0, -1, 0, -1, 0, 0, 1 } },
                { StationComponentsTypeEnum.Рама_, new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } },
                { StationComponentsTypeEnum.ШУ_, new int[]{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } }
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
