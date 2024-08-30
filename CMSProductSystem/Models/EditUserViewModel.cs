using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSProductSystem.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Ime je obavezno")]
        [Display(Name = "Ime")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno")]
        [Display(Name = "Prezime")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Adresa je obavezna")]
        [Display(Name = "Adresa")]
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Grad je obavezan")]
        [Display(Name = "Grad")]
        public string Grad { get; set; }

        [Required(ErrorMessage = "Telefon je obavezan")]
        [Display(Name = "Telefon")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zanimanje je obavezan podatak")]
        [Display(Name = "Zanimanje")]
        public string Zanimanje { get; set; }
        

        [Required(ErrorMessage = "Šifra je obavezna")]
        [DataType(DataType.Password)]
       
        public int Aktivan { get; set; }

        public List<SelectListItem> Aktivnosti { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "-1", Text = "Odaberi"},
            new SelectListItem { Value = "1", Text = "Aktivan"},
            new SelectListItem { Value = "0", Text = "Neaktivan"}
        };

        public List<string> PopisRola { get; set; } = new List<string>
        {

        };

    }
}
