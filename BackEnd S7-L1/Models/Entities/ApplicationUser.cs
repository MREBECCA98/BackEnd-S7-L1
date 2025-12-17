using Microsoft.AspNetCore.Identity;

namespace BackEnd_S7_L1.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }


        public DateTime CreateAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime Birthday { get; set; }
    }
}
