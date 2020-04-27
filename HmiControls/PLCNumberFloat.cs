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
    public partial class PLCNumberFloat : PLCControl<float>

    {
        public PLCNumberFloat()
        {
            InitializeComponent();
            this.OnSomethingChanges += PLCNumber_OnSomethingChanges;
        }

        private void PLCNumber_OnSomethingChanges(object sender, EventArgs e)
        {
            this.label1.Text = this._description;
            this.textBox1.Text = this._value.ToString();
        }
    }
}
