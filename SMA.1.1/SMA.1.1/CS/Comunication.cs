using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using SMA._1._1.CS.Authentication;

namespace SMA.CS
{
    public static class Comunication
    {
        //static String connectionString = "Data Source=tcp:78.139.172.254,1973;Initial Catalog=smaDataBase;User ID=sa;Password=12qwert12";
        //static string connectionString=@"Data Source=OZKARTA\OZKARTA;Initial Catalog=smaDataBase;Integrated Security=True";
       static String connectionString  = @"Data Source=SPARE-PC\SQLEXPRESS;Initial Catalog=smaDataBase;Integrated Security=True";
       static  SqlConnection con;
       static  SqlDataAdapter adapter;
       static  SqlCommand cmd;
       static  SqlDataReader reader;


        public static void getLanguages(Hashtable t)
        {
            using (con=new SqlConnection(connectionString))
            {
                using (cmd=new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getLanguageList";

                    try
                    {
                        con.Open();
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                t.Add(reader["languageName"], reader["languageGUID"]);
                            }
                            
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }

                }
            }
        }

        public static void setVariables(string languageGUID,Hashtable t)
        {
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getTranslatedVariableValue";

                    int counter = 0;
                        try
                        {
                            con.Open();
                            List<string> keys = new List<string>();

                            foreach (string key in t.Keys)
                                keys.Add(key);
                            foreach(string key in keys)
                                {
                                    
                                    
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@languageGUID",languageGUID);
                                    cmd.Parameters.AddWithValue("@variableName",key);
                                    using (reader = cmd.ExecuteReader())
                                    {
                                        while(reader.Read())
                                        {
                                            t[key] = reader[0].ToString();
                                            counter++;
                                            Debug.WriteLine(reader[0].ToString());
                                        }
                                    }
                                    Debug.WriteLine(key+ "   -------  "+t[key].ToString());
                                }
                            con.Close();
                            if (counter == 0)
                            {
                                GlobalVariables.rollBackLanguage();
                            }
                            else
                            {
                                GlobalVariables.updateLanguage();
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.ToString());
                        }
                        
                   }                
            }
        }


        public static void setAccesLevelHashTable()
        {
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "getAccessLevelsList";
                    try
                    {
                        con.Open();
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GlobalVariables.accessLevelsTable.Add(reader["levelName"], reader["levelGUID"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                    
                }
            }
        }
    

       
        public static bool existsUserName(string userName)
        {
            bool result=true;
            using (con=new SqlConnection(connectionString))
            {
                using(cmd=new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "userNameValidation";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userName",userName);
                    try
                    {
                        con.Open();
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader[0].ToString() == "-1")
                                {
                                    result = true;
                                }
                                else
                                {
                                    result = false;
                                }
                            }
                        }
                        con.Close();
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }
                }
            }
            return result;
        }

        public static bool generalRegistration(string defaultLanguage,string accessLevelGUID,string userName,string firstName ,string lastName,string phone
                                            , string email, string password, string salt, string passportId)
        {
            using (con=new SqlConnection(connectionString))
            {
                using(cmd=new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.CommandText = "registerUser";
                    cmd.Parameters.AddWithValue("@defaultLanguage", defaultLanguage);
                    cmd.Parameters.AddWithValue("@userName",userName);
                    cmd.Parameters.AddWithValue("@firstName",firstName);
                    cmd.Parameters.AddWithValue("@lastName",lastName);
                    cmd.Parameters.AddWithValue("@phone",phone);
                    cmd.Parameters.AddWithValue("@email",email);
                    cmd.Parameters.AddWithValue("@passwordHash", password);
                    cmd.Parameters.AddWithValue("@salt", salt);
                    cmd.Parameters.AddWithValue("@accessLevelGUID", accessLevelGUID);
                    cmd.Parameters.AddWithValue("@passportId", passportId);
                    try
                    {
                        con.Open();

                        string userGUID=cmd.ExecuteScalar().ToString();
                        sendConfirmationEmail(email, userName, userGUID, firstName, lastName);
                        con.Close();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                        return false;
                    }
                }
            }

            
        }



        public static bool sendConfirmationEmail(string mailTo,string userName,string activationCode,string fname,string lname)
        {
            using (MailMessage mm = new MailMessage("ozbegi@gmail.com", mailTo))
            {
                mm.Subject = "Account Activation";
                string body = "Hello " +"<h1>"+fname+lname+"('"+ userName+ "')"+ "<h1/>"+",";
                body += "<br /><br />Please click the following link to activate your account";
                body += "<br /><a href = '" + "http://localhost:51857/Account/EmailConfirmation?userGUID=" + activationCode + "'>Click here to activate your account.</a>";
                body += "<br /><br />Thanks";
                mm.Body = body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("ozbegi1@gmail.com", "1donozok1");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                try
                {
                    smtp.Send(mm);
                    return true;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return false;
                }
               
            }
        }

        public static bool activateUser(string userGUID)
        {
            using(con=new SqlConnection(connectionString ))
            {
                using (cmd=new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText="activateUser";
                    cmd.Parameters.AddWithValue("@userGUID", userGUID);

                    try
                    {
                        con.Open();
                        cmd.ExecuteScalar();
                        con.Close();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                        return false;
                    }
                }
            }
        }


        public static bool checkUserAndPassword(string user,string password)
        {
            using (con = new SqlConnection(connectionString))
            {
                using (cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "getSalt";
                    cmd.Parameters.AddWithValue("@user", user);
                    
                    try
                    {
                        con.Open();
                        string userSalt=cmd.ExecuteScalar() as string;
                        string newPasswordHash = GlobalMethods.generateHashedPSWD(password, userSalt);

                        cmd.CommandText = "compareHashedPasswords";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@newPasswordHash", newPasswordHash);
                        string result = cmd.ExecuteScalar() as string;

                        cmd.CommandText = "getAccessLevel";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@user", user);
                        sessionPersister.userRole = cmd.ExecuteScalar() as string;
                        
                       
                        cmd.CommandText = "getUserGUID";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@user", user);
                        sessionPersister.userGUID = cmd.ExecuteScalar() as string;

                        con.Close();

                        if (result.Trim().Equals("1"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                        return false;
                    }
                }
            }
        }
    }

}