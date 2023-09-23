using static JWTToken.Helper.Helper;

namespace JWTToken.Object
{
    public class EncodedObject
    {
        public class Payload
        {
            public string? aud { get; set; }
            public string? iss { get; set; }
            public string? sub { get; set; }
            public int exp { get; set; }
        }

        public class Response
        {
            public class Data
            {
                public string? jwtToken { get; set; }
            }

            public class ResponseEncoded
            {
                public MetaData? metaData { get; set; }
                public Data? data { get; set; }

            }
        }
    }
}
