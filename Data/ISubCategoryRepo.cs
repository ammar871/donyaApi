using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface ISubCategoryRepo
    {
        
        
        bool SaveChanges();
        void CreateSubCategory(SubCategory ct);
        IEnumerable<SubCategory> GetAll();

        void UpdateSubCategory(SubCategory cmd);
        void DeleteSubCategory(SubCategory cmd);

        SubCategory GetSubCategoryById(int id);
         IEnumerable<SubCategory> GetSubCategoryByCategoryId(int id);
    }
}