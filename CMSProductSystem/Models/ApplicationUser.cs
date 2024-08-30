using Microsoft.AspNetCore.Identity;

namespace CMSProductSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
        public string Telefon { get; set; }
        public int Aktivan { get; set; }

        public string Zanimanje { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
