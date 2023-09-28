namespace PortFolio.Models
{
    public class JWT
    {
        public string Secret { get; set; }

        public string ValidAudience { get; set; }

        public string ValidIssuer { get; set; }

    }
}
