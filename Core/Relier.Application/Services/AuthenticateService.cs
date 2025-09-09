using AutoMapper;
using Relier.Application.DTOs;
using Relier.Application.Interfaces;
using Relier.Domain.Account;
using Relier.Domain.Entities;

namespace Relier.Application.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAuthenticateRepository _repository;
        private readonly IMapper _mapper;

        public AuthenticateService(IAuthenticateRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _repository.Authenticate(email, password);
            if (result is null)
                return false;

            return (result.Email.Trim() == email.Trim() && result.Password.Trim() == password.Trim());
        }

        public Task LogOut()
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> RegisterUser(UserDTO user)
        {
            var entity = _mapper.Map<User>(user);
            var response = await _repository.RegisterUser(entity);
            return _mapper.Map<UserDTO>(response);
        }
    }
}
