using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HmiControls
{
    public partial class Zona1Layout : UserControl
    {
        public Zona1Layout()
        {
            InitializeComponent();
            this.Paint += PLCBoolean_Paint;
        }

        private void plcNumber2_Load(object sender, EventArgs e)
        {

        }


        private string _variableName;
        public string VariableName
        {
            get
            {
                return this._variableName;
            }
            set
            {
                this._variableName = value;
                             
            }
        }

        private void PLCBoolean_Paint(object sender, PaintEventArgs e)
        {

            
        }


        private void TrackingItem_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
