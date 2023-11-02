namespace MyProject.Service
{
    public class TokenOptions
    {
        public string SecurityKey { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
    }
}