using Domain.Models;
using Domain.Models.BlogFolder;
using Domain.Models.DiscussionsFolder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Header> Headers { get; set; }
        public DbSet<Constant> Constants { get; set; }
        public DbSet<HomeHeaderWord> HomeHeaderWords { get; set; }
        public DbSet<School> Schools  { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<DiscussionReply> DiscussionsReplies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogEvaluation> BlogsEvaluations { get; set; }
        public DbSet<BlogComment> BlogsComments { get; set; }
        public DbSet<AboutContent> AboutContent { get; set; }
        public DbSet<OurGoals> OurGoals { get; set; }


    }
}
