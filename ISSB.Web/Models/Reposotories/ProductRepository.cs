using ISSB.Web.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISSB.Web.Models.Reposotories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository 
    {
        public ProductRepository(DataContext context) : base(context)
        {

        }
    }
}
