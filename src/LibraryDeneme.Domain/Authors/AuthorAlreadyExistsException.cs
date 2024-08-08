using Volo.Abp;

namespace LibraryDeneme.Authors;

public class AuthorAlreadyExistsException : BusinessException
{
    public AuthorAlreadyExistsException(string name)
        : base(LibraryDenemeDomainErrorCodes.AuthorAlreadyExists)
    {
        WithData("name", name);
    }
}
