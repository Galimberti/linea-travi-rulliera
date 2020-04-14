using DatabaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalimbertiHMIgl
{
    public class PlcAlarmListRulliera1 : PlcAlarmList
    {

      



        public new void init()
        {

            this.registerAlarm("RULLI_CENTRO_TAGLI.All1_Spazio_Scarico_Su_C1_Non_Suff");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All2_Timeout_Scarico_Pz_Da_Hundegger");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All3_Spazio_Scarico_Su_C2_Non_Suff");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All4_Ftc_Scarico_C1_Su_C2");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All5_Timeout_Scarico_Pz_Da_Catenaria_C1");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All6_Timeout_Scarico_Pz_Da_Catenaria_C2");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All7_Errore_Ftc_Scarico_C2_Su_R1");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All10_Presenza_Pezzo_Uscita_C1");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All11_Mancata_Lettura_Ftc_Rotaz");

            this.registerAlarm("RULLI_CENTRO_TAGLI.All20_Timeout_Salita_Rulliera_R1");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All21_Timeout_Discesa_Rulliera_R1");



            this.registerAlarm("RULLI_CENTRO_TAGLI.All30_Sel_Tipo_Rotazione");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All40_Pezzo_Bloccato_su_Rulliera_R1");


            this.registerAlarm("RULLI_CENTRO_TAGLI.All50_Drive_Rotaz_Catenaria_C1");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All51_Drive_Solleva_Catenaria_C1");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All52_Drive_Ribalta_Catenaria_C1");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All53_Drive_Rotazione_Catenaria_C2");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All54_Drive_Rotazione_Rulliera_R1");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All55_Inverter_Salita_R1");

            this.registerAlarm("RULLI_CENTRO_TAGLI.All100_Emergenza");


            this.registerAlarm("RULLI_CENTRO_TAGLI.All60_C1P1_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All61_R1P1_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All61_R1P1_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All62_R1P2_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All63_R1P3_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All64_R1P4_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All65_R1P5_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All66_R1P6_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All67_R3P4_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All68_R3P1_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All69_R3P2_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All70_R3P3_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All71_C4P1_Pls_Eme");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All72_PB_Eme_Macinatore");

            base.init();

        }
    }
}
