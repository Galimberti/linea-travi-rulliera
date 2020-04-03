
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
        private CycleHundegger hundegger = new CycleHundegger();
        private readonly PlcAlarmListAspirazione plcAlarmListAspirazione = new PlcAlarmListAspirazione();
        private readonly PlcAlarmListRulliera1 plcAlarmListRulliera1 = new PlcAlarmListRulliera1();
        private readonly PlcAlarmListRulliera2 plcAlarmListRulliera2 = new PlcAlarmListRulliera2();

        private readonly TrackingCorrentiAspirazione plcTrackingAspirazione = new TrackingCorrentiAspirazione();

        public Form1()
        {
            Font = new Font(Font.Name, 8.25f * 96f / CreateGraphics().DpiX, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);

            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        System.Timers.Timer timerRulliera = null;
        System.Timers.Timer timerAspirazione = null;
        System.Timers.Timer timerBricc = null;
        System.Timers.Timer timerDatabse = null;
        System.Timers.Timer timerDB = null;

        public void initIOAspirazione()
        {

            this.plcGridIOAsp.Vertical = false;

            this.plcGridIOAsp.push(360, 800, true);
            this.plcGridIOAsp.CurrList.addTitle("50A9");
            this.plcGridIOAsp.CurrList.addBool(".I_Feedback_Contattore_55KM1");
            this.plcGridIOAsp.CurrList.addBool(".I_Feedback_Contattore_55KM2");

            /*
            this.plcGridIOAsp.CurrList.addBoolSwitchSimple(".test");
            this.plcGridIOAsp.CurrList.addNumber(".test");
            this.plcGridIOAsp.CurrList.addNumberEdit(".test");
            this.plcGridIOAsp.CurrList.addBoolSwitch("A","B", ".test");
            */
            this.plcGridIOAsp.CurrList.addBool(".I_Feedback_Contattore_55KM4");
            this.plcGridIOAsp.CurrList.addBool(".I_Feedback_Contattore_56KM1");
            this.plcGridIOAsp.CurrList.addBool(".I_Feedback_Contattore_56KM2");
            this.plcGridIOAsp.CurrList.addBool(".I_Feedback_Contattore_55KM1");
            this.plcGridIOAsp.CurrList.addBool(".I_Feedback_Contattore_56KM3");
            this.plcGridIOAsp.CurrList.addBool(".I_Feedback_Contattore_56KM4");

            this.plcGridIOAsp.CurrList.addBool(".I_Pls_Reset_Alr");
            this.plcGridIOAsp.CurrList.addBool(".I_Stop_Impianto_61SB2");
            this.plcGridIOAsp.CurrList.addBool(".I_Feedback_Contattore_57KM1");
            this.plcGridIOAsp.CurrList.addBool(".I_Free_2");
            this.plcGridIOAsp.CurrList.addBool(".I_Stato_Interruttore_Alim_Brichettatrice_20QF1");
            this.plcGridIOAsp.CurrList.addBool(".I_Stato_Interruttore_Rilevat_Scintille_20QF2");
            this.plcGridIOAsp.CurrList.addBool(".I_En_Scarico_Container");
            this.plcGridIOAsp.CurrList.addBool(".I_Ripristino_Emergenza_61SB3");

            this.plcGridIOAsp.CurrList.addTitle("50A10");
            this.plcGridIOAsp.CurrList.addBool(".I_Temperatura_Motore_Ventilatore_Aspiraz_1");
            this.plcGridIOAsp.CurrList.addBool(".I_Temperatura_Motore_Ventilatore_Aspiraz_2");
            this.plcGridIOAsp.CurrList.addBool(".I_Temperatura_Motore_Coclea_Estrazione");
            this.plcGridIOAsp.CurrList.addBool(".I_Temperatura_Motore_Valvola_Stellare_Filtro");
            this.plcGridIOAsp.CurrList.addBool(".I_Temperatura_Motore_Coclea_Bricchettatrice");
            this.plcGridIOAsp.CurrList.addBool(".I_Temperatura_Motore_Aspirazione_Ciclone");
            this.plcGridIOAsp.CurrList.addBool(".I_Temperatura_Motore_Valvola_Stell_Ciclone");
            this.plcGridIOAsp.CurrList.addBool(".I_Temperatura_Motore_Nastro_Evac_Bricchetti");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Velocita_Coclea_Estrazione");
            this.plcGridIOAsp.CurrList.addBool(".I_Pressostato_Linea_Aria");
            this.plcGridIOAsp.CurrList.addBool(".I_Soglia_1_Temperatura_Filtro");
            this.plcGridIOAsp.CurrList.addBool(".I_Soglia_2_Temperatura_Filtro");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Velocita_Valvola_Stellare_Bag");
            this.plcGridIOAsp.CurrList.addBool(".I_Soglia_U1_Controllo_Polvere");
            this.plcGridIOAsp.CurrList.addBool(".I_Soglia_U2_Controllo_Polvere");
            this.plcGridIOAsp.pop();

            this.plcGridIOAsp.push(360, 800, true);
            this.plcGridIOAsp.CurrList.addTitle("50A11");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Velocita_Coclea_Bricchettatrice");
            this.plcGridIOAsp.CurrList.addBool(".I_FC_Apertura_Coclea_Bricchettatrice");
            this.plcGridIOAsp.CurrList.addBool(".I_Livello_Membrana_Bricchettatrice");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Scarico_In_Bricchettatrice");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Scarico_In_Container");
            this.plcGridIOAsp.CurrList.addBool(".I_Proximity_Tubo_Scarico_Silos");
            this.plcGridIOAsp.CurrList.addBool(".I_Free_4");
            this.plcGridIOAsp.CurrList.addBool(".I_Free_5");
            this.plcGridIOAsp.CurrList.addBool(".I_QE_Bricchettatrice_En_Carico");
            this.plcGridIOAsp.CurrList.addBool(".I_QE_Bricchettatrice_Scarico_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_QE_Bricchettatrice_Ok");
            this.plcGridIOAsp.CurrList.addBool(".I_Sensore_Serranda_Ventilatore_1_Aperto");
            this.plcGridIOAsp.CurrList.addBool(".I_Sensore_Serranda_Ventilatore_1_Chiuso");
            this.plcGridIOAsp.CurrList.addBool(".I_Sensore_Serranda_Ventilatore_2_Aperto");
            this.plcGridIOAsp.CurrList.addBool(".I_Sensore_Serranda_Ventilatore_2_Chiuso");
            this.plcGridIOAsp.CurrList.addBool(".I_QE_Bricchettatrice_All_Temperatura");

            this.plcGridIOAsp.CurrList.addTitle("50A12");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Velocita_Valvola_Stellare_Ciclone");
            this.plcGridIOAsp.CurrList.addBool(".I_Sensore_Livello_Valvola_Stellare_Ciclone");
            this.plcGridIOAsp.CurrList.addBool(".I_QE_Bricchettatrice_Livello_Max");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Velocita_Nastro_Bricchetti");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Espulsione_3_Nastro_Bricchetti_IND");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Espulsione_3_Nastro_Bricchetti_AV");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Espulsione_2_Nastro_Bricchetti_IND");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Espulsione_2_Nastro_Bricchetti_AV");
            this.plcGridIOAsp.CurrList.addBool(".I_QE_Rilevatore_Scintille_Stato_1");
            this.plcGridIOAsp.CurrList.addBool(".I_QE_Rilevatore_Scintille_Stato_2");
            this.plcGridIOAsp.CurrList.addBool(".I_QE_Rilevatore_Scintille_Stato_3");
            this.plcGridIOAsp.CurrList.addBool(".I_Free_9");
            this.plcGridIOAsp.CurrList.addBool(".I_Start_Da_Macinatore");
            this.plcGridIOAsp.CurrList.addBool(".I_Start_Macinatore_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Espulsione_1_Nastro_Bricchetti_IND");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Espulsione_1_Nastro_Bricchetti_AV");
            this.plcGridIOAsp.pop();
            this.plcGridIOAsp.push(360, 800, true);

            this.plcGridIOAsp.CurrList.addTitle("50A23");
            this.plcGridIOAsp.CurrList.addNumber(".I_Px_Velocita_Valvola_Stellare_Ciclone");
            this.plcGridIOAsp.CurrList.addNumber(".I_Sensore_Livello_Valvola_Stellare_Ciclone");
            this.plcGridIOAsp.CurrList.addNumber(".I_QE_Bricchettatrice_Livello_Max");

            this.plcGridIOAsp.CurrList.addTitle("50A25");
            this.plcGridIOAsp.CurrList.addNumber(".I_Temperatura_Act_Colcea_Bricc");
            this.plcGridIOAsp.CurrList.addNumber(".I_Temperatura_Act_Silos");

            this.plcGridIOAsp.CurrList.addTitle("50A22");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Scorniciatrice_Aperta");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Scorniciatrice_Chiusa");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Multilame_Aperta");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Multilame_Chiusa");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Hundegger_Aperta");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Hundegger_Chiusa");


            this.plcGridIOAsp.CurrList.addTitle("50A22");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Scorniciatrice_Aperta");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Scorniciatrice_Chiusa");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Multilame_Aperta");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Multilame_Chiusa");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Hundegger_Aperta");
            this.plcGridIOAsp.CurrList.addBool(".I_Px_Serranda_Hundegger_Chiusa");

            this.plcGridIOAsp.CurrList.addTitle("60A10");
            this.plcGridIOAsp.CurrList.addBool(".I_Troncactrice_Tetti_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_Aspirazione_Lower_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_Sega_Orizzontale_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_Pialla_Filo_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_Scorniciatrice_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_Pialla_Spessore_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_Refendino_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_Reinhardt_Run");


            this.plcGridIOAsp.CurrList.addTitle("60A11");
            this.plcGridIOAsp.CurrList.addBool(".I_Multilame_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_Aspirazione_Pavimenti_Run");
            this.plcGridIOAsp.CurrList.addBool(".I_Scarico_Silos_Pavimenti");
            this.plcGridIOAsp.CurrList.addBool(".I_Macchina_12_Run");

        }

        public void createTrackingItem(PLCList list, String baseVar, int number)
        {
            for (int i = 1; i <= number; i++)
            {
                String varB = baseVar + "[" + i + "]";
                list.push(210, 220, true);
                list.addBool("Busy", varB+ ".Busy");
                list.addNumberEdit("R", varB+ ".Rotazione");
                list.addNumberEdit("Z", varB + ".Larghezza");
                list.addNumberEdit("Y", varB + ".Altezza");
                list.addNumberEdit("L", varB + ".Lunghezza");
                list.addNumberEdit("Scar", varB + ".Lato_scarico");
                list.addButton("Reset", "").OnUIChanges += (control, e) =>
                {
                    control.PLC.doWithPLC((p) =>
                    {
                        p.writeBool(varB + ".Busy", false);
                        p.writeInt16(varB + ".Rotazione", 0);
                        p.writeDouble(varB + ".Larghezza", 0.0);
                        p.writeDouble(varB + ".Altezza", 0.0);
                        p.writeDouble(varB + ".Lunghezza", 0.0);
                        p.writeInt16(varB + ".Rotazione", 0);
                    });
                };
 

                 list.addButton("Set", "").OnUIChanges += (control, e) =>
                 {
                     control.PLC.doWithPLC((p) =>
                     {
                         p.writeBool(varB + ".Busy", true);
                     });
                 };
                list.pop();
            }
        }

        private void Form1_OnUIChanges(PLCControl<bool> control, bool e)
        {
            throw new NotImplementedException();
        }

        public void initTrackingZ1()
        {

            this.plcTracking1.Vertical = true;
            this.plcTracking1.push(1200, 240, false, "C1");
            createTrackingItem(this.plcTracking1, ".Buffer_C1", 6);
            this.plcTracking1.pop();

            this.plcTracking1.push(1200, 240, false, "C2");
            createTrackingItem(this.plcTracking1, ".Buffer_C2", 6);
            this.plcTracking1.pop();

            this.plcTracking1.push(1200, 240, false, "R1");
            createTrackingItem(this.plcTracking1, ".Buffer_R1", 6);
            this.plcTracking1.pop();
         
        }

        public void initTrackingZ2()
        {

            this.plcTracking2.Vertical = true;

            this.plcTracking2.push(1200, 260, false);
                this.plcTracking2.push(600, 260, false, "R2");
                    createTrackingItem(this.plcTracking2, ".Buffer_R2", 3);
                this.plcTracking2.pop();
                this.plcTracking2.push(600, 260, false, "R3");
                    createTrackingItem(this.plcTracking2, ".Buffer_R3", 3);
                this.plcTracking2.pop();
            this.plcTracking2.pop();

            this.plcTracking2.push(1200, 240, false, "C3");
            createTrackingItem(this.plcTracking2, ".Buffer_C3", 6);
            this.plcTracking2.pop();
            this.plcTracking2.push(1200, 240, false, "C4");
            createTrackingItem(this.plcTracking2, ".Buffer_C4", 6);
            this.plcTracking2.pop();
            this.plcTracking2.push(1200, 240, false, "C5");
            createTrackingItem(this.plcTracking2, ".Buffer_C5", 6);
            this.plcTracking2.pop();
            this.plcTracking2.push(1200, 240, false, "C6");
            createTrackingItem(this.plcTracking2, ".Buffer_C6", 6);
            this.plcTracking2.pop();




        }





        private void Form1_Load(object sender, EventArgs ev)
        {

            this.plcRulliera = new PLC(new DriverBeckhoff(ConfigurationSettings.AppSettings.Get("AmsNetId"), int.Parse(ConfigurationSettings.AppSettings.Get("AmsPort"))));
            this.plcRulliera.tryConnect();

            this.plcAspirazione = new PLC(new DriverBeckhoff(ConfigurationSettings.AppSettings.Get("AmsNetId_A"), int.Parse(ConfigurationSettings.AppSettings.Get("AmsPort_A"))));
            this.plcAspirazione.tryConnect();

            this.plcBricc = new PLC(new DriverModBus(ConfigurationSettings.AppSettings.Get("Bricc_IP"), int.Parse(ConfigurationSettings.AppSettings.Get("Bricc_Port"))));
            this.plcBricc.tryConnect();

            plcAlarmAsp.register(this.plcAspirazione);
            this.initTrackingZ1();
            this.initTrackingZ2();
            this.initIOAspirazione();
         

            PLCControlUtils.RegisterAll(this.plcRulliera, this.tabControl3);
            PLCControlUtils.RegisterAll(this.plcRulliera, this.tracking2);
            PLCControlUtils.RegisterAll(this.plcRulliera, this.groupBox25);
            PLCControlUtils.RegisterAll(this.plcAspirazione, this.Valvole);
            PLCControlUtils.RegisterAll(this.plcBricc, this.tabPage12);

            this.plcAlarmListAspirazione.comm = this.plcAspirazione;
            this.plcAlarmListAspirazione.init();

            this.plcAlarmListRulliera1.comm = this.plcRulliera;
            this.plcAlarmListRulliera1.init();

            this.plcAlarmListRulliera2.comm = this.plcRulliera;
            this.plcAlarmListRulliera2.init();

            this.plcTrackingAspirazione.comm = this.plcAspirazione;
            this.plcTrackingAspirazione.log = this.log;
            this.plcTrackingAspirazione.Init();

            listView1.View = View.Details;
            listView1.Columns.Add("Allarme");
            listView1.GridLines = true;
            listView1.Columns[0].Width = -1;
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;

            listViewAlZ1.View = View.Details;
            listViewAlZ1.Columns.Add("Allarme");
            listViewAlZ1.Columns[0].Width = -1;
            listViewAlZ1.GridLines = true;
            listViewAlZ1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewAlZ1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewAlZ1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;


            listAlarmZ2.View = View.Details;
            listAlarmZ2.Columns.Add("Allarme");
            listAlarmZ2.Columns[0].Width = -1;
            listAlarmZ2.GridLines = true;
            listAlarmZ2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listAlarmZ2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listAlarmZ2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;

            listView2.View = View.Details;
            listView2.Columns.Add("Data");
            listView2.Columns.Add("Allarme");
            listView2.GridLines = true;
            listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listView2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;

            listViewStoricoAlarmZ1.View = View.Details;
            listViewStoricoAlarmZ1.Columns.Add("Data");
            listViewStoricoAlarmZ1.Columns.Add("Allarme");
            listViewStoricoAlarmZ1.GridLines = true; 
            listViewStoricoAlarmZ1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewStoricoAlarmZ1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewStoricoAlarmZ1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;

            listViewStoricoAlarmZ2.View = View.Details;
            listViewStoricoAlarmZ2.Columns.Add("Data");
            listViewStoricoAlarmZ2.Columns.Add("Allarme");
            listViewStoricoAlarmZ2.GridLines = true;
            listViewStoricoAlarmZ2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listViewStoricoAlarmZ2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewStoricoAlarmZ2.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;

            this.hundegger.init(this.plcRulliera);

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

            timerDB = new System.Timers.Timer();
            timerDB.Interval = 500;
            timerDB.Elapsed += (s, e) =>
            {
                timerDB.Enabled = false;
                this.hundegger.processDB();
                timerDB.Enabled = true;
            };
            timerDB.Start();

            plcAspSelManMode.OnUIChanges += PlcAspSelManMode_OnUIChanges;


        

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

      

        private static void checkAlarm(IPlcDriver c, String variable)
        {
            bool presenza = (bool)c.readBool("RULLI_CENTRO_TAGLI.RD_Anticipo_Pz_Da_Hundegger");

            if (presenza)
                throw new Exception(variable);

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

            this.listViewAlZ1.Invoke(new Action(
                () =>
                {
                    aggiornaAllarmiZ1();
                }));

            this.listAlarmZ2.Invoke(new Action(
             () =>
             {
                 aggiornaAllarmiZ2();
             }));

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
                    foreach (var err in this.plcAspirazione.GetReadWriteErrors().ToArray())
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
            listView1.Columns[0].Width = -1;
        }

        public void aggiornaAllarmiZ1()
        {

            foreach (var al in this.plcAlarmListRulliera1.newAlarms)
            {
                log.LogRulliera("ALARM_ACTIVE", "1", al.Value, new TimeSpan());

            }

          
            listViewAlZ1.Items.Clear();

            foreach (var al in plcAlarmListRulliera1.activeAlarms)
            {
                var a = new ListViewItem(new string[] { al.Value });
                listViewAlZ1.Items.Add(a);
            }
            listViewAlZ1.Columns[0].Width = -1;

            this.plcAlarmZ1.PLCValue = plcAlarmListRulliera1.activeAlarms.Count > 0;

        }

        public void aggiornaAllarmiZ2()
        {
            foreach (var al in this.plcAlarmListRulliera2.newAlarms)
            {
                log.LogRulliera("ALARM_ACTIVE", "2", al.Value, new TimeSpan());

            }

            listAlarmZ2.Items.Clear();

            foreach (var al in plcAlarmListRulliera2.activeAlarms)
            {
                var a = new ListViewItem(new string[] { al.Value });
                listAlarmZ2.Items.Add(a);
            }
            listAlarmZ2.Columns[0].Width = -1;

            this.plcAlarmZ2.PLCValue = plcAlarmListRulliera2.activeAlarms.Count > 0;
        }

        public void aggiornaStoricoAllarmi()
        {
            listViewStoricoAlarmZ1.Items.Clear();
            listViewStoricoAlarmZ2.Items.Clear();
            listView2.Items.Clear();
            foreach (var al in this.log.GetLogAspirazione())
            {
                var a = new ListViewItem(al);
                listView2.Items.Add(a);
            }
            foreach (var al in this.log.GetLogRullieraZ1())
            {
                var a = new ListViewItem(al);
                listViewStoricoAlarmZ1.Items.Add(a);
            }
            foreach (var al in this.log.GetLogRullieraZ2())
            {
                var a = new ListViewItem(al);
                listViewStoricoAlarmZ2.Items.Add(a);
            }
            listViewStoricoAlarmZ1.Columns[0].Width = -1;
            listViewStoricoAlarmZ2.Columns[0].Width = -1;
            listView2.Columns[0].Width = -1;
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

        private void plcBooleanSwitchSimple2_Load(object sender, EventArgs e)
        {

        }

        private void plcNumberEdit26_Load(object sender, EventArgs e)
        {

        }

        private void plcBoolean81_Load(object sender, EventArgs e)
        {

        }

        private void plcBoolean82_Load(object sender, EventArgs e)
        {

        }
    }
}
