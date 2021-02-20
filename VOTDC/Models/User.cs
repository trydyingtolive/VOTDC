using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VOTDC.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }

        public bool IsAdmin { get; set; } = false;
        public List<Favorite> Favorites { get; set; }
    }
}
