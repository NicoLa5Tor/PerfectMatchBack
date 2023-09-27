namespace PerfectMatchBack.Models.Response
{
    public class Response
    {
        public int success { get; set; } = 0;
        public string messageError { get; set; }
        public object data { get; set; }
    }
}
