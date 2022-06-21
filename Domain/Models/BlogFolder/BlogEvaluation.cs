using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.BlogFolder
{
    public class BlogEvaluation : BaseEntity
    {
        public int Id { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

    }
}
