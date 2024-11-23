using GrpcService_CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace GrpcService_CRUD.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products => Set<Product>();
    }
}

