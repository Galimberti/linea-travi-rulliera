using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GalimbertiHMIgl;

namespace HmiControls
{
    public partial class DUT_Motori : UserControl
    {
        public DUT_Motori()
        {
            InitializeComponent();
        }

        private void plcNumber2_Load(object sender, EventArgs e)
        {

        }



        private string _variableName = "";
        public string VariableName
        {
            get
            {
                return this._variableName;
            }
            set
            {
                this._variableName = value;
                this.plcBooleanButton1.VariableName = this._variableName + ".Pb_Start_Man";
                this.plcBooleanButton2.VariableName = this._variableName + ".Pb_Stop_Man";
                // this.plcLargh.VariableName = this.VariableName + ".Rotazione";
                this.plcBoolean1.VariableName = this._variableName + ".Run_Man_On";
                this.plcBoolean2.VariableName = this._variableName + ".Run_Auto_On";
                this.plcBoolean3.VariableName = this._variableName + ".Run_On";
            }
        }



        private string _label;
        public string Label
        {
            get
            {
                return this._label;
            }
            set
            {
                this._label = value;
                this.groupBox1.Text = _label;
            }
        }
    }
}
