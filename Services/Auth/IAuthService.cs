namespace Ticket.Services.Auth
{
    public interface IAuthService
    {
        Task<(bool ok, string message)> LoginAsync(string username, string password);
        void Logout();
    }
}