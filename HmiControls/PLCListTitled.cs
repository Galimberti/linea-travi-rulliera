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
    public partial class PLCListTitled : PLCLayout

    {

        public PLCListTitled()
        {
     
            InitializeComponent();
          
        }

        public String Title
        {
            get
            {
                return this.groupBox1.Text;
            }
            set
            {
                this.groupBox1.Text = value;
            }
        }

        protected override FlowLayoutPanel currentLayout()
        {
            return this.layout;
        }
    }
}
