using PerfectMatchBack.DTOs;

namespace PerfectMatchBack.Models.Response
{
    public class UserAccessDTO
    {
        public UserDTO userDto { get; set; }
        public AccessDTO accessDto { get; set; }
    }
}
