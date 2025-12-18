using System.ComponentModel.DataAnnotations;

namespace BackEnd_S7_L1.Models.Dto.Request
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
