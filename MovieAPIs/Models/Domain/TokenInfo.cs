namespace PortFolio.Models.Domain
{
    public class TokenInfo
    {

        public int Id { get; set; }

        public string username { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiry { get; set; }
    }
}
