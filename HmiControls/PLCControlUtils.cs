using GalimbertiHMIgl;
using PLCDrivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HmiControls
{
    public static class PLCControlUtils
    {
        public static IEnumerable<Control> getAllControls(Control control)
        {
            var controls = control.Controls.Cast<Control>();
            return controls.SelectMany(ctrl => getAllControls(ctrl)).Concat(controls);
        }

        public static void RegisterAll(PLC plc, Control control)
        {
            foreach (var c in getAllControls(control))
            {
                if (c is PLCControl<Boolean>)
                {
                    (c as PLCControl<Boolean>).register(plc);
                }
                if (c is PLCControl<Int16>)
                {
                    (c as PLCControl<Int16>).register(plc);
                }
                if (c is PLCControl<Double>)
                {
                    (c as PLCControl<Double>).register(plc);
                }
                if (c is PLCControl<float>)
                {
                    (c as PLCControl<float>).register(plc);
                }
            }
        }
    }
}
