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
    public partial class PLCBoolean : PLCControl<Boolean>
    {



        public PLCBoolean()
        {
            InitializeComponent();
            this.Paint += PLCBoolean_Paint;
            this.OnSomethingChanges += PLCBoolean_OnSomethingChanges;
          
        }

        private void PLCBoolean_OnSomethingChanges(object sender, EventArgs e)
        {
            this.label1.Text = this._description;
            this.Refresh();
        }

        private void PLCBoolean_Paint(object sender, PaintEventArgs e)
        {

            Color active = this.GreenMode ? Color.Green : Color.Red;
            Color main = this._value ^ this._notMode ? active : Color.Gray;
            Color border = ControlPaint.Light(main);

            this.label1.ForeColor = main;

            var control = sender as Control;
            using (Pen pen = new Pen(border, 3))
            {
                Rectangle rectangle = new Rectangle(3, 3, 15, 15);
                e.Graphics.DrawEllipse(pen, rectangle);
                e.Graphics.FillEllipse(new SolidBrush(main), rectangle);
                //e.Graphics.DrawString(count.ToString(), new Font(new FontFamily("Arial"), 8), new SolidBrush(Color.White),
                //     rectangle.X,
                //     rectangle.Y);
            }
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
    }
}
