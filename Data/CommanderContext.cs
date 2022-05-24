using System;
using Commander.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{



    public class CommanderContext : IdentityDbContext<User>
    {

        public CommanderContext(DbContextOptions<CommanderContext> otp) : base(otp)
        {

        }





        public DbSet<Command> Commands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<SubCategory> SubCategories { get; set; }

        public DbSet<Adds> Adds { get; set; }
        public DbSet<CategoryProfission> CategoryProfession { get; set; }

        public DbSet<Profession> AddsProfession { get; set; }


         public DbSet<professional> Professioners { get; set; }



        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Driver_Field> Driver_Fields { get; set; }
        public DbSet<Driver_Order> Driver_Orders { get; set; }

        internal object Where(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }

}