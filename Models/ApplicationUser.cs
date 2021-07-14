using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CarFish.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual string Email { get; set; }
    }
}