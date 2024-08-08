using Volo.Abp.Application.Dtos;

namespace LibraryDeneme.Authors;

public class GetAuthorListDto : PagedAndSortedResultRequestDto
{
    public string? Filter { get; set; }
}
