using LibraryDeneme.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace LibraryDeneme.Inventorys
{
    public class BookLookupDto : EntityDto<Guid>
	{
		public string Name { get; set; }
		
	}
}
