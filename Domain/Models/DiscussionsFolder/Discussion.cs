using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DiscussionsFolder
{
    public class Discussion : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<DiscussionReply> Replies { get; set;  }
    }
}
