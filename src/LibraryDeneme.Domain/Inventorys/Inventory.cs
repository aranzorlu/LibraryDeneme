using LibraryDeneme.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace LibraryDeneme.Inventorys;

    public class Inventory : FullAuditedAggregateRoot<Guid>
    {
        public Guid ShelfId { get; set; }
        
        public Guid BookId { get; set; }
        public FloorNumber Floor {  get; set; }

        public BolumType Bolum { get; set; }

        public string SerialNo { get; set; }

        public StatuType BookStatu {  get; set; }
    }

