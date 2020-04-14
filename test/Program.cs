
using GalimbertiHMIgl;
using Modbus.Device;
using PLCDrivers;
using PLCDrivers.Beckhoff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            ValmecCycle cycle = new ValmecCycle();
          // cycle.connect();

            var t = cycle.readRecipe();
            //max 600h
            //  80-  300h
            cycle.sendReceipe(3,250, 250);

            /*
            DriverDELTA mb = new DriverDELTA("192.168.30.160", 502);
            mb.connect();
            var read1 =  mb.readInt16("D580");



            string ipAddress = "192.168.30.160"; //use TCP for example
            int tcpPort = 502;
            TcpClient tcpClient = new TcpClient("192.168.30.160", 502);
           
            ModbusIpMaster master = ModbusIpMaster.CreateIp(tcpClient);
            master.Transport.Retries = 0;
            var read = master.ReadHoldingRegisters(0, 4676, 1);
            for (byte i = 0; i < 255; i++) {
              
                Console.WriteLine("***************************************" + i);
                foreach (ushort r in read){
                    Console.WriteLine(r);

                }
            }
           

    */
           

            // output: 
            // Input 100=0
            // Input 101=0
            // Input 102=0
            // Input 103=0
            // Input 104=0
        }
    }
}
