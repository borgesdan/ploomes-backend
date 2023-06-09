﻿using Ploomes.Application.Data.Entities.Sql;
using Ploomes.Application.Data.Shared;

namespace Ploomes.Application.Contracts
{
    public class UserGetByUidResponse
    {
        public string? Uid { get; set; }
        public string? Email { get; set; }
        public string? AccessLevel { get; set; }
        public DateTime CreationDate { get; set; }
        public string? Name { get; set; }

        public UserGetByUidResponse(UserEntity entity)
        {
            Uid = entity.Uid.ToString().ToLower();
            Email = entity.Email;
            AccessLevel = entity.AccessLevel == AccessLevelType.Buyer ? "comprador" : "vendedor";
            CreationDate = entity.CreationDate;
            Name = entity.Person.FullName;
        }
    }
}
