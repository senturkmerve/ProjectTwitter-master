using ProjectTwitter.Core.Map;
using ProjectTwitter.Model.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTwitter.Map.Option
{
    public class CommentMap:CoreMap<Comment>
    {
        public CommentMap()
        {
            ToTable("dbo.Comments");
            Property(x => x.CommentContent).HasMaxLength(50).IsOptional();

            HasRequired(x => x.AppUser)
               .WithMany(x => x.Comments)
               .HasForeignKey(x => x.AppUserID)
               .WillCascadeOnDelete(false);


            HasRequired(x => x.Tweet)
               .WithMany(x => x.Comments)
               .HasForeignKey(x => x.TweetID)
               .WillCascadeOnDelete(false);


        }
        
    }
}
