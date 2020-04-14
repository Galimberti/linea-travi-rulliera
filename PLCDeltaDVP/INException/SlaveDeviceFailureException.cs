using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IndustrialNetworks.ModbusCore.INException
{
    public class SlaveDeviceFailureException : Exception
    {
        public SlaveDeviceFailureException()
            : base(IMessage.SLAVE_DEVICE_FAILURE) { }

        public SlaveDeviceFailureException(string message)
            : base(message) { }

        public SlaveDeviceFailureException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public SlaveDeviceFailureException(string message, Exception innerException)
            : base(message, innerException) { }

        public SlaveDeviceFailureException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected SlaveDeviceFailureException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
