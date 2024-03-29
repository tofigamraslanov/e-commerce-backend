﻿using ECommerceBackend.Application.Features.AppUsers.Commands.FacebookLogin;
using ECommerceBackend.Application.Features.AppUsers.Commands.GoogleLogin;
using ECommerceBackend.Application.Features.AppUsers.Commands.LoginUser;
using ECommerceBackend.Application.Features.AppUsers.Commands.RefreshTokenLogin;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }
        
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }

        [HttpPost("facebook-login")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginCommandRequest request)
        {
            var response = await Mediator!.Send(request);
            return Ok(response);
        }
    }
}