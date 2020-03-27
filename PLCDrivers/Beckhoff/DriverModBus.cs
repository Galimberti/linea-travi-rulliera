using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCDrivers.Beckhoff
{
    public class DriverModBus : IPlcDriver
    {

        ModbusClient client = null;
        public int port = 851;
        public string host = "5.55.57.104.1.1";

        public DriverModBus(string host, int port)
        {
            this.host = host;
            this.port = port;

            client = new ModbusClient(this.host, this.port);
        }


        public override void connect()
        {
            this.client.Connect();
        }

        public override void Dispose()
        {
            this.client.Disconnect();
        }

        public override bool isConnected()
        {
            return this.client.Connected;
        }

        public override bool readBool(string var)
        {
            var splits = var.Split('.');
            //var read = this.client.ReadCoils(int.Parse(splits[0]), 16);
            //return read[int.Parse(splits[1])];

            var read = this.client.ReadHoldingRegisters(int.Parse(splits[0]), 1);

            string s = Convert.ToString(read[0], 2); //Convert to binary in a string

            int[] bits = s.PadLeft(8, '0') // Add 0's from left
                         .Select(c => int.Parse(c.ToString())) // convert each char to int
                         .ToArray();

            return bits[int.Parse(splits[1])] == 1;
        }

        public override double readDouble(string var)
        {
            throw new NotImplementedException();
        }

        public override short readInt16(string var)
        {
            int[] readHoldingRegisters = this.client.ReadHoldingRegisters(int.Parse(var), 1);    //Read 10 Holding Registers from Server, starting with Address 1
            return Convert.ToInt16(readHoldingRegisters[0]);
        }

        public override void writeBool(string var, bool value)
        {
            throw new NotImplementedException();
        }

        public override void writeDouble(string var, double value)
        {
            throw new NotImplementedException();
        }

        public override void writeInt16(string var, short value)
        {
            throw new NotImplementedException();
        }

        public override int readInt32(string var)
        {
            throw new NotImplementedException();
        }

        public override void writeInt32(string var, int value)
        {
            throw new NotImplementedException();
        }
    }
}
