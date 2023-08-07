using System;
using System.Collections.Generic;
using TaskProject.Models;

namespace TaskProject.DAL
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<Categories> GetCategories();
        Categories GetCategoryByID(int CatID);
        void InsertCategory(Categories categories);
        void DeleteCategory(int CatID);
        void UpdateCategory(Categories categories);
        void Save();
    }
}
