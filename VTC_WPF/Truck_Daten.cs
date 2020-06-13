using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VTCManager
{
    public class Truck_Daten : INotifyPropertyChanged
    {
        private string hersteller;
        private string modell;
        private string name;
        private string telemetryVersion;
        private string vorwaertsGaenge;
        private string rueckwaertsGange;
        private string gang;
        private string rpm_max;
        private string fuel_max;
        private string fuel_bestand;
        private string fuel_verbrauch;
        private string rpm;
        private string kmh;
        private string mph;
        private string elektrik_Status;
        private string adblue_max;
        private string adblue_bestand;
        private string tempomat;
        // TRAILER VALUES
        private string angehangen;
        
        private string rad_schaden;
        private string chassis_schaden;

        // JOB DATEN
        private string fracht_geladen;
        private string spezial_job;
        private string market;
        private string start_ort;
        private string ziel_ort;
        private string start_firma;
        private string ziel_firma;
        private string einkommen;
        private string geplante_distanz;
        private string fracht_gewicht;
        private string fracht_name;
        private string fracht_schaden;

        // NAVIGATIONS DATEN
        private string navigation_distanz;
        private string navigation_zeit;
        private string navigation_speed_limit;

        // MAUTSTATION
        private string maut_bezahlt;

        // TANKEN
        private string tanken_bezahlt;


        public string MAUT_BEZAHLT
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
        public string TANKEN_BEZAHLT
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
        public string NAVIGATION_DISTANZ
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


        public string NAVIGATION_ZEIT
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
        public string NAVIGATION_SPEED_LIMIT
        {
            get { return navigation_speed_limit; }
            set
            {
                if (navigation_speed_limit != value)
                {
                    navigation_speed_limit = value;
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
        public string FRACHT_GEWICHT
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

        public string GEPLANTE_DISTANZ
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


        public string JOB_EINKOMMEN
        {
            get { return einkommen; }
            set
            {
                if (einkommen != value)
                {
                    einkommen = value;
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

        public string SPEZIAL_JOB
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
        public string FRACHT_GELADEN
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

        public string CHASSIS_SCHADEN
        {
            get { return chassis_schaden; }
            set
            {
                if (chassis_schaden != value)
                {
                    chassis_schaden = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public string RAD_SCHADEN
        {
            get { return rad_schaden; }
            set
            {
                if (rad_schaden != value)
                {
                    rad_schaden = value;
                    NotifyPropertyChanged();
                }
            }

        }
        public string FRACHT_SCHADEN
        {
            get { return fracht_schaden; }
            set
            {
                if (fracht_schaden != value)
                {
                    fracht_schaden = value;
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
        public string TEMPOMAT
        {
            get { return tempomat; }
            set
            {
                if (tempomat != value)
                {
                    tempomat = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string GANG
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
        public string FUEL_VERBRAUCH
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

        public string FUEL_BESTAND
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
        public string NAME
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
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
        public string FUEL_MAX
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

        public string RPM_MAX
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

        public string RueckwaertsGaenge
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

        public string VorwaertsGaenge
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
        public string RPM
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
        public string KMH
        {
            get { return kmh; }
            set
            {
                if (kmh != value)
                {
                    kmh = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string MPH
        {
            get { return mph; }
            set
            {
                if (mph != value)
                {
                    mph = value;
                    NotifyPropertyChanged();
                }
            }

        }

        public string Elektrik_Status
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
