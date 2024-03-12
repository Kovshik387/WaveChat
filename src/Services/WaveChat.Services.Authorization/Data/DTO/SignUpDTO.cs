﻿using FluentValidation;

namespace WaveChat.Services.Authorization.Data.DTO;

public class SignUpDTO
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
