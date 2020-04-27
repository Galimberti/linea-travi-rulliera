using DatabaseInterface;
using PLCDrivers;
using PLCDrivers.Beckhoff;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalimbertiHMIgl
{
    public class Runner
    {
        private PLC plcRulliera;
        private PLC plcBricc;
        private PLC plcAspirazione;
        private CycleHundegger hundegger = new CycleHundegger();
        private readonly PlcAlarmListAspirazione plcAlarmListAspirazione = new PlcAlarmListAspirazione();
        private readonly PlcAlarmListRulliera1 plcAlarmListRulliera1 = new PlcAlarmListRulliera1();
        private readonly PlcAlarmListRulliera2 plcAlarmListRulliera2 = new PlcAlarmListRulliera2();
        private readonly TrackingCorrentiAspirazione plcTrackingAspirazione = new TrackingCorrentiAspirazione();


        System.Timers.Timer timerRulliera = null;
        System.Timers.Timer timerAspirazione = null;
        System.Timers.Timer timerBricc = null;
        System.Timers.Timer timerDatabse = null;

        PostgresLog log = new PostgresLog();

        public void init()
        {
       
            this.plcRulliera = new PLC(new DriverBeckhoff(ConfigurationSettings.AppSettings.Get("AmsNetId"), int.Parse(ConfigurationSettings.AppSettings.Get("AmsPort"))));
            this.plcRulliera.tryConnect();

            this.plcAspirazione = new PLC(new DriverBeckhoff(ConfigurationSettings.AppSettings.Get("AmsNetId_A"), int.Parse(ConfigurationSettings.AppSettings.Get("AmsPort_A"))));
            this.plcAspirazione.tryConnect();

            this.plcBricc = new PLC(new DriverModBus(ConfigurationSettings.AppSettings.Get("Bricc_IP"), int.Parse(ConfigurationSettings.AppSettings.Get("Bricc_Port"))));
            this.plcBricc.tryConnect();

            this.plcAlarmListAspirazione.comm = this.plcAspirazione;
            this.plcAlarmListAspirazione.init();

            this.plcAlarmListRulliera1.comm = this.plcRulliera;
            this.plcAlarmListRulliera1.init();

            this.plcAlarmListRulliera2.comm = this.plcRulliera;
            this.plcAlarmListRulliera2.init();

            this.plcTrackingAspirazione.comm = this.plcAspirazione;
            this.plcTrackingAspirazione.log = this.log;
            this.plcTrackingAspirazione.Init();

            this.hundegger.init(this.plcRulliera, null);

            timerRulliera = new System.Timers.Timer();
            timerRulliera.Interval = 50;
            timerRulliera.Elapsed += (s, e) =>
            {
                timerRulliera.Enabled = false;
                doLoopRulliera();
                timerRulliera.Enabled = true;
            };
            timerRulliera.Start();

            timerAspirazione = new System.Timers.Timer();
            timerAspirazione.Interval = 50;
            timerAspirazione.Elapsed += (s, e) =>
            {
                timerAspirazione.Enabled = false;
                doLoopAspirazione();
                timerAspirazione.Enabled = true;
            };
            timerAspirazione.Start();

            timerBricc = new System.Timers.Timer();
            timerBricc.Interval = 500;
            timerBricc.Elapsed += (s, e) =>
            {
                timerBricc.Enabled = false;
                doLoopBricc();
                timerBricc.Enabled = true;
            };
            timerBricc.Start();
        }



        private void doLoopAspirazione()
        {
            try
            {
                this.plcAspirazione.Poll();

                foreach (var al in plcAlarmListAspirazione.newAlarms)
                {
                    log.LogAspirazione("ALARM_ACTIVE", al.Value, new TimeSpan());
                }
            }
            catch (Exception ex)
            {
               
            }
        }


        private void doLoopRulliera()
        {
            try
            {
                this.plcRulliera.Poll();

                foreach (var al in this.plcAlarmListRulliera1.newAlarms)
                {
                    log.LogRulliera("ALARM_ACTIVE","1", al.Value, new TimeSpan());

                }

                foreach (var al in this.plcAlarmListRulliera2.newAlarms)
                {
                    log.LogRulliera("ALARM_ACTIVE","2", al.Value, new TimeSpan());

                }
            }
            catch (Exception ex)
            {

            }
        }


        private void doLoopBricc()
        {
            try
            {
                this.plcBricc.Poll();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
