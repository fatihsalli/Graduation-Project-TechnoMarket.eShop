namespace TechnoMarket.Services.Catalog.UnitOfWorks.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();
    }
}
