using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VOTDC.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        public Guid VerseId { get; set; }
        public DateTime StartDate { get; set; }
        public int PageSize { get; set; }
    }
}
