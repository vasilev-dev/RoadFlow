namespace RoadFlow.Common.Configurations.Jwt
{
    public class JwtConfiguration
    {
        public bool ValidateIssuerSigningKey { get; set; }
        
        /// <summary>
        /// From RoadFlow.Common secrets
        /// </summary>
        public string IssuerSigningKey { get; set; }
        
        public bool ValidateIssuer { get; set; } = true;
        
        public string ValidIssuer { get; set; }
        
        public bool ValidateAudience { get; set; } = true;
        public string ValidAudience { get; set; }
        
        public bool RequireExpirationTime { get; set; }
        
        public bool ValidateLifetime { get; set; } = true;
        
        public int AccessTokenLifetimeInMinutes { get; set; }
        
        public int RefreshTokenLifetimeInDays { get; set; }
    }
}