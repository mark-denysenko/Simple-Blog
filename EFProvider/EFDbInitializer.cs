using System;
using System.Collections.Generic;
using System.Data.Entity;
using Core;

namespace EFProvider
{
    public class EFDbInitializer: DropCreateDatabaseAlways<EFBlogContext>
    {
        protected override void Seed(EFBlogContext context)
        {
            IList<User> defaultUsers = new List<User>();
            defaultUsers.Add(new User { Nickname = "FirstNick", Email = "email1@some.com", PasswordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=" });
            defaultUsers.Add(new User { Nickname = "Mark", Email = "mark@some.com", PasswordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=" });
            defaultUsers.Add(new User { Nickname = "Oleg", Email = "oleg@some.com", PasswordHash = "pmWkWSBCL51Bfkhn79xPuKBKHz//H6B+mY6G9/eieuM=" });

            IList<Post> defaultPosts = new List<Post>();
            defaultPosts.Add(new Post { Title = "First Post", Body = "Test Description", Date = DateTime.Now.AddDays(-10), User = defaultUsers[0] });
            defaultPosts.Add(new Post { Title = "Second Post", Body = "Test2 Description", Date = DateTime.Now.AddDays(-1), User = defaultUsers[1] });
            defaultPosts.Add(new Post { Title = "Third Post", Body = "Test3 Description", Date = DateTime.Now.AddDays(-5), User = defaultUsers[1] });
            defaultPosts.Add(new Post { Title = "Fourth Post", Body = "Test4 Description", Date = DateTime.Now.AddDays(-3), User = defaultUsers[2] });

            IList<Comment> defaultComments = new List<Comment>();
            defaultComments.Add(new Comment { Body = "Yeah!!!", Date = DateTime.Now.AddMinutes(-15.0), Post = defaultPosts[0], User = defaultUsers[1] });
            defaultComments.Add(new Comment { Body = "Cool!!!", Date = DateTime.Now.AddMinutes(-11.0), Post = defaultPosts[0], User = defaultUsers[2] });
            defaultComments.Add(new Comment { Body = "So so..", Date = DateTime.Now.AddDays(-1), Post = defaultPosts[2], User = defaultUsers[0] });
            defaultComments.Add(new Comment { Body = "Nothing interesting", Date = DateTime.Now.AddDays(-2), Post = defaultPosts[3], User = defaultUsers[1] });

            context.Users.AddRange(defaultUsers);
            context.Posts.AddRange(defaultPosts);
            context.Comments.AddRange(defaultComments);

            base.Seed(context);
        }
    }
}
