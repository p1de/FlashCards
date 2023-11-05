using FlashCards.Core.Application.Common.Interfaces.Authentication;
using FlashCards.Core.Application.Common.Interfaces.Persistance;
using FlashCards.Core.Application.CQRS.Authentication.Common;
using FlashCards.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FlashCards.Core.Application.CQRS.Authentication.Queries.Login
{
    internal class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
    {
        private readonly ILogger<LoginQueryHandler> _logger;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IOnlineGenericRepository<User> _userRepository;

        public LoginQueryHandler(
            ILogger<LoginQueryHandler> logger,
            IJwtTokenGenerator jwtTokenGenerator,
            IOnlineGenericRepository<User> userRepository)
        {
            _logger = logger;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetItemByKeyAsync("Email", query.Email) is not User user || (user.Password != query.Password))
            {
                _logger.LogError("Invalid email or password");
                return new AuthenticationResult(Guid.Empty.ToString(), string.Empty, query.Email, string.Empty);
            }

            var token = _jwtTokenGenerator.GenerateJwtToken(user.Id, user.Username);

            return new AuthenticationResult(user.Id, user.Username, user.Email, token);
        }
    }
}
