using DatabaseInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalimbertiHMIgl
{
    public class PlcAlarmListRulliera2 : PlcAlarmList
    {

      



        public new void init()
        {

            this.registerAlarm("RULLI_CENTRO_TAGLI.All110_Presenza_Trave_Su_R2");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All111_Pezzo_Bloccato_Su_Rulliera_R2");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All113_Spazio_Scarico_Su_C5_Non_Suff");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All115_Presenza_Pezzo_Uscita_C5");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All120_Timeout_Salita_Rulliera_R2");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All121_Timeout_Discesa_Rulliera_R2");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All122_Timeout_Salita_Rulliera_R3");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All123_Timeout_Discesa_Rulliera_R3");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All124_Timeout_Scarico_Pz_Da_Rulliera_R2");
            this.registerAlarm("RULLI_CENTRO_TAGLI.Al1125_Timeout_Scarico_Pz_Da_Catenaria_C5");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All155_Drive_Rotaz_Catenaria_C3");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All156_Drive_Rotaz_Catenaria_C5");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All157_Drive_Rotaz_Catenaria_C6");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All158_Drive_Rotaz_Catenaria_R3");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All159_Drive_Rotaz_Catenaria_R2");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All1160_Inverter_Salita_R2");
            this.registerAlarm("RULLI_CENTRO_TAGLI.All1161_Inverter_Salita_R3");


            base.init();

        }
    }
}
