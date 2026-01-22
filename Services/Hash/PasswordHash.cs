using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Services.Hash
{
    public class PasswordHash : IPasswordHash
    {
        private const string salt = "TicketProjectCik";

        public string Hash(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password + salt);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool Verify(string password, string passwordHash)
        {
            var hashedPassword = Hash(password);
            return hashedPassword == passwordHash;
        }
    }
}