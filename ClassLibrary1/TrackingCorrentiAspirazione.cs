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

        public void doSnapshot()
        {
            for (int i = 0; i < this.logCurr.Count; i++ )
            {
                List<Int16> values = this.logCurr[i];

                long mid = 0;
                if (values.Count != 0)
                {
                    foreach (Int16 v in values)
                    {
                        mid += v;
                    }
                    mid = (long)(mid / values.Count);
                    values.Clear();
                }

                this.log.LogAspirazioneEC("EC_AVG_" + (i + 1), variables[i], new TimeSpan(), (int) mid);
            }
        }
            List<List<Int16>> logCurr = new List<List<Int16>>();
        public void Init()
        {

            this.variables.Add("MAIN.Inverter_10U1_Act_Current");
            this.variables.Add("MAIN.Inverter_11U1_Act_Current");
            this.variables.Add("MAIN.Inverter_12U1_Act_Current");
            this.variables.Add("MAIN.Inverter_13U1_Act_Current");
            this.variables.Add("MAIN.Inverter_14U1_Act_Current");
            this.variables.Add("MAIN.Inverter_15U1_Act_Current");
            this.variables.Add("MAIN.Inverter_16U1_Act_Current");

            this.logCurr.Add(new List<Int16>());
            this.logCurr.Add(new List<Int16>());
            this.logCurr.Add(new List<Int16>());
            this.logCurr.Add(new List<Int16>());
            this.logCurr.Add(new List<Int16>());
            this.logCurr.Add(new List<Int16>());
            this.logCurr.Add(new List<Int16>());


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
                            this.logCurr[i].Add(this.values[i]);

                            double d = Math.Abs(this.values[i] - newList[i]);
                            double f = this.values[i];
                            if (  (  d/ f) > 0.3 ) {
                                this.log.LogAspirazioneEC("EC_CHANGE_" + (i + 1), variables[i], new TimeSpan(), newList[i]);
                                this.values[i] = newList[i];
                            }

                        }
                   

                });
            });

        }
    }



    }

