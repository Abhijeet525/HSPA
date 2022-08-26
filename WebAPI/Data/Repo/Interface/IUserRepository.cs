using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Data.Repo.Interface
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
    }
}
