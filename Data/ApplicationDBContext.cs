using ApiLoginToken.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiLoginToken.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<User>Users { get; set; }


    }
}
