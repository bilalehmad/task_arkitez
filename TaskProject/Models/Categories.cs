using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskProject.Models
{
    public class Categories
    {
        [Key]
        public int CatID { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
