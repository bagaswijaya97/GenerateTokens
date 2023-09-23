using System.IdentityModel.Tokens.Jwt;
using static JWTToken.Helper.Helper;

namespace JWTToken.Object
{
    public class DecodedObject
    {
        public class Payload
        {
            public string? jwtToken { get; set; }
        }

        public class Response
        {
            public class Data
            {
                public object? Header { get; set; }
                public JwtPayload? Payload { get; set; }
                public bool? IsExpired { get; set; }
                public string?  ExpiryDate { get; set; }
            }
            public class ResponseDecoded
            {
                public MetaData? metaData { get; set; } 
                public Data? data { get; set; }
            }
        }
    }
}
