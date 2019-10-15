using LinkContractor.BLL;
using LinkContractor.DAL;
using LinkContractor.DAL.Entities;
using LinkContractor.DAL.Interfaces;
using Xunit;
using C = LinkContractor.Tests.DbContextCreator;

namespace LinkContractor.Tests
{
    public class UnitTest1
    {
        private IUnitOfWork UnitOfWork { get; }
        private IBlMain BlMain { get; }

        public UnitTest1()
        {
            var Context = C.CreateWith(
                new[]
                {
                    new SavedData(),
                    new SavedData(),
                },
                new[]
                {
                    new ShortCode(),
                    new ShortCode(),
                });
            UnitOfWork = new UnitOfWork(Context);
            BlMain = new BlMain(UnitOfWork);
        }

        [Fact]
        public void Test1()
        {
        }
    }
}