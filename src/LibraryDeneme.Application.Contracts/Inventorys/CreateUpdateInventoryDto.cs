using LibraryDeneme.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDeneme.Inventorys
{
    public class CreateUpdateInventoryDto
    {
        [Required]

        public BolumType Bolum { get; set; }

        [Required]

        public FloorNumber Floor { get; set; }

        [Required]

        public string SerialNo { get; set; }  

        public Guid BookId { get; set; }

        public Guid ShelfId { get; set; }

        [Required]
        public StatuType BookStatu { get; set; }    

       
       
    }
}
