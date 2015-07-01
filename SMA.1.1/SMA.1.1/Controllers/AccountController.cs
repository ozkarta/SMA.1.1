using SMA.CS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using SMA._1._1.CS.Authentication;

namespace SMA.Controllers
{


    //
    //
    //          SAITS EMATEBA  KOMPANIEBISTVIS PERSONALURI  SAITEBIS AWYOBA  romlebic mibmulebi iknebian  ozzle-s domanze
    //
    //
    //

    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegisterValidation()
        {

            string defaultLanguage = Request["defaultLanguage"].ToString();
            string accessLevel = Request["accessLevel"].ToString();
            string userName = Request["user_name"].ToString();
            string firstName = Request["first_name"].ToString();
            string lastName = Request["last_name"].ToString();
            string phone = Request["phone"].ToString();
            string email = Request["email"].ToString();
            string password = Request["password"].ToString();
            string passwordConfirmation = Request["password_confirmation"].ToString();
            string passportId = Request["passportID"].ToString();

            if (GlobalMethods.isValidMail(email))
            {
                if (GlobalMethods.isValidPassword(password, passwordConfirmation))
                {
                    if (!GlobalMethods.userNameValidation(userName))
                    {

                        if (GlobalMethods.registerUser(defaultLanguage, accessLevel, userName, firstName, lastName, phone, email, password, passportId))
                        {
                            //ViewBag.ErrorMessage = "";
                            GlobalVariables.errorOnRegister = false;
                            GlobalVariables.errorOnRegisterMessage = "";
                            ViewBag.Message = "Registration was complete succesfully, To activate your profile please  check " +
                                                " mail  <h4>"+email+"</h4>  and follow  instructions  ";

                            return View("RegisterValidation");
                        }
                        else
                        {
                            //ViewBag.ErrorMessage = "There was error registerring the user, Please try later.";
                            GlobalVariables.errorOnRegister = true;
                            GlobalVariables.errorOnRegisterMessage = "There was error registerring the user, Please try later.";
                            return RedirectToAction("Register","Account");
                        }

                    }
                    else
                    {
                        //ViewBag.ErrorMessage = "User name you entered already exists";
                        GlobalVariables.errorOnRegister = true;
                        GlobalVariables.errorOnRegisterMessage = "User name you entered already exists";
                        return RedirectToAction("Register", "Account");
                    }
                }
                else
                {
                    //ViewBag.ErrorMessage = "The Password is incorrect";
                    GlobalVariables.errorOnRegister = true;
                    GlobalVariables.errorOnRegisterMessage = "The Password is incorrect";
                    return RedirectToAction("Register", "Account");
                }

            }
            else
            {
               // ViewBag.ErrorMessage = "The Mail Is Incorrect";
                GlobalVariables.errorOnRegister = true;
                GlobalVariables.errorOnRegisterMessage = "The Mail Is Incorrect";
                return RedirectToAction("Register", "Account");
            }
        }

        [HttpPost]
        public RedirectToRouteResult LogInValidation()
        {
            string userName = Request["user_name"].ToString();
            string password = Request["password"].ToString();

            if (GlobalMethods.logInCheck(userName, password))
            {
                sessionPersister.userName = userName;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //ViewBag.ErrorMessage = "UserName or Password Is incorrect";
                GlobalVariables.errorOnLogIn = true;
                return RedirectToAction("LogIn","Account");
            }
            
        }

        public ActionResult Register()
        {
            if (checkIfLoggedInAndRedirect() != null)
            {
                return checkIfLoggedInAndRedirect();
            }
            if (GlobalVariables.errorOnRegister)
            {
                ViewBag.ErrorMessage = GlobalVariables.errorOnRegisterMessage;
                GlobalVariables.errorOnRegisterMessage = "";
                GlobalVariables.errorOnRegister = false;
            }
            else
            {
                ViewBag.ErrorMessage = "";
            }


            return View("Register");
        }
        public ActionResult LogIn()
        {
            if (checkIfLoggedInAndRedirect() != null)
            {
                return checkIfLoggedInAndRedirect();
            }
            if (GlobalVariables.errorOnLogIn)
            {
                ViewBag.ErrorMessage = "UserName or Password Is incorrect";
                GlobalVariables.errorOnLogIn = false;
            }
            else
            {
                ViewBag.ErrorMessage = "";
            }
            return View("LogIn");
        }
        public RedirectToRouteResult EmailConfirmation()
        {
            string userGUID = Request["userGUID"].ToString();
            if(Comunication.activateUser(userGUID))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Error", "Shared");
            }

           
        }

        public ActionResult logout()
        {
            sessionPersister.logout();
            return RedirectToAction("Index", "Home");
        }

        public RedirectToRouteResult checkIfLoggedInAndRedirect()
        {
            if (sessionPersister.userRole != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return null;
            }
        }
    }
}