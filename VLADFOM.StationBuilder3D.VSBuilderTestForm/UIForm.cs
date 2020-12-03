using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VLADFOM.StationBuilder3D.clslib;

namespace VLADFOM.StationBuilder3D.VSBuilderTestForm
{
    public partial class UIForm : Form
    {
        private string pumpStationType;
        private string pumpName;
        private string pumpsType;
        private int pumpsCount;
        private double waterConsumption;
        private string controlCabinetSize;

        public string PumpStationType
        {
            get { return comboBoxStationType.Text; }
            set { comboBoxStationType.Text = value; }
        }
        public string PumpName
        {
            get { return textBoxPumpName.Text; }
            set { textBoxPumpName.Text = value; }
        }
        public string PumpsType
        {
            get { return comboBoxPumpsType.Text; }
            set { comboBoxPumpsType.Text = value; }
        }
        public int PumpsCount
        {
            get { return (int)numericUpDownPumpsCount.Value; }
            set { numericUpDownPumpsCount.Value = value; }
        }
        public double WaterConsumption
        {
            get { return double.Parse(textBoxWaterConsumption.Text); }
            set { textBoxWaterConsumption.Text = value.ToString(); }
        }
        public string ControlCabinetSize
        {
            get { return comboBoxControlCabinetSize.Text; }
            set { comboBoxControlCabinetSize.Text = value; }
        }


        public UIForm()
        {
            InitializeComponent();
        }


        private void textBoxPumpName_TextChanged(object sender, EventArgs e)
        {
            {
                if (textBoxPumpName.Text.Trim(' ').Equals("BL_40_170-7.5_2"))
                {
                    textBoxPumpName.BackColor = Color.LightGreen;
                    errorProviderUIForm.Clear();
                }
                else if (textBoxPumpName.Text.Equals(""))
                {
                    textBoxPumpName.BackColor = Color.Empty;
                    errorProviderUIForm.Clear();
                }
                else
                {
                    textBoxPumpName.BackColor = Color.PaleVioletRed;
                    errorProviderUIForm.SetError(textBoxPumpName, $"Насос \"{textBoxPumpName.Text}\" не существует (проверьте правильность ввода)");
                }
            }
        }

        private void textBoxWaterConsumption_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double.Parse(textBoxWaterConsumption.Text, CultureInfo.GetCultureInfo("Ru-ru"));
                textBoxWaterConsumption.BackColor = Color.LightGreen;
                errorProviderUIForm.Clear();
            }
            catch
            {
                if (textBoxWaterConsumption.Text.Trim(' ').Equals(string.Empty))
                {
                    textBoxWaterConsumption.BackColor = Color.White;
                    errorProviderUIForm.Clear();
                }
                else
                {
                    textBoxWaterConsumption.BackColor = Color.PaleVioletRed;
                    errorProviderUIForm.SetError(textBoxWaterConsumption, "Значение поля \"Расход (Q)\" имело неверный формат (используйте \",\" вместо разделителя)");
                }
            }
        }

        private void cmdAssemblyStart_Click(object sender, EventArgs e)
        {
            StationScheme stationSchemeFireProtection = new StationScheme();
            stationSchemeFireProtection.stationComponents = new Dictionary<StationComponentsTypeEnum, int[]>
            {
                { StationComponentsTypeEnum.Насос_основной, new int[]{80,100}},
                { StationComponentsTypeEnum.КЭ_катушка_эксцентрическая, new int[]{0,0}},
                { StationComponentsTypeEnum.ЗД_затвор_дисковый_подводящей_линии_всасывающего_коллектора, new int[]{0,0} },
                { StationComponentsTypeEnum.ТВ_тройник_всасывающий,new int[]{0,0} },
                { StationComponentsTypeEnum.ККР_катушка_концентрическая_резьбовая, new int []{0,0} },
                { StationComponentsTypeEnum.ОКФ_обратный_клапан_фланцевый, new int[]{0,0} },
                { StationComponentsTypeEnum.КР_катушка_резьбовая,new int[]{0,0} },
                { StationComponentsTypeEnum.ТН_тройник_напорный, new int[]{0,0} },
                { StationComponentsTypeEnum.Рама_, new int[]{0,0} },
                { StationComponentsTypeEnum.ШУ_, new int[]{0,0} }
            };


            stationSchemeFireProtection.StationType = StationTypeEnum.Пожаротушения;
            PumpStation pumpStation = new PumpStation(this.PumpName, "", this.ControlCabinetSize, true, this.PumpsCount,
                this.WaterConsumption, 0, 0, 16,
                CollectorsMaterialEnum.Нержавеющая_сталь, stationSchemeFireProtection);
        }
    }
}
