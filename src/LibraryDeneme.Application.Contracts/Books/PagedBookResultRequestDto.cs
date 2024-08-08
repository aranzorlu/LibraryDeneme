using Volo.Abp.Application.Dtos;

namespace LibraryDeneme.Books
{
    public class PagedBookResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
