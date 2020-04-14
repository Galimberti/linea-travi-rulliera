using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace IndustrialNetworks.ModbusCore.Comm
{
    public delegate void EventConnectionStateChanged(string status);
    public interface IModbusMaster
    {
        void Connection();

        void Disconnection();

        byte[] ReadCoilStatus(byte slaveAddress, uint startAddress, ushort numberOfPoints);

        byte[] ReadInputStatus(byte slaveAddress, uint startAddress, ushort numberOfPoints);

        byte[] ReadHoldingRegisters(byte slaveAddress, uint startAddress, ushort numberOfPoints);

        byte[] ReadInputRegisters(byte slaveAddress, uint startAddress, ushort numberOfPoints);

        byte[] WriteSingleCoil(byte slaveAddress, uint startAddress, bool value);

        byte[] WriteMultipleCoils(byte slaveAddress, uint startAddress, byte[] values);

        byte[] WriteSingleRegister(byte slaveAddress, uint startAddress, byte[] values);

        byte[] WriteMultipleRegisters(byte slaveAddress, uint startAddress, byte[] values);
    }
}
