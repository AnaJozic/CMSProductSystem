using System.ComponentModel.DataAnnotations;

namespace CMSProductSystem.Models
{
    public class Kategorija
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Naziv je obavezan")]
        [Display(Name = "Naziv")]
        public string Naziv { get; set; }

        public ICollection<Proizvod> Product { get; set; } = new List<Proizvod>();
    }
}
