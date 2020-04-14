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
    public partial class FormReadingContactY0ToY5FromSlaveDevice02 : Form
    {
        private IModbusMaster objIModbusMaster = null;
        public FormReadingContactY0ToY5FromSlaveDevice02()
        {
            InitializeComponent();
        }

        private void FormReadingContactY0ToY5FromSlaveDevice02_Load(object sender, EventArgs e)
        {
            try
            {
                objIModbusMaster = new ModbusASCIIMaster("COM20", 9600, 7, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.Even);
                objIModbusMaster.Connection();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReadCoils_Click(object sender, EventArgs e)
        {
            ModbusScan.Start();            
        }

        private void ModbusScan_Tick(object sender, EventArgs e)
        {
            try
            {
                byte slaveAddress = 1;
                uint startAddress = 1280; // Start Address: Y0
                ushort numberOfPoints = 8; // Reads 6 coils.
                byte[] bytes = objIModbusMaster.ReadCoilStatus(slaveAddress, startAddress, numberOfPoints);
                if (bytes != null)
                {
                    bool[] result = Bit.ToArray(bytes);
                    txtResult.Text = string.Empty; // clear text.
                    for (int i = 0; i < numberOfPoints; i++)
                    {
                        txtResult.Text += string.Format("{0} # ", result[i]);
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
