using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalimbertiHMIgl
{
    public class PLCControl<T> : UserControl
    {

        public event EventHandler OnSomethingChanges;

        public delegate void UIChangesEventHandler(PLCControl<T> control, T e);
        public event UIChangesEventHandler OnUIChanges;


        protected T _value;
        protected string _description;

        public  T PLCValue
        {
            get
            {
                return _value;
            }

            set
            {

                
                if (this._value.Equals(value))
                    return;

                this._value = value;
                this.OnSomethingChanges(this, null);
            }
        }

        public String PLCDescription
        {
            get
            {
                return _description;
            }

            set
            {
                if (this._description == value)
                    return;

                this._description = value;
                this.OnSomethingChanges(this, null);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PLCControl
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "PLCControl";
            this.Size = new System.Drawing.Size(153, 27);
            this.Load += new System.EventHandler(this.PLCControl_Load);
            this.ResumeLayout(false);

        }

        private void PLCControl_Load(object sender, EventArgs e)
        {

        }

        public string VariableName
        {
            get;
            set;
        }


        protected void fireUIChanges(T value)
        {
            if (this.OnUIChanges != null)
                this.OnUIChanges(this, value);
        }

        protected void fireOnSomethingChanges()
        {
            if (this.OnSomethingChanges != null)
                this.OnSomethingChanges(this, null);
        }

        public void InvokeOn(Action Action)
        {
            this.Invoke(Action);
        }


    }
}
