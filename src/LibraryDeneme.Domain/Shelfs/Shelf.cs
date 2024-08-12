
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace LibraryDeneme.Shelfs
{
    public class Shelf : AuditedAggregateRoot<Guid>
    {
        public string ShelfName { get; set; }
        public SType ShelfType {  get; set; }
    }
}
