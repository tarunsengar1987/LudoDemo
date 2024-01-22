﻿using System.ComponentModel.DataAnnotations;

namespace LudoWebAPI.Models.Entity
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
