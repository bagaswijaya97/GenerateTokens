using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using static JWTToken.Credentials.Credentials;
using static JWTToken.Helper.Helper;
using static JWTToken.Object.EncodedObject;
using static JWTToken.Object.EncodedObject.Response;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncodedController : ControllerBase
    {
        // POST api/<EncodedController>
        [HttpPost]
        public ResponseEncoded Post([FromBody] Payload value)
        {
            #region Initial Variable

            string strPrivateKeyPem = "";
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            RSA objRSA;
            ResponseEncoded objResult = new ResponseEncoded();
            MetaData objMetaData = new MetaData();
            Data objData = new Data();

            #endregion

            #region Filtering

            // Remove headers and newlines
            strPrivateKeyPem = Encoded.privateKeyPem;
            strPrivateKeyPem = strPrivateKeyPem.Replace("-----BEGIN RSA PRIVATE KEY-----", "");
            strPrivateKeyPem = strPrivateKeyPem.Replace("-----END RSA PRIVATE KEY-----", "");
            strPrivateKeyPem = strPrivateKeyPem.Replace("\r", "").Replace("\n", "");

            #endregion

            #region Actions

            try
            {
                // Convert Base64 to bytes
                byte[] privateKeyBytes = Convert.FromBase64String(strPrivateKeyPem);

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64), // DateTime terbentuknya token
                new Claim(JwtRegisteredClaimNames.Exp, ToUnixEpochDate(DateTime.Now.AddHours(value.exp)).ToString(), ClaimValueTypes.Integer64), // Menentukan waktu expired token dalam satuan jam
                new Claim(JwtRegisteredClaimNames.Aud, value.aud ?? "DefaultAud"),
                new Claim(JwtRegisteredClaimNames.Iss, value.iss ?? "DefaultIss"),
                new Claim(JwtRegisteredClaimNames.Sub, value.sub ?? "DefaultSub"),
                };

                using (objRSA = RSA.Create())
                {
                    objRSA.ImportRSAPrivateKey(privateKeyBytes, out _);

                    var key = new RsaSecurityKey(objRSA);

                    var token = new JwtSecurityToken(
                        new JwtHeader(new SigningCredentials(key, SecurityAlgorithms.RsaSha256)),
                        new JwtPayload(claims)
                    );

                    jwtTokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = jwtTokenHandler.WriteToken(token);

                    objData.jwtToken = jwtToken;
                }

                objRSA.Dispose();

                objMetaData.code = Constan.CONST_RES_CD_SUCCESS;
                objMetaData.message = Constan.CONST_RES_MESSAGE_SUCCESS_TOKEN;

                objResult.metaData = objMetaData;
                objResult.data = objData;

                return objResult;

            }
            catch (Exception ex)
            {
                objMetaData.code = Constan.CONST_RES_CD_ERROR;
                objMetaData.message = Constan.CONST_RES_MESSAGE_ERROR + " " + ex;
                objData.jwtToken = null;

                objResult.metaData = objMetaData;
                objResult.data = objData;

                return objResult;
            }

            #endregion

        }
    }
}
