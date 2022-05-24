using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public interface IAddRepo
    {

        bool SaveChanges();
        void CreateAdd(Adds ct);
        IEnumerable<Adds> GetAll();

        void UpdateAdds(Adds cmd);
        void DeleteAdds(Adds cmd);

        Adds GetAddById(int id);
        IEnumerable<Adds> GetAddsByCategoryId(int id);

        General GetCounter();
    }
}