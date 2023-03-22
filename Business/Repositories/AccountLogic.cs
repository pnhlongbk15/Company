using AutoMapper;
using Business.Models;
using Business.Repositories.Interfaces;
using Data.Domain.Entities;
using Data.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Repositories
{
    public class AccountLogic : IAccountLogic
    {
        public AccountLogic(IAccountService service, IMapper mapper, IEmailSender emailSender, IConfiguration configuration)
        {
            _mapper = mapper;
            _service = service;
            _emailSender = emailSender;
            _configuration = configuration;
        }
        private IAccountService _service;
        private IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public async Task LoginAsync(LoginModel mLogin)
        {
            try
            {
                var user = _mapper.Map<User>(mLogin);
                var token = await _service.LoginAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> LoginStepTwo(TwoStepModel mTwoStep, string email)
        {
            try
            {
                dynamic result = await _service.LoginStepTwo(mTwoStep.TwoFactorCode, email);
                var roles = result.roles;
                var user = result.user;

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var r in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, r));
                }

                var authSigninKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JsonWebTokenKeys:IssuerSigninKey"])
                );
                var token = new JwtSecurityToken(
                    issuer: _configuration["JsonWebTokenKeys:ValidIssuer"],
                    audience: _configuration["JsonWebTokenKeys:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: new SigningCredentials(
                        authSigninKey,
                        SecurityAlgorithms.HmacSha256
                    ),
                    claims: authClaims
                );

                return new
                {
                    api_key = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    Role = roles,
                    status = "Login successfully."
                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RegisterAsync(RegistrationModel mRegistration, Func<String, String, String> FactoryUrl)
        {
            try
            {
                var user = _mapper.Map<User>(mRegistration);
                var token = await _service.RegisterAsync(user);
                var confirmUrl = FactoryUrl(token, user.Email);

                var contentEmail = "Please click here: <a href=\"#URL#\">Click here.</a>";
                contentEmail = contentEmail.Replace("#URL#", confirmUrl);

                // send
                await _emailSender.SendEmailAsync(
                    mRegistration.Email,
                    "Authen your account.",
                    contentEmail
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
