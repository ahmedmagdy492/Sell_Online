using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SellOnline_Test
{
    [TestClass]
    public class TestHelpers
    {
        [TestMethod]
        public void TestConvertByteArrToHex()
        {
            Sell_Online.Helpers.Sha256Hasher sha256Hasher = new Sell_Online.Helpers.Sha256Hasher();
            string result = sha256Hasher.Hash("ahmed");
            string expected = "9af2921d3fd57fe886c9022d1fcc055d53a79e4032fa6137e397583884e1a5de";

            Assert.AreEqual(expected, result);
        }
    }
}
