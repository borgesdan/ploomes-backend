namespace Ploomes.Application.Contracts
{
    public class UserTokenResponse
    {        
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
        public string? ExpirationLongDate { get; set; }
    }
}
