using FlashCards.Core.Application.Common.Interfaces.Authentication;
using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Domain.Entities.Users;
using Microsoft.Extensions.Logging;

namespace FlashCards.Core.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IGenericRepository<User> _userRepository;

        public AuthenticationService(ILogger<AuthenticationService> logger, IJwtTokenGenerator jwtTokenGenerator, IGenericRepository<User> userRepository)
        {
            _logger = logger;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            if (_userRepository.GetItemByKeyAsync("Email", email).Result is not User user || (user.Password != password))
            {
                _logger.LogError("Invalid email or password");
                return new AuthenticationResult(Guid.Empty, "", email, "");
            }

            var token = _jwtTokenGenerator.GenerateJwtToken(user.Id, user.Username);

            return new AuthenticationResult(user.Id, user.Username, email, token);
        }

        public AuthenticationResult Register(string username, string email, string password)
        {
            if (_userRepository.GetItemByKeyAsync("Email", email).Result is not null)
            {
                _logger.LogError("User with given email already exists.");
                return new AuthenticationResult(Guid.Empty, username, email, "");
            }

            if (_userRepository.GetItemByKeyAsync("Username", username).Result is not null)
            {
                _logger.LogError("User with given username already exists.");
                return new AuthenticationResult(Guid.Empty, username, email, "");
            }

            var user = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            _userRepository.AddItemAsync(user);

            var token = _jwtTokenGenerator.GenerateJwtToken(user.Id, username);
            return new AuthenticationResult(user.Id, username, email, token);
        }
    }
}