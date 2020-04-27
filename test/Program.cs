
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
            PLC d1 = new PLC(new DriverDELTA("192.168.30.160", 502));
            d1.tryConnect();
            var result =  d1.driver.readInt16("D420");

            Console.Out.WriteLine(result);


            // output: 
            // Input 100=0
            // Input 101=0
            // Input 102=0
            // Input 103=0
            // Input 104=0
        }
    }
}
