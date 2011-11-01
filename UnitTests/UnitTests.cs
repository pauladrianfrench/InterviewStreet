namespace UnitTests
{
    using NUnit.Framework;
    using PointsInAPlane;
    using System.Collections.Generic;
    using System;
    using System.Diagnostics;

    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void TestArray()
        {
            TestSet set1 = new TestSet();
            set1.AddPoint(new MyPoint(0, 0));
            set1.AddPoint(new MyPoint(1, 1));
            set1.AddPoint(new MyPoint(2, 2));
            set1.AddPoint(new MyPoint(5, 0));
            set1.AddPoint(new MyPoint(6, 1));
            set1.AddPoint(new MyPoint(7, 2));
            set1.AddPoint(new MyPoint(8, 5));

            Assert.AreEqual(7, set1.Count, "Array size");

            set1.RemoveAt(4);
            Assert.AreEqual(6, set1.Count, "Array size after remove");

            Assert.True(set1.Points[0].Equals(new MyPoint(0, 0)), "After remove [0]");
            Assert.True(set1.Points[1].Equals(new MyPoint(1, 1)), "After remove [1]");
            Assert.True(set1.Points[2].Equals(new MyPoint(2, 2)), "After remove [2]");
            Assert.True(set1.Points[3].Equals(new MyPoint(5, 0)), "After remove [3]");
            Assert.True(set1.Points[4].Equals(new MyPoint(7, 2)), "After remove [4]");
            Assert.True(set1.Points[5].Equals(new MyPoint(8, 5)), "After remove [5]");
        }


        [Test]
        public void Test1()
        {
            TestSet set1 = new TestSet();
            set1.AddPoint(new MyPoint(0, 0));
            set1.AddPoint(new MyPoint(0, 1));
            set1.AddPoint(new MyPoint(1, 0));

            ResultCollection res = Solution.CalcPoints(set1).GetBestSet();
            OutputResults(res);
        }

        [Test]
        public void Test2()
        {
            TestSet set1 = new TestSet();
            set1.AddPoint(new MyPoint(0, 0));
            set1.AddPoint(new MyPoint(1, 1));
            set1.AddPoint(new MyPoint(2, 2));
            set1.AddPoint(new MyPoint(5, 0));
            set1.AddPoint(new MyPoint(6, 1));
            set1.AddPoint(new MyPoint(7, 2));
            set1.AddPoint(new MyPoint(8, 5));

            ResultCollection res = Solution.CalcPoints(set1).GetBestSet();
            OutputResults(res);
        }

        [Test]
        public void Testx()
        {
            TestSet set1 = new TestSet();
            set1.AddPoint(new MyPoint(1, 6));
            set1.AddPoint(new MyPoint(2, 5));
            set1.AddPoint(new MyPoint(2, 6));
            set1.AddPoint(new MyPoint(3, 4));
            set1.AddPoint(new MyPoint(3, 5));
            set1.AddPoint(new MyPoint(3, 6));
            set1.AddPoint(new MyPoint(3, 7));
            set1.AddPoint(new MyPoint(5, 5));
            set1.AddPoint(new MyPoint(5, 6));
            set1.AddPoint(new MyPoint(6, 4));
            //set1.AddPoint(new MyPoint(6, 5));
            //set1.AddPoint(new MyPoint(6, 6));
            //set1.AddPoint(new MyPoint(7, 5));
            //set1.AddPoint(new MyPoint(8, 5));
            //set1.AddPoint(new MyPoint(8, 4));
            //set1.AddPoint(new MyPoint(9, 6));

            ResultCollection res = Solution.CalcPoints(set1).GetBestSet();
            OutputResults(res);
        }

        public void OutputResults(ResultCollection res)
        {
            int count = 0;
            foreach (Results r in res.ResultSet)
            {
                Console.WriteLine("Result set {0}", ++count);
                Console.WriteLine("Permutuations: {0}", r.Permutations);
                foreach (Line l in r.Lines)
                {
                    Console.Write("Line: ");
                    foreach (MyPoint p in l.Points)
                    {
                        Console.Write("({0}, {1}) ", p.X, p.Y);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.Read();
        }
    }
}