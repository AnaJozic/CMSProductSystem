using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSProductSystem.Models
{
    public class Proizvod
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Naziv je obavezan")]
        [Display(Name = "Naziv")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Opis je obavezan")]
        [Display(Name = "Opis")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Količina je obavezna")]
        [Display(Name = "Količina")]
        public int Kolicina { get; set; }

        [Required(ErrorMessage = "Cijena je obavezna")]
        [Display(Name = "Cijena")]
        public decimal Cijena { get; set; }


        [Required(ErrorMessage = "Kategorija je obavezna")]
        [Display(Name = "Kategorija")]
        public int CategoryID { get; set; }

        //[Required(ErrorMessage = "Slika je obavezna")]
        [Display(Name = "Slika")]
        [BindProperty]
        [NotMapped]
        public IFormFile? SlikaOdb { get; set; }

        //[Required(ErrorMessage = "Slika je obavezna")]
        [Display(Name = "Slika")]
        public string? Slika { get; set; }

        [NotMapped]
        public List<SelectListItem> KategorijaLista { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "-1", Text = "Odaberi"}
        };
        public Kategorija? Category { get; set; }
    }
}
