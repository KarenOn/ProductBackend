using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISSB.Web.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Nombre Categoria")]
        public string CategoryName { get; set; }
    }
}
