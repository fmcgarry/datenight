﻿using System.ComponentModel.DataAnnotations;

namespace DateNight.Infrastructure.Auth;

public class TokenServiceOptions
{
    [Required]
    public string Audience { get; set; } = string.Empty;

    [Required]
    public string Issuer { get; set; } = string.Empty;

    [Required]
    public string Key { get; set; } = string.Empty;
}
