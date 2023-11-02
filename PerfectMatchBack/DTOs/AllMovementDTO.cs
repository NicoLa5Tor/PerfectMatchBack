namespace PerfectMatchBack.DTOs
{
    public class MovementDTO
    {
        public int IdMovement { get; set; }
        public int? IdSeller { get; set; }
        public int? IdBuyer { get; set; }
        public int? IdPublication { get; set; }
        public string Date { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
        public string Publication { get; set; }
        public string Amount { get; set; }
    }
}
