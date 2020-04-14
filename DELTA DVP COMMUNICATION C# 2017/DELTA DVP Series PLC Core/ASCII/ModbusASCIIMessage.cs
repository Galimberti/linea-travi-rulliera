using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IndustrialNetworks.ModbusCore.Comm;

namespace IndustrialNetworks.ModbusCore.ASCII
{
    public partial class ModbusASCIIMessage : BaseMessage
    {
        private const char Header = ':';
        private const char CR = '\r';
        private const char LF = '\n';
        private string Trailer = string.Empty + CR;

        private string Read(byte slaveAddress, uint startAddress, byte functionCode, uint numberOfPoints)
        {
            string frame = string.Format("{0:X2}", slaveAddress);
            frame += string.Format("{0:X2}", functionCode);
            frame += string.Format("{0:X4}", startAddress);
            frame += string.Format("{0:X4}", numberOfPoints);
            byte[] bytes = Conversion.HexToBytes(frame);
            byte lrc = LRC(bytes);
            return Header + frame + lrc.ToString("X2") + Trailer;
        }

        private string Write(byte slaveAddress, uint startAddress, byte functionCode, byte[] value)
        {
            string frame = string.Format("{0:X2}", slaveAddress); // Địa chỉ slave.
            frame += string.Format("{0:X2}", functionCode); // Mã hàm modbus.
            frame += string.Format("{0:X4}", startAddress); // Địa chỉ bắt đầu của coil.
            frame += string.Format("{0:X2}", value[0]); // Dữ liệu cần ghi xuống coil.
            frame += string.Format("{0:X2}", value[1]); // Dữ liệu cần ghi xuống coil.
            byte[] bytes = Conversion.HexToBytes(frame);
            byte lrc = LRC(bytes);
            return Header + frame + lrc.ToString("X2") + Trailer;
        }

        private string WriteAll(byte slaveAddress, uint startAddress, byte functionCode, byte[] values)
        {
            string frame = string.Format("{0:X2}", slaveAddress); // Địa chỉ slave.
            frame += string.Format("{0:X2}", functionCode); // Mã hàm modbus.
            frame += string.Format("{0:X4}", startAddress); // Địa chỉ bắt đầu của coils.
            frame += string.Format("{0:X4}", (functionCode == 15) ? values.Length * 8 : values.Length / 2); // Số lượng coils.
            frame += string.Format("{0:X2}", values.Length); // Số byte cần ghi.
            foreach (byte item in values)
            {
                frame += string.Format("{0:X2}", item);
            }
            byte[] bytes = Conversion.HexToBytes(frame);
            byte lrc = LRC(bytes);
            return Header + frame + lrc.ToString("X2") + Trailer;
        }

        private byte LRC(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("Đối số truyền vào là null không hợp lệ");
            byte lrc = 0;
            foreach (byte byt in data)
                lrc += byt;
            lrc = (byte)((lrc ^ 0xFF) + 1);
            return lrc;
        }

        protected string ReadCoilStatusMessage(byte slaveAddress, uint startAddress, ushort numberOfPoints)
        {
            try
            {
                string frame = string.Format("{0:X2}", slaveAddress);   // Slave Address
                frame += string.Format("{0:X2}", FUNCTION_01);          // Function  
                frame += string.Format("{0:X4}", startAddress);         // Starting Address
                frame += string.Format("{0:X4}", numberOfPoints);       // Quantity of Coils.
                byte[] bytes = Conversion.HexToBytes(frame);            // Calculate LRC.
                byte lrc = LRC(bytes);
                return Header + frame + lrc.ToString("X2") + Trailer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string ReadInputStatusMessage(byte slaveAddress, uint startAddress, ushort numberOfPoints)
        {
            try
            {
                string frame = string.Format("{0:X2}", slaveAddress);      // Slave Address
                frame += string.Format("{0:X2}", FUNCTION_02);             // Function  
                frame += string.Format("{0:X4}", startAddress);            // Starting Address
                frame += string.Format("{0:X4}", numberOfPoints);          // Quantity of Coils.
                byte[] bytes = Conversion.HexToBytes(frame);               // Calculate LRC.
                byte lrc = LRC(bytes);
                return Header + frame + lrc.ToString("X2") + Trailer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string ReadHoldingRegistersMessage(byte slaveAddress, uint startAddress, ushort numberOfPoints)
        {
            return this.Read(slaveAddress, startAddress, FUNCTION_03, numberOfPoints);
        }

        protected string ReadInputRegistersMessage(byte slaveAddress, uint startAddress, ushort numberOfPoints)
        {
            try
            {
                string frame = string.Format("{0:X2}", slaveAddress);   // Slave Address
                frame += string.Format("{0:X2}", FUNCTION_04);          // Function  
                frame += string.Format("{0:X4}", startAddress);         // Starting Address
                frame += string.Format("{0:X4}", numberOfPoints);       // Quantity of Coils.
                byte[] bytes = Conversion.HexToBytes(frame);            // Calculate LRC.
                byte lrc = LRC(bytes);
                return Header + frame + lrc.ToString("X2") + Trailer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string WriteSingleCoilMessage(byte slaveAddress, uint startAddress, bool value)
        {
            try
            {
                string frame = string.Format("{0:X2}", slaveAddress);   // Slave Address.
                frame += string.Format("{0:X2}", FUNCTION_05);          // Function.
                frame += string.Format("{0:X4}", startAddress);         // Coil Address.
                frame += string.Format("{0:X4}", value ? 65280 : 0);    // Write Data.
                byte[] bytes = Conversion.HexToBytes(frame);
                byte lrc = LRC(bytes);
                return Header + frame + lrc.ToString("X2") + Trailer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string WriteMultipleCoilsMessage(byte slaveAddress, uint startAddress, byte[] values)
        {
            try
            {
                string frame = string.Format("{0:X2}", slaveAddress); // Slave Address.
                frame += string.Format("{0:X2}", FUNCTION_15); // Function.
                frame += string.Format("{0:X4}", startAddress); // Coil Address.
                frame += string.Format("{0:X4}", (FUNCTION_15 == 15) ? values.Length * 8 : values.Length / 2); // Quantity of Coils.
                frame += string.Format("{0:X2}", values.Length); // Byte Count.
                foreach (byte item in values)
                {
                    frame += string.Format("{0:X2}", item); // Write Data
                }
                byte[] bytes = Conversion.HexToBytes(frame);
                byte lrc = LRC(bytes);
                return Header + frame + lrc.ToString("X2") + Trailer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string WriteSingleRegisterMessage(byte slaveAddress, uint startAddress, byte[] values)
        {
            try
            {
                string frame = string.Format("{0:X2}", slaveAddress);   // Slave Address.
                frame += string.Format("{0:X2}", FUNCTION_06);         // Function.
                frame += string.Format("{0:X4}", startAddress);         // Register Address.
                foreach (byte item in values)
                {
                    frame += string.Format("{0:X2}", item);               // Write Data.
                }                
                byte[] bytes = Conversion.HexToBytes(frame);
                byte lrc = LRC(bytes);
                return Header + frame + lrc.ToString("X2") + Trailer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected string WriteMultipleRegistersMessage(byte slaveAddress, uint startAddress, byte[] values)
        {
            try
            {
                string frame = string.Format("{0:X2}", slaveAddress); // Slave Address.
                frame += string.Format("{0:X2}", FUNCTION_16); // Function.
                frame += string.Format("{0:X4}", startAddress); // Starting Address.
                frame += string.Format("{0:X4}", (FUNCTION_16 == 15) ? values.Length * 8 : values.Length / 2); // Quantity of Registers.
                frame += string.Format("{0:X2}", values.Length); // Byte Count.
                foreach (byte item in values)
                {
                    frame += string.Format("{0:X2}", item); // Write Data
                }
                byte[] bytes = Conversion.HexToBytes(frame);
                byte lrc = LRC(bytes);
                return Header + frame + lrc.ToString("X2") + Trailer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Command Code：17, Report Slave ID
        /// </summary>
        /// <param name="slaveAddress">Địa chỉ slave</param>
        /// <returns>Trả về chuỗi message</returns>
        protected string ReportSlaveIDMessage(byte slaveAddress)
        {
            try
            {
                string frame = string.Format("{0:X2}", slaveAddress); // Slave Address.
                frame += string.Format("{0:X2}", FUNCTION_17); // Function.
                byte[] bytes = Conversion.HexToBytes(frame);
                byte lrc = LRC(bytes);
                return Header + frame + lrc.ToString("X2") + Trailer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
