﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VOTDC.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User 
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }

        public bool IsAdmin { get; set; } = false;
        public List<Favorite> Favorites { get; set; }
    }
}
