namespace HmiControls
{
    partial class DUT_Puliz_Filtro
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
            this.btnChiudi = new GalimbertiHMIgl.PLCBooleanButton();
            this.btnApri = new GalimbertiHMIgl.PLCBooleanButton();
            this.plcBoolean1 = new GalimbertiHMIgl.PLCBoolean();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.plcBoolean1);
            this.groupBox1.Controls.Add(this.btnChiudi);
            this.groupBox1.Controls.Add(this.btnApri);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 88);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item[]";
            // 
            // btnChiudi
            // 
            this.btnChiudi.GreenMode = true;
            this.btnChiudi.Location = new System.Drawing.Point(116, 50);
            this.btnChiudi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChiudi.Name = "btnChiudi";
            this.btnChiudi.NotMode = false;
            this.btnChiudi.PLCDescription = "Chiudi";
            this.btnChiudi.PLCValue = false;
            this.btnChiudi.Size = new System.Drawing.Size(104, 28);
            this.btnChiudi.TabIndex = 2;
            this.btnChiudi.VariableName = null;
            // 
            // btnApri
            // 
            this.btnApri.GreenMode = true;
            this.btnApri.Location = new System.Drawing.Point(6, 50);
            this.btnApri.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnApri.Name = "btnApri";
            this.btnApri.NotMode = false;
            this.btnApri.PLCDescription = "Apri";
            this.btnApri.PLCValue = false;
            this.btnApri.Size = new System.Drawing.Size(104, 28);
            this.btnApri.TabIndex = 1;
            this.btnApri.VariableName = null;
            // 
            // plcBoolean1
            // 
            this.plcBoolean1.BackColor = System.Drawing.Color.Transparent;
            this.plcBoolean1.GreenMode = true;
            this.plcBoolean1.Location = new System.Drawing.Point(6, 18);
            this.plcBoolean1.Margin = new System.Windows.Forms.Padding(0);
            this.plcBoolean1.Name = "plcBoolean1";
            this.plcBoolean1.NotMode = false;
            this.plcBoolean1.PLCDescription = null;
            this.plcBoolean1.PLCValue = false;
            this.plcBoolean1.Size = new System.Drawing.Size(222, 30);
            this.plcBoolean1.TabIndex = 3;
            this.plcBoolean1.VariableName = null;
            // 
            // DUT_Puliz_Filtro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DUT_Puliz_Filtro";
            this.Size = new System.Drawing.Size(234, 95);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private GalimbertiHMIgl.PLCBooleanButton btnApri;
        private GalimbertiHMIgl.PLCBooleanButton btnChiudi;
        private GalimbertiHMIgl.PLCBoolean plcBoolean1;
    }
}
