
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Timers;
using System.IO;
using System.Xml;
using System.Threading;
using TwinCAT.Ads;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using PLCDrivers;
using PLCDrivers.Beckhoff;
using HmiControls;
using DatabaseInterface;

namespace GalimbertiHMIgl
{
    public partial class Form1 : Form
    {
        private PLC plcRulliera;
        private PLC plcBricc;
        private PLC plcAspirazione;
        private readonly PlcAlarmListAspirazione plcAlarmListAspirazione = new PlcAlarmListAspirazione();

        public Form1()
        {

            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        System.Timers.Timer timerRulliera = null;
        System.Timers.Timer timerAspirazione = null;
        System.Timers.Timer timerBricc = null;
        System.Timers.Timer timerDatabse = null;


        private void Form1_Load(object sender, EventArgs ev)
        {

            this.plcRulliera = new PLC(new DriverBeckhoff(ConfigurationSettings.AppSettings.Get("AmsNetId"), int.Parse(ConfigurationSettings.AppSettings.Get("AmsPort"))));
            this.plcRulliera.tryConnect();

            this.plcAspirazione = new PLC(new DriverBeckhoff(ConfigurationSettings.AppSettings.Get("AmsNetId_A"), int.Parse(ConfigurationSettings.AppSettings.Get("AmsPort_A"))));
            this.plcAspirazione.tryConnect();

            this.plcBricc = new PLC(new DriverModBus(ConfigurationSettings.AppSettings.Get("Bricc_IP"), int.Parse(ConfigurationSettings.AppSettings.Get("Bricc_Port"))));
            this.plcBricc.tryConnect();

            plcBooleanAspAlarm.register(this.plcAspirazione);

            PLCControlUtils.RegisterAll(this.plcRulliera, this.tabControl3);
            PLCControlUtils.RegisterAll(this.plcRulliera, this.groupBox25);
            PLCControlUtils.RegisterAll(this.plcAspirazione, this.Valvole);
            PLCControlUtils.RegisterAll(this.plcBricc, this.tabPage12);

            this.plcAlarmListAspirazione.comm = this.plcAspirazione;
            this.plcAlarmListAspirazione.init();

            listView1.View = View.Details;
            listView1.Columns.Add("Allarme");
            listView1.GridLines = true;
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;

            listView2.View = View.Details;
            listView2.Columns.Add("Data");
            listView2.Columns.Add("Allarme");
            listView2.GridLines = true;
            listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;

            timerRulliera = new System.Timers.Timer();
            timerRulliera.Interval = 50;
            timerRulliera.Elapsed += (s, e) =>
            {
                timerRulliera.Enabled = false;
                doLoopRulliera();
                this.cycle();
                this.cycle_alarms();
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

            plcAspSelManMode.OnUIChanges += PlcAspSelManMode_OnUIChanges;


            var _watcher = new FileSystemWatcher();
            _watcher.Path = ConfigurationSettings.AppSettings.Get("Folder");
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            _watcher.Filter = "*_7.xml";
            _watcher.Created += _watcher_Created;
            _watcher.Error += new ErrorEventHandler((x, y) => Console.WriteLine("Error"));
            _watcher.EnableRaisingEvents = true;

            var _watcher_anticipo = new FileSystemWatcher();
            _watcher_anticipo.Path = ConfigurationSettings.AppSettings.Get("Folder");
            _watcher_anticipo.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            _watcher_anticipo.Filter = "*_1.xml";
            _watcher_anticipo.Created += _watcher_Created_Anticipo;
            _watcher_anticipo.Error += new ErrorEventHandler((x, y) => Console.WriteLine("Error"));
            _watcher_anticipo.EnableRaisingEvents = true;


            timerDatabse = new System.Timers.Timer();
            timerDatabse.Interval = 60*1000;
            timerDatabse.Elapsed += (s, e) =>
            {
                timerDatabse.Enabled = false;
                this.listView1.Invoke(new Action(
                  () =>
                  {
                      aggiornaStoricoAllarmi();
                  }
                ));
                timerDatabse.Enabled = true;
            };
            timerDatabse.Start();
        }

        private void PlcAspSelManMode_OnUIChanges(PLCControl<bool> control, bool e)
        {
            this.plcAspirazione.doWithPLC((plc) =>
            {
                plc.writeBool(".HMI_Sel_Man_Mode", !e);
            });
        }

        private void cycle_alarms()
        {

            this.plcRulliera.doWithPLC(c =>
            {
                this.plcAlarm.PLCValue = false;
                this.plcAlarm.PLCDescription = "";
                try
                {
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All1_Spazio_Scarico_Su_C1_Non_Suff");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All2_Timeout_Scarico_Pz_Da_Hundegger");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All3_Spazio_Scarico_Su_C2_Non_Suff");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All4_Ftc_Scarico_C1_Su_C2");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All5_Timeout_Scarico_Pz_Da_Catenaria_C1");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All6_Timeout_Scarico_Pz_Da_Catenaria_C2");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All7_Errore_Ftc_Scarico_C2_Su_R1");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All10_Presenza_Pezzo_Uscita_C1");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All11_Mancata_Lettura_Ftc_Rotaz");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All30_Sel_Tipo_Rotazione");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All50_Drive_Rotaz_Catenaria_C1");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All51_Drive_Solleva_Catenaria_C1");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All52_Drive_Ribalta_Catenaria_C1");

                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All53_Drive_Rotazione_Catenaria_C2");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All54_Drive_Rotazione_Rulliera_R1");
                    checkAlarm(c, "RULLI_CENTRO_TAGLI.All100_Emergenza");
                } catch (Exception ex)
                {
                    this.plcAlarm.PLCValue = true;
                    this.plcAlarm.PLCDescription = ex.Message;

                }

            });

        }


        private static void checkAlarm(IPlcDriver c, String variable)
        {
            bool presenza = (bool)c.readBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger");

            if (presenza)
                throw new Exception(variable);

        }

        bool prev_WR_En_Anticipo_Hundegger = false;
        private void cycle()
        {

            this.plcRulliera.doWithPLC(c =>
            {
                bool presenza = c.readBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger");
                bool presenzaAck = c.readBool("RULLI_CENTRO_TAGLI.WR_En_Anticipo_Pz_Hundegger");

                if (presenza && presenzaAck)
                {
                    c.writeBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", false);
                }
            });



            this.plcRulliera.doWithPLC(c =>
            {

                bool presenza = c.readBool("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger");
                bool presenzaAck = c.readBool("RULLI_CENTRO_TAGLI.WR_En_Scarico_Hundegger");
                if (presenza && presenzaAck)
                {
                    c.writeBool("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger", false);
                }
            });
        }


        private void _watcher_Created(object sender, FileSystemEventArgs e)
        {

            while (!IsFileReady(e.FullPath)) ;

            XmlDocument doc = new XmlDocument();
            doc.Load(e.FullPath);

            XmlNode node = doc.DocumentElement.SelectSingleNode("/RectangularPart");
            //Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".",","));
            //Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", ","));
            //Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", ","));

            Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".", "."));
            Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", "."));
            Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", "."));

            if (x < 500)
                return;


            this.plcRulliera.doWithPLC(c =>
            {
                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Larghezza_Pz_Da_Hundegger", y);

                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Altezza_Pz_Da_Hundegger", z);

                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Rotazione_Pz_Da_Hundegger", getRotation(doc));

                c.writeBool("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger", true);

            });

        }

        private void _watcher_Created_Anticipo(object sender, FileSystemEventArgs e)
        {
            while (!IsFileReady(e.FullPath)) ;

            XmlDocument doc = new XmlDocument();
            doc.Load(e.FullPath);

            XmlNode node = doc.DocumentElement.SelectSingleNode("/RectangularPart");
            // Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".", ","));
            // Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", ","));
            // Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", ","));


            Double x = Double.Parse(node.Attributes["DimensionX"]?.InnerText.Replace(".", "."));
            Double y = Double.Parse(node.Attributes["DimensionY"]?.InnerText.Replace(".", "."));
            Double z = Double.Parse(node.Attributes["DimensionZ"]?.InnerText.Replace(".", "."));

            if (x < 500)
                return;


            this.plcRulliera.doWithPLC(c =>
            {
                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Larghezza_Anticipo_Pz_Da_Hundegger", y);

                c.writeDouble("RULLI_CENTRO_TAGLI.RD_Altezza_Anticipo_Pz_Da_Hundegger", z);

                c.writeBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", true);

            });

        }


        public int getRotation(XmlDocument doc)
        {
            try
            {
                var nodes = doc.DocumentElement.SelectNodes("//StandardFrame[@Attributes='VisibleFace']");

                if (nodes.Count > 0)
                {
                    int frame = int.Parse(nodes[0].Attributes["FrameId"].InnerText);
                    String setting = ConfigurationSettings.AppSettings.Get("FrameId" + frame);
                    return int.Parse(setting);
                }
            }
            catch (Exception ex)
            {

            }
            return 99;
        }


        private void doLoopRulliera()
        {
            this.plcConnessione.doWithUI(() => this.plcConnessione.PLCValue = this.plcRulliera.IsConnected);
            this.plcCiclica.doWithUI(() => this.plcCiclica.PLCValue = true);
            this.plcAnomalia.doWithUI(() => this.plcAnomalia.PLCValue = false);
            try
            {
                this.plcRulliera.Poll();

            } catch (Exception ex)
            {
                this.plcAnomalia.doWithUI(() => this.plcAnomalia.PLCValue = true);
            } finally
            {
                if (this.plcRulliera.GetReadWriteErrors().Count > 0)
                {
                    foreach (var err in this.plcRulliera.GetReadWriteErrors())
                    {
                        Console.WriteLine("PLC ERROR : " + err);
                    }
                    this.plcAnomalia.doWithUI(() => this.plcAnomalia.PLCValue = true);
                }
                this.plcCiclica.doWithUI(() => this.plcCiclica.PLCValue = false);
            }

        }

        private void doLoopBricc()
        {
            this.plcConnessioneBricc.doWithUI(() => this.plcConnessioneBricc.PLCValue = this.plcBricc.IsConnected);
            this.plcCiclicaBricc.doWithUI(() => this.plcCiclicaBricc.PLCValue = true);
            this.plcAnomaliaBricc.doWithUI(() => this.plcAnomaliaBricc.PLCValue = false);
            try
            {
                this.plcBricc.Poll();
            }
            catch (Exception ex)
            {
                this.plcAnomaliaBricc.doWithUI(() => this.plcAnomaliaBricc.PLCValue = true);
            }
            finally
            {
                if (this.plcBricc.GetReadWriteErrors().Count > 0)
                {
                    foreach (var err in this.plcBricc.GetReadWriteErrors())
                    {
                        Console.WriteLine("PLC BRICCH. ERROR : " + err);
                    }
                    this.plcAnomaliaBricc.doWithUI(() => this.plcAnomaliaBricc.PLCValue = true);
                }
                this.plcCiclicaBricc.doWithUI(() => this.plcCiclicaBricc.PLCValue = false);
            }

        }

        private void doLoopAspirazione()
        {
            this.plcConnessioneAsp.doWithUI(() => this.plcConnessioneAsp.PLCValue = this.plcAspirazione.IsConnected);
            this.plcCiclicaAsp.doWithUI(() => this.plcCiclicaAsp.PLCValue = true);
            this.plcAnomaliaAsp.doWithUI(() => this.plcAnomaliaAsp.PLCValue = false);
            try
            {
                this.plcAspirazione.Poll();
            }
            catch (Exception ex)
            {
                this.plcAnomaliaAsp.doWithUI(() =>this.plcAnomaliaAsp.PLCValue = true);
            }
            finally
            {
                if (this.plcAspirazione.GetReadWriteErrors().Count > 0)
                {
                    foreach (var err in this.plcAspirazione.GetReadWriteErrors())
                    {
                        Console.WriteLine("PLC ASPIRAZIONE ERROR : " + err);
                    }
                    this.plcAnomaliaAsp.doWithUI(() => this.plcAnomaliaAsp.PLCValue = true);
                }
                this.plcCiclicaAsp.doWithUI(() => this.plcCiclicaAsp.PLCValue = false);
            }

            this.listView1.Invoke(new Action(
    () =>
    {
        aggiornaAllarmi();
    }
));


        }



        PostgresLog log = new PostgresLog();

        public void aggiornaAllarmi()
        {

            foreach (var al in plcAlarmListAspirazione.newAlarms)
            {
                log.LogAspirazione("ALARM_ACTIVE", al.Value, new TimeSpan());

            }

            listView1.Items.Clear();

            foreach (var al in plcAlarmListAspirazione.activeAlarms)
            {
                var a = new ListViewItem(new string[] { al.Value });
                listView1.Items.Add(a);
            }
        }


        public void aggiornaStoricoAllarmi()
        {

            listView2.Items.Clear();

            foreach (var al in this.log.GetLogAspirazione())
            {
                var a = new ListViewItem(al);
                listView2.Items.Add(a);
            }
        }

        public static bool IsFileReady(string filename)
        {
            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None))
                    return inputStream.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox12_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox13_Enter(object sender, EventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void plcBooleanButton13_Load(object sender, EventArgs e)
        {

        }

        private void plcBooleanButton11_Load(object sender, EventArgs e)
        {
           
        }


        static void AddRow()
        {

        }

        private void plcBooleanButton11_Click(object sender, EventArgs e)
        {
            
        }


        private void tabPage10_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage12_Click(object sender, EventArgs e)
        {

        }

        private void plcBooleanSwitchSimple4_Load(object sender, EventArgs e)
        {

        }
    }
}
