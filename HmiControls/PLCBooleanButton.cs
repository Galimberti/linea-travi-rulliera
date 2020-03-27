using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLCDrivers;

namespace GalimbertiHMIgl
{
    public partial class PLCBooleanButton : PLCControl<Boolean>
    {
        public Action<IPlcDriver> onPress;
        public Action<IPlcDriver> onRelease;



        public PLCBooleanButton()
        {
            InitializeComponent();
            this.OnSomethingChanges += PLCBoolean_OnSomethingChanges;
        }

        private void PLCBoolean_OnSomethingChanges(object sender, EventArgs e)
        {
            this.button1.Text = this._description;
        }

        bool _greenMode = true;
        public Boolean GreenMode
        {
            get
            {
                return this._greenMode;
            }
            set
            {
                if (this._greenMode == value)
                    return;
                this._greenMode = value;
                this.PLCBoolean_OnSomethingChanges(this, null);
            }
        }

        bool _notMode = false;
        public Boolean NotMode
        {
            get
            {
                return this._notMode;
            }
            set
            {
                if (this._notMode == value)
                    return;

                this._notMode = value;

                this.PLCBoolean_OnSomethingChanges(this, null);
            }
        }


        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.onPress != null)
                this.onPress(this._plc.driver);

            this.fireUIChanges(true);
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.onRelease != null)
                this.onRelease(this._plc.driver);

            this.fireUIChanges(false);
        }
    }
}
