using Moq;

namespace TechnoMarket.Services.Customer.UnitTests
{
    public static class MockExtensions
    {
        //TODO: IQueryable ve Moq arasındaki sorun çözülecek. GetAll ve GetByEmail metotları için aşağıdaki generic extension metot yazıldı StackOverFlow dan, kontrol edilecek.
        public static void SetupIQueryable<T>(this Mock<T> mock, IQueryable queryable)
        where T : class, IQueryable
        {
            mock.Setup(r => r.GetEnumerator()).Returns(queryable.GetEnumerator());
            mock.Setup(r => r.Provider).Returns(queryable.Provider);
            mock.Setup(r => r.ElementType).Returns(queryable.ElementType);
            mock.Setup(r => r.Expression).Returns(queryable.Expression);
        }
    }
}
