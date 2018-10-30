using System;
using System.Collections.Generic;
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
            //Database.SetInitializer<EFBlogContext>(new DropCreateDatabaseAlways<EFBlogContext>());

            //Database.SetInitializer(new EFDbInitializer());
        }
    }
}
