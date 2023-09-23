namespace JWTToken.Helper
{
    public class Helper
    {

        #region Function

        public static long ToUnixEpochDate(DateTime date)
        {
            return new DateTimeOffset(date).ToUnixTimeSeconds();
        }

        #endregion

        #region Contan

        public static class Constan
        {
            public const string CONST_RES_CD_SUCCESS = "200";
            public const string CONST_RES_CD_ERROR = "400";

            public const string CONST_RES_MESSAGE_SUCCESS = "Success !";
            public const string CONST_RES_MESSAGE_ERROR = "Error !";
            public const string CONST_RES_MESSAGE_ERROR_EMPTY = "Parameter Must Be Not Empty !";

            public const string CONST_RES_MESSAGE_SUCCESS_TOKEN = "Success Generate Token!";

        }

        #endregion

        #region Object

        public class MetaData
        {
            public string? code { get; set; }
            public string? message { get; set; }

        }

        #endregion

    }
}
