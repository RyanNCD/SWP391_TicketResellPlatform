using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly TicketResellPlatformContext _context;
        public UserRepository(TicketResellPlatformContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(sc => sc.UserId.Equals(id));
        }
        public async Task<User> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await _dbSet
                .Include(u => u.Role)
                .Include(u => u.Wallet)
                // .ThenInclude(ur => ur.RoleName)
                .FirstOrDefaultAsync(u => u.Username == usernameOrEmail || u.Email == usernameOrEmail);
        }

    }

}
