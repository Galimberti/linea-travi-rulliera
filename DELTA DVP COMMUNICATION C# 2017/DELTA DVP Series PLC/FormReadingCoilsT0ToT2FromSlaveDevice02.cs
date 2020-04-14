using IndustrialNetworks.ModbusCore.ASCII;
using IndustrialNetworks.ModbusCore.Comm;
using IndustrialNetworks.ModbusCore.DataTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndustrialNetworks.DVPSeries
{
    public partial class FormReadingCoilsT0ToT2FromSlaveDevice02 : Form
    {

        private byte slaveAddress = 2;
        private uint startAddress = 1536; // start address: T0.
        ushort numberOfPoints = 3; // Reads 3 Timers.

        private IModbusMaster objIModbusMaster = null;

        public FormReadingCoilsT0ToT2FromSlaveDevice02()
        {
            InitializeComponent();
        }

        private void FormReadingCoilsT0ToT2FromSlaveDevice02_Load(object sender, EventArgs e)
        {
            try
            {
                objIModbusMaster = new ModbusASCIIMaster("COM10", 9600, 7, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.Even);
                objIModbusMaster.Connection();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReadTimer_Click(object sender, EventArgs e)
        {
            try
            {
                ModbusScan.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModbusScan_Tick(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = objIModbusMaster.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                txtResult.Text = string.Empty; // Clear Text.
                if(bytes != null)
                {
                    ushort[] result = Word.ToArray(bytes);
                    foreach (ushort item in result)
                    {
                        txtResult.Text += string.Format("[{0}], ", item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
