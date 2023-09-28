﻿using System.ComponentModel.DataAnnotations;

namespace PortFolio.Models.DTO
{
    public class LoginModel
    {

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
