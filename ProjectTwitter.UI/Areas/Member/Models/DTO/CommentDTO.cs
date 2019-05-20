using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectTwitter.UI.Areas.Member.Models.DTO
{
    public class CommentDTO
    {
        public Guid TweetID { get; set; }
        public string TweetContent { get; set; }
        public string CommentContent { get; set; }
      //public Guid CommentUserID { get; set; }
        public Guid TweetUserID { get; set; }
        public string TweetUserName { get; set; }
    }
}