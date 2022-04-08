using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISSB.Web.Models.ViewModel
{
    public class ProductViewModel : Product
    {
        [Display(Name = "Imagen Producto")]
        public IFormFile ImageFile { get; set; }
    }
}
