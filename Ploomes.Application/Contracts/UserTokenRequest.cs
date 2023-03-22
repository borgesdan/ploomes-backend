using Ploomes.Application.Data.Shared;

namespace Ploomes.Application.Contracts
{
    public class UserTokenRequest
    {
        public Guid UserUid { get; set; }
        public string? Email { get; set; }
        public AccessLevelType AccessLevel { get; set; }
    }
}
