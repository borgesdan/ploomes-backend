using Ploomes.Application.Data.Entities.Sql;
using Ploomes.Application.Data.Shared;

namespace Ploomes.Application.Contracts
{
    public class UserGetByUidResponse
    {
        public Guid Uid { get; set; }
        public string? Email { get; set; }
        public AccessLevelType AccessLevel { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Name { get; set; }

        public UserGetByUidResponse(UserEntity entity)
        {
            Uid = entity.Uid;
            Email = entity.Email;
            AccessLevel = entity.AccessLevel;
            CreationDate = entity.CreationDate;
            Name = entity.Person.FullName;
        }
    }
}
