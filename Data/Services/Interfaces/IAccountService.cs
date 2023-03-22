using Data.Domain.Entities;

namespace Data.Services.Interfaces
{
    public interface IAccountService
    {
        Task<String> RegisterAsync(User eUser);
        Task<String> LoginAsync(User user);
        Task<Object> LoginStepTwo(string twoFactorCode, string email);
    }
}
