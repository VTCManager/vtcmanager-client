using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VTCManager
{
    public class Truck_Daten : INotifyPropertyChanged
    {
        // Algemeines
        private string telemetryVersion;
        private uint dll_version;
        private string spiel;
        private bool is_game_running;
        private bool sdk_activ;
        private string anzeige_liter_gallonen;
        private string anzeige_km_miles;
        private string anzeige_to_lbs;
        private string txt_fahrt;


        // Truck
        private string hersteller;
        private string hersteller_id;
        private string modell;

        private int vorwaertsGaenge;
        private int rueckwaertsGange;
        private int gang;
        private double fuel_max;
        private double fuel_bestand;
        private int fuel_rest;
        private double fuel_verbrauch;
        private double rpm;
        private double rpm_max;

        private bool elektrik_Status;
        private bool abblendlicht;
        private bool normmallicht;
        private bool parking_brake;
        private bool brake_visibility;
        private string adblue_max;
        private string adblue_bestand;
        private double tempomat_kmh;
        private double tempomat_mph;
        private bool blinker_links;
        private bool blinker_rechts;
        private int speed_kmh;
        private double speed_tacho_kmh;
        private int speed_mph;
        private double speed_tacho_mph;


        // Truck Schaden
        private double truck_motor_schaden;
        private double truck_getriebe_schaden;
        private double truck_fahrwerk_schaden;
        private double truck_chassis_schaden;
        private double truck_raeder_schaden;



    // TRAILER VALUES
    private string angehangen;

    // Trailer Schaden
        private double trailer_fracht_schaden;
        private double trailer_fahrwerk_schaden;
        private double trailer_chassis_schaden;


    // JOB DATEN
        private bool fracht_geladen;
        private bool spezial_job;
        private string market;
        private string start_ort;
        private string ziel_ort;
        private string start_firma;
        private string ziel_firma;
        private double job_einkommen;
        private int geplante_distanz;
        private double fracht_gewicht;
        private int fracht_gewicht_tonnen;
        private string fracht_name;

    // JOB ABGABE

        private string abgabe_job_beendet;
        private bool abgabe_autoparking;
        private double abgabe_frachtschaden;
        private string abgabe_abgabezeit;
        private int abgabe_distanz_km;
        private int abgabe_xp;
        private double abgabe_einnahmen;

    // NAVIGATIONS DATEN
        private double navigation_distanz;
        private double navigation_zeit;
        private int navigation_speed_limit_kmh;
        private int navigation_speed_limit_mph;
        private string anzeige_speed_limit;
        private int rest_strecke;


    // MAUTSTATION
        private int maut_bezahlt;

    // TANKEN
        private int tanken_bezahlt;

    // STRAFEN
        private int strafe_bezahlt;

    // FÄHREN
        private int faehre_bezahlt;
        private string faehre_abfahrt_von;
        private string faehre_ankunft_in;





        // BEGIN INOTIFYPROPERTYCHANGED DONT CHANGE ANYTHING !

        public uint DLL_VERSION
        {
            get { return dll_version; }
            set
            {
                if (dll_version != value)
                {
                    dll_version = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ANZEIGE_SPEED_LIMIT
        {
            get { return anzeige_speed_limit; }
            set
            {
                if (anzeige_speed_limit != value)
                {
                    anzeige_speed_limit = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string HERSTELLER_ID
        {
            get { return hersteller_id; }
            set
            {
                if (hersteller_id != value)
                {
                    hersteller_id = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int FRACHT_GEWICHT_TONNEN
        {
            get { return fracht_gewicht_tonnen; }
            set
            {
                if (fracht_gewicht_tonnen != value)
                {
                    fracht_gewicht_tonnen = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool SDK_ACTIVE
        {
            get { return sdk_activ; }
            set
            {
                if (sdk_activ != value)
                {
                    sdk_activ = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string ANZEIGE_TO_LBS
        {
            get { return anzeige_to_lbs; }
            set
            {
                if (anzeige_to_lbs != value)
                {
                    anzeige_to_lbs = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string SPIEL
        {
            get { return spiel; }
            set
            {
                if (spiel != value)
                {
                    spiel = value;
                    NotifyPropertyChanged();
                }
            }
        }



        public string TXT_FAHRT
        {
            get { return txt_fahrt; }
            set
            {
                if (txt_fahrt != value)
                {
                    txt_fahrt = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ANZEIGE_KM_MILES
        {
            get { return anzeige_km_miles; }
            set
            {
                if (anzeige_km_miles != value)
                {
                    anzeige_km_miles = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public int FUEL_REST
        {
            get { return fuel_rest; }
            set
            {
                if (fuel_rest != value)
                {
                    fuel_rest = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool BRAKE_VISIBILITY
        {
            get { return brake_visibility; }
            set
            {
                if (brake_visibility != value)
                {
                    brake_visibility = value;
                    NotifyPropertyChanged();
                }

            }
        }

        public bool PARKING_BRAKE
        {
            get { return parking_brake; }
            set
            {
                if (parking_brake != value)
                {
                    parking_brake = value;
                    NotifyPropertyChanged();
                }

            }
        }
        public bool ABBLENDLICHT
        {
            get { return abblendlicht; }
            set
            {
                if(abblendlicht != value)
                {
                    abblendlicht = value;
                    NotifyPropertyChanged();
                }

            }
        }

        public bool NORMMALLICHT
        {
            get { return normmallicht; }
            set
            {
                if (normmallicht != value)
                {
                    normmallicht = value;
                    NotifyPropertyChanged();
                }

            }
        }

        private bool _is_job_data_visible;
        public bool IS_JOB_DATA_VISIBLE
        {
            get { return _is_job_data_visible; }
            set
            {
                _is_job_data_visible = value;
                NotifyPropertyChanged();
            }
        }
        public string ABGABE_JOB_BEENDET
        {
            get { return abgabe_job_beendet; }
            set
            {
                if (abgabe_job_beendet != value)
                {
                    abgabe_job_beendet = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool ABGABE_AUTOPARKING
        {
            get { return abgabe_autoparking; }
            set
            {
                if (abgabe_autoparking != value)
                {
                    abgabe_autoparking = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public int ABGABE_DISTANZ_KM
        {
            get { return abgabe_distanz_km; }
            set
            {
                if (abgabe_distanz_km != value)
                {
                    abgabe_distanz_km = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public int REST_STRECKE
        {
            get { return rest_strecke; }
            set
            {
                if (rest_strecke != value)
                {
                    rest_strecke = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public int ABGABE_XP
        {
            get { return abgabe_xp; }
            set
            {
                if (abgabe_xp != value)
                {
                    abgabe_xp = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string ABGABE_ABGABEZEIT
        {
            get { return abgabe_abgabezeit; }
            set
            {
                if (abgabe_abgabezeit != value)
                {
                    abgabe_abgabezeit = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public double ABGABE_FRACHTSCHADEN
        {
            get { return abgabe_frachtschaden; }
            set
            {
                if (abgabe_frachtschaden != value)
                {
                    abgabe_frachtschaden = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double ABGABE_EINNAHMEN
        {
            get { return abgabe_einnahmen; }
            set
            {
                if (abgabe_einnahmen != value)
                {
                    abgabe_einnahmen = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public int FAEHRE_BEZAHLT
        {
            get { return faehre_bezahlt; }
            set
            {
                if (faehre_bezahlt != value)
                {
                    faehre_bezahlt = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public string FAEHRE_ABFAHRT_VON
        {
            get { return faehre_abfahrt_von; }
            set
            {
                if (faehre_abfahrt_von != value)
                {
                    faehre_abfahrt_von = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string FAEHRE_ANKUNFT_IN
        {
            get { return faehre_ankunft_in; }
            set
            {
                if (faehre_ankunft_in != value)
                {
                    faehre_ankunft_in = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public int STRAFE_BEZAHLT
        {
            get { return strafe_bezahlt; }
            set
            {
                if (strafe_bezahlt != value)
                {
                    strafe_bezahlt = value;
                    NotifyPropertyChanged();
                }
            }

        }

        // SCHADEN TRUCK ANFANG
        public double TRUCK_MOTOR_SCHADEN
        {
            get { return truck_motor_schaden; }
            set
            {
                if (truck_motor_schaden != value)
                {
                    truck_motor_schaden = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double TRUCK_GETRIEBE_SCHADEN
        {
            get { return truck_getriebe_schaden; }
            set
            {
                if (truck_getriebe_schaden != value)
                {
                    truck_getriebe_schaden = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double TRUCK_CHASSIS_SCHADEN
        {
            get { return truck_chassis_schaden; }
            set
            {
                if (truck_chassis_schaden != value)
                {
                    truck_chassis_schaden = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public double TRUCK_FAHRWERK_SCHADEN
        {
            get { return truck_fahrwerk_schaden; }
            set
            {
                if (truck_fahrwerk_schaden != value)
                {
                    truck_fahrwerk_schaden = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double TRUCK_RAEDER_SCHADEN
        {
            get { return truck_raeder_schaden; }
            set
            {
                if (truck_raeder_schaden != value)
                {
                    truck_raeder_schaden = value;
                    NotifyPropertyChanged();
                }
            }

        }

        // ENDE SCHADEN TRUCK


        // ANFANG TRAILER SCHADEN

        public double TRAILER_FRACHT_SCHADEN
        {
            get { return trailer_fracht_schaden; }
            set
            {
                if (trailer_fracht_schaden != value)
                {
                    trailer_fracht_schaden = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public double TRAILER_FAHRWERK_SCHADEN
        {
            get { return trailer_fahrwerk_schaden; }
            set
            {
                if (trailer_fahrwerk_schaden != value)
                {
                    trailer_fahrwerk_schaden = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double TRAILER_CHASSIS_SCHADEN
        {
            get { return trailer_chassis_schaden; }
            set
            {
                if (trailer_chassis_schaden != value)
                {
                    trailer_chassis_schaden = value;
                    NotifyPropertyChanged();
                }
            }

        }


        // ENDE TRAILER SCHADEN

        public string ANZEIGE_LITER_GALLONEN
        {
            get { return anzeige_liter_gallonen; }
            set
            {
                if (anzeige_liter_gallonen != value)
                {
                    anzeige_liter_gallonen = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public int SPEED_KMH
        {
            get { return speed_kmh; }
            set
            {
                if (speed_kmh != value)
                {
                    speed_kmh = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public double SPEED_TACHO_KMH
        {
            get { return speed_tacho_kmh; }
            set
            {
                if (speed_tacho_kmh != value)
                {
                    speed_tacho_kmh = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public int SPEED_MPH
        {
            get { return speed_mph; }
            set
            {
                if (speed_mph != value)
                {
                    speed_mph = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public double SPEED_TACHO_MPH
        {
            get { return speed_tacho_mph; }
            set
            {
                if (speed_tacho_mph != value)
                {
                    speed_tacho_mph = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public bool BLINKER_RECHTS
        {
            get { return blinker_rechts; }
            set
            {
                if (blinker_rechts != value)
                {
                    blinker_rechts = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public bool BLINKER_LINKS
        {
            get { return blinker_links; }
            set
            {
                if (blinker_links != value)
                {
                    blinker_links = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public bool IS_GAME_RUNNING
        {
            get { return is_game_running; }
            set
            {
                if (is_game_running != value)
                {
                    is_game_running = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public int MAUT_BEZAHLT
        {
            get { return maut_bezahlt; }
            set
            {
                if (maut_bezahlt != value)
                {
                    maut_bezahlt = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public int TANKEN_BEZAHLT
        {
            get { return tanken_bezahlt; }
            set
            {
                if (tanken_bezahlt != value)
                {
                    tanken_bezahlt = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double NAVIGATION_DISTANZ
        {
            get { return navigation_distanz; }
            set
            {
                if (navigation_distanz != value)
                {
                    navigation_distanz = value;
                    NotifyPropertyChanged();
                }
            }

        }


        public double NAVIGATION_ZEIT
        {
            get { return navigation_zeit; }
            set
            {
                if (navigation_zeit != value)
                {
                    navigation_zeit = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public int NAVIGATION_SPEED_LIMIT_KMH
        {
            get { return navigation_speed_limit_kmh; }
            set
            {
                if (navigation_speed_limit_kmh != value)
                {
                    navigation_speed_limit_kmh = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public int NAVIGATION_SPEED_LIMIT_MPH
        {
            get { return navigation_speed_limit_mph; }
            set
            {
                if (navigation_speed_limit_mph != value)
                {
                    navigation_speed_limit_mph = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string FRACHT_NAME
        {
            get { return fracht_name; }
            set
            {
                if (fracht_name != value)
                {
                    fracht_name = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double FRACHT_GEWICHT
        {
            get { return fracht_gewicht; }
            set
            {
                if (fracht_gewicht != value)
                {
                    fracht_gewicht = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public int GEPLANTE_DISTANZ
        {
            get { return geplante_distanz; }
            set
            {
                if (geplante_distanz != value)
                {
                    geplante_distanz = value;
                    NotifyPropertyChanged();
                }
            }

        }


        public double JOB_EINKOMMEN
        {
            get { return job_einkommen; }
            set
            {
                if (job_einkommen != value)
                {
                    job_einkommen = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string ZIEL_FIRMA
        {
            get { return ziel_firma; }
            set
            {
                if (ziel_firma != value)
                {
                    ziel_firma = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public string START_FIRMA
        {
            get { return start_firma; }
            set
            {
                if (start_firma != value)
                {
                    start_firma = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public string ZIEL_ORT
        {
            get { return ziel_ort; }
            set
            {
                if (ziel_ort != value)
                {
                    ziel_ort = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public string START_ORT
        {
            get { return start_ort; }
            set
            {
                if (start_ort != value)
                {
                    start_ort = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string MARKET
        {
            get { return market; }
            set
            {
                if (market != value)
                {
                    market = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public bool SPEZIAL_JOB
        {
            get { return spezial_job; }
            set
            {
                if (spezial_job != value)
                {
                    spezial_job = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public bool FRACHT_GELADEN
        {
            get { return fracht_geladen; }
            set
            {
                if (fracht_geladen != value)
                {
                    fracht_geladen = value;
                    NotifyPropertyChanged();
                }
            }

        }

     
        public string ANGEHANGEN
        {
            get { return angehangen; }
            set
            {
                if (angehangen != value)
                {
                    angehangen = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double TEMPOMAT_KMH
        {
            get { return tempomat_kmh; }
            set
            {
                if (tempomat_kmh != value)
                {
                    tempomat_kmh = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public double TEMPOMAT_MPH
        {
            get { return tempomat_mph; }
            set
            {
                if (tempomat_mph != value)
                {
                    tempomat_mph = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public int GANG
        {
            get { return gang; }
            set
            {
                if (gang != value)
                {
                    gang = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double FUEL_VERBRAUCH
        {
            get { return fuel_verbrauch; }
            set
            {
                if (fuel_verbrauch != value)
                {
                    fuel_verbrauch = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public string ADBLUE_BESTAND
        {
            get { return adblue_bestand; }
            set
            {
                if (adblue_bestand != value)
                {
                    adblue_bestand = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public double FUEL_BESTAND
        {
            get { return fuel_bestand; }
            set
            {
                if (fuel_bestand != value)
                {
                    fuel_bestand = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string MODELL
        {
            get { return modell; }
            set
            {
                if (modell != value)
                {
                    modell = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public string HERSTELLER
        {
            get { return hersteller; }
            set
            {
                if (hersteller != value)
                {
                    hersteller = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public string ADBLUE_MAX
        {
            get { return adblue_max; }
            set
            {
                if (adblue_max != value)
                {
                    adblue_max = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double FUEL_MAX
        {
            get { return fuel_max; }
            set
            {
                if (fuel_max != value)
                {
                    fuel_max = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public double RPM_MAX
        {
            get { return rpm_max; }
            set
            {
                if (rpm_max != value)
                {
                    rpm_max = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public int RUECKWAERTS_GAENGE
        {
            get { return rueckwaertsGange; }
            set
            {
                if (rueckwaertsGange != value)
                {
                    rueckwaertsGange = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public int VorwaertsGaenge
        {
            get { return vorwaertsGaenge; }
            set
            {
                if (vorwaertsGaenge != value)
                {
                    vorwaertsGaenge = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string TelemetryVersion
        {
            get { return telemetryVersion; }
            set
            {
                if (telemetryVersion != value)
                {
                    telemetryVersion = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public double RPM
        {
            get { return rpm; }
            set
            {
                if (rpm != value)
                {
                    rpm = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public bool ELEKTRIK_STATUS
        {
            get { return elektrik_Status; }
            set
            {
                if (elektrik_Status != value)
                {
                    elektrik_Status = value;
                    NotifyPropertyChanged();
                }
            }

        }





        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
