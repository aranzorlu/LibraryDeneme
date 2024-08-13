using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace LibraryDeneme.Shelfs
{
    public class ShelfAlreadyExistsException : BusinessException
    {
        public ShelfAlreadyExistsException(string shelfname)
            : base(LibraryDenemeDomainErrorCodes.ShelfAlreadyExists)
        {
            WithData("name", shelfname);
        }
    }
}
