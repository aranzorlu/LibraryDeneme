using LibraryDeneme.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraryDeneme.Shelfs
{
    public class ShelfDto : AuditedEntityDto<Guid>
    {
        public string ShelfName { get; set; }

        public SType ShelfType { get; set; }

        public BolumType ShelfBolum {  get; set; }

       
    }
}
