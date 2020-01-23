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
    public partial class PLCBooleanArea : Panel
    {



        public PLCBooleanArea()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.FromArgb(55, 100, 100, 100);

        }

        int alpha = 255;
        public int Alpha
        {
            get
            {
                return alpha;
            }
            set
            {
                this.alpha = value;
                this.BackColor = Color.FromArgb(55, this.BackColor);
            }
        }

    }
}
