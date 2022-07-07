using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ABC_Bank.DBContext.Interfaces
{
    public abstract class DatabaseContext<TContext, TModel> : DbContext where TContext : DbContext where TModel : class
    {
        public DbSet<TModel> DbSet
        {
            get;
            private set;
        }

        public DatabaseContext() : base (GetConnection(), false)
        {
            Database.SetInitializer<TContext>(null);
            this.DbSet = base.Set<TModel>();
        }

        public static DbConnection GetConnection(string connectionString = "ABC")
        {
            var connection = ConfigurationManager.ConnectionStrings[connectionString];
            var factory = DbProviderFactories.GetFactory(connection.ProviderName);
            var dbCon = factory.CreateConnection();
            dbCon.ConnectionString = connection.ConnectionString;
            return dbCon;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
