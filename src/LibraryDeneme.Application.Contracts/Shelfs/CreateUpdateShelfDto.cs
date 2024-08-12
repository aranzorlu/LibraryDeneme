using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LibraryDeneme.Books;

namespace LibraryDeneme.Shelfs
{
    public class CreateUpdateShelfDto
    {
        [Required]
        [StringLength(128)]

        public string ShelfName { get; set; } = string.Empty;

        [Required]

        public SType ShelfType { get; set; } = SType.Undefined;

    }
}
