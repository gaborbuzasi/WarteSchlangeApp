using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarteSchlange.API.Models
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<QueueEntryModel>().HasKey(x => new { x.QueueId, x.UserId });
        }

        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<ImagesModel> Images { get; set; }
        public DbSet<LogModel> Logs { get; set; }
        public DbSet<MetadataModel> Metadata { get; set; }
        public DbSet<OpeningTimeModel> OpeningTimes { get; set; }
        public DbSet<QueueModel> Queues { get; set; }
        public DbSet<QueueEntryModel> QueueEntries { get; set; }
        public DbSet<UserModel> Users { get; set; }
    }
}
