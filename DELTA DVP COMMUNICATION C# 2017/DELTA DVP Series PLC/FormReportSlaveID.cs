using IndustrialNetworks.ModbusCore.ASCII;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IndustrialNetworks.DVPSeries
{
    public partial class FormReportSlaveID : Form
    {
        private ModbusASCIIMaster objModbusASCIIMaster = null;

        public FormReportSlaveID()
        {
            InitializeComponent();
        }

        private void FormReportSlaveID_Load(object sender, EventArgs e)
        {
            try
            {
                objModbusASCIIMaster = new ModbusASCIIMaster("COM10", 9600, 7, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.Even);
                objModbusASCIIMaster.Connection();
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
                const byte slaveAddress = 2;
                byte[] bytes = objModbusASCIIMaster.ReportSlaveID(slaveAddress); // Slave Address = 2.
                switch (bytes[4])
                {
                    case 255:
                        btnPLCStatus.Text = "RUN";
                        btnPLCStatus.BackColor = Color.Lime;
                        btnPLCStatus.ForeColor = Color.Black;
                        break;
                    case 0:
                        btnPLCStatus.Text = "STOP";
                        btnPLCStatus.BackColor = Color.Red;
                        btnPLCStatus.ForeColor = Color.White;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPLCStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnPLCStatus.Text.Equals("RUN"))
                {
                    objModbusASCIIMaster.WriteSingleCoil(2, 3120, false);
                }
                else
                {
                    objModbusASCIIMaster.WriteSingleCoil(2, 3120, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
