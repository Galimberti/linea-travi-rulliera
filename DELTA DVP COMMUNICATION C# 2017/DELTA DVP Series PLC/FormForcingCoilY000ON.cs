using IndustrialNetworks.ModbusCore.ASCII;
using IndustrialNetworks.ModbusCore.Comm;
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
    public partial class FormForcingCoilY000ON : Form
    {
        private byte slaveAddress = 2;
        private uint startAddress = 1280;
        private bool value = false;
        private IModbusMaster objIModbusMaster = null;

        public FormForcingCoilY000ON()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void FormForcingCoilY000ON_Load(object sender, EventArgs e)
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

        private void btnON_Click(object sender, EventArgs e)
        {
            try
            {
                value = true;
                startAddress = (uint)txtAddress.Value;
                objIModbusMaster.WriteSingleCoil(slaveAddress, startAddress, value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOFF_Click(object sender, EventArgs e)
        {
            try
            {
                value = false;
                startAddress = (uint)txtAddress.Value;
                objIModbusMaster.WriteSingleCoil(slaveAddress, startAddress, value);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
