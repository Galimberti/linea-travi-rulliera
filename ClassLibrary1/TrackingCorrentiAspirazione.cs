using DatabaseInterface;
using PLCDrivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalimbertiHMIgl
{
    public class TrackingCorrentiAspirazione
    {

        public PLC comm;
        public PostgresLog log;

        public List<string> variables = new List<string>();
        public List<Int16>  values = new List<Int16>();


        public TrackingCorrentiAspirazione()
        {

        }

        public void Init()
        {

            this.variables.Add("MAIN.Inverter_10U1_Act_Current");
            this.variables.Add("MAIN.Inverter_11U1_Act_Current");
            this.variables.Add("MAIN.Inverter_12U1_Act_Current");
            this.variables.Add("MAIN.Inverter_13U1_Act_Current");
            this.variables.Add("MAIN.Inverter_14U1_Act_Current");
            this.variables.Add("MAIN.Inverter_15U1_Act_Current");
            this.variables.Add("MAIN.Inverter_16U1_Act_Current");


            foreach (string al in variables)
            {
                this.values.Add(0);
            }

            comm.pollActions.Add(() =>
            {
                comm.doWithPLC(c =>
                {

                    List<Int16> newList = new List<Int16>();
                    
                    foreach ( string al in variables)
                    {
                        try
                        {
                            Int16 value = c.readInt16(al);
                            newList.Add(value);
                        }
                        catch (Exception ex)
                        {
                            this.log.LogAspirazione("ERROR", al, new TimeSpan());
                            this.log.LogAspirazione("ERROR_DETAIL", ex.Message, new TimeSpan());
                            Console.WriteLine("Tracking Correnti Aspirazione -->" + al);
                        }
                    }


                        for (int i = 0; i < this.values.Count; i++)
                        {
                            double d = Math.Abs(this.values[i] - newList[i]);
                            double f = this.values[i];
                            if (  (  d/ f) > 0.1 ) {
                                this.log.LogAspirazioneEC("EC_CHANGE_" + (i + 1), "Corrente modificata", new TimeSpan(), newList[i]);
                                this.values[i] = newList[i];
                            }

                        }
                   

                });
            });

        }
    }



    }

