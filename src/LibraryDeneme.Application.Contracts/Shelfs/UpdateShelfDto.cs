using LibraryDeneme.Authors;
using LibraryDeneme.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDeneme.Shelfs
{
    public class UpdateShelfDto
    {
        [Required]
        [StringLength(ShelfConsts.MaxShelfNameLength)]
        public string ShelfName { get; set; } = string.Empty;

        [Required]
        public SType  ShelfType { get; set; }

        [Required]
        public BolumType ShelfBolum {  get; set; }  

       
    }
}
