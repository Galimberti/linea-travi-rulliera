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

        public ModbusClient client = null;
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
            var read = this.client.ReadCoils(int.Parse(var), 1);
            return read[0];
        }

        public override double readDouble(string var)
        {
            int[] readHoldingRegisters = this.client.ReadHoldingRegisters(int.Parse(var),4);    //Read 10 Holding Registers from Server, starting with Address 1
            return ModbusClient.ConvertRegistersToDouble(readHoldingRegisters);
        }

        public override short readInt16(string var)
        {
            int[] readHoldingRegisters = this.client.ReadHoldingRegisters(int.Parse(var), 2);    //Read 10 Holding Registers from Server, starting with Address 1
            return  (short)ModbusClient.ConvertRegistersToInt(readHoldingRegisters);
        }

        public override void writeBool(string var, bool value)
        {
            this.client.WriteSingleCoil(int.Parse(var), value);
        }

        public override void writeDouble(string var, double value)
        {
            var reg = ModbusClient.ConvertDoubleToRegisters(value);
            this.client.WriteMultipleRegisters(int.Parse(var), reg);    //Read 10 Holding Registers from Server, starting with Address 1
        }

        public override void writeInt16(string var, short value)
        {
            var reg = ModbusClient.ConvertIntToRegisters(value);
            this.client.WriteMultipleRegisters(int.Parse(var), reg);    //Read 10 Holding Registers from Server, starting with Address 1
        }

        public override int readInt32(string var)
        {
            throw new NotImplementedException();
        }

        public override void writeInt32(string var, int value)
        {
            throw new NotImplementedException();
        }

        public override float readFloat(string var)
        {
            int[] readHoldingRegisters = this.client.ReadHoldingRegisters(int.Parse(var), 2);    //Read 10 Holding Registers from Server, starting with Address 1
            return ModbusClient.ConvertRegistersToFloat(readHoldingRegisters, ModbusClient.RegisterOrder.LowHigh);

        }

        public override void writeFloat(string var, float value)
        {
            var reg = ModbusClient.ConvertFloatToRegisters(value);
            this.client.WriteMultipleRegisters(int.Parse(var), reg);    //Read 10 Holding Registers from Server, starting with Address 1

        }
    }
}
