using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VOTDC.ViewModels
{
    public class VerseViewModel
    {
        public Guid Id { get; set; }
        public string ImageLink { get; set; }
        public bool IsFavorite { get; set; }
        public string ReferenceText { get; set; }
        public string VerseText { get; set; }
        public DateTime VerseDate { get; set; }
    }
}
