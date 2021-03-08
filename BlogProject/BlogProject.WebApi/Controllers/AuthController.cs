using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogProject.Business.Interfaces;
using BlogProject.Business.Tools.JWT;
using BlogProject.DTO.Dtos.AppUserDtos;
using BlogProject.WebApi.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        private readonly IJwtService _jwtService;
        public AuthController(IAppUserService appUserService, IJwtService jwtService)
        {
            _appUserService = appUserService;
            _jwtService = jwtService;
        }
        [HttpPost("[action]")]
        [ValidModel]
        public async Task<IActionResult> SignIn(AppUserSignInDto appUserSignInDto)
        {
            var user = await _appUserService.CheckUserAsync(appUserSignInDto);
            if (user != null)
            {
                return Created("", _jwtService.GenerateJwt(user));
            }
            return BadRequest("User name or password is wrong");
        }
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> Activeuser()
        {
            var user = await _appUserService.FindByNameAsync(User.Identity.Name);
            return Ok(new AppUserDto { Id = user.Id, Name = user.Name, Surname = user.Surname });
        }
    }
}
