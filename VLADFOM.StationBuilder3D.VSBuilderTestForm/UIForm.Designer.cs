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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxPressureValueForStation = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxCollectorsMaterial = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderUIForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPumpsCount)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxStationType
            // 
            this.comboBoxStationType.FormattingEnabled = true;
            this.comboBoxStationType.Items.AddRange(new object[] {
            "Пожаротушения",
            "Повышения давления",
            "Совмещённая",
            "Ф-Драйв",
            "Мультидрайв"});
            this.comboBoxStationType.Location = new System.Drawing.Point(149, 34);
            this.comboBoxStationType.Name = "comboBoxStationType";
            this.comboBoxStationType.Size = new System.Drawing.Size(140, 21);
            this.comboBoxStationType.TabIndex = 0;
            this.comboBoxStationType.Text = "Не установлено";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Тип установки";
            // 
            // textBoxPumpName
            // 
            this.textBoxPumpName.Location = new System.Drawing.Point(149, 74);
            this.textBoxPumpName.Name = "textBoxPumpName";
            this.textBoxPumpName.Size = new System.Drawing.Size(140, 20);
            this.textBoxPumpName.TabIndex = 2;
            this.textBoxPumpName.TextChanged += new System.EventHandler(this.textBoxPumpName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 77);
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
            this.label3.Location = new System.Drawing.Point(21, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Количество насосов";
            // 
            // textBoxWaterConsumption
            // 
            this.textBoxWaterConsumption.Location = new System.Drawing.Point(149, 175);
            this.textBoxWaterConsumption.Name = "textBoxWaterConsumption";
            this.textBoxWaterConsumption.Size = new System.Drawing.Size(140, 20);
            this.textBoxWaterConsumption.TabIndex = 6;
            this.textBoxWaterConsumption.Text = "0";
            this.textBoxWaterConsumption.TextChanged += new System.EventHandler(this.textBoxWaterConsumption_TextChanged);
            // 
            // numericUpDownPumpsCount
            // 
            this.numericUpDownPumpsCount.Location = new System.Drawing.Point(149, 142);
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
            this.label4.Location = new System.Drawing.Point(21, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Расход (Q, м3/ч)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Шкаф управления";
            // 
            // comboBoxControlCabinetSize
            // 
            this.comboBoxControlCabinetSize.FormattingEnabled = true;
            this.comboBoxControlCabinetSize.Items.AddRange(new object[] {
            "600x600x250",
            "800x800x300",
            "1000x1200x300"});
            this.comboBoxControlCabinetSize.Location = new System.Drawing.Point(149, 212);
            this.comboBoxControlCabinetSize.Name = "comboBoxControlCabinetSize";
            this.comboBoxControlCabinetSize.Size = new System.Drawing.Size(140, 21);
            this.comboBoxControlCabinetSize.TabIndex = 10;
            this.comboBoxControlCabinetSize.Text = "Не установлено";
            // 
            // comboBoxPumpsType
            // 
            this.comboBoxPumpsType.FormattingEnabled = true;
            this.comboBoxPumpsType.Items.AddRange(new object[] {
            "Горизонтальный",
            "Вертикальный"});
            this.comboBoxPumpsType.Location = new System.Drawing.Point(149, 106);
            this.comboBoxPumpsType.Name = "comboBoxPumpsType";
            this.comboBoxPumpsType.Size = new System.Drawing.Size(140, 21);
            this.comboBoxPumpsType.TabIndex = 11;
            this.comboBoxPumpsType.Text = "Не установлено";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Тип насоса";
            // 
            // cmdAssemblyStart
            // 
            this.cmdAssemblyStart.Location = new System.Drawing.Point(96, 327);
            this.cmdAssemblyStart.Name = "cmdAssemblyStart";
            this.cmdAssemblyStart.Size = new System.Drawing.Size(115, 23);
            this.cmdAssemblyStart.TabIndex = 13;
            this.cmdAssemblyStart.Text = "Начать сборку";
            this.cmdAssemblyStart.UseVisualStyleBackColor = true;
            this.cmdAssemblyStart.Click += new System.EventHandler(this.cmdAssemblyStart_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(321, 37);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(210, 256);
            this.richTextBox1.TabIndex = 14;
            this.richTextBox1.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 252);
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
            this.comboBoxPressureValueForStation.Location = new System.Drawing.Point(149, 249);
            this.comboBoxPressureValueForStation.Name = "comboBoxPressureValueForStation";
            this.comboBoxPressureValueForStation.Size = new System.Drawing.Size(140, 21);
            this.comboBoxPressureValueForStation.TabIndex = 16;
            this.comboBoxPressureValueForStation.Text = "Не установлено";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 288);
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
            this.comboBoxCollectorsMaterial.Location = new System.Drawing.Point(149, 285);
            this.comboBoxCollectorsMaterial.Name = "comboBoxCollectorsMaterial";
            this.comboBoxCollectorsMaterial.Size = new System.Drawing.Size(140, 21);
            this.comboBoxCollectorsMaterial.TabIndex = 18;
            this.comboBoxCollectorsMaterial.Text = "Не установлено";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 447);
            this.Controls.Add(this.comboBoxCollectorsMaterial);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBoxPressureValueForStation);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.richTextBox1);
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
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox comboBoxPressureValueForStation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxCollectorsMaterial;
        private System.Windows.Forms.Label label8;
    }
}