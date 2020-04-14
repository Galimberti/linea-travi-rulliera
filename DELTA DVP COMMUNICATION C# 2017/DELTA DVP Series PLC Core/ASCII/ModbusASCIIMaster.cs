using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using IndustrialNetworks.ModbusCore.Comm;
using System.IO.Ports;
using IndustrialNetworks.ModbusCore.DataTypes;

namespace IndustrialNetworks.ModbusCore.ASCII
{
    public partial class ModbusASCIIMaster : ModbusASCIIMessage, IModbusMaster
    {
        private const int DELAY = 100;// delay 100 ms
        private ModbusSerialPortAdapter SerialAdaper = null;
        public ModbusASCIIMaster(string portName, int baudRate, int databits, StopBits stopbits, Parity parity)
        {
            try
            {
                SerialPort sp = new SerialPort(portName, baudRate, parity, databits, stopbits);
                SerialAdaper = new ModbusSerialPortAdapter(sp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ModbusASCIIMaster(SerialPort sp)
        {
            try
            {
                SerialAdaper = new ModbusSerialPortAdapter(sp);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Connection()
        {
            try
            {
                if (SerialAdaper == null) throw new Exception("Modbus SerialPort Adapter Is Null");
                this.SerialAdaper.Connect();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Disconnection()
        {
            this.SerialAdaper.Close();
        }

        /// <summary>
        /// Command Code：17, Report Slave ID
        /// </summary>
        /// <param name="slaveAddress">Địa chỉ slave</param>
        /// <returns>byte[]</returns>
        public byte[] ReportSlaveID(byte slaveAddress)
        {
            try
            {
                string frame = this.ReportSlaveIDMessage(slaveAddress);
                this.SerialAdaper.WriteLine(frame);
                Thread.Sleep(DELAY);
                string buffReceiver = this.SerialAdaper.ReadLine();
                string tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
                byte[] data = Conversion.HexToBytes(tempStrg);
                if (buffReceiver.Length == 10) this.ModbusExcetion(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] ReadCoilStatus(byte slaveAddress, uint startAddress, ushort numberOfPoints)
        {
            try
            {
                string frame = this.ReadCoilStatusMessage(slaveAddress, startAddress, numberOfPoints);
                this.SerialAdaper.WriteLine(frame);
                Thread.Sleep(DELAY);
                string buffReceiver = this.SerialAdaper.ReadLine();
                string tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
                byte[] messageReceived = Conversion.HexToBytes(tempStrg);
                if (buffReceiver.Length == 10) this.ModbusExcetion(messageReceived);
                byte[] data = new byte[messageReceived[2]];
                Array.Copy(messageReceived, 3, data, 0, data.Length);
                return Bit.ToByteArray(Bit.ToArray(data));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] ReadHoldingRegisters(byte slaveAddress, uint startAddress, ushort numberOfPoints)
        {
            try
            {
                string frame = this.ReadHoldingRegistersMessage(slaveAddress, startAddress, numberOfPoints);
                this.SerialAdaper.WriteLine(frame);
                Thread.Sleep(DELAY);
                string buffReceiver = this.SerialAdaper.ReadLine();
                if (string.IsNullOrEmpty(buffReceiver)) return new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
                string tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
                byte[] messageReceived = Conversion.HexToBytes(tempStrg);
                if (buffReceiver.Length == 10) this.ModbusExcetion(messageReceived);
                byte[] data = new byte[messageReceived[2]];
                Array.Copy(messageReceived, 3, data, 0, data.Length);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] ReadInputRegisters(byte slaveAddress, uint startAddress, ushort numberOfPoints)
        {
            try
            {
                string frame = this.ReadInputRegistersMessage(slaveAddress, startAddress, numberOfPoints);
                this.SerialAdaper.WriteLine(frame);
                Thread.Sleep(DELAY);
                string buffReceiver = this.SerialAdaper.ReadLine();
                string tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
                byte[] messageReceived = Conversion.HexToBytes(tempStrg);
                if (buffReceiver.Length == 10) this.ModbusExcetion(messageReceived);
                byte[] data = new byte[messageReceived[2]];
                Array.Copy(messageReceived, 3, data, 0, data.Length);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] ReadInputStatus(byte slaveAddress, uint startAddress, ushort numberOfPoints)
        {
            try
            {
                string frame = this.ReadInputStatusMessage(slaveAddress, startAddress, numberOfPoints);
                this.SerialAdaper.WriteLine(frame);
                Thread.Sleep(DELAY);
                string buffReceiver = this.SerialAdaper.ReadLine();
                string tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
                byte[] messageReceived = Conversion.HexToBytes(tempStrg);
                if (buffReceiver.Length == 10) this.ModbusExcetion(messageReceived);
                byte[] data = new byte[messageReceived[2]];
                Array.Copy(messageReceived, 3, data, 0, data.Length);
                return Bit.ToByteArray(Bit.ToArray(data));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] WriteMultipleCoils(byte slaveAddress, uint startAddress, byte[] values)
        {
            try
            {
                string frame = this.WriteMultipleCoilsMessage(slaveAddress, startAddress, values);
                this.SerialAdaper.WriteLine(frame);
                Thread.Sleep(DELAY);
                string buffReceiver = this.SerialAdaper.ReadLine();
                string tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
                byte[] data = Conversion.HexToBytes(tempStrg);
                if (buffReceiver.Length == 10) this.ModbusExcetion(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] WriteMultipleRegisters(byte slaveAddress, uint startAddress, byte[] values)
        {
            try
            {
                string frame = this.WriteMultipleRegistersMessage(slaveAddress, startAddress, values);
                this.SerialAdaper.WriteLine(frame);
                Thread.Sleep(DELAY);
                string buffReceiver = this.SerialAdaper.ReadLine();
                string tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
                byte[] data = Conversion.HexToBytes(tempStrg);
                if (buffReceiver.Length == 10) this.ModbusExcetion(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] WriteSingleCoil(byte slaveAddress, uint startAddress, bool value)
        {
            try
            {
                string frame = this.WriteSingleCoilMessage(slaveAddress, startAddress, value);
                this.SerialAdaper.WriteLine(frame);
                Thread.Sleep(DELAY);
                string buffReceiver = this.SerialAdaper.ReadLine();
                string tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
                byte[] data = Conversion.HexToBytes(tempStrg);
                if (buffReceiver.Length == 10) this.ModbusExcetion(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] WriteSingleRegister(byte slaveAddress, uint startAddress, byte[] values)
        {
            try
            {
                string frame = this.WriteSingleRegisterMessage(slaveAddress, startAddress, values);
                this.SerialAdaper.WriteLine(frame);
                Thread.Sleep(DELAY);
                string buffReceiver = this.SerialAdaper.ReadLine();
                string tempStrg = buffReceiver.Substring(1, buffReceiver.Length - 2);
                byte[] data = Conversion.HexToBytes(tempStrg);
                if (buffReceiver.Length == 10) this.ModbusExcetion(data);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetModbusSerialPortAdapter(ModbusSerialPortAdapter iModbusSerialPortAdapter)
        {
            try
            {
                SerialAdaper = iModbusSerialPortAdapter;
            }
            catch (Exception ex)
            {

                throw ex;
            } 
        }


    }
}
