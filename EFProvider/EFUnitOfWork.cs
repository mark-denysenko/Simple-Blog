using System;
using Interfaces;
using Core;
using System.Data.Entity;

namespace EFProvider
{
    public class EFUnitOfWork : IUnitOfWork   //, IDisposable
    {
        private EFBlogContext db = new EFBlogContext();

        #region Implementation UoW 
        private IRepository<Post> _postRepository;
        private IRepository<User> _userRepository;
        private IRepository<Comment> _commentRepository;

        public IRepository<Post> Posts
        {
            get
            {
                if (_postRepository == null)
                    _postRepository = new EFPostRepository(db);
                return _postRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new EFUserRepository(db);
                return _userRepository;
            }
        }

        public IRepository<Comment> Comments
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new EFCommentRepository(db);
                return _commentRepository;
            }
        }
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
