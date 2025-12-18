using System.ComponentModel.DataAnnotations;

namespace BackEnd_S7_L1.Models.Dto.Request
{
    //Request = quello che TU mandi al server.
    public class StudentRequestDto
    {
        [Required]
        public Guid Id { get; set; }

        public string? Nome { get; set; }


        public string? Cognome { get; set; }


        public string? Email { get; set; }
    }
}
