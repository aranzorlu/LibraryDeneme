﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraryDeneme.Inventorys
{
    public class ShelfLookupDto : EntityDto<Guid>
    {
        public string ShelfName { get; set; }
    }
}
