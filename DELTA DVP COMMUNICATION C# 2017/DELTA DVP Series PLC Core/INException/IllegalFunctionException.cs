using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IndustrialNetworks.ModbusCore.INException
{
    public class IllegalFunctionException : Exception
    {
        public IllegalFunctionException()
            : base(IMessage.ILLEGAL_FUNCTION) { }

        public IllegalFunctionException(string message)
            : base(message) { }

        public IllegalFunctionException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public IllegalFunctionException(string message, Exception innerException)
            : base(message, innerException) { }

        public IllegalFunctionException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected IllegalFunctionException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
