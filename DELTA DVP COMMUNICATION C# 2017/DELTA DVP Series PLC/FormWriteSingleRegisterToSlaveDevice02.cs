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
    public partial class FormWriteSingleRegisterToSlaveDevice02 : Form
    {
        private byte slaveAddress = 2;
        private uint startAddress = 4096;
        private byte[] values = null;

        private IModbusMaster objIModbusMaster = null;

        public FormWriteSingleRegisterToSlaveDevice02()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void FormWriteSingleRegisterToSlaveDevice02_Load(object sender, EventArgs e)
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

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                startAddress = (uint)txtAddress.Value;
                values = Int.ToByteArray((short)txtValue.Value);
                objIModbusMaster.WriteSingleRegister(slaveAddress, startAddress, values);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
