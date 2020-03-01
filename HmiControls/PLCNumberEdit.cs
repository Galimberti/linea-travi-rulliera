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

        Boolean _touched = false;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void PLCNumber_OnSomethingChanges(object sender, EventArgs e)
        {
            this.label1.Text = this._description;

            if (!this.textBox1.Focused)
            {
                this._touched = false;
                this.textBox1.Text = this._value.ToString();
                this._touched = false;
                this.toChange = this.textBox1.Text;
                this.refreshTouched();
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double result;
            if (Double.TryParse(toChange, out result))
            {
                this.textBox1.Text = toChange;
                this.fireUIChanges(result);
            }
            this._touched = false;
            this.refreshTouched();
        }

        public void refreshTouched()
        {
            if (this._touched)
                this.textBox1.BackColor = Color.Yellow;
            else
                this.textBox1.BackColor = Color.FromArgb(255,255,255,255);

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            this._touched = true;
            refreshTouched();
        }

        String toChange = "";
        private void textBox1_Leave(object sender, EventArgs e)
        {
            
            this.toChange = this.textBox1.Text;
            this.textBox1.Text = this._value.ToString();
            this._touched = false;
            this.refreshTouched();
        }
    }
}
