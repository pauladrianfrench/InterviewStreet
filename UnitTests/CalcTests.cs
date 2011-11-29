

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

            Assert.AreEqual(8, val);
            Assert.AreEqual(192, permute);
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

            Assert.AreEqual(36, val);
            Assert.AreEqual(864, permute);
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

            Assert.AreEqual(36, val);
            Assert.AreEqual(864, permute);
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

            Assert.AreEqual(8, val);
            Assert.AreEqual(192, permute);
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
            ResultCollection2 res = Solution.CalculatePoints(set1);
            int val = res.ResultSet.Count;
            int permute = 0;
            Assert.IsTrue(val > 0, "Number of results greater than zero");
            int c = res.ResultSet[0].Count;
            for (int i = 0; i < val; ++i)
            {
                Assert.AreEqual(c, res.ResultSet[i].Count);
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
            Assert.AreEqual(91, val);
            Assert.AreEqual(2184, permute);
        }

        [Test]
        public void TestSingleLine()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(1, 5));
            set1.Add(new MyPoint(2, 8));
            set1.Add(new MyPoint(3, 11));
            set1.Add(new MyPoint(4, 14));
            set1.Add(new MyPoint(5, 17));
            set1.Add(new MyPoint(6, 20));
            set1.Add(new MyPoint(7, 23));
            set1.Add(new MyPoint(8, 26));

            uint set = 0;
            for (int i = 0; i < 8; i++)
			{
			 set |= (uint)Solution.PowOfTwo[i];
			}
           
            uint workSet = set;

            Assert.AreEqual(8, Solution.CountBits(workSet));

            int idxi = Solution.GetItemIndex(workSet, 0);
            int idxj = Solution.GetItemIndex(workSet, 1);

            uint currentLine = 0;
            currentLine |= (uint)Solution.PowOfTwo[idxi];
            currentLine |= (uint)Solution.PowOfTwo[idxj];
            LineParams par = new LineParams(set1[idxi], set1[idxj]);

            workSet &= (~currentLine);

            Assert.AreEqual(6, Solution.CountBits(workSet));

            int counter = Solution.CountBits(workSet);
            for (int k = counter-1; k >= 0; --k)
            {
                int id = Solution.GetItemIndex(workSet, k);
                if (par.IsCollinear(set1[id]))
                {
                    currentLine |= (uint)Solution.PowOfTwo[id];
                    workSet &= (uint)~Solution.PowOfTwo[id];
                }
                else
                {
                    throw new Exception("All points should be in this line");
                }
            }
            Assert.AreEqual(8, Solution.CountBits(currentLine));
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
            Assert.AreEqual(91, val);
            Assert.AreEqual(2184, permute);
        }
    }
}
