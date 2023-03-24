using Data.Configuration;
using Data.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Domain
{

    public class CompanyContext : IdentityDbContext<User>
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                string tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            // seed data
            var Id1 = Guid.NewGuid().ToString();
            var Id2 = Guid.NewGuid().ToString();
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = Id1, Name = "IT" },
                new Department { Id = Id2, Name = "Sale" }
            );

            modelBuilder.Entity<Employee>().HasData(new Employee
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Uncle",
                LastName = "Bob",
                Email = "uncle.bob@gmail.com",
                DateOfBirth = new DateTime(1979, 04, 25),
                PhoneNumber = "999-888-7777",
                DepartmentId = Id1,
            }, new Employee
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = "Jan",
                LastName = "Kirsten",
                Email = "jan.kirsten@gmail.com",
                DateOfBirth = new DateTime(1981, 07, 13),
                PhoneNumber = "111-222-3333",
                DepartmentId = Id2,
            });


            modelBuilder.Entity<Employee>(buildEntity =>
            {
                buildEntity.InsertUsingStoredProcedure("Employees_Insert", buildAction =>
                {
                    buildAction.HasParameter("Id")
                            .HasParameter("FirstName")
                            .HasParameter("LastName")
                            .HasParameter("DateOfBirth")
                            .HasParameter("PhoneNumber")
                            .HasParameter("Email")
                            .HasParameter("DepartmentId", b => b.HasName("DepartmentName"));
                });
                buildEntity.UpdateUsingStoredProcedure("Employees_Update", buildAction =>
                {
                    buildAction.HasOriginalValueParameter("Id")
                            .HasParameter("FirstName")
                            .HasParameter("LastName")
                            .HasParameter("DateOfBirth")
                            .HasParameter("PhoneNumber")
                            .HasParameter("Email")
                            .HasParameter("DepartmentId", b => b.HasName("DepartmentName"));
                });

            });
            modelBuilder.Entity<Department>(buildEntity =>
            {
                buildEntity.InsertUsingStoredProcedure("Departments_Insert", b => b.HasParameter("Id").HasParameter("Name"));
                buildEntity.UpdateUsingStoredProcedure("Departments_Update", b => b.HasOriginalValueParameter("Id").HasParameter("Name"));
            });
        }
    }
}