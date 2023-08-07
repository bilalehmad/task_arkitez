using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskProject.Models
{
    public class Products
    {

        [Key]
        public int ProdID { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }

        public int CatID { get; set; }
        public Categories Categories { get; set; }
    }
}
