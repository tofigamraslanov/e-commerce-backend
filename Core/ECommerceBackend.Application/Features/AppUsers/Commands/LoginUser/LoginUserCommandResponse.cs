﻿using ECommerceBackend.Application.Dtos;

namespace ECommerceBackend.Application.Features.AppUsers.Commands.LoginUser;

public class LoginUserCommandResponse
{
}

public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
{
    public TokenDto Token { get; set; } = null!;
}

public class LoginUserErrorCommandResponse : LoginUserCommandResponse
{
    public string Message { get; set; } = null!;
}