using ProjectTwitter.Model.Option;
using ProjectTwitter.Service.Option;
using ProjectTwitter.UI.Areas.Member.Models.DTO;
using ProjectTwitter.UI.Areas.Member.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectTwitter.UI.Areas.Member.Controllers
{
    public class HomeController : Controller
    {
        AppUserService appUserService;
        TweetService tweetService;
        CommentService commentService;
        public HomeController()
        {
            appUserService = new AppUserService();
            tweetService = new TweetService();
            commentService = new CommentService();

        }
        public ActionResult Index()
        {
            TweetDetailVM model = new TweetDetailVM()
            {
                AppUsers = appUserService.GetActive(),
                Tweets=tweetService.GetActive().OrderByDescending(x=>x.CreatedDate).ToList(),
                Comments=commentService.GetActive().OrderByDescending(x=>x.CreatedDate).ToList(),
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Tweet tweet)
        {
            tweet.AppUserID = appUserService.GetByDefault(x => x.UserName == User.Identity.Name).ID;
            tweet.CreatedDate = DateTime.Now;
            tweetService.Add(tweet);
            return Redirect("/Member/Home/Index");

        }
        public ActionResult TweetDelete(Guid id)
        {
            Tweet tweet = tweetService.GetByID(id);
            tweet.Status = Core.Enum.Status.Deleted;
            //tweet.ModifiedDate = DateTime.Now;
            tweetService.Update(tweet);
            return Redirect("/Member/Home/Index");
        }

        public ActionResult CommentAdd(Guid id)
        {
            Tweet tweet = tweetService.GetByID(id);
            CommentDTO commentDTO = new CommentDTO();
            commentDTO.TweetID = tweet.ID;
            commentDTO.TweetUserID = tweet.AppUserID;
            commentDTO.TweetContent = tweet.TweetContent;
            AppUser user = appUserService.GetByDefault(x => x.UserName == User.Identity.Name);
            commentDTO.TweetUserName = user.UserName;
            return View(commentDTO);

        }

        [HttpPost]
        public ActionResult CommentAdd(CommentDTO comment)
        {
            Comment data = new Comment();
            AppUser user = appUserService.GetByDefault(x => x.UserName == User.Identity.Name);
            data.AppUserID = user.ID;
            data.CommentUserName = user.UserName;
            data.TweetID = comment.TweetID;
            data.CommentContent = comment.CommentContent;
            commentService.Add(data);
            return Redirect("/Member/Home/Index");


        }
        public ActionResult CommentDelete(Guid id)
        {
            Comment comment = new Comment();
            comment = commentService.GetByID(id);
            comment.Status = Core.Enum.Status.Deleted;
            commentService.Update(comment);
            return Redirect("/Member/Home/Index");
        }

    }
}