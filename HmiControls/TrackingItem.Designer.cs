namespace HmiControls
{
    partial class TrackingItem
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.plcLargh = new GalimbertiHMIgl.PLCNumber();
            this.plcBusy = new GalimbertiHMIgl.PLCBoolean();
            this.plcAlt = new GalimbertiHMIgl.PLCNumber();
            this.plcRotazione = new GalimbertiHMIgl.PLCNumber();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.plcLargh);
            this.groupBox1.Controls.Add(this.plcBusy);
            this.groupBox1.Controls.Add(this.plcAlt);
            this.groupBox1.Controls.Add(this.plcRotazione);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 147);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item[]";
            // 
            // plcLargh
            // 
            this.plcLargh.Location = new System.Drawing.Point(6, 110);
            this.plcLargh.Name = "plcLargh";
            this.plcLargh.PLCDescription = "Y";
            this.plcLargh.PLCValue = 0D;
            this.plcLargh.Size = new System.Drawing.Size(138, 35);
            this.plcLargh.TabIndex = 6;
            this.plcLargh.VariableName = null;
            // 
            // plcBusy
            // 
            this.plcBusy.GreenMode = true;
            this.plcBusy.Location = new System.Drawing.Point(6, 25);
            this.plcBusy.Name = "plcBusy";
            this.plcBusy.NotMode = false;
            this.plcBusy.PLCDescription = "Busy";
            this.plcBusy.PLCValue = false;
            this.plcBusy.Size = new System.Drawing.Size(138, 26);
            this.plcBusy.TabIndex = 5;
            this.plcBusy.VariableName = null;
            // 
            // plcAlt
            // 
            this.plcAlt.Location = new System.Drawing.Point(6, 84);
            this.plcAlt.Name = "plcAlt";
            this.plcAlt.PLCDescription = "Z";
            this.plcAlt.PLCValue = 0D;
            this.plcAlt.Size = new System.Drawing.Size(138, 35);
            this.plcAlt.TabIndex = 4;
            this.plcAlt.VariableName = null;
            this.plcAlt.Load += new System.EventHandler(this.plcNumber2_Load);
            // 
            // plcRotazione
            // 
            this.plcRotazione.Location = new System.Drawing.Point(6, 57);
            this.plcRotazione.Name = "plcRotazione";
            this.plcRotazione.PLCDescription = "R";
            this.plcRotazione.PLCValue = 0D;
            this.plcRotazione.Size = new System.Drawing.Size(138, 33);
            this.plcRotazione.TabIndex = 3;
            this.plcRotazione.VariableName = null;
            // 
            // TrackingItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "TrackingItem";
            this.Size = new System.Drawing.Size(157, 157);
            this.Load += new System.EventHandler(this.TrackingItem_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private GalimbertiHMIgl.PLCNumber plcLargh;
        private GalimbertiHMIgl.PLCBoolean plcBusy;
        private GalimbertiHMIgl.PLCNumber plcAlt;
        private GalimbertiHMIgl.PLCNumber plcRotazione;
    }
}
