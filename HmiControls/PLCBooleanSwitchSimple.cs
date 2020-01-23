using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalimbertiHMIgl
{
    public partial class PLCBooleanSwitchSimple : PLCControl<Boolean>
    {
        public PLCBooleanSwitchSimple()
        {
            InitializeComponent();
            this.OnSomethingChanges += PLCBooleanSwitch_OnSomethingChanges;
        }

        private void PLCBooleanSwitch_OnSomethingChanges(object sender, EventArgs e)
        {
            this.plcBooleanOn.PLCValue = this._value;
            this.plcBooleanOn.PLCDescription = this._value ? this._descriptionOn : this._descriptionOff;
        }

        protected string _descriptionOn = "";
        protected string _descriptionOff = "";

        public String DescriptionOn
        {
            get
            {
                return this._descriptionOn;
            }
            set
            {
                this._descriptionOn = value;
                this.PLCBooleanSwitch_OnSomethingChanges(this, null);
            }
        }

        public String DescriptionOff
        {
            get
            {
                return this._descriptionOff;
            }
            set
            {
                this._descriptionOff = value;
                this.PLCBooleanSwitch_OnSomethingChanges(this, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.fireUIChanges(!this.PLCValue);
        }
    }
}
