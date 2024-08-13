using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraryDeneme.Shelfs
{
    public class GetShelfListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
