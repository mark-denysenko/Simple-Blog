using System;
using Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
