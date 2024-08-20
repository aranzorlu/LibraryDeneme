using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryDeneme.Books;

public class CreateUpdateBookDto
{
    public Guid ShelfId { get; set; }
    public Guid AuthorId { get; set; }

    [Required]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public BookType Type { get; set; } = BookType.Undefined;

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishDate { get; set; } = DateTime.Now;

    [Required]
    public float Price { get; set; }

    [Required]

    public FloorNumber Floor { get; set; } = FloorNumber.Kat1;

    [Required]

    public BolumType Bolum {  get; set; } 

}
    