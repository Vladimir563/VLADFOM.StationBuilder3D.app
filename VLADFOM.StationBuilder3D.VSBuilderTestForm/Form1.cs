using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VLADFOM.StationBuilder3D.VSBuilderTestForm
{
    public partial class VSBuilder3DForm : Form
    {
        public VSBuilder3DForm()
        {
            InitializeComponent();
            PumpStationFormComponent stationFormComponent = new PumpStationFormComponent(null,null,"",0,0,this.Controls);
            ComponentsPicture.Location = new Point(this.Width + ComponentsPicture.Width / 2, this.Height + ComponentsPicture.Height / 2);

        }

        private void CmdComponentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oldComboBox = (ComboBox)sender;
            ComponentResourceManager resources = new ComponentResourceManager(typeof(VSBuilder3DForm));

            ////Getting reference of the current button instance
            //Button oldButton = (Button)sender;

            //Creation new buttons


            //Creation a picture box
            //PictureBox newPictureBox = new PictureBox();
            //newPictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            //newPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pumpImage.Image")));
            //newPictureBox.Location = new System.Drawing.Point(365, 220);
            //newPictureBox.Name = "pumpImage";
            //newPictureBox.Size = new System.Drawing.Size(60, 48);
            //newPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            //newPictureBox.TabStop = false;

            //Changing the new butons text
            //buttonAddPumpLeft.Text = "+";
            //buttonAddPumpLeft.Width = oldButton.Width;
            //buttonAddPumpLeft.Width = oldButton.Height;

            //buttonRemovePumpLeft.Text = "-";
            //buttonRemovePumpLeft.Width = oldButton.Width;
            //buttonRemovePumpLeft.Width = oldButton.Height;

            //buttonAddPumpRight.Text = "+";
            //buttonRemovePumpRight.Text = "-";
            //rotateImageOnTheRight.Text = "⭯";
            //rotateImageOnTheLeft.Text = "⭮";

            //newbutton.Width = oldbutton.Width;
            //newbutton.Height = oldbutton.Height;

            //Point newLocationForButton = new Point(2,3);
            //Point newLocationForImage = new Point(1,1);

            //newbutton.Location = new Point(oldbutton.Location.X + oldbutton.Height - 120, oldbutton.Location.Y);
            //pictureBox.Location = new Point(oldbutton.Location.X, oldbutton.Location.Y + oldbutton.Height - 170);


            ////Добавляем событие нажатия на новую кнопку
            ////(то же что и при нажатии на исходную)
            //newbutton.Click += new EventHandler(cmdAddPump_Click);
            ////Добавляем элемент на форму
            //if (newbutton.Location.Y > 50 ) 
            //{
            //    this.Controls.Add(pictureBox);
            //    this.Controls.Add(newbutton);
            //}

        }
    }
}
