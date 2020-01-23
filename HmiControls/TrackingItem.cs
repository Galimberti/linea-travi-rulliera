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
    public partial class TrackingItem : UserControl
    {
        public TrackingItem()
        {
            InitializeComponent();
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
                this.plcBusy.VariableName = this._variableName + ".Busy";
                // this.plcLargh.VariableName = this.VariableName + ".Rotazione";
                this.plcLargh.VariableName = this._variableName + ".Larghezza";
                this.plcAlt.VariableName = this._variableName + ".Altezza";
               
            }
        }

        private void TrackingItem_Load(object sender, EventArgs e)
        {

        }
    }
}
