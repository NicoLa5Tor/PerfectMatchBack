namespace PerfectMatchBack.Models
{
    public class RecoverPass
    {
        public int IdRecover { get; set; }
        public int IdUser { get; set; }
        public string Token { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
