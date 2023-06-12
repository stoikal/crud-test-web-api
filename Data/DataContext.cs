using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
  }
}