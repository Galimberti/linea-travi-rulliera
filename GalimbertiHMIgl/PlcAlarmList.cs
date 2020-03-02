using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalimbertiHMIgl
{
    public class PlcAlarmList
    {

        public CommunicationManager comm;

        public  List<KeyValuePair<string,string>>  alarms = new List<KeyValuePair<string, string>>();
        public  List<KeyValuePair<string, string>> activeAlarms = new List<KeyValuePair<string, string>>();

        public void registerAlarm(string var, string message)
        {
            var add = new KeyValuePair<string, string>(var, message);
            alarms.Add(add); 
        }

        public void registerAlarm(string var)
        {
            var add = new KeyValuePair<string, string>(var, var);
            alarms.Add(add);
        }



        public void init()
        {
            comm.pollActions.Add(() =>
            {
                comm.doWithClient(c =>
                {
                    List<KeyValuePair<string, string>> newList = new List<KeyValuePair<string, string>>();

                    foreach (KeyValuePair<string, string> al in alarms)
                    {
                        try
                        {
                            bool value = (bool)c.ReadSymbol(
                                                       al.Key, typeof(bool), reloadSymbolInfo: false);
                            if (value)
                                newList.Add(al);


                        } catch (Exception  ex)
                        {
                            Console.WriteLine("Allarme -->" + al.Key);
                        }
                      

                        

                    }

                    this.activeAlarms = newList;

                }, "alarms");
            });

        }
    }
}
