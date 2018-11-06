using System;
using Core;

namespace Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Post> Posts { get; }
        IRepository<Comment> Comments { get; }

        void Save();
    }
}
