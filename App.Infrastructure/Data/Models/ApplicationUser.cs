﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure.Data.Models
{
    public class ApplicationUser:IdentityUser
    {
        public bool PasswordSetupRequired { get; set; }
    }
}
