using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Services.Hash
{
    public interface IPasswordHash
    {
        string Hash(string password);
        bool Verify(string password, string passwordHash);
    }
}