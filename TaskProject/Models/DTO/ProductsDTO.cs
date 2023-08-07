using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskProject.Models.DTO
{
    public class ProductsDTO
    {
        public int ProdID { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }

        public int CatID { get; set; }
        public string Category { get; set; }
    }
}
