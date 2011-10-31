namespace PointsInAPlane
{
    using System;
    using System.Collections.Generic;

    public class MyPoint
    {
        public MyPoint(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public double X { get; set; }
        public double Y { get; set; }
        public MyPoint Clone() { return new MyPoint(this.X, this.Y); }
    }

    public class Line
    {
        double m;
        double c;
        bool isVert;

        public Line()
        {
            Points = new List<MyPoint>();
            m = 0;
            c = 0;
            isVert = false;
        }
        
        public List<MyPoint> Points { get; private set; }

        public Line Clone()
        {
            Line ret = new Line();
            int len = this.Points.Count;
            for (int i = 0; i < len; i++)
			{
			  ret.Points.Add(this.Points[i].Clone());
			}
            ret.m = this.m;
            ret.c = this.c;
            ret.isVert = this.isVert;
            return ret;
        }

        public bool Equals(Line other)
        {
            if (this.Points.Count != other.Points.Count)
            {
                return false;
            }
            
            foreach (MyPoint p in this.Points)
            {
                if (other.Points.FindAll(x => x.X == p.X && x.Y == p.Y).Count == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public bool AddValidPoint(MyPoint p)
        {
            if (Points.Count < 2)
            {
                Points.Add(p);
                
                if (Points.Count == 2)
                {
                    double rise = Points[1].Y - Points[0].Y;
                    double run = Points[1].X - Points[0].X;

                    isVert = (run == 0.0);
                    if (!isVert)
                    {
                        m = rise / run;
                        c = Points[0].Y - m * Points[0].X;
                    }
                }
                return true;
            }
            else
            {
                return AddCollinearPoint(p);
            }
        }

        private bool AddCollinearPoint(MyPoint p)
        {
            if (this.HasPoint(p))
            {
                Points.Add(p);
                return true;
            }
            return false;
        }

        private bool HasPoint(MyPoint p)
        {
            if (isVert)
            {
                return p.X == Points[0].X;
            }
            else
            {
                return p.Y == m * p.X + c;
            }
        }
    }

    public class TestSet
    {
        public TestSet()
        {
            this.Points = new List<MyPoint>();
        }
        public List<MyPoint> Points { get; set; }

        public TestSet Clone()
        {
            TestSet ret = new TestSet();
            int len = this.Points.Count;
            for (int i = 0; i < len; i++)
            {
                ret.Points.Add(this.Points[i].Clone());
            }
            return ret;
        }
    }

    public class ResultSet
    {
        public ResultSet()
        {
            this.Lines = new List<Line>();
        }
        public List<Line> Lines { get; set; }

        public ResultSet Clone()
        {
            ResultSet ret = new ResultSet();
            int len = this.Lines.Count;
            for (int i = 0; i < len; i++)
			{
			  ret.Lines.Add(this.Lines[i].Clone());
			}

            return ret;
        }

        public bool Equals(ResultSet other)
        {
            if (this.Lines.Count != other.Lines.Count)
            {
                return false;
            }

            foreach(Line l in this.Lines)
            {
                if (other.Lines.FindAll(X => X.Equals(l)).Count == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class Solution
    {
        static int loopCheck = 0;
        static void Main(string[] args)
        {
            List<TestSet> testSets = ReadInput();

            foreach (TestSet set in testSets)
            {
                CalcPoints(set);
            }
        }

        private static List<TestSet> ReadInput()
        {
            List<TestSet> tests = new List<TestSet>();

            string testCount = Console.ReadLine();

            int nTest = Convert.ToInt32(testCount);

            for (int i = 0; i < nTest; i++)
            {
                TestSet set = new TestSet();
                string pointCount = Console.ReadLine();
                int nPoint = Convert.ToInt32(pointCount);
                for (int j = 0; j < nPoint; j++)
                {
                    string point = (i + 1 == nTest && j + 1 == nPoint) ? Console.In.ReadToEnd() : Console.ReadLine();
                    string x = point.Split(' ')[0];
                    string y = point.Split(' ')[1];
                    set.Points.Add(new MyPoint(Convert.ToDouble(x), Convert.ToDouble(y)));
                }
                tests.Add(set);
            }
            return tests;
        }

        public static List<ResultSet> CalcPoints(TestSet set)
        {
            List<ResultSet> ret = new List<ResultSet>();
            ResultSet res = new ResultSet();

            ret = CalcPointsRecurse2(set, res, ret);

            return ret;
        }

        public static List<ResultSet> CalcPointsRecurse2(TestSet set, ResultSet res, List<ResultSet> ret)
        {
            int nSetPoint = set.Points.Count;
            if (nSetPoint <= 2)
            {
                Line l = new Line();
                foreach (MyPoint p in set.Points)
                {
                    l.AddValidPoint(p);
                }
                if (l.Points.Count > 0)
                {
                    res.Lines.Add(l);
                }
                ret.Add(res);
                return ret;
            }

            for (int i = 0; i < nSetPoint-1; i++)
            {
                for (int j = i+1; j < nSetPoint; j++)
                {
                    if (i != j)
                    {
                        ResultSet workRes = res.Clone();
                        TestSet workSet = set.Clone();
                        Line currentLine = new Line();

                        currentLine.AddValidPoint(set.Points[i]);
                        currentLine.AddValidPoint(set.Points[j]);

                        workSet.Points.RemoveAt(j);
                        workSet.Points.RemoveAt(i);

                        int nPoints = workSet.Points.Count;

                        for (int k = nPoints - 1; k >= 0; k--)
                        {
                            if (currentLine.AddValidPoint(workSet.Points[k]))
                            {
                                workSet.Points.RemoveAt(k);
                            }

                            if (++loopCheck > 10000)
                            {
                                throw new Exception("Dat be too much loopin boy!");
                            }
                        }
                        workRes.Lines.Add(currentLine);
                        if (workSet.Points.Count > 0)
                        {
                            ret = CalcPointsRecurse2(workSet, workRes, ret);
                        }
                        else
                        {
                            ret.Add(workRes);
                        }
                    }
                }
            }
            return ret;
        }
    }
 }
