using Relier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relier.Domain.Account
{
    public interface IAuthenticateRepository
    {
        Task<User?> Authenticate(string email, string password);
        Task<bool> RegisterUser(User user);
        Task LogOut();
    }
}
