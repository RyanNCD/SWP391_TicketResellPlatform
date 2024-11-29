using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }

        void Save();
        Task<int> SaveChangesAsync();
    }
}
