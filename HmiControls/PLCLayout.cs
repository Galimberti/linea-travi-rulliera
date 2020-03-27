using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalimbertiHMIgl
{
    public partial class PLCLayout : UserControl

    {
        public PLCLayout()
        {
     
            InitializeComponent();
            this._currList.Push(this);
        }


        protected  virtual FlowLayoutPanel currentLayout()
        {
            return null;
        }

        protected bool _vertical = false;

        public bool Vertical
        {
            get
            {
                return _vertical;
            }

            set
            {
                this._vertical = value;
                if (this._vertical)
                    this.currentLayout().FlowDirection = FlowDirection.TopDown;
                else
                    this.currentLayout().FlowDirection = FlowDirection.LeftToRight;
            }
        }


        protected Stack<PLCLayout> _currList = new Stack<PLCLayout>();

        public PLCLayout CurrList
        {
            get
            {
                return _currList.Peek();
            }
        }
        public void push(int width, int height, bool vertical)
        {
            var control = new PLCList();
            control.Vertical = vertical;
            setSize(control, width, height);
            this.CurrList.currentLayout().Controls.Add(control);
            this._currList.Push(control);
           
        }

        public void push(int width, int height, bool vertical, String title)
        {
            var control = new PLCListTitled();
            control.Title = title;
            control.Vertical = vertical;
            setSize(control, width, height);
            this.CurrList.currentLayout().Controls.Add(control);
            this._currList.Push(control);
            
        }

        public void pop()
        {
            this._currList.Pop();
        }


        public PLCBoolean addBool(String desc, String var)
        {
            var control = new PLCBoolean();
            control.PLCDescription = desc;
            control.VariableName = var;
            setSize(control, this.CurrList.Width - 45, 25);
            this.CurrList.currentLayout().Controls.Add(control);
            return control;
        }

        public void addTitle(String desc)
        {
            var control = new Label();
            control.Text = desc;
            control.TextAlign = ContentAlignment.MiddleLeft;
            control.Font = new Font(control.Font, FontStyle.Bold);
            setSize(control, this.CurrList.Width, 25);
            this.CurrList.currentLayout().Controls.Add(control);
        }

        public PLCBoolean addBool(String var)
        {
            return this.addBool(var, var);
        }



        public PLCBooleanSwitch addBoolSwitch(String descOn, String descOff, String var)
        {
            var control = new PLCBooleanSwitch();
            control.PLCDescription = descOn;
            control.DescriptionOn = descOn;
            control.DescriptionOff = descOff;

            control.VariableName = var;
            setSize(control, this.CurrList.Width, 55);
            this.CurrList.currentLayout().Controls.Add(control);
            return control;
        }

        public PLCNumber addNumber(String desc, String var)
        {
            var control = new PLCNumber();
            control.PLCDescription = desc;
            control.VariableName = var;
            setSize(control, this.CurrList.Width - 45, 25);
            this.CurrList.currentLayout().Controls.Add(control);
            return control;
        }


        public PLCNumberEdit addNumberEdit(String desc, String var)
        {
            var control = new PLCNumberEdit();
            control.PLCDescription = desc;
            control.VariableName = var;
            setSize(control, this.CurrList.Width, 25);
            this.CurrList.currentLayout().Controls.Add(control);
            return control;
        }

        public PLCBooleanButton addButton(String desc, String var)
        {
            var control = new PLCBooleanButton();
            control.PLCDescription = desc;
            control.VariableName = var;
            setSize(control, this.CurrList.Width - 45, 25);
            this.CurrList.currentLayout().Controls.Add(control);
            return control;
        }

        public PLCNumberEdit addNumberEdit(String var)
        {
            return addNumberEdit(var, var);
        }

        public void addNumber(String var)
        {
            this.addNumber(var, var);
        }

        public void addBoolSwitchSimple(String desc, String var)
        {
            var control = new PLCBooleanSwitchSimple();
            control.PLCDescription = desc;
            control.DescriptionOn = desc;
            control.DescriptionOff = desc;

            control.VariableName = var;
            setSize(control, this.CurrList.Width, 25);
            this.CurrList.currentLayout().Controls.Add(control);
        }

        public void addBoolSwitchSimple(String var)
        {
            this.addBoolSwitchSimple(var, var);
        }

        protected void setSize(Control control, int width, int height)
        {
            control.MaximumSize = new Size(width - 30, height);
            control.Size = new Size(width, height);
        }



    }
}
