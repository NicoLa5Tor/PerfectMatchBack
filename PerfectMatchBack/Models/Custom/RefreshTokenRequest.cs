namespace PerfectMatchBack.Models.Custom
{
    public class RefreshTokenRequest
    {
        public string TokenExpire { get; set; }
        public string RefreshToken { get; set; }
    }
}
