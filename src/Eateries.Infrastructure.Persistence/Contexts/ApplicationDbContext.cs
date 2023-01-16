﻿using Eateries.Application.Interfaces;
using Eateries.Domain.Common;
using Eateries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Eateries.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IDateTimeService _dateTime;

        public ApplicationDbContext(
            ILoggerFactory loggerFactory,
            IDateTimeService dateTime)
            : base()
        {
            this._loggerFactory = loggerFactory;
            this._dateTime = dateTime;
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /* var _mockData = this.Database.GetService<IMockService>();
             var seedPositions = _mockData.SeedPositions(1000);
             builder.Entity<Position>().HasData(seedPositions);
 */
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EateriesDb;Integrated Security=True;");
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}