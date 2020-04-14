namespace IndustrialNetworks.DVPSeries
{
    partial class FormReadingCoilsT0ToT2FromSlaveDevice02
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnReadTimer = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.ModbusScan = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnReadTimer);
            this.groupBox1.Controls.Add(this.txtResult);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 246);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "\tCommand Code：03, Read Holding Register";
            // 
            // btnReadTimer
            // 
            this.btnReadTimer.Location = new System.Drawing.Point(24, 190);
            this.btnReadTimer.Name = "btnReadTimer";
            this.btnReadTimer.Size = new System.Drawing.Size(108, 38);
            this.btnReadTimer.TabIndex = 1;
            this.btnReadTimer.Text = "READ TIMER";
            this.btnReadTimer.UseVisualStyleBackColor = true;
            this.btnReadTimer.Click += new System.EventHandler(this.btnReadTimer_Click);
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.Location = new System.Drawing.Point(24, 32);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(468, 152);
            this.txtResult.TabIndex = 0;
            // 
            // ModbusScan
            // 
            this.ModbusScan.Tick += new System.EventHandler(this.ModbusScan_Tick);
            // 
            // FormReadingCoilsT0ToT2FromSlaveDevice02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 270);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormReadingCoilsT0ToT2FromSlaveDevice02";
            this.Text = "Reading Coils T0~T2 from slave device 02";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormReadingCoilsT0ToT2FromSlaveDevice02_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnReadTimer;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Timer ModbusScan;
    }
}