using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class SqlCategoryRepo : ICategoryRepo
    {


        private readonly CommanderContext _context;

        public SqlCategoryRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCategory(Category ct)


        {

            
                if (ct == null)
                {
                    throw new ArgumentNullException(nameof(ct));
                }

                _context.Categories.Add(ct);
            

        }

        public void DeleteCategory(Category cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Categories.Remove(cmd);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCategory(Category cmd)
        {

        }
        
    }
}