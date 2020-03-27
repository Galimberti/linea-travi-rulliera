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
            this.plcLunghezza = new GalimbertiHMIgl.PLCNumber();
            this.plcLargh = new GalimbertiHMIgl.PLCNumber();
            this.plcBusy = new GalimbertiHMIgl.PLCBoolean();
            this.plcAlt = new GalimbertiHMIgl.PLCNumber();
            this.plcRotazione = new GalimbertiHMIgl.PLCNumber();
            this.SuspendLayout();
            // 
            // plcLunghezza
            // 
            this.plcLunghezza.Location = new System.Drawing.Point(0, 102);
            this.plcLunghezza.Margin = new System.Windows.Forms.Padding(0);
            this.plcLunghezza.Name = "plcLunghezza";
            this.plcLunghezza.PLCDescription = "L";
            this.plcLunghezza.PLCValue = 0D;
            this.plcLunghezza.Size = new System.Drawing.Size(145, 35);
            this.plcLunghezza.TabIndex = 11;
            this.plcLunghezza.VariableName = null;
            // 
            // plcLargh
            // 
            this.plcLargh.Location = new System.Drawing.Point(0, 76);
            this.plcLargh.Margin = new System.Windows.Forms.Padding(0);
            this.plcLargh.Name = "plcLargh";
            this.plcLargh.PLCDescription = "Y";
            this.plcLargh.PLCValue = 0D;
            this.plcLargh.Size = new System.Drawing.Size(145, 35);
            this.plcLargh.TabIndex = 10;
            this.plcLargh.VariableName = null;
            // 
            // plcBusy
            // 
            this.plcBusy.BackColor = System.Drawing.Color.Transparent;
            this.plcBusy.GreenMode = true;
            this.plcBusy.Location = new System.Drawing.Point(0, 0);
            this.plcBusy.Margin = new System.Windows.Forms.Padding(0);
            this.plcBusy.Name = "plcBusy";
            this.plcBusy.NotMode = false;
            this.plcBusy.PLCDescription = "Busy";
            this.plcBusy.PLCValue = false;
            this.plcBusy.Size = new System.Drawing.Size(145, 26);
            this.plcBusy.TabIndex = 9;
            this.plcBusy.VariableName = null;
            // 
            // plcAlt
            // 
            this.plcAlt.Location = new System.Drawing.Point(0, 50);
            this.plcAlt.Margin = new System.Windows.Forms.Padding(0);
            this.plcAlt.Name = "plcAlt";
            this.plcAlt.PLCDescription = "Z";
            this.plcAlt.PLCValue = 0D;
            this.plcAlt.Size = new System.Drawing.Size(145, 35);
            this.plcAlt.TabIndex = 8;
            this.plcAlt.VariableName = null;
            // 
            // plcRotazione
            // 
            this.plcRotazione.Location = new System.Drawing.Point(0, 23);
            this.plcRotazione.Margin = new System.Windows.Forms.Padding(0);
            this.plcRotazione.Name = "plcRotazione";
            this.plcRotazione.PLCDescription = "R";
            this.plcRotazione.PLCValue = 0D;
            this.plcRotazione.Size = new System.Drawing.Size(145, 33);
            this.plcRotazione.TabIndex = 7;
            this.plcRotazione.VariableName = null;
            // 
            // TrackingItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plcLunghezza);
            this.Controls.Add(this.plcLargh);
            this.Controls.Add(this.plcBusy);
            this.Controls.Add(this.plcAlt);
            this.Controls.Add(this.plcRotazione);
            this.Name = "TrackingItem";
            this.Size = new System.Drawing.Size(150, 131);
            this.Load += new System.EventHandler(this.TrackingItem_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private GalimbertiHMIgl.PLCNumber plcLargh;
        private GalimbertiHMIgl.PLCBoolean plcBusy;
        private GalimbertiHMIgl.PLCNumber plcAlt;
        private GalimbertiHMIgl.PLCNumber plcRotazione;
        private GalimbertiHMIgl.PLCNumber plcLunghezza;
    }
}
