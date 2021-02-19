using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VOTDC.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User 
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }

        //This is rolling my own security. Bad.
        //I just didn't want to mess around with MS Identity for a demo app.
        public string Password { get; set; }

        public bool IsAdmin { get; set; } = false;
        public List<Favorite> Favorites { get; set; }
    }
}
