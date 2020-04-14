using IndustrialNetworks.ModbusCore.ASCII;
using IndustrialNetworks.ModbusCore.Comm;
using IndustrialNetworks.ModbusCore.DataTypes;
using System;
using System.Windows.Forms;

namespace IndustrialNetworks.DVPSeries
{
    public partial class FormReading3CountersFromSlaveDevice_02 : Form
    {
        private byte slaveAddress = 2;
        private uint startAddress = 3584;
        private ushort numberOfPoints = 3; // Reads 3 counters.

        private IModbusMaster objIModbusMaster = null;
        public FormReading3CountersFromSlaveDevice_02()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void FormReading3CountersFromSlaveDevice_02_Load(object sender, EventArgs e)
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
                byte[] bytes= objIModbusMaster.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                txtResult.Text = string.Empty; // clear text.
                if(bytes != null)
                {
                    short[] result = Int.ToArray(bytes);
                    foreach (short item in result)
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
