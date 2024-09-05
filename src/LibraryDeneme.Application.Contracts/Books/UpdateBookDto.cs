using LibraryDeneme.Authors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LibraryDeneme.Books
{
    public class UpdateBookDto
    {
        [Required]
        [StringLength(AuthorConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public BookType Type { get; set; }

        public Guid AuthorId { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }
    }
}
