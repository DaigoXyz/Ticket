using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Services.User
{
    public interface IUserService
    {
        Task<(bool ok, string message)> RegisterAsync(string username, string name, string password);
    }
}