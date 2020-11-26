using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using StationBuilder3DAddin;

namespace VLADFOM.StationBuilder3D.Addin
{
    [ProgId(StationBuilder3DAddIn.SWTASKPANE_PROGID)]
    public partial class StationBuilder3DHostUI : UserControl
    {
        public StationBuilder3DHostUI()
        {
            InitializeComponent();
        }
    }
}
