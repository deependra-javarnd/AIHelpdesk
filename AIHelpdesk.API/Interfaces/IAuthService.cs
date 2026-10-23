using AIHelpdesk.API.DTOs;

namespace AIHelpdesk.API.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginRequest request);
    }
}
