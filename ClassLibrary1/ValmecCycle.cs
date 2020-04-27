using DatabaseInterface;
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
        public PLC d1 = new PLC(new DriverDELTA("192.168.30.160", 502));
        public PLC d2 = new PLC(new DriverDELTA("192.168.30.161", 502));
        public PLC d3 = new PLC(new DriverDELTA("192.168.30.162", 502));
        public PLC d4 = new PLC(new DriverDELTA("192.168.30.163", 502));
        public PLC d5 = new PLC(new DriverDELTA("192.168.30.164", 502));
        private PLC plcRulliera;

        FileLog log;

        public bool dataSent = false;
        public void init(PLC plc, FileLog log)
        {
            this.plcRulliera = plc;
            this.log = log;

            this.d1.pollActions.Add( () =>
                {

                    this.d1.doWithPLC(c =>
                    {
                        var status = c.readBool("M0");
                        var speed = c.readInt16("D420");

                        this.plcRulliera.doWithPLC((r) =>
                        {
                            r.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Run", status);
                            r.writeInt16("RULLI_CENTRO_TAGLI.RD_Levigatrice_Act_Speed", speed);
                        });
                    });

                }
               
            );

          

           this.plcRulliera.pollActions.Add(
           () => {
               this.plcRulliera.doWithPLC(c =>
               {
                   bool presenza = c.readBool(".Buffer_R3[1].Busy");
                   var ricetta = c.readInt16(".Buffer_R3[1].Nr_Ricetta");
                   bool ready = c.readBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Ready");
                   double altezza = c.readDouble(".Buffer_R3[1].Altezza");
                   double larghezza = c.readDouble(".Buffer_R3[1].Larghezza");

                   if (!presenza)
                   {
                       c.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Ready", false);
                       dataSent = false;
                   }

                  

                   if (ricetta != 0)
                   {
                       if (presenza && !ready && !dataSent)
                       {
                         
                           log.log("VALMEC AVVIO INVIO DATI");

                           this.d1.doWithPLC((p) =>
                           {
                               try
                               {
                                  
                                   d2.tryConnect();
                                   d3.tryConnect();
                                   d4.tryConnect();
                                   d5.tryConnect();

                                   stopCycle();
                                   sendReceipe(ricetta, altezza, larghezza);
                                   startCycle();

                               
                                   d2.Dispose();
                                   d3.Dispose();
                                   d4.Dispose();
                                   d5.Dispose();

                                   dataSent = true;

                                   this.plcRulliera.doWithPLC(r =>
                                   {
                                       r.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Ready", true);
                                   });
                               } catch (Exception ex)
                               {
                                   log.log("VALMEC ERRORE :" + ex.Message);
                               }
                               
                           });
                           
                       }                     
                   }
               });
           }
       );


        }


        public void sendReceipe()
        {

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

            Thread.Sleep(3000);

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

            Thread.Sleep(1500);

        }

        public int readRecipe()
        {
            return d1.driver.readInt16("D580");
        }
    }
}
