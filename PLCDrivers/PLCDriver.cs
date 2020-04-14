using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCDrivers
{
    public abstract class IPlcDriver : IDisposable
    {

        public abstract Boolean readBool(string var);
        public abstract void writeBool(string var, Boolean value);

        public abstract Double readDouble(string var);
        public abstract void writeDouble(string var, Double value);

        public abstract Int16 readInt16(string var);
        public abstract void writeInt16(string var, Int16 value);

        public abstract Int32 readInt32(string var);
        public abstract void writeInt32(string var, Int32 value);

        public abstract float readFloat(string var);
        public abstract void  writeFloat(string var, float value);

        public abstract bool isConnected();
        public abstract void connect();

        public abstract void Dispose();
    }
}
