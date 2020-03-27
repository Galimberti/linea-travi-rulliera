using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwinCAT.Ads;

namespace PLCDrivers.Beckhoff
{
    public class DriverBeckhoff : IPlcDriver
    {
        private readonly object syncLock = new object();
        public int port = 851;
        public string host = "5.55.57.104.1.1";
        private readonly TcAdsClient client = new TcAdsClient();

        public DriverBeckhoff(string host, int port)
        {
            this.host = host;
            this.port = port;
        }


        public override void connect()
        {
            lock (this.syncLock)
            {
                this.client.Connect(host, port);
            }
        }

        public override void Dispose()
        {
            lock (this.syncLock)
            {
                this.client.Disconnect();
                this.client.Dispose();
            }
        }

        public override bool isConnected()
        {
            return this.client.IsConnected;
        }

        public override bool readBool(string var)
        {
            lock (this.syncLock)
            {
                return (Boolean)this.client.ReadSymbol(
                       var, typeof(Boolean),
                       reloadSymbolInfo: false);
            }
         
        }

        public override double readDouble(string var)
        {
            lock (this.syncLock)
            {
                return (Double)this.client.ReadSymbol(
                    var, typeof(Double),
                    reloadSymbolInfo: false);
            }
        }

        public override short readInt16(string var)
        {
            lock (this.syncLock)
            {
                return (Int16)this.client.ReadSymbol(
                   var, typeof(Int16),
                   reloadSymbolInfo: false);
            }
        }

        public override void writeBool(string var, bool value)
        {
            lock (this.syncLock)
            {
                this.client.WriteSymbol(var,
                     value,
                      reloadSymbolInfo: false);
            }
        }

        public override void writeDouble(string var, double value)
        {
            lock (this.syncLock)
            {
                this.client.WriteSymbol(var,
                    value,
                     reloadSymbolInfo: false);
            }
        }

        public override void writeInt16(string var, short value)
        {
            lock (this.syncLock)
            {
                this.client.WriteSymbol(var,
                    value,
                     reloadSymbolInfo: false);
            }
        }

        public override int readInt32(string var)
        {
            lock (this.syncLock)
            {
                return (Int16)this.client.ReadSymbol(
                   var, typeof(Int32),
                   reloadSymbolInfo: false);
            }
        }

        public override void writeInt32(string var, Int32 value)
        {
            lock (this.syncLock)
            {
                this.client.WriteSymbol(var,
                    value,
                     reloadSymbolInfo: false);
            }
        }
    }
   
}
