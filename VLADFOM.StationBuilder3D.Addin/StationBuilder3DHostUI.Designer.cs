namespace VLADFOM.StationBuilder3D.Addin
{
    partial class StationBuilder3DHostUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmdUIFormStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdUIFormStart
            // 
            this.cmdUIFormStart.Location = new System.Drawing.Point(56, 71);
            this.cmdUIFormStart.Name = "cmdUIFormStart";
            this.cmdUIFormStart.Size = new System.Drawing.Size(75, 23);
            this.cmdUIFormStart.TabIndex = 0;
            this.cmdUIFormStart.Text = "Начать";
            this.cmdUIFormStart.UseVisualStyleBackColor = true;
            this.cmdUIFormStart.Click += new System.EventHandler(this.cmdUIFormStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Автоматическая сборка";
            // 
            // StationBuilder3DHostUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdUIFormStart);
            this.MaximumSize = new System.Drawing.Size(500, 2000);
            this.Name = "StationBuilder3DHostUI";
            this.Size = new System.Drawing.Size(200, 1000);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdUIFormStart;
        private System.Windows.Forms.Label label1;
    }
}
