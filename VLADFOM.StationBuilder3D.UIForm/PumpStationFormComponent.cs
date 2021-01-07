using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace VLADFOM.StationBuilder3D.VSBuilderTestForm
{
    class PumpStationFormComponent
    {
        private PictureBox oldPictureBox;
        private ComboBox oldComboBox;
        private PictureBox pictureBox;
        private ComboBox comboBox;
        private Button cmdAddLeft;
        private Button cmdAddRight;
        private Button cmdAddTop;
        private Button cmdAddBottom;
        private string imagePath;
        private int offsetByX;
        private int offsetByY;
        private ControlCollection control;

        public PictureBox OldPictureBox
        {
            get { return oldPictureBox; }
            set { oldPictureBox = value; }
        }
        public ComboBox OldComboBox
        {
            get { return oldComboBox; }
            set { oldComboBox = value; }
        }
        public PictureBox PictureBox
        {
            get { return pictureBox; }
            set { pictureBox = value; }
        }
        public ComboBox ComboBox
        {
            get { return comboBox; }
            set { comboBox = value; }
        }
        public Button CmdAddLeft
        {
            get { return cmdAddLeft; }
            set { cmdAddLeft = value; }
        }
        public Button CmdAddRight
        {
            get { return cmdAddRight; }
            set { cmdAddRight = value; }
        }
        public Button CmdAddTop
        {
            get { return cmdAddTop; }
            set { cmdAddTop = value; }
        }
        public Button CmdAddBottom
        {
            get { return cmdAddBottom; }
            set { cmdAddBottom = value; }
        }
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }
        public int OffsetByX
        {
            get { return offsetByX; }
            set { offsetByX = value; }
        }
        public int OffsetByY
        {
            get { return offsetByY; }
            set { offsetByY = value; }
        }
        public ControlCollection Control
        {
            get { return control; }
            set { control = value; }
        }

        public PumpStationFormComponent(PictureBox oldPictureBox, ComboBox oldComboBox, string imagePath, int offsetByX, 
            int offsetByY, ControlCollection control)
        {
            OldPictureBox = oldPictureBox;
            OldComboBox = oldComboBox;
            ImagePath = imagePath;
            OffsetByX = offsetByX;
            OffsetByY = offsetByY;
            Control = control;
        }

    }
}
