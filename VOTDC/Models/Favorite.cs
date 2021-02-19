using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VOTDC.Models
{
    [Index(nameof(UserId))]
    [Index(nameof(VerseId))]
    [Index(nameof(UserId), nameof(VerseId))]
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int VerseId { get; set; }
        public Verse Verse { get; set; }

        public DateTime CreatedDateTime { get; set; }
    }
}
