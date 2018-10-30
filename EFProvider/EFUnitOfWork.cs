using System;
using Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using System.Data.Entity;

namespace EFProvider
{
    public class EFUnitOfWork : IUnitOfWork   //, IDisposable
    {
        private EFBlogContext db = new EFBlogContext();

        #region Implementation UoW 
        private IRepository<Post> postRepository;
        private IRepository<User> userRepository;
        private IRepository<Comment> commentRepository;

        public IRepository<Post> Posts
        {
            get
            {
                if (postRepository == null)
                    postRepository = new EFPostRepository(db);
                return postRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new EFUserRepository(db);
                return userRepository;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (commentRepository == null)
                    commentRepository = new EFCommentRepository(db);
                return commentRepository;
            }
        }
        #endregion

        #region but DB sets its like repository EF implementation

        public DbSet<User> UsersDB { get { return db.Users; } }
        public DbSet<Post> PostsDB { get { return db.Posts; } }
        public DbSet<Comment> CommentsDB { get { return db.Comments; } }

        #endregion

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
