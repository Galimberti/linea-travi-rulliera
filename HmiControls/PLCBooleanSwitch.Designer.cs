namespace GalimbertiHMIgl
{
    partial class PLCBooleanSwitch
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
            this.button1 = new System.Windows.Forms.Button();
            this.plcBooleanOff = new GalimbertiHMIgl.PLCBoolean();
            this.plcBooleanOn = new GalimbertiHMIgl.PLCBoolean();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(184, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(58, 54);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // plcBooleanOff
            // 
            this.plcBooleanOff.GreenMode = false;
            this.plcBooleanOff.Location = new System.Drawing.Point(3, 30);
            this.plcBooleanOff.Name = "plcBooleanOff";
            this.plcBooleanOff.NotMode = true;
            this.plcBooleanOff.PLCDescription = null;
            this.plcBooleanOff.PLCValue = false;
            this.plcBooleanOff.Size = new System.Drawing.Size(150, 26);
            this.plcBooleanOff.TabIndex = 1;
            // 
            // plcBooleanOn
            // 
            this.plcBooleanOn.GreenMode = true;
            this.plcBooleanOn.Location = new System.Drawing.Point(3, 3);
            this.plcBooleanOn.Name = "plcBooleanOn";
            this.plcBooleanOn.NotMode = false;
            this.plcBooleanOn.PLCDescription = null;
            this.plcBooleanOn.PLCValue = false;
            this.plcBooleanOn.Size = new System.Drawing.Size(150, 26);
            this.plcBooleanOn.TabIndex = 0;
            // 
            // PLCBooleanSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.plcBooleanOff);
            this.Controls.Add(this.plcBooleanOn);
            this.Name = "PLCBooleanSwitch";
            this.Size = new System.Drawing.Size(242, 54);
            this.ResumeLayout(false);

        }

        #endregion

        private PLCBoolean plcBooleanOn;
        private PLCBoolean plcBooleanOff;
        private System.Windows.Forms.Button button1;
    }
}
