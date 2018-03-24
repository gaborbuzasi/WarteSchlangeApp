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


        public DbSet<CompanyModel> Companies { get; set; }
        public DbSet<QueueModel> Queues { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<LogModel> Logs { get; set; }


    }
}
