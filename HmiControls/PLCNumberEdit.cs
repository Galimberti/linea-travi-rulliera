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
    public partial class PLCNumberEdit :  PLCControl<Double>
    {
        public PLCNumberEdit()
        {
            InitializeComponent();
            this.OnSomethingChanges += PLCNumber_OnSomethingChanges;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PLCNumber_OnSomethingChanges(object sender, EventArgs e)
        {
            this.label1.Text = this._description;

            if (!this.textBox1.Focused)
                this.textBox1.Text = this._value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double result;
            if (Double.TryParse(this.textBox1.Text, out result))
            {
                this.fireUIChanges(result);
            }
            
        }
    }
}
