using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VLADFOM.StationBuilder3D.clsbllib;
using VLADFOM.StationBuilder3D.clslib;

namespace BBtest3D
{
    class StationBuilder3DBusinessLogic
    {
        static void Main(string[] args)
        {
            StationScheme stationSchemeFireProtection = new StationScheme();

            #region ForNotAutomaticAssembly
            //берем данные из формы (если включен режим "полуавтоматической сборки") и заполняем каждый массив 
            //где ([0 - меньший DN, 0 - главный  (больший)DN])
            //stationSchemeFireProtection.stationComponents = new Dictionary<StationComponentsTypeEnum, int[]>
            //{
            //    { StationComponentsTypeEnum.Насос_основной, new int[]{65,80}},
            //    { StationComponentsTypeEnum.КЭ_катушка_эксцентрическая, new int[]{80,100}},
            //    { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, new int[]{0,100} },
            //    { StationComponentsTypeEnum.ТВ_тройник_всасывающий,new int[]{100,125} },
            //    { StationComponentsTypeEnum.ККР_катушка_концентрическая_резьбовая, new int []{65,100} },
            //    { StationComponentsTypeEnum.ОКФ_обратный_клапан_фланцевый, new int[]{0,100} },
            //    { StationComponentsTypeEnum.КР_катушка_резьбовая,new int[]{0,100} },
            //    { StationComponentsTypeEnum.ТН_тройник_напорный, new int[]{100,125} },
            //    { StationComponentsTypeEnum.Рама_, new int[]{0,0} },
            //    { StationComponentsTypeEnum.ШУ_, new int[]{0,0} }
            //};
            #endregion

            BusinessLogic businessLogic = new BusinessLogic();
            businessLogic.StartAssembly("Пожаротушения", "BL_40_170-7.5_2", " ", "Горизонтальный", 2,
                32, "800х800х300", 0, 0, 16, "Нержавеющая_сталь");
        }
    }
}
