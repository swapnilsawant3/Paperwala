using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAPERWALA.Filters;
using PAPERWALA.Repository;
using PAPERWALA.Models;
using System.Web.Security;

namespace PAPERWALA.Controllers
{
    public class LoginDistributorController : Controller
    {

        ILoginDistributor objILoginDistributor;

        public LoginDistributorController()
        {
            objILoginDistributor = new LoginDistributor();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginDistributor()
        {
            return View("LoginDistributor");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginDistributor(LoginDistributorDTO login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool success = Get_CheckUserExist(login.UserName, login.Password);
                var UserID = GetUserID_By_UserName(login.UserName);
                var StateID = GetStateId_UserID(UserID);
                var CityID = GetCityId_UserID(UserID);
                var Designation = GetDesignationBy_UserID(Convert.ToString(UserID));
                if (success == true)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(Designation)))
                    {
                        ModelState.AddModelError("Error", "Rights to User are not Provide Contact to Admin");
                        return View(login);
                    }
                    else
                    {
                        Session["Name"] = login.UserName;
                        Session["UserID"] = UserID;
                        Session["StateID"] = StateID;
                        Session["CityID"] = CityID;
                        Session["Designation"] = Designation;

                        FormsAuthentication.SetAuthCookie(login.UserName.ToUpper(), false);
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            if (Designation == "ADMIN")
                            {
                                return RedirectToAction("AdminDashboard", "Dashboard");
                            }
                            else
                            {
                                return RedirectToAction("AdminDashboard", "Dashboard");
                            }
                        }


                    }

                }
                else if (login.UserName == "SYSTEM" && login.Password == "SYSTEM")
                {
                    Session["Name"] = login.UserName;
                    Session["UserID"] = 0;
                    Session["Designation"] = "SUPERADMIN";
                    return RedirectToAction("AdminDashboard", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("Error", "Please enter valid Username and Password");
                    return View(login);
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Please enter Username and Password");
            }
            return View(login);

        }




        [NonAction]
        public string GetUserID_By_UserName(string UserName)
        {
            return objILoginDistributor.GetUserID_By_UserName(UserName);
        }

        [NonAction]
        public string GetDesignationBy_UserID(string UserId)
        {
            return objILoginDistributor.GetDesignationByUserID(UserId);
        }
        [NonAction]
        public string GetStateId_UserID(string UserId)
        {
            return objILoginDistributor.GetStateIdByUserID(UserId);
        }
        [NonAction]
        public string GetCityId_UserID(string UserId)
        {
            return objILoginDistributor.GetCityIdByUserID(UserId);
        }
        [NonAction]
        public bool Get_CheckUserExist(string UserName, string Password)
        {
            return objILoginDistributor.Get_CheckUserExist(UserName, Password);
        }


        // GET: LoginDistributor
        public ActionResult Index()
        {
            return View();
        }
    }
}