namespace HmiControls
{
    partial class Asse
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
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.plcNumber2 = new GalimbertiHMIgl.PLCNumber();
            this.plcNumberEdit7 = new GalimbertiHMIgl.PLCNumberEdit();
            this.plcNumberEdit9 = new GalimbertiHMIgl.PLCNumberEdit();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.plcNumber2);
            this.groupBox7.Controls.Add(this.plcNumberEdit7);
            this.groupBox7.Controls.Add(this.plcNumberEdit9);
            this.groupBox7.Location = new System.Drawing.Point(3, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(317, 183);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Asse 1";
            // 
            // plcNumber2
            // 
            this.plcNumber2.Location = new System.Drawing.Point(6, 114);
            this.plcNumber2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plcNumber2.Name = "plcNumber2";
            this.plcNumber2.PLCDescription = "Posizione";
            this.plcNumber2.PLCValue = 0D;
            this.plcNumber2.Size = new System.Drawing.Size(233, 28);
            this.plcNumber2.TabIndex = 4;
            this.plcNumber2.VariableName = "R1_Rotazione_JogSpeed";
            // 
            // plcNumberEdit7
            // 
            this.plcNumberEdit7.Location = new System.Drawing.Point(6, 148);
            this.plcNumberEdit7.Name = "plcNumberEdit7";
            this.plcNumberEdit7.PLCDescription = "Preset Pos";
            this.plcNumberEdit7.PLCValue = 0D;
            this.plcNumberEdit7.Size = new System.Drawing.Size(299, 29);
            this.plcNumberEdit7.TabIndex = 3;
            this.plcNumberEdit7.VariableName = null;
            // 
            // plcNumberEdit9
            // 
            this.plcNumberEdit9.Location = new System.Drawing.Point(6, 31);
            this.plcNumberEdit9.Name = "plcNumberEdit9";
            this.plcNumberEdit9.PLCDescription = "Velocità";
            this.plcNumberEdit9.PLCValue = 0D;
            this.plcNumberEdit9.Size = new System.Drawing.Size(299, 29);
            this.plcNumberEdit9.TabIndex = 1;
            this.plcNumberEdit9.VariableName = null;
            // 
            // Asse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox7);
            this.Name = "Asse";
            this.Size = new System.Drawing.Size(333, 195);
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private GalimbertiHMIgl.PLCNumber plcNumber2;
        private GalimbertiHMIgl.PLCNumberEdit plcNumberEdit7;
        private GalimbertiHMIgl.PLCNumberEdit plcNumberEdit9;
    }
}
