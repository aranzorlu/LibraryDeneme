﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraryDeneme.Books
{
    public class GetBookListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
