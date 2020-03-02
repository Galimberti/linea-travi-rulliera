using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;

namespace GalimbertiHMIgl
{
    public class CommunicationManager : IDisposable
    {
        public int port = 851;
        public string host = "5.55.57.104.1.1";
        private readonly TcAdsClient client = new TcAdsClient();
        public  readonly List<Action> pollActions  = new List<Action>();
        private readonly Dictionary<string, DateTime> readWriteErrors = new Dictionary<string, DateTime>();
        private bool connected;
        private DateTime? lastErrorTime = null;

        public CommunicationManager()
        {
        }



        public void Poll()
        {
            foreach (var action in this.pollActions)
            {
                action();
            }
        }

        public bool IsConnected
        {
            get { return this.connected; }
        }

        public ReadOnlyCollection<string> GetReadWriteErrors()
        {
            var result = this.readWriteErrors.Keys
                .OrderBy(x => x)
                .ToList();
            return result.AsReadOnly();
        }

        public void Register(Control control)
        {
            if (control == null) return;

            if (control is PLCControl<Boolean>)
            {
                this.register(control as PLCControl<Boolean>);
            }
            else if (control is PLCControl<Double>)
            {
                this.register(control as PLCControl<Double>);
            }
            else if (control is PLCControl<Int16>)
            {
                this.register(control as PLCControl<Int16>);
            }
        }

        private void register(PLCControl<Double> indicator)
        {
            Console.WriteLine(indicator.PLCDescription);

            if (String.IsNullOrEmpty(indicator.VariableName))
                return;

            this.pollActions.Add(() =>
            {
                this.doWithClient(c =>
                {

                    if (string.IsNullOrWhiteSpace(indicator.VariableName))
                    {
                        return;
                    }
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("VAR : " + indicator.VariableName);
                    double value = (double)c.ReadSymbol(
                        indicator.VariableName.Trim(), typeof(double),
                        reloadSymbolInfo: false);

                    indicator.InvokeOn(() => indicator.PLCValue = value);

                }, indicator.VariableName);
            });

            indicator.OnUIChanges += (s, e) =>
            {
                this.doWithClient(c =>
                {
                    c.WriteSymbol(indicator.VariableName,
                        e,
                        reloadSymbolInfo: false);
                }, indicator.VariableName);
            };

        }

        private void register(PLCControl<Boolean> indicator)
        {
            Console.WriteLine(indicator.PLCDescription);


            if (String.IsNullOrEmpty(indicator.VariableName))
                return;

            this.pollActions.Add(() =>
            {
                this.doWithClient(c =>
                {
                    if (string.IsNullOrWhiteSpace(
                        indicator.VariableName))
                    {
                        return;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("VAR : " + indicator.VariableName);


                    bool value = (bool)c.ReadSymbol(
                        indicator.VariableName.Trim(), typeof(bool),
                        reloadSymbolInfo: false);

                   indicator.InvokeOn(() => indicator.PLCValue = value);
                }, indicator.VariableName);
            });
             
            indicator.OnUIChanges += (s, e) =>
            {
                this.doWithClient(c =>
                {
                    c.WriteSymbol(indicator.VariableName,
                        e,
                        reloadSymbolInfo: false);
                }, indicator.VariableName);
            };
        }


        private void register(PLCControl<Int16> indicator)
        {
            Console.WriteLine(indicator.PLCDescription);


            if (String.IsNullOrEmpty(indicator.VariableName))
                return;

            this.pollActions.Add(() =>
            {
                this.doWithClient(c =>
                {
                    if (string.IsNullOrWhiteSpace(
                        indicator.VariableName))
                    {
                        return;
                    }

                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("VAR : " + indicator.VariableName);
                    short value = (short)c.ReadSymbol(
                        indicator.VariableName.Trim(), typeof(short),
                        reloadSymbolInfo: false);

                   indicator.InvokeOn(() => indicator.PLCValue = value);
                }, indicator.VariableName);
            });

            indicator.OnUIChanges += (s, e) =>
            {
                this.doWithClient(c =>
                {
                    c.WriteSymbol(indicator.VariableName,
                        e,
                        reloadSymbolInfo: false);
                }, indicator.VariableName);
            };
        }

        public void doWithClient(
            Action<TcAdsClient> action,
            string variableName)
        {
            this.tryConnect();
            if (this.connected)
            {
                try
                {
                    action(this.client);
                    this.readWriteSuccess(variableName);
                }
                catch (AdsException ex)
                {
                    Console.WriteLine(ex);
                    readWriteError(variableName);
                }
            }
        }

        public void tryConnect()
        {
            if (!this.connected)
            {
                if (this.lastErrorTime.HasValue)
                {
                    // wait a bit before re-establishing connection
                    var elapsed = DateTime.Now
                        .Subtract(this.lastErrorTime.Value);
                    if (elapsed.TotalMilliseconds < 3000)
                    {
                        return;
                    }
                }
                try
                {
                    this.client.Connect(this.host, this.port);
                    this.connected = this.client.IsConnected;
                }
                catch (AdsException)
                {
                    connectError();
                }
            }
        }

        private void connectError()
        {
            this.connected = false;
            this.lastErrorTime = DateTime.Now;
        }

        private void readWriteSuccess(string variableName)
        {
            if (this.readWriteErrors.ContainsKey(variableName))
            {
                this.readWriteErrors.Remove(variableName);
            }
        }

        private void readWriteError(string variableName)
        {
            if (this.readWriteErrors.ContainsKey(variableName))
            {
                this.readWriteErrors[variableName] = DateTime.Now;
            }
            else
            {
                this.readWriteErrors.Add(variableName, DateTime.Now);
            }
        }

        public void Dispose()
        {
            this.client.Dispose();
        }
    }
}
