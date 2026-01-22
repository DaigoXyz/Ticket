using Ticket.Models;

namespace Ticket.Services.State
{
    public class AuthState
    {
        public Users? CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;

        public void SetUser(Users user)
        {
            CurrentUser = user;
        }

        public event Action? OnChange;

        public void Logout()
        {
            CurrentUser = null;
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}