using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskProject.DBContext;
using TaskProject.Models;
using TaskProject.Models.DTO;

namespace TaskProject.DAL
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {

        private bool disposed = false;
        private readonly DBContextIdentity dBContext;

        public CategoryRepository(DBContextIdentity dBContext)
        {
            this.dBContext = dBContext;
        }
        public IEnumerable<Categories> GetCategories()
        {
            return dBContext.Categories.ToList();
        }
        public Categories GetCategoryByID(int id)
        {
            return dBContext.Categories.FirstOrDefault(x => x.CatID == id);
        }
        public void InsertCategory(Categories categories)
        {
            dBContext.Categories.Add(categories);
        }
        public void DeleteCategory(int id)
        {
            Categories cat = dBContext.Categories.FirstOrDefault(x => x.CatID == id);
            dBContext.Categories.Remove(cat);
        }
        public void UpdateCategory(Categories categories)
        {
            dBContext.Entry(categories).State = EntityState.Modified;
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
