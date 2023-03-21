namespace Ploomes.Application.Data.Shared
{
    /// <summary>Específica o tipo de acesso do usuário do sitema.</summary>
    public enum AccessLevelType
    {
        Anonymous,
        Buyer,
        Seller,
        Admin,
        SuperAdmin
    }

    /// <summary>Classe auxíliar para verificar se determinado tipo de acesso está dentro de uma condição.</summary>
    public struct AccessLevelReader
    {
        public static bool IsSeller(AccessLevelType accessLevelType)
            => accessLevelType == AccessLevelType.Seller
            || IsAdmin(accessLevelType);

        public static bool IsAdmin(AccessLevelType accessLevelType)
            => accessLevelType == AccessLevelType.Admin
            || accessLevelType == AccessLevelType.SuperAdmin;
    }
}