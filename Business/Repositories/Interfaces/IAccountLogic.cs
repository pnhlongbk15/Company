using Business.Models;

namespace Business.Repositories.Interfaces
{
    public interface IAccountLogic
    {
        Task RegisterAsync(RegistrationModel mRegistration, Func<String, String, String> FactoryUrl);
        Task LoginAsync(LoginModel mLogin);
        Task<Object> LoginStepTwo(TwoStepModel mTwoStep, string email);
        Task ConfirmEmail(string token, string email);
    }
}
