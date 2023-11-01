using System.ComponentModel.DataAnnotations;

namespace PerfectMatchBack.DTOs.RecoverPass
{
    public class NewPass
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public string Pass { get; set; } = string.Empty;
        [Required]
        [Compare("Pass")]
        public string Pass2 { get; set; } = string.Empty;
    }
}
