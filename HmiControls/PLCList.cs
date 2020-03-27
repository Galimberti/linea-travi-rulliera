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
    public partial class PLCList : PLCLayout

    {
        public PLCList()
        {
     
            InitializeComponent();
          
        }

        protected override FlowLayoutPanel currentLayout()
        {
            return this.layout;
        }
    }
}
