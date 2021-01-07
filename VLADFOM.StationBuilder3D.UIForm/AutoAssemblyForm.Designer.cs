namespace VLADFOM.StationBuilder3D.VSBuilderTestForm
{
    partial class VSBuilder3DForm
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
            this.CmdComponentComboBox = new System.Windows.Forms.ComboBox();
            this.ComponentsPicture = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ComponentsPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // CmdComponentComboBox
            // 
            this.CmdComponentComboBox.FormattingEnabled = true;
            this.CmdComponentComboBox.Items.AddRange(new object[] {
            "ЗД",
            "Кр_шаровый",
            "ОКФ",
            "ОКР",
            "Насос_жокей",
            "КЭ",
            "КЭР",
            "КК",
            "ККР",
            "К",
            "КР",
            "ПР_Р",
            "Американка",
            "Ниппель_НН",
            "Ниппель_ВН"});
            this.CmdComponentComboBox.Location = new System.Drawing.Point(618, 194);
            this.CmdComponentComboBox.Name = "CmdComponentComboBox";
            this.CmdComponentComboBox.Size = new System.Drawing.Size(100, 21);
            this.CmdComponentComboBox.TabIndex = 2;
            this.CmdComponentComboBox.Text = "Компонент";
            this.CmdComponentComboBox.SelectedIndexChanged += new System.EventHandler(this.CmdComponentComboBox_SelectedIndexChanged);
            // 
            // ComponentsPicture
            // 
            this.ComponentsPicture.Location = new System.Drawing.Point(618, 221);
            this.ComponentsPicture.Name = "ComponentsPicture";
            this.ComponentsPicture.Size = new System.Drawing.Size(100, 100);
            this.ComponentsPicture.TabIndex = 7;
            this.ComponentsPicture.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "ЗД",
            "Кр_шаровый",
            "ОКФ",
            "ОКР",
            "Насос_жокей",
            "КЭ",
            "КЭР",
            "КК",
            "ККР",
            "К",
            "КР",
            "ПР_Р",
            "Американка",
            "Ниппель_НН",
            "Ниппель_ВН"});
            this.comboBox1.Location = new System.Drawing.Point(618, 327);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(100, 21);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Text = "Действие";
            // 
            // VSBuilder3DForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1120, 561);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.CmdComponentComboBox);
            this.Controls.Add(this.ComponentsPicture);
            this.Name = "VSBuilder3DForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "VisualStationBulider3DForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ComponentsPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox CmdComponentComboBox;
        private System.Windows.Forms.PictureBox ComponentsPicture;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

