using ProjectTwitter.Core.Enum;
using ProjectTwitter.Model.Option;
using ProjectTwitter.Service.Option;
using ProjectTwitter.UI.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectTwitter.UI.Controllers
{
    public class AccountController : Controller
    {
        AppUserService _appUserService;
        public AccountController()
        {
            _appUserService = new AppUserService();
        }
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AppUser user = _appUserService.FindByUserName(User.Identity.Name);
                if (user.Status == Status.Active || user.Status == Status.Updated)
                {
                    string cookie = user.UserName;
                    FormsAuthentication.SetAuthCookie(cookie, true);
                    Session["ID"] = user.ID;
                    Session["FullName"] = user.FirstName + ' ' + user.LastName;
                    Session["ImagePath"] = user.UserImage;
                    //Session["ProfileImage"] = user.XSmallUserImage;
                    //Session["Bio"] = user.Biography;
                    return Redirect("/Member/Home/Index");
                }
                else
                {
                    ViewData["error"] = "Username or Password is wrong!";
                    return View();
                }
            }
            else
            {
                TempData["class"] = "custom-hide";
                return View();
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM credential)
        {
            if (ModelState.IsValid)
            {
                if (_appUserService.CheckCredentials(credential.UserName, credential.Password))
                {
                    AppUser user = _appUserService.FindByUserName(credential.UserName);
                    if (user.Status == Status.Active || user.Status == Status.Updated)
                    {
                        string cookie = user.UserName;
                        FormsAuthentication.SetAuthCookie(cookie, true);
                        Session["ID"] = user.ID;
                        Session["FullName"] = user.FirstName + ' ' + user.LastName;
                        Session["ImagePath"] = user.UserImage;
                        //Session["ProfileImage"] = user.XSmallUserImage;
                        //Session["Bio"] = user.Biography;
                        // return Redirect("/Member/Home/Index");
                        return Redirect("/Member/Home/Index");
                    }
                    else
                    {
                        ViewData["error"] = "Username or Password is wrong!";
                        return View();
                    }

                }
                else
                {
                    ViewData["error"] = "Username or Password is wrong!";
                    return View();
                }
            }
            else
            {
                TempData["class"] = "custom-hide";
                return View();
            }
        }

        public ActionResult LogOut()
        {

            FormsAuthentication.SignOut();
            return Redirect("/Account/Login");
        }

    }
}