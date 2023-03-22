using Business.Models;
using Business.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        public AccountController(IAccountLogic logicBusiness)
        {
            _logicBusiness = logicBusiness;
        }
        private readonly IAccountLogic _logicBusiness;

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel mLogin)
        {
            try
            {
                await _logicBusiness.LoginAsync(mLogin);

                return Ok("OTP send to your mail.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LoginStepTwo(TwoStepModel mTwoStep, string email)
        {
            try
            {
                var result = await _logicBusiness.LoginStepTwo(mTwoStep, email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel mRegistration)
        {
            try
            {
                var FactoryUrl = (string token, string email) =>
                {
                    return Url.Action(
                            nameof(ConfirmEmail),
                            "Account",
                            new { token, email },
                            Request.Scheme
                        );
                };
                await _logicBusiness.RegisterAsync(mRegistration, FactoryUrl);

                return Ok("Register successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {

            if (token == null || email == null)
            {
                return BadRequest("Invalid email confirmation url.");
            }
            try
            {
                await _logicBusiness.ConfirmEmail(token, email);
                return Ok("Confirm your email successful.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
