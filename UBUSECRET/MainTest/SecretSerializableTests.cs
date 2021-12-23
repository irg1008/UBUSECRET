using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Main.Tests
{
    [TestClass()]
    public class SecretSerializableTests
    {

        readonly User owner = new User("Owner", "owner@user.com", "P@ssword");
        Secret s;

        [TestInitialize()]
        public void Startup()
        {
            s = new Secret("A", "Secret a", owner);
        }

        [TestCleanup]
        public void CleanUp()
        {
            s = null;
        }

        [TestMethod()]
        public void To_JSONTest()
        {
            string expectedJSON = @"
            {
                'id': 2,
                'title': A,
                'message: 'Secret a',
                
            }";
            string sJSON = s.To_JSON();
            
            Assert.AreEqual(expectedJSON, sJSON);
        }

        [TestMethod()]
        public void From_JSONTest()
        {
            Assert.Fail();
        }
    }
}