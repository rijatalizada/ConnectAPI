using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BlogFolder
{
    public class Blog : BaseEntity
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<BlogEvaluation> BlogEvaluations { get; set; }
        public List<BlogComment> BlogComments { get; set; }

    }
}
