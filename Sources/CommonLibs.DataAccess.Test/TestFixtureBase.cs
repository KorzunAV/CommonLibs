using CommonLibs.DataAccess.NHibernate.Common.Daos;
using CommonLibs.DataAccess.NHibernate.Common.Interfaces;
using CommonLibs.DataAccess.NHibernate.Common.IoC;
using CommonLibs.DataAccess.NHibernate.Common.NHibernate;
using Ninject;
using NUnit.Framework;

namespace MakeHappy.DataAccess.Test
{
    public class TestFixtureBase<T>
        where T : DaoBase
    {
        private ISessionManager _sessionManager;
        private StandardKernel _kernel;
        protected T Dao;

        [TestFixtureSetUp]
        public virtual void TestFixtureSetUp()
        {
            _kernel = new StandardKernel(new DataAccessModule());
            _sessionManager = _kernel.Get<ISessionManager>();
            Dao = _kernel.Get<T>();
            Assert.IsNotNull(Dao);
        }

        [SetUp]
        public void SetUp()
        {
            _sessionManager.OpenSession();
            _sessionManager.BeginTransaction();
        }
        
        [TearDown]
        public void Dispose()
        {
            _sessionManager.RollbackTransaction();
            _sessionManager.CloseSession();
            _kernel.Dispose();
        }
    }
}
