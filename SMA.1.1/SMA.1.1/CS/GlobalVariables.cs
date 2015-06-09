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

            manualInit();
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