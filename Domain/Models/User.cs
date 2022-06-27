using Domain.Models.BlogFolder;
using Domain.Models.DiscussionsFolder;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string ProfileImage { get; set; }
        public bool isActive { get; set; } 
        public List<Discussion> Discussions { get; set; }
        public List<DiscussionReply> DiscussionsReplies { get; set; }
        public List<Rating> DiscussionsGivenRating { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<BlogEvaluation> BlogEvaluations { get; set; }
        public List<BlogComment> BlogComments { get; set; }

    }
}
