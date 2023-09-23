﻿using Microsoft.AspNetCore.Identity;

namespace MovieAPIs.Models.Domain
{
    public class ApplicationUser  : IdentityUser 
    {

        public string Name { get; set; }
    }
}
