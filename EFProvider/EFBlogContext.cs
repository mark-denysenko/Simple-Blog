using System.Data.Entity;
using Core;

namespace EFProvider
{
    public class EFBlogContext : DbContext
    {
        public DbSet<Post>    Posts    { get; set; }
        public DbSet<User>    Users    { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public EFBlogContext()
            :base("EFBlogDb")
        {
            // Init values for DB (drop and create db each time)
            //Database.SetInitializer(new EFDbInitializer());
        }
    }
}
