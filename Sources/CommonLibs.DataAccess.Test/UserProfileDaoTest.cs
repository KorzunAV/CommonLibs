using NUnit.Framework;

namespace MakeHappy.DataAccess.Test.Daos
{
    [TestFixture]
    public class UserProfileDaoTest : TestFixtureBase<UserProfileDao>
    {
        [Test]
        public void GetTest()
        {
            const long id = 1;
            var data = Dao.Get(1);
            Assert.IsNotNull(data);
            Assert.IsNotNull(data.Id);
            Assert.IsTrue(data.Id == id);
            Assert.IsNotNull(data.Email);
            Assert.IsNotNull(data.RegistrationDate);
        }
    }
}
