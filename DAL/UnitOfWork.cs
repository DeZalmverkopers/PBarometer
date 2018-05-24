using DAL.EF;

namespace DAL
{
  public class UnitOfWork
  {
    private DbContext context;

    internal DbContext Context
    {
      get
      {
        if (context == null) context = new DbContext(true);
        return context;
      }
    }

    public void CommitChanges()
    {
      context.CommitChanges();
    }
  }
}
