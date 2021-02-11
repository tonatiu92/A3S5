using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WpfAppProblemeInfo
{

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int result = Tools.puissance(2, 4);
            Assert.AreEqual(16, result);
        }
        public void Test_bool_int()
        {
            bool[] tab = new bool[] { false, true, false, true, true };
            int result = Tools.Bool_to_Int(tab, 4);
            Assert.AreEqual(result, 11);
        }

    }
}
