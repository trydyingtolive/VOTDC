using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VOTDC.Models
{
    public class ApiResponse
    {
        public List<Verse> Verses { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public bool HasMorePages { get; set; }
        public int? Id { get; set; }
    }

}
