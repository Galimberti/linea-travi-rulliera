using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCDrivers
{
    public class PLC
    {
        public   readonly List<Action> pollActions = new List<Action>();
        private  readonly List<string> readWriteErrors = new List<string>();
        public IPlcDriver driver;


        public PLC(IPlcDriver driver)
        {
            this.driver = driver;
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
            get { return this.driver.isConnected(); }
        }

        public ReadOnlyCollection<string> GetReadWriteErrors()
        {
            lock (this.readWriteErrors) {
                return this.readWriteErrors.AsReadOnly();
            }
           
        }

 

        public void doWithPLC(Action<IPlcDriver> action)
        {
            this.tryConnect();
            if (this.IsConnected)
            {
                try
                {
                    action(this.driver);
                }
                catch (Exception ex)
                {
                    readWriteError(ex.Message);
                }
            }
        }

        public void tryConnect()
        {
            if (!this.driver.isConnected())
            {
                try
                {
                    this.driver.connect();
                }
                catch (Exception ex)
                {
                    readWriteError(ex.Message);
                }
            }
        }


        public void readWriteError(string message)
        {
            lock (this.readWriteErrors)
            {
                this.readWriteErrors.Add(DateTime.Now + ":" + message);
            }
          
        }

        public void Dispose()
        {
            this.driver.Dispose();
        }
    }
}
