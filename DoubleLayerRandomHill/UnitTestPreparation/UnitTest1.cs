using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DoubleLayerRandomHill;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UnitTestPreparation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SimpleTestForMoreThanOne()
        {
            Dictionary<string, int> actual = new Dictionary<string, int>();
            actual.Add("BA", 1);
            actual.Add("AA",2);
            actual.Add("AB",1);
            actual.Add("A.", 1);
            actual = actual.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
            
            Dictionary<string, int> expected = DoubleLayerRandomHill.Preparation.UniquesDict("AABAA.", 2);
            bool fb = actual.SequenceEqual(expected);
            Assert.AreEqual(fb,true,"Loozer");
        }

        [TestMethod]
        public void TestDigitFromString()
        {
            List<int> expected = new List<int>();
            expected.AddRange(new []{1,4,3,2});
            var actual= DoubleLayerRandomHill.Preparation.FormDigitString("BADC");
            bool fg = expected.SequenceEqual(actual);
            Assert.AreEqual(fg,false,"Loozer");
        }
        [TestMethod]
        public void TestHill()
        {
            List<int> res = new List<int>();
            double[,] mat = { {1,1},{1,1}};
            List<int> list = new List<int>();
            list.AddRange(new []{1,1,1,1});
            var actual = DoubleLayerRandomHill.Code.HillPlusRandomEncrypt(list,mat,32,res);
            List<int> hjef = new List<int>();
            bool hg = hjef.SequenceEqual(actual);


            Assert.AreEqual(true,hg,"");
        }
        [TestMethod]
        public void TestCalcOfLen()
        {
            List<int> rand = Enumerable.Range(0, 211).ToList();
            int[] actual = Preparation.CalcRandomList(rand);
            int[] expected = new[] { 0, 0, 0, 2, 1, 1 };
            bool isEqual = actual.SequenceEqual(expected);
            Assert.AreEqual(isEqual,true,"Loozer");
        }
        [TestMethod]
        public void TestMaskNumver()
        {
            List<int> list = Enumerable.Range(0, 500).Select(x => Preparation.FindMaskNumber()).ToList();
            bool isOk = list.All(x=>x%Preparation.BASE==0);

            Assert.AreEqual(isOk,true,"Loozer");
        }
    
    }
}
