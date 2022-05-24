using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ICategoryRepo
    {
        bool SaveChanges();
        void CreateCategory(Category ct);
        IEnumerable<Category> GetAll();

        void UpdateCategory(Category cmd);
        void DeleteCategory(Category cmd);

        Category GetCategoryById(int id);




    }
}