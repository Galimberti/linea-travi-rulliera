namespace HmiControls
{
    partial class DUT_Motori
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
            this.plcBooleanButton1 = new GalimbertiHMIgl.PLCBooleanButton();
            this.plcBooleanButton2 = new GalimbertiHMIgl.PLCBooleanButton();
            this.plcBoolean1 = new GalimbertiHMIgl.PLCBoolean();
            this.plcBoolean2 = new GalimbertiHMIgl.PLCBoolean();
            this.plcBoolean3 = new GalimbertiHMIgl.PLCBoolean();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.plcBoolean3);
            this.groupBox1.Controls.Add(this.plcBoolean2);
            this.groupBox1.Controls.Add(this.plcBoolean1);
            this.groupBox1.Controls.Add(this.plcBooleanButton2);
            this.groupBox1.Controls.Add(this.plcBooleanButton1);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(149, 183);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Item[]";
            // 
            // plcBooleanButton1
            // 
            this.plcBooleanButton1.GreenMode = true;
            this.plcBooleanButton1.Location = new System.Drawing.Point(8, 20);
            this.plcBooleanButton1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plcBooleanButton1.Name = "plcBooleanButton1";
            this.plcBooleanButton1.NotMode = false;
            this.plcBooleanButton1.PLCDescription = "Start Man.";
            this.plcBooleanButton1.PLCValue = false;
            this.plcBooleanButton1.Size = new System.Drawing.Size(135, 30);
            this.plcBooleanButton1.TabIndex = 0;
            this.plcBooleanButton1.VariableName = "Pb_Start_Man";
            // 
            // plcBooleanButton2
            // 
            this.plcBooleanButton2.GreenMode = true;
            this.plcBooleanButton2.Location = new System.Drawing.Point(8, 54);
            this.plcBooleanButton2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plcBooleanButton2.Name = "plcBooleanButton2";
            this.plcBooleanButton2.NotMode = false;
            this.plcBooleanButton2.PLCDescription = "Stop Man.";
            this.plcBooleanButton2.PLCValue = false;
            this.plcBooleanButton2.Size = new System.Drawing.Size(135, 30);
            this.plcBooleanButton2.TabIndex = 1;
            this.plcBooleanButton2.VariableName = "Pb_Stop_Man";
            // 
            // plcBoolean1
            // 
            this.plcBoolean1.BackColor = System.Drawing.Color.Transparent;
            this.plcBoolean1.GreenMode = true;
            this.plcBoolean1.Location = new System.Drawing.Point(8, 88);
            this.plcBoolean1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plcBoolean1.Name = "plcBoolean1";
            this.plcBoolean1.NotMode = false;
            this.plcBoolean1.PLCDescription = "Run Man On";
            this.plcBoolean1.PLCValue = false;
            this.plcBoolean1.Size = new System.Drawing.Size(135, 26);
            this.plcBoolean1.TabIndex = 2;
            this.plcBoolean1.VariableName = "Run_Man_On";
            // 
            // plcBoolean2
            // 
            this.plcBoolean2.BackColor = System.Drawing.Color.Transparent;
            this.plcBoolean2.GreenMode = true;
            this.plcBoolean2.Location = new System.Drawing.Point(8, 118);
            this.plcBoolean2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plcBoolean2.Name = "plcBoolean2";
            this.plcBoolean2.NotMode = false;
            this.plcBoolean2.PLCDescription = "Run Auto On";
            this.plcBoolean2.PLCValue = false;
            this.plcBoolean2.Size = new System.Drawing.Size(135, 26);
            this.plcBoolean2.TabIndex = 3;
            this.plcBoolean2.VariableName = "Run_Auto_On";
            // 
            // plcBoolean3
            // 
            this.plcBoolean3.BackColor = System.Drawing.Color.Transparent;
            this.plcBoolean3.GreenMode = true;
            this.plcBoolean3.Location = new System.Drawing.Point(8, 148);
            this.plcBoolean3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plcBoolean3.Name = "plcBoolean3";
            this.plcBoolean3.NotMode = false;
            this.plcBoolean3.PLCDescription = "Run";
            this.plcBoolean3.PLCValue = false;
            this.plcBoolean3.Size = new System.Drawing.Size(135, 26);
            this.plcBoolean3.TabIndex = 4;
            this.plcBoolean3.VariableName = "Run_On";
            // 
            // DUT_Motori
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "DUT_Motori";
            this.Size = new System.Drawing.Size(157, 189);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private GalimbertiHMIgl.PLCBooleanButton plcBooleanButton1;
        private GalimbertiHMIgl.PLCBoolean plcBoolean3;
        private GalimbertiHMIgl.PLCBoolean plcBoolean2;
        private GalimbertiHMIgl.PLCBoolean plcBoolean1;
        private GalimbertiHMIgl.PLCBooleanButton plcBooleanButton2;
    }
}
