using System.ComponentModel.DataAnnotations;

namespace BackEnd_S7_L1.Models.Dto.Request
{
    public class RegisterRequestDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Cognome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime Birthday { get; set; }
    }
}
