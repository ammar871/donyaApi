using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class SqlSubCategoryRepo : ISubCategoryRepo
    {

        private readonly CommanderContext _context;

        public SqlSubCategoryRepo(CommanderContext context)
        {
            _context = context;
        }


        public void CreateSubCategory(SubCategory ct)
        {
            if (ct == null)
            {
                throw new ArgumentNullException(nameof(ct));
            }

            _context.SubCategories.Add(ct);
        }



        public void DeleteSubCategory(SubCategory cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.SubCategories.Remove(cmd);
        }



        public IEnumerable<SubCategory> GetAll()
        {
            return _context.SubCategories.ToList();
        }

        public IEnumerable<SubCategory> GetSubCategoryByCategoryId(int id)
        {
            List<SubCategory> items = _context.SubCategories.ToList();
            List<SubCategory> newList = new List<SubCategory>();
            foreach (SubCategory subCategory in items)
            {

                newList.Add(_context.SubCategories.FirstOrDefault(p => p.CategoryId == id));

            }

            return newList.ToList();

        }

        public SubCategory GetSubCategoryById(int id)
        {
            return _context.SubCategories.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateSubCategory(SubCategory cmd)
        {

        }
    }
}