using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VOTDC.Models
{
    [Index(nameof(ResourseId))]
    public class Verse
    {
        [Key]
        public int Id { get; set; }
        public Guid ResourseId { get; set; }
        public string Book { get; set; }
        public string Chapter { get; set; }
        public string Verses { get; set; }
    }
}
