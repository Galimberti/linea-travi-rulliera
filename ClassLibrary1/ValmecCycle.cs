using PLCDrivers;
using PLCDrivers.Beckhoff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GalimbertiHMIgl
{

    public class  VelmecCycle
    {
        PLC d1 = new PLC(new DriverDELTA("192.168.30.160", 502));
        PLC d2 = new PLC(new DriverDELTA("192.168.30.161", 502));
        PLC d3 = new PLC(new DriverDELTA("192.168.30.162", 502));
        PLC d4 = new PLC(new DriverDELTA("192.168.30.163", 502));
        PLC d5 = new PLC(new DriverDELTA("192.168.30.164", 502));
        private PLC plcRulliera;


        bool sentData = false;
        public void init(PLC plc)
        {
            this.plcRulliera = plc;
            d1.connect();
            d2.connect();
            d3.connect();
            d4.connect();
            d5.connect();

           this.plcRulliera.pollActions.Add(
           () => {
               this.plcRulliera.doWithPLC(c =>
               {
                   bool presenza = c.readBool(".Buffer_R3[1].Busy");
                   var ricetta = c.readInt16(".Buffer_R3[1].Nr_Ricetta");

                   if (ricetta != 0)
                   {
                       if (presenza && !sentData)
                       {
                           stopCycle();
                           sendReceipe(ricetta, c.readDouble(".Buffer_R3[1].Altezza"), c.readDouble(".Buffer_R3[1].Larghezza"));
                           startCycle();
                           sentData = true;
                       }
                       else
                       {
                           sentData = false;
                       }
                   }
               });
           }
       );


        }


        public void startCycle()
        {
            d1.driver.writeBool("M0", true);
            d2.driver.writeBool("M0", true);
            d3.driver.writeBool("M0", true);
            d4.driver.writeBool("M0", true);
            d5.driver.writeBool("M0", true);
        }

        public void stopCycle()
        {
            d1.driver.writeBool("M0", false);
            d2.driver.writeBool("M0", false);
            d3.driver.writeBool("M0", false);
            d4.driver.writeBool("M0", false);
            d5.driver.writeBool("M0", false);
        }

        public void sendReceipe(short recipe, double h, double l)
        {

            stopCycle();

            Thread.Sleep(300);

            d1.driver.writeInt16("D580", recipe);
            d2.driver.writeInt16("D580", recipe);
            d3.driver.writeInt16("D580", recipe);
            d4.driver.writeInt16("D580", recipe);
            d5.driver.writeInt16("D580", recipe);

            Thread.Sleep(300);

            d1.driver.writeBool("M110", true);
            d2.driver.writeBool("M110", true);
            d3.driver.writeBool("M110", true);
            d4.driver.writeBool("M110", true);
            d5.driver.writeBool("M110", true);

            Thread.Sleep(300);

            d1.driver.writeBool("M110", false);
            d2.driver.writeBool("M110", false);
            d3.driver.writeBool("M110", false);
            d4.driver.writeBool("M110", false);
            d5.driver.writeBool("M110", false);

            // ALTEZZA
            d1.driver.writeFloat("D408", (float) h);
            d2.driver.writeFloat("D408", (float)h);
            d3.driver.writeFloat("D408", (float)h);
            d4.driver.writeFloat("D408", (float)h);
            d5.driver.writeFloat("D408", (float)h);


            // LARGHEZZA
            d1.driver.writeFloat("D410", (float)l);
            d2.driver.writeFloat("D410", (float)l);
            d3.driver.writeFloat("D410", (float)l);
            d4.driver.writeFloat("D410", (float)l);
            d5.driver.writeFloat("D410", (float)l);

            d1.driver.writeBool("M200", true);
            d2.driver.writeBool("M200", true);
            d3.driver.writeBool("M200", true);
            d4.driver.writeBool("M200", true);
            d5.driver.writeBool("M200", true);

            Thread.Sleep(300);

            d1.driver.writeBool("M200", false);
            d2.driver.writeBool("M200", false);
            d3.driver.writeBool("M200", false);
            d4.driver.writeBool("M200", false);
            d5.driver.writeBool("M200", false);

            bool stable = false;
            float[] old = new float[12];
            float[] newValues = new float[12];
            while (!stable)
            {
                newValues[0]= d1.driver.readFloat("D2");
                newValues[1] = d1.driver.readFloat("D82");

                newValues[2] = d2.driver.readFloat("D42");
                newValues[3] = d2.driver.readFloat("D122");

                newValues[4] = d3.driver.readFloat("D2");
                newValues[5] = d3.driver.readFloat("D82");

                newValues[6] = d4.driver.readFloat("D2");
                newValues[7] = d4.driver.readFloat("D42");
                newValues[8] = d4.driver.readFloat("D122");

                newValues[9] = d5.driver.readFloat("D42");
                newValues[10] = d5.driver.readFloat("D82");
                newValues[11] = d5.driver.readFloat("D122");
                stable = Enumerable.SequenceEqual(old, newValues);
                old = newValues;
            }
     
        }

        public int readRecipe()
        {
            return d1.driver.readInt16("D580");
        }
    }
}
