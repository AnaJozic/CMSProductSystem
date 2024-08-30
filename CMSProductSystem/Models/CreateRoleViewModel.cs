using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMSProductSystem.Models
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Unos imena role je obavezan!")]
        public string RoleName { get; set; }
    }
}
