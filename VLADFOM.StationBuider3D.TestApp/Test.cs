using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLADFOM.StationBuilder3D.clslib;
namespace BBtest3D
{
    class Test
    {
        static void Main(string[] args)
        {
            StationScheme stationSchemeFireProtection = new StationScheme();

            //берем данные из формы (если включен режим "полуавтоматической сборки") и заполняем каждый массив 
            //где ([0 - меньший DN, 0 - главный  (больший)DN])
            stationSchemeFireProtection.stationComponents = new Dictionary<StationComponentsTypeEnum, int[]>
            {
                { StationComponentsTypeEnum.Насос_основной, new int[]{65,80}},
                { StationComponentsTypeEnum.КЭ_катушка_эксцентрическая, new int[]{80,100}},
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, new int[]{0,100} },
                { StationComponentsTypeEnum.ТВ_тройник_всасывающий,new int[]{100,125} },
                { StationComponentsTypeEnum.ККР_катушка_концентрическая_резьбовая, new int []{65,100} },
                { StationComponentsTypeEnum.ОКФ_обратный_клапан_фланцевый, new int[]{0,100} },
                { StationComponentsTypeEnum.КР_катушка_резьбовая,new int[]{0,100} },
                { StationComponentsTypeEnum.ТН_тройник_напорный, new int[]{100,125} },
                { StationComponentsTypeEnum.Рама_, new int[]{0,0} },
                { StationComponentsTypeEnum.ШУ_, new int[]{0,0} }
            };

            //автоматическая сборка
            //stationSchemeFireProtection.stationComponents = new Dictionary<StationComponentsTypeEnum, int[]>
            //{
            //    { StationComponentsTypeEnum.Насос_основной, new int[]{80,100}},
            //    { StationComponentsTypeEnum.КЭ_катушка_эксцентрическая, new int[]{0,0}},
            //    { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, new int[]{0,0} },
            //    { StationComponentsTypeEnum.ТВ_тройник_всасывающий,new int[]{0,0} },
            //    { StationComponentsTypeEnum.ККР_катушка_концентрическая_резьбовая, new int []{0,0} },
            //    { StationComponentsTypeEnum.ОКФ_обратный_клапан_фланцевый, new int[]{0,0} },
            //    { StationComponentsTypeEnum.КР_катушка_резьбовая,new int[]{0,0} },
            //    { StationComponentsTypeEnum.ТН_тройник_напорный, new int[]{0,0} },
            //    { StationComponentsTypeEnum.Рама_, new int[]{0,0} },
            //    { StationComponentsTypeEnum.ШУ_, new int[]{0,0} }
            //};



            stationSchemeFireProtection.StationType = StationTypeEnum.Пожаротушения;
            PumpStation pumpStation = new PumpStation("BL65-125", "", "600x800x250", false, 3, 104, 0, 0, 16, CollectorsMaterialEnum.Нержавеющая_сталь, stationSchemeFireProtection);

            foreach (var component in pumpStation.stationComponents)
            {
                Console.WriteLine(component.ComponentsName);
            }

            Console.ReadKey();
        }
    }
}
