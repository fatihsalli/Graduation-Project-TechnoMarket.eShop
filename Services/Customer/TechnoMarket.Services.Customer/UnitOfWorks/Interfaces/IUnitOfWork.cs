namespace TechnoMarket.Services.Customer.UnitOfWorks.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Commit();

    }
}
