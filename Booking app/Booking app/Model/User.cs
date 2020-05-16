﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking_app.Model
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public User(string email, string password, string name)
        {
            Email = email;
            Password = password;
            Name = name;
        }

        public override string ToString()
        {
            return $"Email:{Email}, Password:{Password}, Name:{Name}"
        }
    }
}
