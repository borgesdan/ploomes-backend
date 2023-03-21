using Ploomes.Application.Data.Entities.Sql;
using Ploomes.Application.Data.Shared;

namespace Ploomes.Application.Contracts
{
    public class UserGetByUidResponse
    {
        public Guid Uid { get; set; }
        public string? PrimaryLogin { get; set; }
        public string? SecondaryLogin { get; set; }
        public AccessLevelType AccessLevel { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Name { get; set; }

        public UserGetByUidResponse(UserEntity entity)
        {
            Uid = entity.Uid;
            PrimaryLogin = entity.PrimaryLogin;
            SecondaryLogin = entity.SecondaryLogin;
            AccessLevel = entity.AccessLevel;
            CreationDate = entity.CreationDate;
            Name = entity.Person.Name;
        }
    }
}
