namespace BackEnd_S7_L1.Models.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
