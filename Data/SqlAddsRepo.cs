using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data
{
    public class SqlAddsRepo : IAddRepo
    {


        private readonly CommanderContext _context;

        public SqlAddsRepo(CommanderContext context)
        {
            _context = context;
        }


        public void CreateAdd(Adds ct)
        {
            if (ct == null)
            {
                throw new ArgumentNullException(nameof(ct));
            }

            _context.Adds.Add(ct);
        }

        public void DeleteAdds(Adds cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Adds.Remove(cmd);
        }



        public IEnumerable<Adds> GetAddsByCategoryId(int id)
        {
            List<Adds> items = _context.Adds.ToList();
            List<Adds> newList = new List<Adds>();

            foreach (Adds add in items)
            {

                if (id == add.CategoryId)
                {

                    newList.Add(add);

                }



            }

            return newList.ToList();
        }

        public IEnumerable<Adds> GetAll()
        {
            return _context.Adds.ToList();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateAdds(Adds cmd)
        {

        }



        public Adds GetAddById(int id)
        {
            return _context.Adds.FirstOrDefault(p => p.Id == id);
        }

        public General GetCounter()
        {
            General general=new General{
             CounterAdds =_context.Adds.ToList().Count+"" ,
             CounterCategories =_context.Categories.ToList().Count+"",
             CounterSub =_context.SubCategories.ToList().Count+""
             

            };
           
            return general;
        }
    }
}