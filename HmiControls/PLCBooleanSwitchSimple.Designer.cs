namespace GalimbertiHMIgl
{
    partial class PLCBooleanSwitchSimple
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
            this.plcBooleanOn = new GalimbertiHMIgl.PLCBoolean();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // plcBooleanOn
            // 
            this.plcBooleanOn.GreenMode = true;
            this.plcBooleanOn.Location = new System.Drawing.Point(2, 2);
            this.plcBooleanOn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.plcBooleanOn.Name = "plcBooleanOn";
            this.plcBooleanOn.NotMode = false;
            this.plcBooleanOn.PLCDescription = null;
            this.plcBooleanOn.PLCValue = false;
            this.plcBooleanOn.Size = new System.Drawing.Size(112, 21);
            this.plcBooleanOn.TabIndex = 0;
            this.plcBooleanOn.VariableName = null;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(138, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 24);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PLCBooleanSwitchSimple
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.plcBooleanOn);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "PLCBooleanSwitchSimple";
            this.Size = new System.Drawing.Size(182, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private PLCBoolean plcBooleanOn;
        private System.Windows.Forms.Button button1;
    }
}
