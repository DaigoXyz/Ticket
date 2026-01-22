using Ticket.Repositories.IRepositories;
using Ticket.Services.Hash;
using Ticket.Services.State;

namespace Ticket.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHash _hash;
        private readonly AuthState _state;

        public AuthService(IUserRepository users, IPasswordHash hash, AuthState state)
        {
            _users = users;
            _hash = hash;
            _state = state;
        }

        public async Task<(bool ok, string message)> LoginAsync(string username, string password)
        {
            username = username.Trim();

            var user = await _users.GetByUsernameAsync(username);
            if (user == null) return (false, "User not found");

            if (!user.IsActive) return (false, "User is not active");

            if (!_hash.Verify(password, user.PasswordHash))
                return (false, "Invalid password");

            _state.SetUser(user);
            return (true, "OK");
        }

        public void Logout() => _state.Logout();
    }
}