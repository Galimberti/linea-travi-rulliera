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
    public partial class FormForceMultipleCoilsY0ToY15FromSlaveDevice02 : Form
    {
        private byte slaveAddress = 2;
        private uint startAddress = 1280;
        private IModbusMaster objIModbusMaster = null;
        public FormForceMultipleCoilsY0ToY15FromSlaveDevice02()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void FormForceMultipleCoilsY0ToY15FromSlaveDevice02_Load(object sender, EventArgs e)
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

        private void btnWriteMultipleCoils_Click(object sender, EventArgs e)
        {
            try
            {
                bool[] outputs = new bool[16] { true, false, false, false, true, true, false, false, false, true, true, false, false, true, false, true };
                byte[] values = Bit.ToByteArray(outputs);
                objIModbusMaster.WriteMultipleCoils(slaveAddress, startAddress, values);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
