﻿using System;
using Volo.Abp.Application.Dtos;

namespace LibraryDeneme.Books;

public class BookDto : AuditedEntityDto<Guid>
{
         
    public Guid AuthorId { get; set; }

    public string AuthorName { get; set; }
    public string Name { get; set; }

    public BookType Type { get; set; }

    public DateTime PublishDate { get; set; }

}