using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TIENDA.Core;
using TIENDA.Data.Services;
using TIENDA.Models;

namespace TIENDA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var result = await _userService.Login(model);

            return Ok(result);
        }

        
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
