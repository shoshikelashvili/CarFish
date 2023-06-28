using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CarFish.Shared.Models;
using DotNetEd.CoreAdmin.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DotNetEd.CoreAdmin.ViewModels
{
    public class ImagesViewModel
    {
        public List<Images> ImageList { get; set; }
    }
}
