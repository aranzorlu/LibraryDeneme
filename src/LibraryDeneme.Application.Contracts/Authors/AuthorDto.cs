﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraryDeneme.Authors
{
    public class AuthorDto : AuditedEntityDto<Guid>

    {
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string ShortBio {  get; set; }

    }
}
