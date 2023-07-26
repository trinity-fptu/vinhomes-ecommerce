using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.CustomMiddleware
{
    public class FirebaseAuthentication
    {
        private readonly string _firebaseProjectId;

        public FirebaseAuthentication(string firebaseProjectId)
        {
            _firebaseProjectId = firebaseProjectId;
        }

        public ClaimsPrincipal VerifyIdToken(string idToken)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = $"https://securetoken.google.com/{_firebaseProjectId}",
                ValidateAudience = true,
                ValidAudience = _firebaseProjectId,
                ValidateLifetime = true,
                IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                {
                    var json = new WebClient().DownloadString(parameters.ValidIssuer + "/.well-known/jwks.json");
                    var jwks = JsonConvert.DeserializeObject<Jwks>(json);
                    return (IEnumerable<SecurityKey>)jwks.Keys.Select(key =>
                        new RsaSecurityKey(
                            new RSAParameters
                            {
                                Modulus = Base64UrlEncoder.DecodeBytes(key.N),
                                Exponent = Base64UrlEncoder.DecodeBytes(key.E)
                            })
                        { KeyId = key.Kid });
                }
            };

            try
            {
                var validatedToken = new JwtSecurityTokenHandler()
                    .ValidateToken(idToken, validationParameters, out var rawValidatedToken);

                return new ClaimsPrincipal(new ClaimsIdentity(validatedToken.Claims));
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }
    }

    public class Jwks
    {
        [JsonProperty("keys")]
        public List<JsonWebKey> Keys { get; set; }
    }

    public class JsonWebKey
    {
        [JsonProperty("kty")]
        public string Kty { get; set; }

        [JsonProperty("use")]
        public string Use { get; set; }

        [JsonProperty("kid")]
        public string Kid { get; set; }

        [JsonProperty("alg")]
        public string Alg { get; set; }

        [JsonProperty("n")]
        public string N { get; set; }

        [JsonProperty("e")]
        public string E { get; set; }
    }
}
