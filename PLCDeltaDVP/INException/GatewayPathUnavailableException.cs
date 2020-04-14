using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IndustrialNetworks.ModbusCore.INException
{
    public class GatewayPathUnavailableException : Exception
    {
        public GatewayPathUnavailableException()
            : base(IMessage.GATEWAY_PATH_UNAVAILABLE) { }

        public GatewayPathUnavailableException(string message)
            : base(message) { }

        public GatewayPathUnavailableException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public GatewayPathUnavailableException(string message, Exception innerException)
            : base(message, innerException) { }

        public GatewayPathUnavailableException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected GatewayPathUnavailableException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
