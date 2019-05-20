using ProjectTwitter.Model.Option;
using ProjectTwitter.Service.Option;
using ProjectTwitter.UI.Areas.Member.Models.DTO;
using ProjectTwitter.UI.Areas.Member.Models.VM;
using ProjectTwitter.Utility;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTwitter.UI.Areas.Member.Controllers
{
    public class TweetController : Controller
    {
        CommentService _commentService;
        AppUserService _appUserService;
        LikeService _likeService;
        TweetService _tweetService;
        public TweetController()
        {
            _commentService = new CommentService();
            _appUserService = new AppUserService();
            _likeService = new LikeService();
            _tweetService = new TweetService();
        }
        //public ActionResult AddTweet()
        //{
        //    List<AppUser> model = _appUserService.GetActive();
        //    return View(model);
        //}
        [HttpPost]
        public ActionResult AddTweet(Tweet data, HttpPostedFileBase Image)
        {
            List<string> UploadedImagePaths = new List<string>();

            UploadedImagePaths = ImageUploader.UploadSingleImage(ImageUploader.OriginalProfileImagePath, Image, 1);

            data.ImagePath = UploadedImagePaths[0];

            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {
                data.ImagePath = ImageUploader.DefaultProfileImagePath;
                data.ImagePath = ImageUploader.DefaultXSmallProfileImage;
                data.ImagePath = ImageUploader.DefaulCruptedProfileImage;
            }
            else
            {
                data.ImagePath = UploadedImagePaths[1];
                data.ImagePath = UploadedImagePaths[2];
            }

            AppUser user = _appUserService.GetByDefault(x => x.UserName == User.Identity.Name);
            data.AppUserID = user.ID;

            data.CreatedDate = DateTime.Now;
            _tweetService.Add(data);

            return Redirect("/Member/Home/Index");



        }
        public ActionResult TweetList()
        {

            //List<Tweet> model = _tweetService.GetActive();
            //return View(model);

            Tweet tweet = _tweetService.GetDefault(x => x.Status == Core.Enum.Status.Active).FirstOrDefault();

            return Json(new
            {
                AppUserImagePath = tweet.AppUser.UserImage,
                FirstName = tweet.AppUser.FirstName,
                LastName = tweet.AppUser.LastName,
                CreatedDate = tweet.CreatedDate.ToString(),
                TweetContent = tweet.TweetContent,
                TweetCount = _tweetService.GetDefault(x => x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated).Count()

                //CommentCount = _commentService.GetDefault(x => x.TweetID == tweetID && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated)).Count(),
                //LikeCount = _likeService.GetDefault(x => x.TweetID == tweetID && (x.Status == Core.Enum.Status.Active || x.Status == Core.Enum.Status.Updated)).Count(),
            }, JsonRequestBehavior.AllowGet);

        }
    }
}
