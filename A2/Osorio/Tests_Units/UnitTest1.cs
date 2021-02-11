using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WpfAppProblemeInfo;

namespace Tests_Units
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
        [TestMethod]
        public void Test_bool_int()
        {
            bool[] tab = new bool[] { false, true, false, true, true };
            int result = Tools.Bool_to_Int(tab, 4);
            Assert.AreEqual(result, 11);
        }
        [TestMethod]
        public void Test_int_bool()
        {
            bool[] tab = new bool[] { false, true, false, true, true };
            bool[] result = Tools.int_to_bit(11, 5);
            Assert.IsFalse(result[0]);
            Assert.IsTrue(result[1]);
            Assert.IsFalse(result[2]);
            Assert.IsTrue(result[3]);
            Assert.IsTrue(result[4]);
        }

        [TestMethod]
        public void Test_Pixel_Moyenn()
        {
            Pixel test = new Pixel(30, 60, 90);
            int result = test.Moyenne();
            Assert.AreEqual(60, result);
        }
        [TestMethod]
        public void Test_Complexe_Norme()
        {
            Complexe z = new Complexe(2, 2);
            Assert.AreEqual(Math.Sqrt(8), z.Norme());
        }
        [TestMethod]
        public void Test_Complexe_Square()
        {
            Complexe z = new Complexe(2, 2);
            Assert.AreEqual(0, z.Square().Reel);
            Assert.AreEqual(8, z.Square().Imaginaire);
        }
        [TestMethod]
        public void Test_Complexe_Add()
        {
            Complexe z = new Complexe(2, 2);
            Complexe c = new Complexe(1, 1);
            Assert.AreEqual(3, z.Add(c).Reel);
            Assert.AreEqual(3, z.Add(c).Imaginaire);
        }


    }
}
