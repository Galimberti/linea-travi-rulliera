using DatabaseInterface;
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

            this.registerAlarm("Alr_Emergenza");

            this.registerAlarm("Alr_Ripari1_Filtro");

            this.registerAlarm("Alr_Ripari2_Filtro");

            this.registerAlarm("Alr_Ripari_Bricchettatrice");

            this.registerAlarm("Alr_Rottura_Filtro");

            this.registerAlarm("Alr_Interruttore_Alim_Brichettatrice_20QF1");

            this.registerAlarm("Alr_Interruttore_Rilevat_Scintille_20QF2");

            this.registerAlarm("Alr_SovraTemp_Motore_Ventilatore_Aspiraz_1");

            this.registerAlarm("Alr_SovraTemp_Motore_Ventilatore_Aspiraz_2");

            this.registerAlarm("Alr_SovraTemp_Motore_Coclea_Estrazione");

            this.registerAlarm("Alr_SovraTemp_Motore_Valvola_Stellare_Bag");

            this.registerAlarm("Alr_SovraTemp_Motore_Coclea_Bricchettatrice");

            this.registerAlarm("Alr_SovraTemp_Motore_Aspirazione_Ciclone");

            this.registerAlarm("Alr_SovraTemp_Motore_Valvola_Stell_Ciclone");

            this.registerAlarm("Alr_SovraTemp_Motore_Nastro_Evac_Bricchetti");

            this.registerAlarm("Alr_Aria_Linea");

            this.registerAlarm("Alr_Soglia_1_Temperatura_Filtro");

            this.registerAlarm("Alr_Soglia_2_Temperatura_Filtro");

            this.registerAlarm("Alr_Soglia_U1_Controllo_Polvere");

            this.registerAlarm("Alr_Soglia_U2_Controllo_Polvere");

            this.registerAlarm("Alr_Timeout_Ventilatore1_Serranda_AP");

            this.registerAlarm("Alr_Timeout_Ventilatore1_Serranda_CH");

            this.registerAlarm("Alr_Timeout_Ventilatore2_Serranda_AP");

            this.registerAlarm("Alr_Timeout_Ventilatore2_Serranda_CH");

            this.registerAlarm("Alr_Coclea_Estrazione_Bloccata");

            this.registerAlarm("Alr_Coclea_Valvola_Stellare_Bag_Bloccata");

            this.registerAlarm("Alr_Coclea_Bricchettatrice_Bloccata");

            this.registerAlarm("Alr_Valvola_Stellare_Ciclone_Bloccata");

            this.registerAlarm("Alr_Nastro_Evacuazione_Bricchetti_Bloccato");

            this.registerAlarm("Alr_Alimentazione_MVT");

            this.registerAlarm("Alr_Sovratemp1_H20");

            this.registerAlarm("Alr_Sovratemp2_H20");

            this.registerAlarm("Alr_Timeout_Deviazione_Lato_Bricchettatrice");

            this.registerAlarm("Alr_Timeout_Deviazione_Lato_Container");

            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_1_Av");

            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_1_Ind");

            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_2_Av");

            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_2_Ind");

            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_3_Av");

            this.registerAlarm("Alr_Timeout_Scarico_Bricchetti_3_Ind");

            this.registerAlarm("Alr_Bricchettatrice_Full");

            this.registerAlarm("Alr_Container_Full");

            this.registerAlarm("Alr_Ev1_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev2_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev3_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev4_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev5_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev6_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev7_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev8_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev9_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev10_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev11_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev12_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev13_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev14_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev15_Pulizia_Filtri_Lavor");

            this.registerAlarm("Alr_Ev16_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev17_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev18_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev19_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev20_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev21_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev22_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev23_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev24_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev25_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev26_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev27_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev28_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev29_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev30_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev31_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev32_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev33_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev34_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev35_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev36_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev37_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev38_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev39_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev40_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev41_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Ev42_Pulizia_Filtri_Lavoro");

            this.registerAlarm("Alr_Compressore_Spento");

            this.registerAlarm("Alr_Ev_Pulizia_Filtri_Bloccata_Aperta");

            this.registerAlarm("Alr_Inverter_Ventilatore_Aspirazione1");

            this.registerAlarm("Alr_Inverter_Ventilatore_Aspirazione2");

            this.registerAlarm("Alr_Inverter_Coclea_Estrazione");

            this.registerAlarm("Alr_Inverter_Valvola_Stellare_Bag");

            this.registerAlarm("Alr_Inverter_Coclea_Bricchettatrice");

            this.registerAlarm("Alr_Inverter_Aspirazione");

            this.registerAlarm("Alr_Inverter_Evacuazione_Bricchetti");

            this.registerAlarm("Alr_Inverter_Valvola_Ciclone");

            this.registerAlarm("Alr_Regolazione_Pid_Aspirazione");

            this.registerAlarm("Alr_Tubo_Ciclone_Intasato");

            this.registerAlarm("Alr_Serranda_Asp_Ciclone");

            this.registerAlarm("Alr_Stop_Ciclo_Da_Pulsante");

            this.registerAlarm("Alr_TMax_Coclea_Silos");

            this.registerAlarm("Alr_TMax_Coclea_Bricchettatrice");

            this.registerAlarm("Alr_Livello_Max_Container_Silos");

            this.registerAlarm("Alr_Livello_Max_Container_Bricchettatrice");

            this.registerAlarm("Alr_Timeout_Serranda_Scorniciatrice_AP");

            this.registerAlarm("Alr_Timeout_Serranda_Scorniciatrice_CH");

            this.registerAlarm("Alr_Timeout_Serranda_Hundegger_AP");

            this.registerAlarm("Alr_Timeout_Serranda_Hundegger_CH");

            this.registerAlarm("Alr_Timeout_Serranda_Multilame_AP");

            this.registerAlarm("Alr_Timeout_Serranda_Multilame_CH");

            this.registerAlarm("Alr_Feedback_Contattore_55KM1");

            this.registerAlarm("Alr_Feedback_Contattore_55KM2");

            this.registerAlarm("Alr_Feedback_Contattore_55KM3");

            this.registerAlarm("Alr_Feedback_Contattore_55KM4");

            this.registerAlarm("Alr_Feedback_Contattore_56KM1");

            this.registerAlarm("Alr_Fedback_Contattore_56KM2");

            this.registerAlarm("Alr_Fedback_Contattore_56KM3");

            this.registerAlarm("Alr_Feedback_Contattore_56KM4");

            this.registerAlarm("Alr_Feedback_Contattore_57KM1");

            /*
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
            this.registerAlarm("Alr_Tubo_Ciclone_Intasato");
            this.registerAlarm("Alr_Compressore_Spento");
            this.registerAlarm("Alr_Ev_Pulizia_Filtri_Bloccata_Aperta");

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
            */

            base.init();

        }
    }
}
