using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskProject.DBContext;
using TaskProject.Models;

namespace TaskProject.DAL
{
    public class ProductRepository : IProductRepository
    {

        private bool disposed = false;
        private readonly DBContextIdentity dBContext;

        public ProductRepository(DBContextIdentity dBContext)
        {
            this.dBContext = dBContext;
        }
        public IEnumerable<Products> GetProducts()
        {
            return dBContext.Products.Include(x => x.Categories).ToList();
        }
        public Products GetProductByID(int id)
        {
            Products pro =  dBContext.Products.Include(x => x.Categories).FirstOrDefault(x => x.ProdID == id);
            return pro;
        }
        public void InsertProduct(Products Products)
        {
            dBContext.Products.Add(Products);
        }
        public void DeleteProduct(int id)
        {
            Products pro = dBContext.Products.FirstOrDefault(x => x.ProdID == id);
            dBContext.Products.Remove(pro);
        }
        public void UpdateProduct(Products Products)
        {
            dBContext.Entry(Products).State = EntityState.Modified;
        }
        public void Save()
        {
            dBContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dBContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
