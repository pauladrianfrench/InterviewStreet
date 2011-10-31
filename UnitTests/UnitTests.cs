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
        public void Test1()
        {
            TestSet set1 = new TestSet();
            set1.Points.Add(new MyPoint(0,0));
            set1.Points.Add(new MyPoint(0, 1));
            set1.Points.Add(new MyPoint(1, 0));

            List<ResultSet> res = Solution.CalcPoints(set1);
            OutputResults(res);
        }

        [Test]
        public void Test2()
        {
            TestSet set1 = new TestSet();
            set1.Points.Add(new MyPoint(3, 4));
            set1.Points.Add(new MyPoint(3, 5));
            set1.Points.Add(new MyPoint(3, 6));
            set1.Points.Add(new MyPoint(5, 5));

            List<ResultSet> res = Solution.CalcPoints(set1);
            OutputResults(res);
        }

        public void OutputResults(List<ResultSet> res)
        {
            foreach (ResultSet r in res)
            {
                Console.WriteLine("Result set");
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
