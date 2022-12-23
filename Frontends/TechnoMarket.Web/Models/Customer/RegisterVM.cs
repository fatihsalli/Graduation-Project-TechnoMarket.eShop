﻿using System.ComponentModel.DataAnnotations;

namespace TechnoMarket.Web.Models.Customer
{
    public class RegisterVM
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string CustomerId { get; set; }

    }
}
