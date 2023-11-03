using FlashCards.Core.Application.Common.Interfaces.Authentication;
using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Core.Application.CQRS.Authentication.Common;
using FlashCards.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlashCards.Core.Application.CQRS.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IOnlineGenericRepository<User> _onlineUserRepository;

        public RegisterCommandHandler(
            ILogger<RegisterCommandHandler> logger,
            IJwtTokenGenerator jwtTokenGenerator,
            IOnlineGenericRepository<User> onlineUserRepository)
        {
            _logger = logger;
            _jwtTokenGenerator = jwtTokenGenerator;
            _onlineUserRepository = onlineUserRepository;
        }

        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (_onlineUserRepository.GetItemByKeyAsync("Email", command.Email).Result is not null)
            {
                _logger.LogError("User with given email already exists.");
                return new AuthenticationResult(Guid.Empty.ToString(), command.Username, string.Empty, string.Empty);
            }

            if (_onlineUserRepository.GetItemByKeyAsync("Username", command.Username).Result is not null)
            {
                _logger.LogError("User with given username already exists.");
                return new AuthenticationResult(Guid.Empty.ToString(), string.Empty, command.Email, string.Empty);
            }

            var user = new User
            {
                Username = command.Username,
                Email = command.Email,
                Password = command.Password
            };

            await _onlineUserRepository.AddItemAsync(user);

            var token = _jwtTokenGenerator.GenerateJwtToken(user.Id, command.Username);
            return new AuthenticationResult(user.Id, command.Username, command.Email, token);
        }
    }
}
