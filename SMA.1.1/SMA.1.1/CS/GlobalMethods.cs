using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net.Mail;

namespace SMA.CS
{
    public static class GlobalMethods
    {
        //static string emailRegex=@"\w{3,100}@\w*.\w{2,11}";
        public static bool isValidMail(string mail)
        {
          /*  bool toReturn = false;
            if (Regex.IsMatch(mail,emailRegex))
                toReturn=true;
            else
                toReturn=false;

            return toReturn;
           * */
            try
            {
                MailAddress ad = new MailAddress(mail);
                ad = null;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }


        }
        public static bool isValidPassword(string pass,string confPass)
        {
            if(pass.ToString()==confPass.ToString()&&pass.Length>5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool userNameValidation(string userName)
        {
            return (Comunication.existsUserName(userName)) ;
        }
        public static bool registerUser(string defaultLanguage,string accessLevel,string userName,string firstName ,string lastName,string phone
                                                                                            , string email, string password, string passportId)
        {
            string salt = generateSalt();
            string hashedPswd = generateHashedPSWD(password, salt);
            return Comunication.generalRegistration(defaultLanguage, accessLevel, userName, firstName, lastName, phone, email, hashedPswd, salt, passportId);
            
        }
        public static bool logInCheck(string user,string pass)
        {
            if (!validateUserAndPassword(user,pass))
            {
                return false;
            }
            return Comunication.checkUserAndPassword(user, pass);            
        }




        //_______________________________HASHING______________________________________________________________________



        public static string generateSalt()
        {
            string salt = "";
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] tokenData = new byte[32];
            rng.GetBytes(tokenData);

            salt = Convert.ToBase64String(tokenData);

            Debug.WriteLine(salt);
            return salt;
        }
        public static string generateHashedPSWD(string text, string saltString)
        {
            
            byte[] plainText = Convert.FromBase64String(Convert.ToBase64String(Encoding.UTF8.GetBytes(text)));
            byte[] salt = Convert.FromBase64String(saltString);
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return Convert.ToBase64String(algorithm.ComputeHash(plainTextWithSaltBytes));
        }


        //____________________________________________________________________________________________________________---

        public static bool validateUserAndPassword(string text,string password)
        {
            if(text.Trim()=="" || password.Trim()=="")
                return false;
            else
                return true;
        }
    }

  
}