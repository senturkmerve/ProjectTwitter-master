using ProjectTwitter.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTwitter.Model.Option
{
    public class Comment:CoreEntity
    {
        public string CommentContent { get; set; }

        public string CommentUserName { get; set; }
        public Guid AppUserID { get; set; }
        public virtual AppUser AppUser { get; set; }

        public Guid TweetID { get; set; }
        public virtual Tweet Tweet { get; set; }

    }
}
