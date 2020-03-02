using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalimbertiHMIgl
{
    public class PlcAlarmListAspirazione : PlcAlarmList
    {


        public new void registerAlarm(string var)
        {
            string al = "MAIN." + var.Trim();
            base.registerAlarm(al);
        }

        public new void init()
        {

            this.registerAlarm("Alr_Emergenza                                                                ");
            this.registerAlarm("Alr_Ripari1_Filtro                                                        ");
            this.registerAlarm("Alr_Ripari2_Filtro                                                        ");
            this.registerAlarm("Alr_Ripari_Bricchettatrice                                        ");
            this.registerAlarm("Alr_Rottura_Filtro                                                        ");
            this.registerAlarm("Alr_Interruttore_Alim_Brichettatrice_20QF1        ");
            this.registerAlarm("Alr_Interruttore_Rilevat_Scintille_20QF2        ");
            this.registerAlarm("Alr_SovraTemp_Motore_Ventilatore_Aspiraz_1        ");
            this.registerAlarm("Alr_SovraTemp_Motore_Ventilatore_Aspiraz_2        ");
            this.registerAlarm("Alr_SovraTemp_Motore_Coclea_Estrazione                ");
            this.registerAlarm("Alr_SovraTemp_Motore_Valvola_Stellare_Bag        ");
            this.registerAlarm("Alr_SovraTemp_Motore_Coclea_Bricchettatrice        ");
            this.registerAlarm("Alr_SovraTemp_Motore_Aspirazione_Ciclone        ");
            this.registerAlarm("Alr_SovraTemp_Motore_Valvola_Stell_Ciclone        ");
            this.registerAlarm("Alr_SovraTemp_Motore_Nastro_Evac_Bricchetti        ");
            this.registerAlarm("Alr_Aria_Linea                                                                ");
            this.registerAlarm("Alr_Soglia_1_Temperatura_Filtro                                ");
            this.registerAlarm("Alr_Soglia_2_Temperatura_Filtro                                ");
            this.registerAlarm("Alr_Soglia_U1_Controllo_Polvere                                ");
            this.registerAlarm("Alr_Soglia_U2_Controllo_Polvere                                ");
            this.registerAlarm("Alr_Soglia_U3_Controllo_Polvere                                ");
            this.registerAlarm("Alr_Timeout_Ventilatore1_Serranda_AP                ");
            this.registerAlarm("Alr_Timeout_Ventilatore1_Serranda_CH                ");
            this.registerAlarm("Alr_Timeout_Ventilatore2_Serranda_AP                ");
            this.registerAlarm("Alr_Timeout_Ventilatore2_Serranda_CH                ");
            this.registerAlarm("Alr_Coclea_Estrazione_Bloccata                                ");
            this.registerAlarm("Alr_Coclea_Valvola_Stellare_Bag_Bloccata        ");
            this.registerAlarm("Alr_Coclea_Bricchettatrice_Bloccata                        ");
            this.registerAlarm("Alr_Valvola_Stellare_Ciclone_Bloccata                ");
            this.registerAlarm("Alr_Nastro_Evacuazione_Bricchetti_Bloccato        ");
            this.registerAlarm("Alr_Rottura_Cavo_Rembe_RSK_1                                ");
            this.registerAlarm("Alr_Rottura_Cavo_Rembe_RSK_2                                ");
            this.registerAlarm("Alr_Rottura_Cavo_Rembe_RSK_3                                ");
            this.registerAlarm("Alr_Rottura_Cavo_Rembe_RSK_4                                ");
            this.registerAlarm("Alr_Rottura_Cavo_Rembe_RSK_5                                ");
            this.registerAlarm("Alr_Rottura_Cavo_Rembe_RSK_6                                ");
            this.registerAlarm("Alr_Rottura_Cavo_Rembe_RSK_7                                ");
            this.registerAlarm("Alr_Alimentazione_MVT                                                ");
            this.registerAlarm("Alr_Sovratemp1_H20                                                        ");
            this.registerAlarm("Alr_Sovratemp2_H20                                                        ");
            this.registerAlarm("Alr_Timeout_Deviazione_Lato_Bricchettatrice        ");
            this.registerAlarm("Alr_Timeout_Deviazione_Lato_Container                ");
            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_1_Av                        ");
            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_1_Ind                ");
            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_2_Av                        ");
            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_2_Ind                ");
            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_3_Av                        ");
            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_3_Ind                ");
            this.registerAlarm("Alr_Bricchettatrice_Full                                        ");
            this.registerAlarm("Alr_Container_Full                                                        ");

            for (int i=1; i <=42; i++)
            {
                this.registerAlarm("Alr_Ev"+i+"_Pulizia_Filtri_Lavoro                                ");
            }

            this.registerAlarm("Alr_Inverter_Ventilatore_Aspirazione1                ");
            this.registerAlarm("Alr_Inverter_Ventilatore_Aspirazione2                ");
            this.registerAlarm("Alr_Inverter_Coclea_Estrazione                                ");
            this.registerAlarm("Alr_Inverter_Valvola_Stellare_Bag                        ");
            this.registerAlarm("Alr_Inverter_Coclea_Bricchettatrice                        ");
            this.registerAlarm("Alr_Inverter_Aspirazione                                        ");
            this.registerAlarm("Alr_Inverter_Evacuazione_Bricchetti                        ");
            this.registerAlarm("Alr_Inverter_Valvola_Ciclone                                ");
            this.registerAlarm("Alr_Regolazione_Pid_Aspirazione               ");


            base.init();

        }
    }
}
