namespace UnitTests
{
    using NUnit.Framework;
    using PointsInAPlaneV2;
    using System.Collections.Generic;
    using System;
    using System.Diagnostics;

    [TestFixture]
    public class BenchMarkTests
    {
        [Test]
        public void TestArray()
        {
            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(0, 0));
            set1.Add(new MyPoint(1, 1));
            set1.Add(new MyPoint(2, 2));
            set1.Add(new MyPoint(5, 0));
            set1.Add(new MyPoint(6, 1));
            set1.Add(new MyPoint(7, 2));
            set1.Add(new MyPoint(8, 5));

            Assert.AreEqual(7, set1.Count, "Array size");

            set1.RemoveAt(4);
            Assert.AreEqual(6, set1.Count, "Array size after remove");

            Assert.True(set1[0].Equals(new MyPoint(0, 0)), "After remove [0]");
            Assert.True(set1[1].Equals(new MyPoint(1, 1)), "After remove [1]");
            Assert.True(set1[2].Equals(new MyPoint(2, 2)), "After remove [2]");
            Assert.True(set1[3].Equals(new MyPoint(5, 0)), "After remove [3]");
            Assert.True(set1[4].Equals(new MyPoint(7, 2)), "After remove [4]");
            Assert.True(set1[5].Equals(new MyPoint(8, 5)), "After remove [5]");
        }

        [Test]
        public void TestPointSet()
        {
            ItemSet pSet = new ItemSet();
            uint expected = 0;
            for (int i = 0; i < 16; i++)
            {
                expected += (uint)Math.Pow(2.0, i);
                pSet.Add(i);
                Assert.True(expected == pSet.GetIndices());
            }

            pSet.RemoveAt(3);
            expected -= 8;
            Assert.AreEqual(15, pSet.CountBits(), "Array size 1");
            Assert.True(expected == pSet.GetIndices());


            MyList<MyPoint> set1 = new MyList<MyPoint>();
            set1.Add(new MyPoint(0, 0));
            set1.Add(new MyPoint(1, 1));
            set1.Add(new MyPoint(2, 2));
            set1.Add(new MyPoint(5, 0));
            set1.Add(new MyPoint(6, 1));
            set1.Add(new MyPoint(7, 2));
            set1.Add(new MyPoint(8, 5));

            pSet.Reset();

            pSet.Add(0);
            pSet.Add(3);
            pSet.Add(4);
            pSet.Add(6);

            Assert.AreEqual(4, pSet.CountBits(), "Array size 2");

            pSet.GetItem(set1, 0);
            pSet.GetItem(set1, 1);
            pSet.GetItem(set1, 2);
            pSet.GetItem(set1, 3);

            Assert.True(set1[0] == pSet.GetItem(set1, 0), "Check 0");
            Assert.True(set1[3] == pSet.GetItem(set1, 1), "Check 1");
            Assert.True(set1[4] == pSet.GetItem(set1, 2), "Check 2");
            Assert.True(set1[6] == pSet.GetItem(set1, 3), "Check 3");
        }

        [Test]
        public void TestResultSet()
        {
            List<MyPoint> set1 = new List<MyPoint>();
            set1.Add(new MyPoint(0, 0));
            set1.Add(new MyPoint(1, 1));
            set1.Add(new MyPoint(2, 2));
            set1.Add(new MyPoint(5, 0));
            set1.Add(new MyPoint(6, 1));
            set1.Add(new MyPoint(7, 2));

            ItemSet iSet1 = new ItemSet();
            ItemSet iSet2 = new ItemSet();

            iSet1.Add(0);
            iSet1.Add(2);
            iSet1.Add(4);

            iSet2.Add(1);
            iSet2.Add(3);
            iSet2.Add(5);

            Results2 r2 = new Results2();
            r2.AddResult(iSet1.GetIndices());

            Results2 r3 = r2.Clone();
            r3.AddResult(iSet2.GetIndices());

            Assert.AreEqual(2, r3.Count);
            Assert.AreEqual(1, r2.Count);

            ItemSet iSet3 = iSet1.Clone();
            iSet3.Add(1);
            iSet3.Add(3);
            iSet3.Add(5);

            Assert.AreEqual(3, iSet1.Count);
            Assert.AreEqual(6, iSet3.Count);
        }

        [Test]
        public void TestResult()
        {
            uint first = 7;
            uint second = 56;
            uint third =448;
            uint fourth = 3584;
            uint fifth = 28672;

            Results2 res = new Results2();
            res.AddResult(second);
            res.AddResult(first);
            res.AddResult(fourth);
            res.AddResult(fifth);
            res.AddResult(third);

            Assert.AreEqual(first, res.GetLine(0).GetIndices());
            Assert.AreEqual(second, res.GetLine(1).GetIndices());
            Assert.AreEqual(third, res.GetLine(2).GetIndices());
            Assert.AreEqual(fourth, res.GetLine(3).GetIndices());
            Assert.AreEqual(fifth, res.GetLine(4).GetIndices());

            Results2 res2 = new Results2();
            res2.AddResult(second);
            res2.AddResult(fifth);
            res2.AddResult(third);
            res2.AddResult(first);
            res2.AddResult(fourth);

            Assert.AreEqual(first, res2.GetLine(0).GetIndices());
            Assert.AreEqual(second, res2.GetLine(1).GetIndices());
            Assert.AreEqual(third, res2.GetLine(2).GetIndices());
            Assert.AreEqual(fourth, res2.GetLine(3).GetIndices());
            Assert.AreEqual(fifth, res2.GetLine(4).GetIndices());

            Assert.IsTrue(res2.Equals(res));

            Results2 res3 = res.Clone();
            Assert.IsTrue(res2.Equals(res3));
        }
        
        [Test]
        public void LoopIndices()
        {
            ItemSet set = new ItemSet();
            set.SetUp(16);
            int nSetPoint = set.Count;
            int count = 0;
            for (int i = 0; i < nSetPoint - 1; i++)
            {
                for (int j = i + 1; j < nSetPoint; j++)
                {
                    if (i != j)
                    {
                        int idx = set.GetItemIndex(i);
                        int idj = set.GetItemIndex(j);
                        Console.WriteLine("{0} - {1}", idx, idj);
                        count++;
                    }
                }
            }
            Console.WriteLine("Number of rows = {0}", count);
            Assert.AreEqual(120, count);
        }

        [Test]
        public void LoopIndicesReducedSet()
        {
            ItemSet set = new ItemSet();
            set.SetUp(16);

            set.RemoveAt(3);
            set.RemoveAt(8);
            int nSetPoint = set.Count;
            int count = 0;
            for (int i = 0; i < nSetPoint - 1; i++)
            {
                for (int j = i + 1; j < nSetPoint; j++)
                {
                    if (i != j)
                    {
                        int idx = set.GetItemIndex(i);
                        int idj = set.GetItemIndex(j);
                        Assert.IsTrue(idx != 3 || idx != 8);
                        Assert.IsTrue(idj != 3 || idj != 8);
                        //Console.WriteLine("{0} - {1}", idx, idj);
                        count++;
                    }
                }
            }
           // Console.WriteLine("Number of rows = {0}", count);
            Assert.AreEqual(91, count);
        }

        [Test]
        public void LneParams()
        {
            LineParams l1 = new LineParams(new MyPoint(1, 1), new MyPoint(2, 2));
            LineParams l2 = new LineParams(new MyPoint(3, 3), new MyPoint(4, 4));
            LineParams l3 = new LineParams(new MyPoint(5, 3), new MyPoint(7, 2));
            LineParams l4 = new LineParams(new MyPoint(1, 3), new MyPoint(2, 4));

            Assert.IsTrue(l1.IsSameLine(l2));
            Assert.IsTrue(l1.IsCollinear(new MyPoint(3, 3)));

            Assert.IsFalse(l1.IsSameLine(l3));
            Assert.IsFalse(l1.IsSameLine(l4));
            Assert.IsFalse(l1.IsCollinear(new MyPoint(7, 2)));
            Assert.IsFalse(l1.IsCollinear(new MyPoint(2, 4)));

            List<LineParams> par = new List<LineParams>();
            par.Add(l1);

            Assert.IsTrue(par.Exists(x => x.IsSameLine(l2)));
            Assert.IsFalse(par.Exists(x => x.IsSameLine(l3)));
        }

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

           // Assert.AreEqual(2, val);
           // Assert.AreEqual(48, permute);

            OutputResults2(set1, res);
        }


        [Test]
        public void TestFast2()
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

            ResultCollection2 res = Solution.CalculatePoints(set1);
            OutputResults2(set1, res);
        }

        [Test]
        public void TestFast3()
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
            OutputResults2(set1, res);
        }

         [Test]
        public void Test16Second()
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
            ResultCollection2 res = Solution.CalculatePoints(set1);
            OutputResults2(set1, res);
        }

        [Test]
        public void SpeedTestCloneTestSet()
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

            for (int i = 0; i < 1000000; ++i)
            {
                MyList<MyPoint> workSet = set1.Clone();
                Line currentLine = new Line();

                currentLine.AddValidPoint(set1[0]);
                currentLine.AddValidPoint(set1[1]);

                workSet.RemoveAt(0);
                workSet.RemoveAt(1);

                int nPoints = workSet.Count;

                for (int k = nPoints - 1; k >= 0; k--)
                {
                    if (currentLine.AddValidPoint(workSet[k]))
                    {
                        workSet.RemoveAt(k);
                    }
                }
            }
        }

        [Test]
        public void SpeedTestCloneItemSet()
        {
            ItemSet seta = new ItemSet();
            seta.SetUp(10);
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

            for (int i = 0; i < 1000000; ++i)
            {
                ItemSet workSet = seta.Clone();
          
                ItemSet currentLine = new ItemSet();

                currentLine.Add(0);
                currentLine.Add(1);
                LineParams par = new LineParams(set1[0], set1[1]);

                workSet.RemoveAt(0);
                workSet.RemoveAt(1);

                for (int k = 0; k < workSet.Count; ++k)
                {
                    int id = workSet.GetItemIndex(k);
                    if (par.IsCollinear(set1[id]))
                    {
                        currentLine.Add(id);
                        workSet.RemoveAt(id);
                    }
                }
            }
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
        public void OutputResults2(MyList<MyPoint> set1, ResultCollection2 res)
        {
            int count = 0;
            for (int k = 0; k < res.ResultSet.Count; k++)
            {
                Results2 r = res.ResultSet[k];
                Console.WriteLine("Result set {0}", ++count);
                Console.WriteLine("Permutuations: {0}", r.Permutations);
                int c = r.Count;
                for (int j = 0; j < c; j++)
                {
                    ItemSet l = r.GetLine(j);
                    if (r.Count == 1)
                    {
                        Console.WriteLine("Emergency");
                    }
                    Console.Write("Line: ");

                    for (int i = 0; i < l.Count; i++)
                    {
                        Console.Write("({0}, {1}) ", l.GetItem(set1, i).X, l.GetItem(set1, i).Y);
                    }

                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.Read();
        }
    }
}

