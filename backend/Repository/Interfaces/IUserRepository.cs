﻿using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserById(int id);
        Task<User> GetByUsernameOrEmailAsync(string usernameOrEmail);
    }
}
