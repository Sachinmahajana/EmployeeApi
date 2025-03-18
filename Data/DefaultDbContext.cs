using EmployeeApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Data
{
    public class DefaultDbContext:DbContext     
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer(GetSqlServerConnection()));
        }
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options) : base(options)
        {

        }

        public DefaultDbContext()
        {
        }

        public DbSet<Employeecs> Employeecs { get; set; }
        public DbSet<Product> Products { get; set; }

            
        private static string GetSqlServerConnection()
        {
            SqlConnectionStringBuilder connectionbuilder = new()
            {
                DataSource = "DESKTOP-PA9CLIP\\SQLEXPRESS",
                InitialCatalog = "EmployeeManagement",
                TrustServerCertificate = true,
                IntegratedSecurity = true,
            };
            return connectionbuilder.ConnectionString;
        }
    }
}
