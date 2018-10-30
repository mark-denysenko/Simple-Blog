using Core;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFProvider
{
    public class EFPostRepository : IRepository<Post>
    {
        private EFBlogContext db;

        public EFPostRepository(EFBlogContext db)
        {
            this.db = db;
        }

        public void Create(Post item)
        {
            db.Posts.Add(item);
        }

        public void Delete(int id)
        {
            Post post = db.Posts.Find(id);
            if (post != null)
                db.Posts.Remove(post);
        }

        public Post Get(int id)
        {
            return db.Posts.Find(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return db.Posts;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Post item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EFPostRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
