

namespace UnitTests
{
    using NUnit.Framework;
    using PointsInAPlaneV2;
    using System.Collections.Generic;
    using System;
    using System.Diagnostics;

    [TestFixture]
    class CalcTests
    {
        [Test]
        public void Test16First()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(1, 6));
            set1.Add(new MyPoint(2, 6));
            set1.Add(new MyPoint(5, 6));
            set1.Add(new MyPoint(3, 6));
            set1.Add(new MyPoint(2, 5));
            set1.Add(new MyPoint(3, 5));
            set1.Add(new MyPoint(5, 5));
            set1.Add(new MyPoint(3, 4));
            set1.Add(new MyPoint(3, 7));
            set1.Add(new MyPoint(6, 4));
            set1.Add(new MyPoint(6, 5));
            set1.Add(new MyPoint(6, 6));
            set1.Add(new MyPoint(7, 5));
            set1.Add(new MyPoint(8, 5));
            set1.Add(new MyPoint(8, 4));
            set1.Add(new MyPoint(9, 6));

            ResultCollection2 res = Solution.CalculatePoints(set1);
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0);
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
                Assert.AreEqual(c, res.ResultSet[i].Count);
                permute += res.ResultSet[i].Permutations;
            }

            Assert.AreEqual(2, val);
            Assert.AreEqual(48, permute);
        }

        [Test]
        public void Test13First()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(8, 4));
            set1.Add(new MyPoint(6, 5));
            set1.Add(new MyPoint(1, 6));
            set1.Add(new MyPoint(3, 6));
            set1.Add(new MyPoint(6, 6));
            set1.Add(new MyPoint(7, 5));
            set1.Add(new MyPoint(8, 5));
            set1.Add(new MyPoint(9, 6));
            set1.Add(new MyPoint(6, 4));
            set1.Add(new MyPoint(2, 6));
            set1.Add(new MyPoint(5, 6));
            set1.Add(new MyPoint(3, 7));
            set1.Add(new MyPoint(2, 5));

            ResultCollection2 res = Solution.CalculatePoints(set1).GetBestSet();
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0);
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
                Assert.AreEqual(c, res.ResultSet[i].Count, "Result sets are not all the same");
                permute += res.ResultSet[i].Permutations;
            }

            Assert.AreEqual(16, val);
            Assert.AreEqual(140520, permute);
        }

        [Test]
        public void Test13Second()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(8, 4));
            set1.Add(new MyPoint(9, 6));
            set1.Add(new MyPoint(1, 6));
            set1.Add(new MyPoint(2, 6));
            set1.Add(new MyPoint(5, 6));
            set1.Add(new MyPoint(3, 6));
            set1.Add(new MyPoint(6, 6));
            set1.Add(new MyPoint(7, 5));
            set1.Add(new MyPoint(8, 5));
            set1.Add(new MyPoint(3, 7));
            set1.Add(new MyPoint(6, 4));
            set1.Add(new MyPoint(6, 5));
            set1.Add(new MyPoint(2, 5));

            ResultCollection2 res = Solution.CalculatePoints(set1).GetBestSet(4);
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0);
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
                Assert.AreEqual(c, res.ResultSet[i].Count, "Result sets are not all the same size");
                permute += res.ResultSet[i].Permutations;
            }

            Assert.AreEqual(1171, val);
            Assert.AreEqual(140520, permute);
        }

        [Test]
        public void Test16Third()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(8, 4));
            set1.Add(new MyPoint(1, 6));
            set1.Add(new MyPoint(2, 6));
            set1.Add(new MyPoint(9, 6));
            set1.Add(new MyPoint(3, 5));
            set1.Add(new MyPoint(5, 5));
            set1.Add(new MyPoint(8, 5));
            set1.Add(new MyPoint(3, 7));
            set1.Add(new MyPoint(6, 4));
            set1.Add(new MyPoint(6, 5));
            set1.Add(new MyPoint(2, 5));
            set1.Add(new MyPoint(5, 6));
            set1.Add(new MyPoint(3, 6));
            set1.Add(new MyPoint(6, 6));
            set1.Add(new MyPoint(7, 5));
            set1.Add(new MyPoint(3, 4));

            ResultCollection2 res = Solution.CalculatePoints(set1);
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0);
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
                Assert.AreEqual(c, res.ResultSet[i].Count);
                permute += res.ResultSet[i].Permutations;
            }

            Assert.AreEqual(2, val);
            Assert.AreEqual(48, permute);
        }

        [Test]
        public void Test3()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
           
            set1.Add(new MyPoint(0, 0));
            set1.Add(new MyPoint(0, 1));
            set1.Add(new MyPoint(1, 0));

            ResultCollection2 res = Solution.CalculatePoints(set1);
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0);
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
                Assert.AreEqual(c, res.ResultSet[i].Count);
                permute += res.ResultSet[i].Permutations;
            }

            Assert.AreEqual(3, val);
            Assert.AreEqual(6, permute);
        }


        [Test]
        public void TestCalc6First()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(0, 0));
            set1.Add(new MyPoint(1, 1));
            set1.Add(new MyPoint(2, 2));
            set1.Add(new MyPoint(7, 2));
            set1.Add(new MyPoint(5, 0));
            set1.Add(new MyPoint(8, 5));
            ResultCollection2 res = Solution.CalculatePoints(set1);
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0);
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
                Assert.AreEqual(c, res.ResultSet[i].Count);
                permute += res.ResultSet[i].Permutations;
            }
            Assert.AreEqual(18, val);
            Assert.AreEqual(108, permute);
        }

        [Test]
        public void TestCalc6Second()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(0, 0));
            set1.Add(new MyPoint(8, 5));
            set1.Add(new MyPoint(7, 2));
            set1.Add(new MyPoint(1, 1));
            set1.Add(new MyPoint(2, 2));
            set1.Add(new MyPoint(5, 0));
            ResultCollection2 res = Solution.CalculatePoints(set1);
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0);
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
                Assert.AreEqual(c, res.ResultSet[i].Count);
                permute += res.ResultSet[i].Permutations;
            }
            Assert.AreEqual(18, val);
            Assert.AreEqual(108, permute);
        }


        [Test]
        public void TestCalc7()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(0, 0));
            set1.Add(new MyPoint(1, 1));
            set1.Add(new MyPoint(2, 2));
            set1.Add(new MyPoint(5, 0));
            set1.Add(new MyPoint(6, 1));
            set1.Add(new MyPoint(7, 2));
            set1.Add(new MyPoint(8, 5));
            ResultCollection2 res = Solution.CalculatePoints(set1).GetBestSet();
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0, "Number of results greater than zero");
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
              //  Assert.AreEqual(c, res.ResultSet[i].Count);
                permute += res.ResultSet[i].Permutations;
            }
            Assert.AreEqual(7, val);
            Assert.AreEqual(42, permute);
        }

        [Test]
        public void TestCalc10First()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(1, 6));
            set1.Add(new MyPoint(2, 5));
            set1.Add(new MyPoint(2, 6));
            set1.Add(new MyPoint(3, 4));
            set1.Add(new MyPoint(3, 5));
            set1.Add(new MyPoint(3, 6));
            set1.Add(new MyPoint(3, 7));
            set1.Add(new MyPoint(5, 5));
            set1.Add(new MyPoint(5, 6));
            set1.Add(new MyPoint(6, 4));

            ResultCollection2 res = Solution.CalculatePoints(set1).GetBestSet();
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0);
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
                Assert.AreEqual(c, res.ResultSet[i].Count);
                permute += res.ResultSet[i].Permutations;
            }
            Assert.AreEqual(78, val);
            Assert.AreEqual(1872, permute);
        }


        [Test]
        public void TestCalc10Second()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();

            set1.Add(new MyPoint(5, 6));
            set1.Add(new MyPoint(3, 5));
            set1.Add(new MyPoint(3, 6));
            set1.Add(new MyPoint(6, 4));
            set1.Add(new MyPoint(2, 6));
            set1.Add(new MyPoint(1, 6));
            set1.Add(new MyPoint(2, 5));
            set1.Add(new MyPoint(3, 7));
            set1.Add(new MyPoint(5, 5));
            set1.Add(new MyPoint(3, 4));

            ResultCollection2 res = Solution.CalculatePoints(set1).GetBestSet();
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0);
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
                Assert.AreEqual(c, res.ResultSet[i].Count);
                permute += res.ResultSet[i].Permutations;
            }
            Assert.AreEqual(78, val);
            Assert.AreEqual(1872, permute);
        }
    }
}
