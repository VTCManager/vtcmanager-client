using System;

namespace VTCManager.Klassen
{
    public class Translation
    {
        public string version,
            login_username,
            login,
            password,
            login_failed,
            waiting_for_ets_ats,
            waehrung, 
            speed_lbl,
            TRUCK_NAME,
            TRUCK_MODELL,
            TAB_Fahrt_Text,
            TAB_FREUNDE_TEXT,
            TAB_VERKEHR_TEXT,
            TAB_EINSTELLUNGEN_TEXT,
            SCHADENSANZEIGE_TITEL,
            TAB_FAHRT_LBL_MOTOR,
            TAB_FAHRT_LBL_GETRIEBE,
            TAB_FAHRT_LBL_FAHRWERK,
            TAB_FAHRT_LBL_FAHRERHAUS,
            TAB_FAHRT_LBL_RAEDER,
            TAB_FAHRT_LBL_FRACHTSCHADEN,
            BUTTON_ETS_STARTEN,
            BUTTON_ATS_STARTEN,
            BUTTON_TMP_STARTEN;
        public Translation(String lang)
        {
            version = "Version: 3.0";
            login = lang == "DE" ? "Login" : "Login";
            password = lang == "DE" ? "Passwort" : "Password";
            login_failed = lang == "DE" ? "Login fehlgeschlagen !" : "Login Failed !";
            waehrung = lang == "DE" ? " €" : " $";
            speed_lbl = lang == "DE" ? "KM/H" : "mp/h";
            TAB_Fahrt_Text = lang == "DE" ? "Fahrt" : "Drive";
            TAB_FREUNDE_TEXT = lang == "DE" ? "Freunde" : "Friends";
            TAB_VERKEHR_TEXT = lang == "DE" ? "Verkehr" : "Traffic";
            TAB_EINSTELLUNGEN_TEXT = lang == "DE" ? "Einstellungen" : "Settings";
            waiting_for_ets_ats = lang == "DE" ? "Warte auf ETS2 / ATS" : "Waiting of ETS2 / ATS";
            TRUCK_NAME = lang == "DE" ? "Hersteller: " : "Manufacturer: ";
            TRUCK_MODELL = lang == "DE" ? "Truck-Modell: " : "Truck-Model: ";
            SCHADENSANZEIGE_TITEL = lang == "DE" ? "Schadensanzeige:" : "Damage-Report:";
            TAB_FAHRT_LBL_MOTOR = lang == "DE" ? "Motor-Schaden:" : "Engine-Damage:";
            TAB_FAHRT_LBL_GETRIEBE = lang == "DE" ? "Getiebe-Schaden:" : "Gearbox-Damage:";
            TAB_FAHRT_LBL_FAHRWERK = lang == "DE" ? "Fahrwerk-Schaden:" : "Chassis-Damage:";
            TAB_FAHRT_LBL_FAHRERHAUS = lang == "DE" ? "Fahrerhaus-Schaden:" : "Cabin-Damage:";
            TAB_FAHRT_LBL_RAEDER = lang == "DE" ? "Räder-Schaden:" : "Wheel-Damage:";
            TAB_FAHRT_LBL_FRACHTSCHADEN = lang == "DE" ? "Frachtschaden:" : "Freight-Damage:";
            BUTTON_ATS_STARTEN = lang == "DE" ? "ATS Starten" : "Start ATS";
            BUTTON_ETS_STARTEN = lang == "DE" ? "ETS2 Starten" : "Start ETS2";
            BUTTON_TMP_STARTEN = lang == "DE" ? "Truckers-MP" : "Truckers-MP";

            
        }
    }
}
