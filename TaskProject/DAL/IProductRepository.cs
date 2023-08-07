using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskProject.Models;

namespace TaskProject.DAL
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Products> GetProducts();
        Products GetProductByID(int ProdID);
        void InsertProduct(Products Products);
        void DeleteProduct(int ProdID);
        void UpdateProduct(Products Products);
        void Save();
    }
}
