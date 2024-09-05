using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace LibraryDeneme.Books
{
    public class BookAlreadyExistsException : BusinessException
    {
        public BookAlreadyExistsException(string name)
            : base(LibraryDenemeDomainErrorCodes.BookAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
