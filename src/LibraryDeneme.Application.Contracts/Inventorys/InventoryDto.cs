using LibraryDeneme.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace LibraryDeneme.Inventorys
{
    public class InventoryDto : AuditedEntityDto<Guid>
    {
        public Guid ShelfId { get; set; }

        public string ShelfName { get; set; }
        public FloorNumber Floor { get; set; }   

        public BolumType Bolum { get; set; }

        public string SerialNo { get; set; }
		public string AuthorName { get; set; }

        public Guid BookId { get; set; }

        public string BookName { get; set; }

        public StatuType BookStatu { get; set; }

        public BookType BookType { get; set; }

        public DateTime PublishDate { get; set; }

        
	}
}
