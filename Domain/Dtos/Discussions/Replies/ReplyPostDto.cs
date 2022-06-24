using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.Discussions.Replies
{
    public class ReplyPostDto
    {
        [Require]
        public string Reply { get; set; }
        [Require]
        public string UserId { get; set; }
        [Require]
        public int DiscussionId { get; set; }
    }
}
