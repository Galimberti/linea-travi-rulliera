using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndustrialNetworks.DVPSeries
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FormReportSlaveID());
            Application.Run(new FormReadingContactY0ToY5FromSlaveDevice02());
            //Application.Run(new FormReadingCoilsT0ToT2FromSlaveDevice02());
            //Application.Run(new FormReadingD0ToD9FromSlaveDevice02());
            //Application.Run(new FormReading3CountersFromSlaveDevice_02());
            //Application.Run(new FormForcingCoilY000ON());
            //Application.Run(new FormWriteSingleRegisterToSlaveDevice02()); 
            //Application.Run(new FormForceMultipleCoilsY0ToY15FromSlaveDevice02());
            //Application.Run(new FormWriteMultipleRegistersD0ToD15ToSlaveDevice02());
        }
    }
}
