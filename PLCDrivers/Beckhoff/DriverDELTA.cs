using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCDrivers.Beckhoff
{
    public class DriverDELTA : DriverModBus
    { 

        public DriverDELTA(string host, int port) : base(host, port)
        {
           
        }

        public override short readInt16(string var)
        {
            int address = 0;
            if (var[0] == 'D')
            {
                var addr = int.Parse(var.Substring(1));
                address += addr + 4096;
            }
            return base.readInt16(address.ToString());
        }

        public override void writeInt16(string var, short value)
        {
            int address = 0;
            if (var[0] == 'D')
            {
                var addr = int.Parse(var.Substring(1));
                address += addr + 4096;
            }
            base.writeInt16(address.ToString(), value);
        }

        public override bool readBool(string var)
        {
            int address = 0;
            if (var[0] == 'M')
            {
                var addr = int.Parse(var.Substring(1));
                address += addr + 2048;
            }
            return base.readBool(address.ToString());
        }

        public override void writeBool(string var, bool value)
        {
            int address = 0;
            if (var[0] == 'M')
            {
                var addr = int.Parse(var.Substring(1));
                address += addr + 2048;
            }
            base.writeBool(address.ToString(), value);
        }

        public override void writeDouble(string var, double value)
        {
            int address = 0;
            if (var[0] == 'D')
            {
                var addr = int.Parse(var.Substring(1));
                address += addr + 4096;
            }
            base.writeDouble(address.ToString(), value);
        }

        public override double readDouble(string var)
        {
            int address = 0;
            if (var[0] == 'D')
            {
                var addr = int.Parse(var.Substring(1));
                address += addr + 4096;
            }
            return base.readDouble(address.ToString());
        }

        public override void writeFloat(string var, float value)
        {
            int address = 0;
            if (var[0] == 'D')
            {
                var addr = int.Parse(var.Substring(1));
                address += addr + 4096;
            }
            base.writeFloat(address.ToString(), value);
        }

        public override float readFloat(string var)
        {
            int address = 0;
            if (var[0] == 'D')
            {
                var addr = int.Parse(var.Substring(1));
                address += addr + 4096;
            }
            return base.readFloat(address.ToString());
        }

    }
}
