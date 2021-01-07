namespace VLADFOM.StationBuilder3D.VSBuilderTestForm
{
    partial class UIForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comboBoxStationType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPumpName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProviderUIForm = new System.Windows.Forms.ErrorProvider(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxWaterConsumption = new System.Windows.Forms.TextBox();
            this.numericUpDownPumpsCount = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxControlCabinetSize = new System.Windows.Forms.ComboBox();
            this.comboBoxPumpsType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmdAssemblyStart = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxPressureValueForStation = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxCollectorsMaterial = new System.Windows.Forms.ComboBox();
            this.checkBoxChooseTheDiameterConnections = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxSuctionDNConnections = new System.Windows.Forms.ComboBox();
            this.comboBoxPressureDNConnections = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxJockeyPumpName = new System.Windows.Forms.TextBox();
            this.checkBoxIsContolCabStandAlone = new System.Windows.Forms.CheckBox();
            this.comboBoxControlCabStAlonePosition = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderUIForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPumpsCount)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxStationType
            // 
            this.comboBoxStationType.FormattingEnabled = true;
            this.comboBoxStationType.Items.AddRange(new object[] {
            "Пожаротушения",
            "Повышения_давления",
            "Совмещённая",
            "Ф-Драйв",
            "Мультидрайв"});
            this.comboBoxStationType.Location = new System.Drawing.Point(140, 6);
            this.comboBoxStationType.Name = "comboBoxStationType";
            this.comboBoxStationType.Size = new System.Drawing.Size(140, 21);
            this.comboBoxStationType.TabIndex = 0;
            this.comboBoxStationType.Text = "Не установлено";
            this.comboBoxStationType.SelectedIndexChanged += new System.EventHandler(this.comboBoxStationType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тип установки";
            // 
            // textBoxPumpName
            // 
            this.textBoxPumpName.Location = new System.Drawing.Point(140, 38);
            this.textBoxPumpName.Name = "textBoxPumpName";
            this.textBoxPumpName.Size = new System.Drawing.Size(140, 20);
            this.textBoxPumpName.TabIndex = 2;
            this.textBoxPumpName.TextChanged += new System.EventHandler(this.textBoxPumpName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Наименование насоса";
            // 
            // errorProviderUIForm
            // 
            this.errorProviderUIForm.ContainerControl = this;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Количество насосов";
            // 
            // textBoxWaterConsumption
            // 
            this.textBoxWaterConsumption.Location = new System.Drawing.Point(140, 265);
            this.textBoxWaterConsumption.Name = "textBoxWaterConsumption";
            this.textBoxWaterConsumption.Size = new System.Drawing.Size(140, 20);
            this.textBoxWaterConsumption.TabIndex = 6;
            this.textBoxWaterConsumption.Text = "0";
            this.textBoxWaterConsumption.TextChanged += new System.EventHandler(this.textBoxWaterConsumption_TextChanged);
            // 
            // numericUpDownPumpsCount
            // 
            this.numericUpDownPumpsCount.Location = new System.Drawing.Point(140, 141);
            this.numericUpDownPumpsCount.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numericUpDownPumpsCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPumpsCount.Name = "numericUpDownPumpsCount";
            this.numericUpDownPumpsCount.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownPumpsCount.TabIndex = 7;
            this.numericUpDownPumpsCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 272);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Расход (Q, м3/ч)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 305);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Шкаф управления";
            // 
            // comboBoxControlCabinetSize
            // 
            this.comboBoxControlCabinetSize.FormattingEnabled = true;
            this.comboBoxControlCabinetSize.Items.AddRange(new object[] {
            "Без ШУ",
            "600x600x250",
            "800x800x300",
            "1000x1200x300"});
            this.comboBoxControlCabinetSize.Location = new System.Drawing.Point(140, 302);
            this.comboBoxControlCabinetSize.Name = "comboBoxControlCabinetSize";
            this.comboBoxControlCabinetSize.Size = new System.Drawing.Size(140, 21);
            this.comboBoxControlCabinetSize.TabIndex = 10;
            this.comboBoxControlCabinetSize.Text = "Не установлено";
            this.comboBoxControlCabinetSize.SelectedIndexChanged += new System.EventHandler(this.comboBoxControlCabinetSize_SelectedIndexChanged);
            // 
            // comboBoxPumpsType
            // 
            this.comboBoxPumpsType.FormattingEnabled = true;
            this.comboBoxPumpsType.Items.AddRange(new object[] {
            "Горизонтальный",
            "Вертикальный"});
            this.comboBoxPumpsType.Location = new System.Drawing.Point(140, 105);
            this.comboBoxPumpsType.Name = "comboBoxPumpsType";
            this.comboBoxPumpsType.Size = new System.Drawing.Size(140, 21);
            this.comboBoxPumpsType.TabIndex = 11;
            this.comboBoxPumpsType.Text = "Не установлено";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Тип насоса";
            // 
            // cmdAssemblyStart
            // 
            this.cmdAssemblyStart.Location = new System.Drawing.Point(86, 500);
            this.cmdAssemblyStart.Name = "cmdAssemblyStart";
            this.cmdAssemblyStart.Size = new System.Drawing.Size(115, 23);
            this.cmdAssemblyStart.TabIndex = 13;
            this.cmdAssemblyStart.Text = "Начать сборку";
            this.cmdAssemblyStart.UseVisualStyleBackColor = true;
            this.cmdAssemblyStart.Click += new System.EventHandler(this.cmdAssemblyStart_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 415);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Требуемое давление";
            // 
            // comboBoxPressureValueForStation
            // 
            this.comboBoxPressureValueForStation.FormattingEnabled = true;
            this.comboBoxPressureValueForStation.Items.AddRange(new object[] {
            "10",
            "16",
            "25",
            "40"});
            this.comboBoxPressureValueForStation.Location = new System.Drawing.Point(140, 412);
            this.comboBoxPressureValueForStation.Name = "comboBoxPressureValueForStation";
            this.comboBoxPressureValueForStation.Size = new System.Drawing.Size(140, 21);
            this.comboBoxPressureValueForStation.TabIndex = 16;
            this.comboBoxPressureValueForStation.Text = "Не установлено";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 451);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Материал обвязки";
            // 
            // comboBoxCollectorsMaterial
            // 
            this.comboBoxCollectorsMaterial.FormattingEnabled = true;
            this.comboBoxCollectorsMaterial.Items.AddRange(new object[] {
            "Нержавеющая_сталь",
            "Чёрная_сталь"});
            this.comboBoxCollectorsMaterial.Location = new System.Drawing.Point(140, 448);
            this.comboBoxCollectorsMaterial.Name = "comboBoxCollectorsMaterial";
            this.comboBoxCollectorsMaterial.Size = new System.Drawing.Size(140, 21);
            this.comboBoxCollectorsMaterial.TabIndex = 18;
            this.comboBoxCollectorsMaterial.Text = "Не установлено";
            // 
            // checkBoxChooseTheDiameterConnections
            // 
            this.checkBoxChooseTheDiameterConnections.AutoSize = true;
            this.checkBoxChooseTheDiameterConnections.Location = new System.Drawing.Point(15, 179);
            this.checkBoxChooseTheDiameterConnections.Name = "checkBoxChooseTheDiameterConnections";
            this.checkBoxChooseTheDiameterConnections.Size = new System.Drawing.Size(248, 17);
            this.checkBoxChooseTheDiameterConnections.TabIndex = 20;
            this.checkBoxChooseTheDiameterConnections.Text = "Указать диаметры подключений установки";
            this.checkBoxChooseTheDiameterConnections.UseVisualStyleBackColor = true;
            this.checkBoxChooseTheDiameterConnections.CheckedChanged += new System.EventHandler(this.checkBoxChooseTheDiameterConnections_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 209);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(166, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "DN всасывающего коллектора";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 236);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(141, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "DN напорного коллектора";
            // 
            // comboBoxSuctionDNConnections
            // 
            this.comboBoxSuctionDNConnections.Enabled = false;
            this.comboBoxSuctionDNConnections.FormattingEnabled = true;
            this.comboBoxSuctionDNConnections.Items.AddRange(new object[] {
            "1\"",
            "1 1/4\"",
            "1 1/2\"",
            "2\"",
            "50",
            "65",
            "80",
            "100",
            "125",
            "150",
            "200",
            "250",
            "300",
            "350",
            "400",
            "450",
            "500",
            "600",
            "700",
            "800",
            "900",
            "1000"});
            this.comboBoxSuctionDNConnections.Location = new System.Drawing.Point(184, 206);
            this.comboBoxSuctionDNConnections.Name = "comboBoxSuctionDNConnections";
            this.comboBoxSuctionDNConnections.Size = new System.Drawing.Size(79, 21);
            this.comboBoxSuctionDNConnections.TabIndex = 23;
            this.comboBoxSuctionDNConnections.Text = "0";
            // 
            // comboBoxPressureDNConnections
            // 
            this.comboBoxPressureDNConnections.Enabled = false;
            this.comboBoxPressureDNConnections.FormattingEnabled = true;
            this.comboBoxPressureDNConnections.Items.AddRange(new object[] {
            "1\"",
            "1 1/4\"",
            "1 1/2\"",
            "2\"",
            "50",
            "65",
            "80",
            "100",
            "125",
            "150",
            "200",
            "250",
            "300",
            "350",
            "400",
            "450",
            "500",
            "600",
            "700",
            "800",
            "900",
            "1000"});
            this.comboBoxPressureDNConnections.Location = new System.Drawing.Point(184, 233);
            this.comboBoxPressureDNConnections.Name = "comboBoxPressureDNConnections";
            this.comboBoxPressureDNConnections.Size = new System.Drawing.Size(79, 21);
            this.comboBoxPressureDNConnections.TabIndex = 24;
            this.comboBoxPressureDNConnections.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(81, 13);
            this.label11.TabIndex = 25;
            this.label11.Text = "Жокей - насос";
            // 
            // textBoxJockeyPumpName
            // 
            this.textBoxJockeyPumpName.Enabled = false;
            this.textBoxJockeyPumpName.Location = new System.Drawing.Point(140, 71);
            this.textBoxJockeyPumpName.Name = "textBoxJockeyPumpName";
            this.textBoxJockeyPumpName.Size = new System.Drawing.Size(140, 20);
            this.textBoxJockeyPumpName.TabIndex = 26;
            // 
            // checkBoxIsContolCabStandAlone
            // 
            this.checkBoxIsContolCabStandAlone.AutoSize = true;
            this.checkBoxIsContolCabStandAlone.Enabled = false;
            this.checkBoxIsContolCabStandAlone.Location = new System.Drawing.Point(15, 343);
            this.checkBoxIsContolCabStandAlone.Name = "checkBoxIsContolCabStandAlone";
            this.checkBoxIsContolCabStandAlone.Size = new System.Drawing.Size(137, 17);
            this.checkBoxIsContolCabStandAlone.TabIndex = 29;
            this.checkBoxIsContolCabStandAlone.Text = "ШУ отдельностоящий";
            this.checkBoxIsContolCabStandAlone.UseVisualStyleBackColor = true;
            this.checkBoxIsContolCabStandAlone.CheckedChanged += new System.EventHandler(this.checkBoxIsContolCabStandAlone_CheckedChanged);
            // 
            // comboBoxControlCabStAlonePosition
            // 
            this.comboBoxControlCabStAlonePosition.Enabled = false;
            this.comboBoxControlCabStAlonePosition.FormattingEnabled = true;
            this.comboBoxControlCabStAlonePosition.Items.AddRange(new object[] {
            "Слева от рамы",
            "Справа от рамы",
            "Сзади рамы",
            "Спереди рамы"});
            this.comboBoxControlCabStAlonePosition.Location = new System.Drawing.Point(140, 375);
            this.comboBoxControlCabStAlonePosition.Name = "comboBoxControlCabStAlonePosition";
            this.comboBoxControlCabStAlonePosition.Size = new System.Drawing.Size(140, 21);
            this.comboBoxControlCabStAlonePosition.TabIndex = 31;
            this.comboBoxControlCabStAlonePosition.Text = "Не установлено";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 378);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "Положение ШУ";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 554);
            this.Controls.Add(this.comboBoxControlCabStAlonePosition);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.checkBoxIsContolCabStandAlone);
            this.Controls.Add(this.textBoxJockeyPumpName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.comboBoxPressureDNConnections);
            this.Controls.Add(this.comboBoxSuctionDNConnections);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.checkBoxChooseTheDiameterConnections);
            this.Controls.Add(this.comboBoxCollectorsMaterial);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBoxPressureValueForStation);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmdAssemblyStart);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxPumpsType);
            this.Controls.Add(this.comboBoxControlCabinetSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownPumpsCount);
            this.Controls.Add(this.textBoxWaterConsumption);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPumpName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxStationType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderUIForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPumpsCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxStationType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPumpName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider errorProviderUIForm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxControlCabinetSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownPumpsCount;
        private System.Windows.Forms.TextBox textBoxWaterConsumption;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxPumpsType;
        private System.Windows.Forms.Button cmdAssemblyStart;
        private System.Windows.Forms.ComboBox comboBoxPressureValueForStation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxCollectorsMaterial;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxChooseTheDiameterConnections;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxPressureDNConnections;
        private System.Windows.Forms.ComboBox comboBoxSuctionDNConnections;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxJockeyPumpName;
        private System.Windows.Forms.CheckBox checkBoxIsContolCabStandAlone;
        private System.Windows.Forms.ComboBox comboBoxControlCabStAlonePosition;
        private System.Windows.Forms.Label label12;
    }
}