using Microsoft.VisualStudio.TestTools.UnitTesting;
using WYAH;

namespace WYAHTest
{
    [TestClass]
    public sealed class ClientTest
    {
        [TestMethod]
        public async Task TestGetIt()
        {
            var client = new Client();
            var response = await client.GetIt();
            Assert.IsNotNull(response);
            Assert.AreEqual(24, response.headers.Count);
            Assert.AreEqual("200", response.status);
        }
    }
}
