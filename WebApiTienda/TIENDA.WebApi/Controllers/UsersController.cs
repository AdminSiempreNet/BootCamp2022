using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Data.Services;
using TIENDA.Models;

namespace TIENDA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var res = await _userService.ListAsync();
            return Ok(res);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> One([FromRoute] int userId)
        {
            var res = await _userService.OneAsync(userId);
            return Ok(res);
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> InsertAsyc(UserRegisterModel model)
        {
            var result = await _userService.InsertAsyc(model);
            //TODO: Enviar email           
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatetAsyc(UserModel model)
        {
            var result = await _userService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] int userId)
        {
            var res = await _userService.DeleteAsync(userId);
            return Ok(res);
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var result = await _userService.Login(model);

            if (result.IsSuccess)
            {
                var userWithToken = GenerateToken(result.Model);

                if (userWithToken.IsSuccess)
                {
                    result.Model = userWithToken.Model;
                }
            }

            return Ok(result);
        }

        private TypedResult<UserModel> GenerateToken(UserModel model)
        {
            var result = new TypedResult<UserModel>();
            try
            {
                var secretKey = _configuration.GetValue<string>("Authentication:SecretKey");
                var iisuer = _configuration.GetValue<string>("Authentication:Issuer");
                var audience = _configuration.GetValue<string>("Authentication:Audience");
                var duration = _configuration.GetValue<int>("Authentication:DurationMinutes");
                var key = Encoding.ASCII.GetBytes(secretKey);
                var expiration = DateTime.UtcNow.AddMinutes(duration);


                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtClaimTypes.Subject, model.Email),
                        new Claim(ClaimTypes.NameIdentifier, model.Name),
                        new Claim(ClaimTypes.Email, model.Email),
                        new Claim(ClaimTypes.Name, model.Name),
                        new Claim(ClaimTypes.Surname, model.LastName),
                    }),
                    Expires = expiration,
                    Issuer = iisuer,
                    Audience = audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                model.Token = tokenHandler.WriteToken(token);
                model.TokenExpiration = expiration;

                result.IsSuccess = true;
                result.Message = "OK";
                result.Code = 1;
                result.Model = model;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Error al generar el token";
                result.Error = ex;
            }

            return result;

        }


        [AllowAnonymous]
        [HttpPost("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordModel model)
        {
            var result = await _userService.RecoverPassword(model);
            return Ok(result);
        }


        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var result = await _userService.ChangePassword(model);
            return Ok(result);
        }

    }
}
