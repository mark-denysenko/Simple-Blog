using Core;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace EFProvider
{
    public class EFCommentRepository : IRepository<Comment>
    {
        private EFBlogContext db;

        public EFCommentRepository(EFBlogContext db)
        {
            this.db = db;
        }

        public void Create(Comment item)
        {
            db.Comments.Add(item);
        }

        public void Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment != null)
                db.Comments.Remove(comment);
        }

        public Comment Get(int id)
        {
            return db.Comments.FirstOrDefault(c => c.CommentId == id);
        }

        public IQueryable<Comment> GetAll()
        {
            return db.Comments.AsQueryable();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Comment item)
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

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        public int Count()
        {
            return db.Comments.Count();
        }
    }
}
