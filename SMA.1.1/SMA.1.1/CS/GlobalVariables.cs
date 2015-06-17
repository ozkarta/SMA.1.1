using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;
using SMA._1._1.CS.Authentication;
using System.Diagnostics;

namespace SMA.CS
{
    public static class GlobalVariables
    {
        public static Hashtable variableTable;
        public static Hashtable languageTable;
        public static Hashtable accessLevelsTable;
        //public static string currentLanguageTrial;
        //public static string currentLanguage;
        public static List<SelectListItem> languageListItems = new List<SelectListItem>();
        public static List<SelectListItem> accessLevelsListItems = new List<SelectListItem>();


        static GlobalVariables()
        {
            variableTable = new Hashtable();
            languageTable = new Hashtable();
            accessLevelsTable = new Hashtable();

            //manualInit();
            defaultInit();

            initVariables();
            initAccessLevelsList();
        }


        //______________________Language_______________________________________
        public static void initVariables()
        {

            Comunication.setVariables(languageTable[sessionPersister.currentLanguageTrial].ToString(), variableTable);
            initDropDownList();
        }
        public static void initDropDownList()
        {
            languageListItems.Clear();

            foreach (DictionaryEntry lang in GlobalVariables.languageTable)
            {
                
                SelectListItem i = new SelectListItem() { Text = lang.Key.ToString(), Value = lang.Value.ToString() };
                if (i.Text == sessionPersister.currentLanguage)
                {
                    i.Selected = true;
                }
                languageListItems.Add(i);

            }

        }
       
        public static string getVariableValue(string key)
        {

            Debug.WriteLine(sessionPersister.currentLanguage);
            string toReturn="NULL";
            if (variableTable[key] != null)
            {
                toReturn=variableTable[key].ToString();
            }


            return toReturn;
        }

        public static  void manualInit()
        {
            Comunication.getLanguages(languageTable);
            sessionPersister.currentLanguageTrial = "ქართული";
            sessionPersister.currentLanguage = "ქართული";

            variableTable.Add("home", "");
            variableTable.Add("about", "");
            variableTable.Add("contact", "");
            variableTable.Add("login", "");
            variableTable.Add("register", "");

            variableTable.Add("default_language", "");
            variableTable.Add("registration_type", "");
            variableTable.Add("registration_user_name", "");
            variableTable.Add("registration_first_name", "");
            variableTable.Add("registration_last_name", "");
            variableTable.Add("registration_phone_number", "");
            variableTable.Add("registration_email", "");
            variableTable.Add("registration_passport_id", "");
            variableTable.Add("registration_password", "");
            variableTable.Add("registration_confirm_password", "");
            variableTable.Add("register_button", "");
            variableTable.Add("register_window_title", "");
            variableTable.Add("register_page_title", "");
            variableTable.Add("log_in_window_title", "");
            variableTable.Add("log_in_user_name", "");
            variableTable.Add("log_in_password", "");
            variableTable.Add("log_in_button", "");
            variableTable.Add("log_in_page_title", "");

        }

        public static void defaultInit()
        {
            Comunication.getLanguages(languageTable);
            sessionPersister.currentLanguageTrial = "ქართული";
            sessionPersister.currentLanguage = "ქართული";

            Comunication.getVariableNames(variableTable);


        }
         public static void rollBackLanguage()
         {
             sessionPersister.currentLanguageTrial = sessionPersister.currentLanguage.ToString();
         }
         public static void updateLanguage()
         {
             sessionPersister.currentLanguage = sessionPersister.currentLanguageTrial.ToString();
         }
        //____________________ACCESS_LEVELS____________________________________________

         public static void initAccessLevelsList()
         {
             Comunication.setAccesLevelHashTable();

             foreach (DictionaryEntry levels in GlobalVariables.accessLevelsTable)
             {
                 SelectListItem item = new SelectListItem() { Text = levels.Key.ToString(), Value = levels.Value.ToString() };
                 accessLevelsListItems.Add(item);
             }
         }
       
    }
}