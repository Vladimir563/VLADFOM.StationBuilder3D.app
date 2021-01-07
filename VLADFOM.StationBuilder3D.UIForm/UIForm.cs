using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using VLADFOM.StationBuilder3D.clsbllib;

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
        private int presureValueForStation;
        private string collectorsMaterial;
        private int suctionCollectorDN;
        private int pressureCollectorDN;
        private string jockeyPumpName;
        private bool isControlCabinetStandAlone;
        private string controlCabStAlonePosition;

        public string ControlCabStAlonePosition
        {
            get { return comboBoxControlCabStAlonePosition.Text; }
            set { comboBoxControlCabStAlonePosition.Text = value; }
        }
        public bool IsControlCabinetStandAlone
        {
            get { return checkBoxIsContolCabStandAlone.Checked; }
            set { checkBoxIsContolCabStandAlone.Checked = value; }
        }
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
        public int PresureValueForStation
        {
            get { return int.Parse(comboBoxPressureValueForStation.Text); }
            set { comboBoxPressureValueForStation.Text = value.ToString(); }
        }
        public string CollectorsMaterial
        {
            get { return comboBoxCollectorsMaterial.Text; }
            set { comboBoxCollectorsMaterial.Text = value; }
        }
        public int PressureCollectorDN
        {
            get { return int.Parse(comboBoxPressureDNConnections.Text); }
            set { comboBoxPressureDNConnections.Text = value.ToString(); }
        }
        public int SuctionCollectorDN
        {
            get { return int.Parse(comboBoxSuctionDNConnections.Text); }
            set { comboBoxSuctionDNConnections.Text = value.ToString(); }
        }
        public string JockeyPumpName
        {
            get { return textBoxJockeyPumpName.Text; }
            set { textBoxJockeyPumpName.Text = value; }
        }


        public UIForm()
        {
            InitializeComponent();
        }

        private void textBoxPumpName_TextChanged(object sender, EventArgs e)
        {
            {
                if (textBoxPumpName.Text.Trim(' ').Equals("BL_40_170-7.5_2") ||
                    textBoxPumpName.Text.Trim(' ').Equals("CR_15-4") ||
                    textBoxPumpName.Text.Trim(' ').Equals("MHI_802-1") ||
                    textBoxPumpName.Text.Trim(' ').Equals("Helix_1009"))
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
            this.Hide();
            BusinessLogic businessLogic = new BusinessLogic();

            businessLogic.StartAssembly(PumpStationType, PumpName, JockeyPumpName, PumpsType, PumpsCount, WaterConsumption,
                ControlCabinetSize, IsControlCabinetStandAlone, ControlCabStAlonePosition, suctionCollectorDN, pressureCollectorDN, PresureValueForStation, CollectorsMaterial);
            this.Close();
        }

        private void checkBoxChooseTheDiameterConnections_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxChooseTheDiameterConnections.Checked)
            {
                textBoxWaterConsumption.Enabled = false;
                textBoxWaterConsumption.Text = "0";
                textBoxWaterConsumption.BackColor = Color.White;
                comboBoxSuctionDNConnections.Enabled = true;
                comboBoxPressureDNConnections.Enabled = true;
            }
            else
            {
                textBoxWaterConsumption.Enabled = true;
                comboBoxSuctionDNConnections.Enabled = false;
                comboBoxPressureDNConnections.Enabled = false;
                comboBoxSuctionDNConnections.Text = "0";
                comboBoxPressureDNConnections.Text = "0";
            }
        }

        private void comboBoxStationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStationType.Text.Equals("Пожаротушения"))
            {
                textBoxJockeyPumpName.Enabled = true;
            }
            else 
            {
                textBoxJockeyPumpName.Enabled = false;
            }
        }

        private void checkBoxIsContolCabStandAlone_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIsContolCabStandAlone.Checked)
            {
                IsControlCabinetStandAlone = true;
                comboBoxControlCabStAlonePosition.Enabled = true;
            }
            else 
            {
                IsControlCabinetStandAlone = false;
                comboBoxControlCabStAlonePosition.Text = "Не установлено";
                comboBoxControlCabStAlonePosition.Enabled = false;
            }
            
        }

        private void comboBoxControlCabinetSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxControlCabinetSize.Text.Equals("Без ШУ")) 
            {
                checkBoxIsContolCabStandAlone.Checked = false;
                checkBoxIsContolCabStandAlone.Enabled = false;
            }
            else checkBoxIsContolCabStandAlone.Enabled = true;
        }
    }
}
