using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VOTDC.ViewModels
{
    public class FavoriteViewModel
    {
        public bool IsLoggedIn { get; set; }
        public Guid VerseId { get; set; }
        public bool IsFavorited { get; set; }
    }
}
