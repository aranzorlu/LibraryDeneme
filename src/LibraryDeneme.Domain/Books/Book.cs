using LibraryDeneme.Authors;
using System;
using System.Reflection;
using System.Reflection.Metadata;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Emailing;

namespace LibraryDeneme.Books;

public class Book : AuditedAggregateRoot<Guid>
{
      
    public Guid AuthorId { get; set; }
    
    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

   /* public float Price { get; set; }

    public FloorNumber Floor {  get; set; }

    public BolumType Bolum { get; set; }

    public StatuType BookStatu { get; set; }
    public Guid ShelfId { get; set; }
*/
    private Book()
    {

    }
    internal Book(Guid id,Guid authorId, string name, BookType type, DateTime publishDate)
    {
        AuthorId = authorId;
        SetName(name);
        Type = type;
        PublishDate = publishDate;
    }
    internal Book ChangeName(string name)
    {
        SetName(name);
        return this;
    }
    private void SetName(string name) 
    {
        Name = Check.NotNullOrWhiteSpace(
             name,
             nameof(name),
             maxLength: AuthorConsts.MaxNameLength
         );
    }




}