﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VTC_WPF.Klassen
{
    public class Translation
    {
        public string login_username;
        public string version;
        public string waiting_for_ets;
        public string logout;
        public string login;
        public string ready;
        public string password;
        public string login_failed;
        public string update_part1;
        public string update_part2;
        public string update_avail_window;
        public string car_lb;
        public string truck_lb;
        public string wait_ets2_is_ready;
        public string freight_lb;
        public string gewicht;
        public string depature_lb;
        public string destination_lb;
        public string no_cargo_lb;
        public string waehrung;
        public string settings_lb;
        public string exit_lb;
        public string topmenuaccount_lb;
        public string not_avail;
        public string error_window;
        public string update_message;
        public string update_caption;
        public string traffic_main_lb;
        public string statistic_panel_topic;
        public string driven_tours_lb;
        public string act_bank_balance;
        public string user_company_lb;
        public string no_company_text;
        public string jb_canc_lb;
        public string save_info;
        public string discord_rpc_tra_p1;
        public string discord_rpc_tra_p2;
        public string progress;
        public string speed_setup_box;
        public string settings_window;
        public string speeding;
        public string loading_text;
        public string settings_window_save_button;
        public string settings_window_groupBox1text;
        public string btn_TruckersMP_suchentext;
        public string settings_window_label3text;
        public string settings_window_titel_text;
        public string settings_window_tmp_error_text;
        public string settings_window_tmp_error_text2;
        public string settings_window_tmp_error_text3;
        public string settings_window_dir_error;
        public string settings_log_failed;
        public string log_ok;
        public string error;
        public string warning;
        public string error_sound_load;
        public string error_sound_missing_file;
        public string Frachtmarkt_no_profiles;
        public string Frachtmarkt_from_City;
        public string Frachtmarkt_from_Company;
        public string Frachtmarkt_to_City;
        public string Frachtmarkt_to_Company;
        public string error_sound_play;
        public string Settings_CheckBox_NUMPAD_ONOFF;
        public string rest_text;
        public string rest_time_days;
        public string rest_time_hours;
        public string rest_time_minutes;
        public string verspaetet;
        public string verspaetet2;
        public Translation(String language)
        {
            version = "Version: 1.1.0";
            if (language == "Deutsch (Deutschland)")
            {
      
                waehrung = " €";
                speeding = " KM/H";
                waiting_for_ets = "Warte auf das Spiel...";
                logout = "Abmelden";
                login = "Anmelden";
                ready = "Bereit";
                password = "Passwort";
                login_username = "Benutzername";
                login_failed = "Benutzername oder Passwort falsch!";
                update_part1 = "Es ist ein neues Update(Version ";
                update_part2 = ") für VTCManager verfügbar! Bitte aktualisiere VTCManager.";
                update_avail_window = "Update verfügbar";
                car_lb = "Auto: ";
                truck_lb = "LKW: ";
                wait_ets2_is_ready = "Initialisierung...";
                freight_lb = "Fracht: ";
                gewicht = " t ";
                depature_lb = "Startort: ";
                destination_lb = "Zielort: ";
                no_cargo_lb = "Freifahrt";
                settings_lb = "Einstellungen";
                exit_lb = "Beenden";
                topmenuaccount_lb = "Account";
                not_avail = "Demnächst verfügbar";
                error_window = "Fehler";
                update_message = "";
                update_caption = "Änderungen in Version 1.1.0\n";
                traffic_main_lb = "Verkehr";
                statistic_panel_topic = "Statistik ";
                driven_tours_lb = "gefahrene Touren: ";
                act_bank_balance = "aktueller Kontostand: ";
                user_company_lb = "angestellt bei: ";
                no_company_text = "Selbstständig";
                jb_canc_lb = "Dein letzter Auftrag wurde abgebrochen!";
                save_info = "Starte VTCManager neu, damit die Änderungen wirksam werden!";
                discord_rpc_tra_p1 = "Aktuelle Tour von ";
                discord_rpc_tra_p2 = " nach ";
                progress = "Fortschritt: ";
                speed_setup_box = "Geschwindigkeit in mph?";
                settings_window = "Einstellungen";
                loading_text = "Lade...";
                settings_window_save_button = "Speichern...";
                settings_window_groupBox1text = "Server Einstellungen";
                btn_TruckersMP_suchentext = "TruckersMP Einstellungen";
                settings_window_label3text = "TruckersMP Pfad";
                settings_window_titel_text = "Einstellungen";
                settings_window_tmp_error_text = "Es fehlt der Pfad zu TruckersMP";
                settings_window_tmp_error_text2 = "Bitte Korrigiere die Angabe dem folgenden Fenster!";
                settings_window_tmp_error_text3 = "Fehler bei TruckersMP Pfad";
                settings_window_dir_error = "Das geforderte Verzeichnis wurde nicht gefunden !";
                settings_log_failed = "Die LOG Datei konnte nicht erstellt / Aktualisiert werden !";
                log_ok = "Log Datei wurde erstellt !";
                error = "Fehler";
                warning = "Warnung";
                error_sound_load = "Das Sound-System konnte nicht initialisiert werden";
                error_sound_missing_file = "Das Sound-System konnte initialisiert werden, aber einige Töne sind nicht verfügbar.";
                Frachtmarkt_no_profiles = "Keine Profile gefunden!";
                Frachtmarkt_from_City = "Start-Ort: ";
                Frachtmarkt_from_Company = "Start-Firma: ";
                Frachtmarkt_to_City = "Ziel-Ort: ";
                Frachtmarkt_to_Company = "Ziel-Firma: ";
                error_sound_play = "Ein kritischer Fehler ist bei der Wiedergabe eines Tones aufgetreten.";
                Settings_CheckBox_NUMPAD_ONOFF = "NUM-Pad Button anzeigen";
                rest_text = "Liefertermin: ";
                rest_time_days = " Tage, ";
                rest_time_hours = " Stunden, ";
                rest_time_minutes = " Minuten";
                verspaetet = "Du bist zu Spät!!! ";
            }
            else
            {
    
                waehrung = " $";
                speeding = " mp/h";
                waiting_for_ets = "Waiting for the Game...";
                logout = "Logout";
                login = "Login";
                ready = "Ready";
                login_username = "Username";
                password = "Password";
                login_failed = "Username or password is wrong!";
                update_part1 = "An new update(version ";
                update_part2 = ") is available! Please update VTCManager.";
                update_avail_window = "Update available";
                car_lb = "Car: ";
                truck_lb = "Truck: ";
                wait_ets2_is_ready = "Initialization...";
                freight_lb = "Freight: ";
                gewicht = " t ";
                depature_lb = "Departure: ";
                destination_lb = "Destination: ";
                no_cargo_lb = "Driving without freigt";
                settings_lb = "Settings";
                exit_lb = "Exit";
                topmenuaccount_lb = "Account";
                not_avail = "Available soon";
                error_window = "Error";
                update_message = "Changes in version 1.1.0:\n" +
"- Credits available\n" +
                    "- Speed is now selectable between km / h and mph\n" +
                    "- We cleaned up! VTCManager now uses even\n" +
                    "  fewer resources! Yeah!\n" +
                    "- using Discord RPC";
                update_caption = "Changes in version 1.1.0\n";
                traffic_main_lb = "Traffic";
                statistic_panel_topic = "Statistics ";
                driven_tours_lb = "driven tours: ";
                act_bank_balance = "bank balance: ";
                user_company_lb = "employed by: ";
                no_company_text = "self-employed";
                jb_canc_lb = "Your last job was canceled!";
                save_info = "Please restart VTCManager to save the changes!";
                discord_rpc_tra_p1 = "Current tour from ";
                discord_rpc_tra_p2 = " to ";
                progress = "Progress: ";
                speed_setup_box = "Speed in mph?";
                settings_window = "Settings";
                loading_text = "Loading...";
                settings_window_save_button = "Save...";
                settings_window_groupBox1text = "Server Settings";
                btn_TruckersMP_suchentext = "TruckersMP Settings";
                settings_window_label3text = "Path to TruckersMP";
                settings_window_titel_text = "Settings";
                settings_window_tmp_error_text = "The path to TruckersMP is missing";
                settings_window_tmp_error_text2 = "Please correct the information in the following window!";
                settings_window_tmp_error_text3 = "TruckersMP path failed";
                settings_window_dir_error = "The Directory was not Found !";
                settings_log_failed = "can't create the log file!";
                log_ok = "Log File was Created !";
                error = "Error";
                warning = "Warning";
                error_sound_load = "An error occured while initialising the sound system";
                error_sound_missing_file = "The sound system has been initialised but some sound my not be available.";
                Frachtmarkt_no_profiles = "No Profiles found!";
                Frachtmarkt_from_City = "Start-City: ";
                Frachtmarkt_from_Company = "Start-Company: ";
                Frachtmarkt_to_City = "Destination-City: ";
                Frachtmarkt_to_Company = "Destination-Company: ";
                error_sound_play = "An error occured while playing a sound.";
                Settings_CheckBox_NUMPAD_ONOFF = "Show NUM-Pad Button";
                rest_text = "Resttime: ";
                rest_time_days = " Days, ";
                rest_time_hours = " Hours, ";
                rest_time_minutes = " Minutes";
                verspaetet = "You're to late!";
            }

        }
    }
}
