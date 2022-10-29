using Microsoft.EntityFrameworkCore;
using SpinitTest.Entities;

namespace SpinitTest.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }
        public DbSet<HistoryLogEntity> StateSearch { get; set; }
    }
}
