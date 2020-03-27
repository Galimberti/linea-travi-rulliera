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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.plcBooleanOff = new GalimbertiHMIgl.PLCBoolean();
            this.plcBooleanOn = new GalimbertiHMIgl.PLCBoolean();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(274, 62);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Image = global::HmiControls.Properties.Resources.icons8_available_updates_50;
            this.button1.Location = new System.Drawing.Point(217, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 53);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.plcBooleanOff);
            this.panel1.Controls.Add(this.plcBooleanOn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(214, 62);
            this.panel1.TabIndex = 0;
            // 
            // plcBooleanOff
            // 
            this.plcBooleanOff.BackColor = System.Drawing.Color.Transparent;
            this.plcBooleanOff.Dock = System.Windows.Forms.DockStyle.Top;
            this.plcBooleanOff.GreenMode = false;
            this.plcBooleanOff.Location = new System.Drawing.Point(0, 25);
            this.plcBooleanOff.Margin = new System.Windows.Forms.Padding(0);
            this.plcBooleanOff.Name = "plcBooleanOff";
            this.plcBooleanOff.NotMode = true;
            this.plcBooleanOff.PLCDescription = null;
            this.plcBooleanOff.PLCError = false;
            this.plcBooleanOff.PLCValue = false;
            this.plcBooleanOff.Size = new System.Drawing.Size(214, 25);
            this.plcBooleanOff.TabIndex = 2;
            this.plcBooleanOff.VariableName = null;
            // 
            // plcBooleanOn
            // 
            this.plcBooleanOn.BackColor = System.Drawing.Color.Transparent;
            this.plcBooleanOn.Dock = System.Windows.Forms.DockStyle.Top;
            this.plcBooleanOn.GreenMode = true;
            this.plcBooleanOn.Location = new System.Drawing.Point(0, 0);
            this.plcBooleanOn.Margin = new System.Windows.Forms.Padding(0);
            this.plcBooleanOn.Name = "plcBooleanOn";
            this.plcBooleanOn.NotMode = false;
            this.plcBooleanOn.PLCDescription = null;
            this.plcBooleanOn.PLCError = false;
            this.plcBooleanOn.PLCValue = false;
            this.plcBooleanOn.Size = new System.Drawing.Size(214, 25);
            this.plcBooleanOn.TabIndex = 1;
            this.plcBooleanOn.VariableName = null;
            // 
            // PLCBooleanSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PLCBooleanSwitch";
            this.Size = new System.Drawing.Size(274, 62);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private PLCBoolean plcBooleanOff;
        private PLCBoolean plcBooleanOn;
    }
}
