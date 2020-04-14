namespace IndustrialNetworks.DVPSeries
{
    partial class FormReportSlaveID
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnPLCStatus = new System.Windows.Forms.Button();
            this.ModbusScan = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnPLCStatus
            // 
            this.btnPLCStatus.BackColor = System.Drawing.Color.Red;
            this.btnPLCStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPLCStatus.ForeColor = System.Drawing.Color.White;
            this.btnPLCStatus.Location = new System.Drawing.Point(71, 53);
            this.btnPLCStatus.Name = "btnPLCStatus";
            this.btnPLCStatus.Size = new System.Drawing.Size(225, 75);
            this.btnPLCStatus.TabIndex = 0;
            this.btnPLCStatus.Text = "STOP";
            this.btnPLCStatus.UseVisualStyleBackColor = false;
            this.btnPLCStatus.Click += new System.EventHandler(this.btnPLCStatus_Click);
            // 
            // ModbusScan
            // 
            this.ModbusScan.Tick += new System.EventHandler(this.ModbusScan_Tick);
            // 
            // FormReportSlaveID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 190);
            this.Controls.Add(this.btnPLCStatus);
            this.Name = "FormReportSlaveID";
            this.Text = "DELTA DVP Series PLC: Report Slave ID";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormReportSlaveID_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPLCStatus;
        private System.Windows.Forms.Timer ModbusScan;
    }
}