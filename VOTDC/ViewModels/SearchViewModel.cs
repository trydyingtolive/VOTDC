﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VOTDC.ViewModels
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
        }
        public SearchViewModel(DateTime startDate, int pageSize)
        {
            StartDate = startDate;
            PageSize = pageSize;
        }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(1, 100)]
        public int PageSize { get; set; }
    }
}
