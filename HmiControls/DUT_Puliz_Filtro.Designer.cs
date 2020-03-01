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
            this.abilita = new GalimbertiHMIgl.PLCBooleanSwitchSimple();
            this.btnApri = new GalimbertiHMIgl.PLCBooleanButton();
            this.btnChiudi = new GalimbertiHMIgl.PLCBooleanButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnChiudi);
            this.groupBox1.Controls.Add(this.btnApri);
            this.groupBox1.Controls.Add(this.abilita);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 88);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item[]";
            // 
            // abilita
            // 
            this.abilita.DescriptionOff = "Abilita Manuale";
            this.abilita.DescriptionOn = "Abilita Manuale";
            this.abilita.Location = new System.Drawing.Point(6, 20);
            this.abilita.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.abilita.Name = "abilita";
            this.abilita.PLCDescription = "Abilita Manuale";
            this.abilita.PLCValue = false;
            this.abilita.Size = new System.Drawing.Size(214, 26);
            this.abilita.TabIndex = 0;
            this.abilita.VariableName = null;
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
        private GalimbertiHMIgl.PLCBooleanSwitchSimple abilita;
        private GalimbertiHMIgl.PLCBooleanButton btnChiudi;
    }
}
