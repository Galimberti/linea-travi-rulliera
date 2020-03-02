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
    public partial class DUT_Asp_Comandi : UserControl
    {
        public DUT_Asp_Comandi()
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

                if (this._variableName == null || this._variableName == "")
                    return;

                this.btnApri.VariableName = ".HMI_PB_Man_Pulizia_Filtro_" + _variableName + "_AP";
                this.btnChiudi.VariableName = ".HMI_PB_Man_Pulizia_Filtro_" + _variableName + "_CH";
                this.abilita.VariableName = ".Pulizia_Filtro_" + _variableName + "_Man_Cmd_On";

                this.groupBox1.Text = "Pulizia Filtro: " + value;
            }
        }

        private void plcBooleanSwitch1_Load(object sender, EventArgs e)
        {

        }
    }
}
