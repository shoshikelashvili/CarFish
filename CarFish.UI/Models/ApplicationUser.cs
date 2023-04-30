using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CarFish.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(36)]
        public override string Id { get; set; }
    }
}