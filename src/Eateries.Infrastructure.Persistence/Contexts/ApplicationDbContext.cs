using Eateries.Application.Interfaces;
using Eateries.Domain.Common;
using Eateries.Domain.Entities;
using Eateries.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eateries.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IDateTimeService _dateTime;

        public ApplicationDbContext() : base()
        {
        }

        public ApplicationDbContext(
            ILoggerFactory loggerFactory,
            IDateTimeService dateTime)
            : base()
        {
            this._loggerFactory = loggerFactory;
            this._dateTime = dateTime;
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cuisine> Cuisines { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<DishIngredient> DishIngredients { get; set; }
        public DbSet<Eatery> Eateries { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuDish> MenuDishes { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DishIngredient>()
                .HasKey(di => new { di.DishId, di.IngredientId });

            modelBuilder.Entity<MenuDish>()
                .HasKey(md => new { md.DishId, md.MenuId });
            
            modelBuilder.Entity<MenuDish>()
                .HasOne(md => md.Dish)
                .WithMany(md => md.MenuDishes)
                .HasForeignKey(md => md.DishId);

            modelBuilder.Entity<MenuDish>()
                .HasOne(md => md.Menu)
                .WithMany(md => md.MenuDishes)
                .HasForeignKey(md => md.MenuId);

            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.DishId);

            modelBuilder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishIngredients)
                .HasForeignKey(di => di.IngredientId);
            
            modelBuilder.Entity<Dish>()
                .HasOne(d => d.Cuisine)     // specifies the navigation property to the Cuisine entity
                .WithMany(c => c.Dishes)    // specifies the collection navigation property to the Dish entity
                .HasForeignKey(d => d.CuisineId); 
            
            modelBuilder.Entity<Address>()
                .HasOne(a => a.Eatery)
                .WithMany(e => e.Addresses)
                .HasForeignKey(a => a.EateryId);

            modelBuilder.Entity<Menu>()
                .HasOne(mu => mu.Eatery)
                .WithMany(mu => mu.Menus)
                .HasForeignKey(mu => mu.EateryId);
            
            modelBuilder.Entity<Eatery>()
                .Property(e => e.EateryType)
                .HasConversion(
                    v => v.ToString(),
                    v => (EateryType)Enum.Parse(typeof(EateryType), v));
            
            modelBuilder.Entity<Cuisine>().HasData(
                new Cuisine { Id = new Guid("a5b0f2b7-5e98-4a7c-836f-0c7a1b9a88b7"), CuisineName = "European"},
                new Cuisine { Id = new Guid("c58f23af-65e4-4ca1-9fb4-6b1dfc7a48a8"), CuisineName = "Armenian"},
                new Cuisine { Id = new Guid("f43a5871-1037-4ef5-ae6d-d08e1b5c7461"), CuisineName = "Russian"}
            );
            
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EateriesDb;" +
                                        "Integrated Security=True;");
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }
    }
}