using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMA._1._1.CS.Authentication
{
    public class ozzIdentity :
        System.Security.Principal .IIdentity
    {

        public ozzIdentity(string name)
        {
            this.Name = name;
        }

        public string AuthenticationType
        {
            get { return "ozzAuthentication"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(this.Name); }
        }

        public string Name
        {
            get;
            private set;
        }
    }

    public class ozzPrincipal :
        System.Security.Principal.IPrincipal
    {

        public ozzPrincipal(ozzIdentity identity)
        {
            this.Identity = identity;
        }

        public System.Security.Principal.IIdentity Identity
        {
            get;
            private set;
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }

    public class myControler :
        Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!string.IsNullOrEmpty(sessionPersister.userName))
            {
                filterContext.HttpContext.User = new ozzPrincipal(new ozzIdentity(sessionPersister.userName));
            }
            base.OnAuthorization(filterContext);
        }
    }

    public static class sessionPersister
    {
        static string userNameSessionVariable = "username";
        static string emailSessionVariable = "email";
        static string userRoleSessionVarialbe = "role";
        static string userGUIDSessionVariable = "userGUID";
        static string currentLanguageTrialSessionVariable = "currentLanguageTrial";
        static string currentLanguageSessionVariable="currentLanguage";
        public static string userName
        {
            get
            {
                if (HttpContext.Current==null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[userNameSessionVariable];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set { HttpContext.Current.Session[userNameSessionVariable] = value; }
        }
        public static string email
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[emailSessionVariable];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set { HttpContext.Current.Session[emailSessionVariable] = value; }
        }


        public static string userRole
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[userRoleSessionVarialbe];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set { HttpContext.Current.Session[userRoleSessionVarialbe] = value; }
        }

        public static string userGUID
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[userGUIDSessionVariable];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set { HttpContext.Current.Session[userGUIDSessionVariable] = value; }
        }

        public static string currentLanguageTrial
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[currentLanguageTrialSessionVariable];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set { HttpContext.Current.Session[currentLanguageTrialSessionVariable] = value; }
        }
        public static string currentLanguage
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[currentLanguageSessionVariable];
                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set { HttpContext.Current.Session[currentLanguageSessionVariable] = value; }
        }
        //______________________________________________________________________________
        public static void logout()
        {
            HttpContext.Current.Session[userRoleSessionVarialbe] = null;
            HttpContext.Current.Session[emailSessionVariable] = null;
            HttpContext.Current.Session[userNameSessionVariable] = null;
            HttpContext.Current.Session[userGUIDSessionVariable] = null;

        }
    }


}