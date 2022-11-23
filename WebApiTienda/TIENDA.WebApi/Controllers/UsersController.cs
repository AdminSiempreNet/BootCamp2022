using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Data.Services;
using TIENDA.Email;
using TIENDA.Models;

namespace TIENDA.WebApi.Controllers
{
    /// <summary>
    /// Gestión de usuarios
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="configuration"></param>
        /// <param name="emailService"></param>
        public UsersController(IUserService userService,
            IConfiguration configuration,
            EmailService emailService)
        {
            _userService = userService;
            _configuration = configuration;
            _emailService = emailService;
        }

        /// <summary>
        /// Lista de todos los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var res = await _userService.ListAsync();
            return Ok(res);
        }

        /// <summary>
        /// Datos del usuario especificado mediante el userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> One([FromRoute] int userId)
        {
            var res = await _userService.OneAsync(userId);
            return Ok(res);
        }

        /// <summary>
        /// Registrar un nuevo usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> InsertAsyc(UserRegisterModel model)
        {
            var result = await _userService.InsertAsyc(model);
            //TODO: Enviar email           
            return Ok(result);
        }

        /// <summary>
        /// Modificar los datos del usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatetAsyc(UserModel model)
        {
            var result = await _userService.UpdateAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Eliminar un usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] int userId)
        {
            var res = await _userService.DeleteAsync(userId);
            return Ok(res);
        }

        /// <summary>
        /// Autenticar el usuario y obtener el token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Recuperar la contraseña
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("RecoverPassword")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordModel model)
        {
            var result = await _userService.RecoverPassword(model);

            if (result.IsSuccess)
            {
                var code = result.Object;

                var to = new List<EmailAddress>
                   {
                       new EmailAddress
                       {
                            DisplayName = model.Email,
                            Address = model.Email,
                       },
                       new EmailAddress
                       {
                           DisplayName = "Eduin",
                           Address = "eduin1178@gmail.com"
                       }
                   };

                var mail = new MailModel
                {
                    To = to,
                    Subject = "Codigo para restablcer clave en TIENDA",
                    Content = $"Su codigo de verificacion para restablecer la clave es es el siguiente {code}",
                    IsBodyHtml = true,
                };

                await _emailService.SendEmailAsync(mail);
            }

            return Ok(result);
        }

        /// <summary>
        /// Restablecer la contraseña
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            var result = await _userService.ResetPassword(model);
            return Ok(result);
        }

        /// <summary>
        /// Cambiar la contraseña
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var result = await _userService.ChangePassword(model);
            return Ok(result);
        }

    }
}
