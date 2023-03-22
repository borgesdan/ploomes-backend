using Ploomes.Application.Data.Entities.Sql;

namespace Ploomes.Application.Contracts
{
    public class UserPostResponse
    {
        public string? Uid { get; set; }        

        public UserPostResponse(UserEntity user) 
        {
            Uid = user.Uid.ToString().ToLower();
        }
    }
}