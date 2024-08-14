using System;
using System.Reflection;
using System.Reflection.Metadata;
using Volo.Abp.Domain.Entities.Auditing;

namespace LibraryDeneme.Books;

public class Book : AuditedAggregateRoot<Guid>
{
    public Guid ShelfId { get; set; }  
    public Guid AuthorId { get; set; }
    
    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

    public float Price { get; set; }

    public FloorNumber Floor {  get; set; }


    
}