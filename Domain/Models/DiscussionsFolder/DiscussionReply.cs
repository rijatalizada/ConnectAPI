using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DiscussionsFolder
{
    public class DiscussionReply : BaseEntity
    {
        public int Id { get; set; }
        public string Reply { get; set; }
        public int DiscussionId { get; set; }
        public Discussion Discussion { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
