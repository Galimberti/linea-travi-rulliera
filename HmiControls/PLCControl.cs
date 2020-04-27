using PLCDrivers;
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
        public PLC _plc;

        protected bool _plcError;

        public PLC PLC
        {
            get
            {
                return _plc;
            }

            set
            {
                this._plc = value;
            }
        }

        public bool PLCError
        {
            get
            {
                return _plcError;
            }

            set
            {
                if (this._plcError == value)
                    return;

                this._plcError = value;
                if (this._plcError)
                {
                    this.BorderStyle = BorderStyle.FixedSingle;
                } else
                {
                    this.BorderStyle = BorderStyle.None;
                }
            }
        }

        public T PLCValue
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
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PLCControl";
            this.Size = new System.Drawing.Size(153, 27);
            this.Load += new System.EventHandler(this.PLCControl_Load);
            this.ResumeLayout(false);

        }

        public void RegisterAll(PLC plcRulliera, TabControl tabControl3)
        {
            throw new NotImplementedException();
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

        public void doWithUI(Action Action)
        {
            this.Invoke(Action);
        }

    
        protected void writeToPLC(T e)
        {
            this._plc.doWithPLC(c =>
            {

                
                if (string.IsNullOrWhiteSpace(this.VariableName))
                    return;

                try
                {


                    if (this is PLCControl<Boolean>)
                    {
                        var value = Convert.ToBoolean((object)e);
                        this._plc.driver.writeBool(VariableName, value);
                    }
                    else if (this is PLCControl<Double>)
                    {
                        var value = Convert.ToDouble((object)e);
                        this._plc.driver.writeDouble(VariableName, value);
                    }
                    else if (this is PLCControl<Int16>)
                    {
                        var value = Convert.ToInt16((object)e);
                        this._plc.driver.writeInt16(VariableName, value);
                    }
                    else if (this is PLCControl<float>)
                    {
                        var value = Convert.ToDouble((object)e);
                        this._plc.driver.writeFloat(VariableName, (float)value);
                    }

                    this.PLCError = false;

                }
                catch (Exception ex)
                {
                    this.PLCError = true;
                    this._plc.readWriteError("WRITE ERROR: " + VariableName + " : " + ex.Message);
                }

            });
        }

        protected void readFromPLC()
        {
            this._plc.doWithPLC(c =>
            {

            
                if (string.IsNullOrWhiteSpace(this.VariableName))
                    return;

                try
                {
                    Object result = null;
                    if (this is PLCControl<Boolean>)
                    {
                        result = this._plc.driver.readBool(this.VariableName);
                    }
                    else if (this is PLCControl<Double>)
                    {
                        result = this._plc.driver.readDouble(this.VariableName);
                    }
                    else if (this is PLCControl<Int16>)
                    {
                        result = this._plc.driver.readInt16(this.VariableName);
                    }
                    else if (this is PLCControl<float>)
                    {
                        result = this._plc.driver.readFloat(this.VariableName);
                    }

                    if (result != null)
                        this.doWithUI(() => PLCValue = (T)result);

                    this.PLCError = false;


                }
                catch (Exception ex)
                {
                    this.PLCError = true;
                    this._plc.readWriteError("READING ERROR: " + VariableName + " : " + ex.Message);
                }

            });
        }

        public void register(PLC plc)
        {
            this._plc = plc;

            this._plc.pollActions.Add(() =>
            {
                this.readFromPLC();
            });

            this.OnUIChanges += (s, e) =>
            {
                this.writeToPLC(e);
                this.readFromPLC();
            };

        }
    }
}
