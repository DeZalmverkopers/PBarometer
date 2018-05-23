namespace DAL.EF
{
  //Configuratie van de databank.
  internal class DbConfiguration : System.Data.Entity.DbConfiguration
  {
    public DbConfiguration()
    {
      SetDefaultConnectionFactory(new System.Data.Entity.Infrastructure.SqlConnectionFactory());
      SetProviderServices("System.Data.SqlClient", System.Data.Entity.SqlServer.SqlProviderServices.Instance);
      SetDatabaseInitializer(new DbInitializer());
    }
  }
}
