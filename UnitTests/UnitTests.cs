﻿namespace UnitTests
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;
    using PointsInAPlaneV2;

    [TestFixture]
    public class UnitTests
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
        public void TestFast()
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

            ////set1.Add(new MyPoint(0, 0));
            ////set1.Add(new MyPoint(0, 1));
            ////set1.Add(new MyPoint(1, 0));

            //set1.AddPoint(new MyPoint(6, 5));
            //set1.AddPoint(new MyPoint(6, 6));
            //set1.AddPoint(new MyPoint(7, 5));
            //set1.AddPoint(new MyPoint(8, 5));
            //set1.AddPoint(new MyPoint(8, 4));
            //set1.AddPoint(new MyPoint(9, 6));

            ResultCollection2 res = Solution.CalculatePoints(set1);
           // OutputResults2(set1, res);
        }

        [Test]
        public void TestFast2()
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
            ResultCollection2 res = Solution.CalculatePoints(set1);
            OutputResults2(set1, res);
        }

        [Test]
        public void SpeedTest1()
        {
            for (int i = 0; i < 1000000; ++i)
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

                Line currentLine = new Line();

                currentLine.AddValidPoint(set1[0]);
                currentLine.AddValidPoint(set1[1]);

                MyList<MyPoint> workSet = set1.Clone();

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
        public void SpeedTest2()
        {
            ItemSet seta = new ItemSet();
            seta.SetUp(10);

            for (int i = 0; i < 100000; ++i)
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

        //public void OutputResults(ResultCollection res)
        //{
        //    int count = 0;
        //    foreach (Results r in res.ResultSet)
        //    {
        //        Console.WriteLine("Result set {0}", ++count);
        //        Console.WriteLine("Permutuations: {0}", r.Permutations);
        //        foreach (Line l in r.Lines)
        //        {
        //            Console.Write("Line: ");
        //            foreach (MyPoint p in l.Points)
        //            {
        //                Console.Write("({0}, {1}) ", p.X, p.Y);
        //            }
        //            Console.WriteLine();
        //        }
        //        Console.WriteLine();
        //    }
        //    Console.Read();
        //}
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
