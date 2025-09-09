using Relier.Application.DTOs;
using Relier.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relier.Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<bool> Authenticate(string email, string password);
        Task<UserDTO> RegisterUser(UserDTO user);
        Task LogOut();
    }
}
