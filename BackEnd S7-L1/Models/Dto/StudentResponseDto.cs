namespace BackEnd_S7_L1.Models.Dto
{
    //Response = quello che IL SERVER ti manda indietro.
    public class StudentResponseDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
    }
}
