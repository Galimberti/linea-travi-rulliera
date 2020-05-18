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
        private PLC plcAspirazione;

        FileLog log;

        public bool dataSent = false;
        bool m2, m3, m4, m5, m6, m7, m8, m9, m10, m11, m12, m13, m14, m15, m16, m17, m18, m19;

        public void init(PLC plc, PLC plcAsp, FileLog log)
        {
            this.plcRulliera = plc;
            this.plcAspirazione = plcAsp;
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

                            if (this.dataSent)
                            {

                              r.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Ready", true);
                              
                            }

                        });
                    });

                }
               
            );

            this.d1.pollActions.Add(() =>
            {

                this.d1.doWithPLC(c =>
                {
                    m2 = c.readBool("M513");
                    m3= m4 = c.readBool("M514");
                });
            }

            );


            this.d2.pollActions.Add(() =>
            {
                this.d2.doWithPLC(c =>
                {
                    m7 = m8 = c.readBool("M515");
                    m5 = m6 = c.readBool("M513");
                });
            }

            );



            this.d3.pollActions.Add(() =>
            {

                this.d3.doWithPLC(c =>
                {
                    m9= c.readBool("M513");
                    m10 = m11 = c.readBool("M514");
                });

            }

           );




            this.d4.pollActions.Add(() =>
            {

                this.d4.doWithPLC(c =>
                {
                    m12 = c.readBool("M513");
                    m13 = c.readBool("M514");
                    m14 = m15 = c.readBool("M515");
                });

            }

           );


            this.d5.pollActions.Add(() =>
            {

                this.d5.doWithPLC(c =>
                {
                    m16 = m17= c.readBool("M513");
                    m18 = c.readBool("M515");
                    m19 = c.readBool("M516");
                });

            }

         );

        this.plcAspirazione.pollActions.Add(() =>
            {
                this.plcAspirazione.doWithPLC(c =>
                {
                    try
                    {
                    
                        c.writeBool("MAIN.RD_Levigatrice_Serranda_1", m2 || m3 || m4);
                        c.writeBool("MAIN.RD_Levigatrice_Serranda_2", m6 || m5 || m9);
                        c.writeBool("MAIN.RD_Levigatrice_Serranda_3", m7 || m8 || m12);
                        c.writeBool("MAIN.RD_Levigatrice_Serranda_4", m11 || m10 || m13);
                        c.writeBool("MAIN.RD_Levigatrice_Serranda_5", m15 || m14 || m18);
                        c.writeBool("MAIN.RD_Levigatrice_Serranda_6", m16 || m17 || m18);
                    }
                    catch (Exception ex)
                    {
                        log.log("scrittura dati serrande " + ex.Message);
                    }
                });
            });

           this.plcRulliera.pollActions.Add(
           () => {
               this.plcRulliera.doWithPLC(c =>
               {

                   try
                   {
                       c.writeBool("M.RD_Levigatrice_Serranda_1", m2 || m3 || m4);
                       c.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Serranda_2", m6 || m5 || m9);
                       c.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Serranda_3", m7 || m8 || m12);
                       c.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Serranda_4", m11 || m10 || m13);
                       c.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Serranda_5", m15 || m14 || m18);
                       c.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Serranda_6", m16 || m17 || m18);
                   } catch (Exception ex)
                   {

                   }
                  





                   bool presenza = c.readBool(".Buffer_R3[1].Busy");
                   var ricetta = c.readInt16(".Buffer_R3[1].Nr_Ricetta");
                   bool ready = c.readBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Ready");
                   double altezza = c.readDouble(".Buffer_R3[1].Altezza");
                   double larghezza = c.readDouble(".Buffer_R3[1].Larghezza");
                   bool richiesta = c.readBool("RULLI_CENTRO_TAGLI.WR_Levigatrice_Pos_Req");

                   if (!presenza)
                   {
                       c.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Ready", false);
                       dataSent = false;
                   }

                   if (!richiesta)
                   {
                       c.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Ready", false);
                       dataSent = false;
                   }



                   if (ricetta != 0 && ricetta !=9999)
                   {
                       if (richiesta && presenza && !ready && !dataSent)
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
                                   //startCycle();

                               
                                   d2.Dispose();
                                   d3.Dispose();
                                   d4.Dispose();
                                   d5.Dispose();

                                   dataSent = true;

                                   /*
                                   this.plcRulliera.doWithPLC(r =>
                                   {
                                       r.writeBool("RULLI_CENTRO_TAGLI.RD_Levigatrice_Ready", true);
                                   });
                                   */
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

            /*
            bool stable = false;
            float[] old = new float[18];
            float[] newValues = new float[18];
            while (!stable)
            {

                Thread.Sleep(5000); 
                newValues[0]= d1.driver.readFloat("D2");
                newValues[1] = d1.driver.readFloat("D42");
                newValues[2] = d1.driver.readFloat("D82");

                newValues[3] = d2.driver.readFloat("D42");
                newValues[4] = d2.driver.readFloat("D2");
                newValues[5] = d2.driver.readFloat("D122");
                newValues[6] = d2.driver.readFloat("D82");

                newValues[7] = d3.driver.readFloat("D2");
                newValues[8] = d3.driver.readFloat("D82");
                newValues[9] = d3.driver.readFloat("D42");

                newValues[10] = d4.driver.readFloat("D2");
                newValues[11] = d4.driver.readFloat("D42");
                newValues[12] = d4.driver.readFloat("D122");
                newValues[13] = d4.driver.readFloat("D82");

                newValues[14] = d5.driver.readFloat("D2");
                newValues[15] = d5.driver.readFloat("D42");
                newValues[16] = d5.driver.readFloat("D82");
                newValues[17] = d5.driver.readFloat("D122");
                stable = Enumerable.SequenceEqual(old, newValues);
                old = newValues;

                
            }
            */

        }

        public int readRecipe()
        {
            return d1.driver.readInt16("D580");
        }
    }
}
