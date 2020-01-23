
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


namespace GalimbertiHMIgl
{
    public partial class Form1 : Form
    {
        private readonly CommunicationManager plcRulliera = new CommunicationManager();
        private readonly CommunicationManager plcAspirazione = new CommunicationManager();

        public Form1()
        {
           
            InitializeComponent();
        }



        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls);
        }


        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        System.Timers.Timer timer = null;
        private void Form1_Load(object sender, EventArgs ev)
        {

            this.plcRulliera.host = ConfigurationSettings.AppSettings.Get("AmsNetId");
            this.plcRulliera.port = int.Parse(ConfigurationSettings.AppSettings.Get("AmsPort"));
            this.plcRulliera.tryConnect();

            this.plcAspirazione.host = ConfigurationSettings.AppSettings.Get("AmsNetId_A");
            this.plcAspirazione.port = int.Parse(ConfigurationSettings.AppSettings.Get("AmsPort_A"));
            this.plcAspirazione.tryConnect();

            foreach (var control in GetAll(this.tabControl3,null))
            {
                this.plcRulliera.Register(control as Control);
            }
            foreach (var control in GetAll(this.groupBox25, null))
            {
                this.plcRulliera.Register(control as Control);
            }

            foreach (var control in GetAll(this.tabControl1, null))
            {
                this.plcAspirazione.Register(control as Control);
            }



            timer = new System.Timers.Timer();
            timer.Interval = 500;
            timer.Elapsed += (s, e) =>
            {
                timer.Enabled = false;
                doLoopRulliera();
                doLoopAspirazione();
                this.cycle();
                this.cycle_alarms();
                timer.Enabled = true;
            };
            timer.Start();


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
        }


        private void cycle_alarms()
        {
          
            this.plcRulliera.doWithClient(c =>
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
                } catch(Exception ex)
                {
                    this.plcAlarm.PLCValue = true;
                    this.plcAlarm.PLCDescription = ex.Message;

                }

            }, "");


          
        }


        private static void checkAlarm (TcAdsClient c,  String variable)
        {
            bool presenza =  (bool)c.ReadSymbol("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", typeof(bool), reloadSymbolInfo: false);

            if (presenza)
                throw new Exception(variable);

        }

        bool prev_WR_En_Anticipo_Hundegger = false;
        private void cycle()
        {

            this.plcRulliera.doWithClient(c =>
            {
                bool presenza = (bool)c.ReadSymbol("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", typeof(bool),
                         reloadSymbolInfo: false);

                bool presenzaAck = (bool)c.ReadSymbol("RULLI_CENTRO_TAGLI.WR_En_Anticipo_Pz_Hundegger", typeof(bool),
                        reloadSymbolInfo: false);

                if (presenza && presenzaAck)
                {
                    c.WriteSymbol("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", false,
                      reloadSymbolInfo: false);
                }

            }, "");



            this.plcRulliera.doWithClient(c =>
            {

                bool presenza = (bool) c.ReadSymbol("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger", typeof(bool),
                        reloadSymbolInfo: false);

                bool presenzaAck = (bool) c.ReadSymbol("RULLI_CENTRO_TAGLI.WR_En_Scarico_Hundegger", typeof(bool),
                        reloadSymbolInfo: false);

                if (presenza && presenzaAck)
                {
                    c.WriteSymbol("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger", false,
                      reloadSymbolInfo: false);
                }

            }, "");
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


            this.plcRulliera.doWithClient(c =>
            {
                c.WriteSymbol("RULLI_CENTRO_TAGLI.RD_Larghezza_Pz_Da_Hundegger",y,
                    reloadSymbolInfo: false);

                c.WriteSymbol("RULLI_CENTRO_TAGLI.RD_Altezza_Pz_Da_Hundegger", z,
                  reloadSymbolInfo: false);

                c.WriteSymbol("RULLI_CENTRO_TAGLI.RD_Rotazione_Pz_Da_Hundegger", getRotation(doc),
                  reloadSymbolInfo: false);

                c.WriteSymbol("RULLI_CENTRO_TAGLI.RD_Presenza_Pz_Da_Hundegger", true,
                    reloadSymbolInfo: false);

            }, "");

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


            this.plcRulliera.doWithClient(c =>
            {
                c.WriteSymbol("RULLI_CENTRO_TAGLI.RD_Larghezza_Anticipo_Pz_Da_Hundegger", y,
                    reloadSymbolInfo: false);

                c.WriteSymbol("RULLI_CENTRO_TAGLI.RD_Altezza_Anticipo_Pz_Da_Hundegger", z,
                  reloadSymbolInfo: false);

                 //c.WriteSymbol("RULLI_CENTRO_TAGLI.RD_Rotazione_Pz_Da_Hundegger", getRotation(doc),
                 //   reloadSymbolInfo: false);

                c.WriteSymbol("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger", true,
                    reloadSymbolInfo: false);

            }, "");

        }


        public int getRotation(XmlDocument doc)
        {
            try
            {
                var nodes = doc.DocumentElement.SelectNodes("//StandardFrame[@Attributes='VisibleFace']");

                if (nodes.Count > 0)
                {
                    int frame = int.Parse(nodes[0].Attributes["FrameId"].InnerText);
                    String setting =  ConfigurationSettings.AppSettings.Get("FrameId"+frame);
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
            this.plcConnessione.InvokeOn( () => this.plcConnessione.PLCValue = this.plcRulliera.IsConnected);
            this.plcCiclica.InvokeOn(() => this.plcCiclica.PLCValue = true);
            this.plcAnomalia.InvokeOn(() => this.plcAnomalia.PLCValue = false);
            try
            {
                this.plcRulliera.Poll();

            } catch (Exception ex)
            {
                this.plcAnomalia.InvokeOn(() => this.plcAnomalia.PLCValue = true);
            } finally
            {
                if (this.plcRulliera.GetReadWriteErrors().Count > 0)
                {
                    foreach (var err in this.plcRulliera.GetReadWriteErrors())
                    {
                        Console.WriteLine("PLC ERROR : " + err);
                    }
                    this.plcAnomalia.InvokeOn(() => this.plcAnomalia.PLCValue = true);
                }
                this.plcCiclica.InvokeOn(() => this.plcCiclica.PLCValue = false);
            }
           
        }

        private void doLoopAspirazione()
        {
            this.plcConnessioneAsp.InvokeOn(() => this.plcConnessioneAsp.PLCValue = this.plcAspirazione.IsConnected);
            this.plcCiclicaAsp.InvokeOn(() => this.plcCiclicaAsp.PLCValue = true);
            this.plcAnomaliaAsp.InvokeOn(() => this.plcAnomaliaAsp.PLCValue = false);
            try
            {
                this.plcAspirazione.Poll();
            }
            catch (Exception ex)
            {
                this.plcAnomaliaAsp.InvokeOn(() => this.plcAnomaliaAsp.PLCValue = true);
            }
            finally
            {
                if (this.plcAspirazione.GetReadWriteErrors().Count > 0)
                {
                    foreach (var err in this.plcAspirazione.GetReadWriteErrors())
                    {
                        Console.WriteLine("PLC ASPIRAZIONE ERROR : " + err);
                    }
                    this.plcAnomaliaAsp.InvokeOn(() => this.plcAnomaliaAsp.PLCValue = true);
                }
                this.plcCiclicaAsp.InvokeOn(() => this.plcCiclicaAsp.PLCValue = false);
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


        GoogleSheetLog log = new GoogleSheetLog();
        private void plcBooleanButton11_Load(object sender, EventArgs e)
        {
           
        }


        static void AddRow()
        {

        }

        private void plcBooleanButton11_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            log.init();
            List<object> l = new List<object>();
            l.Add(DateTime.Now);
            l.Add("t1");
            l.Add("t2");
            l.Add("t3");
            l.Add("t4");
            log.addLog(l);

            String connString = "Host=172.104.249.180;Username=postgres;Password=Galpwd18!;Database=mydatabase";

  


        }
    }
}
