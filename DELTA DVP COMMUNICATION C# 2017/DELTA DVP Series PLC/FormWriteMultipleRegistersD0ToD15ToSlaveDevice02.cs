using IndustrialNetworks.ModbusCore.ASCII;
using IndustrialNetworks.ModbusCore.Comm;
using IndustrialNetworks.ModbusCore.DataTypes;
using System;
using System.Windows.Forms;

namespace IndustrialNetworks.DVPSeries
{
    public partial class FormWriteMultipleRegistersD0ToD15ToSlaveDevice02 : Form
    {
        private byte slaveAddress = 2;
        private uint startAddress = 4096;
        private IModbusMaster objIModbusMaster = null;
        public FormWriteMultipleRegistersD0ToD15ToSlaveDevice02()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void FormWriteMultipleRegistersD0ToD15ToSlaveDevice02_Load(object sender, EventArgs e)
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

        private void btnWriteMultipleRegisters_Click(object sender, EventArgs e)
        {
            try
            {
                short[] shorts = new short[6];
                shorts[0] = (short)txt40001.Value;
                shorts[1] = (short)txt40002.Value;
                shorts[2] = (short)txt40003.Value;
                shorts[3] = (short)txt40004.Value;
                shorts[4] = (short)txt40005.Value;
                shorts[5] = (short)txt40006.Value;
                byte[] values = Int.ToByteArray(shorts);
                objIModbusMaster.WriteMultipleRegisters(slaveAddress, startAddress, values);

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
