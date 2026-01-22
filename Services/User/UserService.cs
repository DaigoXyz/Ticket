using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Models;
using Ticket.Repositories.IRepositories;
using Ticket.Services.Hash;

namespace Ticket.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;
        private readonly IPasswordHash _hash;

        public UserService(IUserRepository users, IPasswordHash hash)
        {
            _users = users;
            _hash = hash;
        }

        public async Task<(bool ok, string message)> RegisterAsync(string username, string name, string password)
        {
            username = (username ?? "").Trim();
            name = name ?? "";

            if (string.IsNullOrWhiteSpace(username)) return (false, "Username must be filled");
            if (string.IsNullOrWhiteSpace(password)) return (false, "Password must be filled");

            var exists = await _users.UsernameExistsAsync(username);
            if (exists) return (false, "Username exists");

            var user = new Users
            {
                Username = username,
                Name = name,
                PasswordHash = _hash.Hash(password),
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            await _users.AddAsync(user);

            return (true, "Register Successfuly.");
        }
    }
}