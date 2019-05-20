using ProjectTwitter.Model.Option;
using ProjectTwitter.Service.Option;
using ProjectTwitter.UI.Areas.Member.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTwitter.UI.Areas.Member.Controllers
{
    public class ProfileController : Controller
    {
        AppUserService appUserService;
        public ProfileController()
        {
            appUserService = new AppUserService();
        }


        public ActionResult Show()
        {
            AppUser user = appUserService.GetByDefault(x => x.UserName == User.Identity.Name);
            return View(user);
        }
        public ActionResult Update(Guid id)
        {
            AppUser appUser = appUserService.GetByID(id);
            AppUserDTO model = new AppUserDTO();
            model.ID = appUser.ID;
            model.FirstName = appUser.FirstName;
            model.LastName = appUser.LastName;
            model.PhoneNumber = appUser.PhoneNumber;
            model.Email = appUser.Email;
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(AppUserDTO data)
        {
            AppUser user = appUserService.GetByID(data.ID);
            user.FirstName = data.FirstName;
            user.LastName = data.LastName;
            user.PhoneNumber = data.PhoneNumber;
            user.Email = data.Email;
            appUserService.Update(user);
            return Redirect("/Member/Profile/Show");
        }
    }
}