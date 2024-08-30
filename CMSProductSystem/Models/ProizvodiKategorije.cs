using System.ComponentModel.DataAnnotations;
namespace CMSProductSystem.Models
{
    public class ProizvodiKategorije
    {
        public Proizvod ProizvodPodaci { get; set; }

        [Display(Name = "Kategorija")]

        public int IDKategorije { get; set; }

        [Display(Name = "Kategorija")]
        public string NazivKategorije { get; set; }
    }
}
