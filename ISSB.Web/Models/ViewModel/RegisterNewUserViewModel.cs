using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISSB.Web.Models.ViewModel
{
    public class RegisterNewUserViewModel
    {
        [Required]
        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Display(Name = "Direccion")]
        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string Address { get; set; }

        [Display(Name = "Telefono")]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters length.")]
        public string PhoneNumber { get; set; }

        //[Display(Name = "City")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a city.")]
        //public int CityId { get; set; }

        //public IEnumerable<SelectListItem> Cities { get; set; }

        //[Display(Name = "Country")]
        //[Range(1, int.MaxValue, ErrorMessage = "You must select a country.")]
        //public int CountryId { get; set; }

        //public IEnumerable<SelectListItem> Countries { get; set; }

        [Required]
        [MinLength(8)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [Display(Name = "Confirmar Contraseña")]
        public string Confirm { get; set; }
    }
}
