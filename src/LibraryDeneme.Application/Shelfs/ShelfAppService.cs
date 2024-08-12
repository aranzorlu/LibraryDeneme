using LibraryDeneme.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using LibraryDeneme.Permissions;


namespace LibraryDeneme.Shelfs
{
    public class ShelfAppService :
        CrudAppService<
            Shelf,
            ShelfDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateShelfDto>,
        IShelfAppService
    {
        public ShelfAppService(IRepository<Shelf,Guid> repository)
            : base(repository) 
        {
            GetPolicyName = LibraryDenemePermissions.Shelfs.Default;
            GetListPolicyName = LibraryDenemePermissions.Shelfs.Default;
            CreatePolicyName = LibraryDenemePermissions.Shelfs.Create;
            UpdatePolicyName = LibraryDenemePermissions.Shelfs.Edit;
            DeletePolicyName = LibraryDenemePermissions.Shelfs.Delete;
        }
    }
}
