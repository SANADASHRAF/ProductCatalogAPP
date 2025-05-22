﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class UserForLoginDto
    {
        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        public string Email { get; set; }

        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        public string Password { get; set; }
    }
}
