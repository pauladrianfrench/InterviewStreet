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

        public bool Equals(MyPoint p)
        {
            return p.X == this.X && p.Y == this.Y;
        }
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

        public List<MyPoint> Points { get; set; }

        public Line Clone()
        {
            Line ret = new Line();
            int len = this.Points.Count;
            for (int i = 0; i < len; i++)
            {
                ret.Points.Add(this.Points[i]);
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

            int nPoint = this.Points.Count;
            for (int i = 0; i < nPoint; i++)
            {
                if (!other.HasPoint(this.Points[i]))
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

                    if (run != 0.0)
                    {
                        m = rise / run;
                        c = Points[0].Y - m * Points[0].X;
                    }
                    else
                    {
                        isVert = true;
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
            if (this.PassesThroughPoint(p))
            {
                Points.Add(p);
                return true;
            }
            return false;
        }

        public bool HasPoint(MyPoint p)
        {
            int nPoint = this.Points.Count;
            for (int i = 0; i < nPoint; i++)
            {
                if (this.Points[i].Equals(p))
                {
                    return true;
                }
            }
            return false;
        }

        private bool PassesThroughPoint(MyPoint p)
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
        MyPoint[] points;
        int count;
        public TestSet()
        {
            this.points = new MyPoint[17];
            count = 0;
        }

        public int Count { get { return count; } }
        public MyPoint[] Points { get { return points; } }

        public void AddPoint(MyPoint p)
        {
            points[count] = p;
            count++;
        }

        public void RemoveAt(int pos)
        {
            for (int i = pos; i < count; i++)
            {
                this.points[i] = this.points[i + 1];  
            }
            count--;
        }

        public TestSet Clone()
        {
            TestSet ret = new TestSet();
            int len = this.Count;
            for (int i = 0; i < len; i++)
            {
                ret.AddPoint(this.Points[i]);
            }
            return ret;
        }
    }

    public class Results
    {
        public Results()
        {
            this.Lines = new List<Line>();
        }
        public List<Line> Lines { get; set; }

        public Results Clone()
        {
            Results ret = new Results();
            int len = this.Lines.Count;
            for (int i = 0; i < len; i++)
            {
                ret.Lines.Add(this.Lines[i].Clone());
            }

            return ret;
        }

        public bool Equals(Results other)
        {
            if (this.Lines.Count != other.Lines.Count)
            {
                return false;
            }

            int len = this.Lines.Count;
            int oLen = other.Lines.Count;
            for (int i = 0; i < len; i++)
            {
                bool found = false;
                for (int j = 0; j < oLen; j++)
                {
                    if (this.Lines[i].Equals(other.Lines[j]))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    return false;
                }
                //if (other.Lines.FindAll(X => X.Equals(this.Lines[i])).Count == 0)
                //{
                //    return false;
                //}
            }

            return true;
        }

        public int Permutations { get { return Permute(Lines.Count); } }

        public static int Permute(int val)
        {
            int fac = 1;
            for (int i = 1; i <= val; i++)
            {
                fac *= i;
            }
            return fac;
        }
    }

    public class ResultCollection
    {
        public ResultCollection()
        {
            this.ResultSet = new List<Results>();
        }

        public List<Results> ResultSet { get; private set; }
        public int MinSetSize
        {
            get
            {
                int min = Int32.MaxValue;

                int nRes = this.ResultSet.Count;
                for (int i = 0; i < nRes; i++)
                {
                    if (this.ResultSet[i].Lines.Count < min)
                    {
                        min = this.ResultSet[i].Lines.Count;
                    }
                }
                return min;
            }
        }
        public bool AddUniqueResult(Results res)
        {
            if (ResultSet.Find(x => x.Equals(res)) != null)
            {
                return false;
            }
            else
            {
                ResultSet.Add(res);
                return true;
            }
        }

        public ResultCollection GetBestSet()
        {
            ResultCollection ret = new ResultCollection();

            int nRes = this.ResultSet.Count;
            for (int i = 0; i < nRes; i++)
            {
                if (this.ResultSet[i].Lines.Count == this.MinSetSize)
                {
                    ret.ResultSet.Add(this.ResultSet[i]);
                }
            }

            return ret;
        }
    }

    public class CallPointResult
    {
        public CallPointResult()
        {
            DoMore = false;
            Reset = false;
        }
        public TestSet WorkingSet { get; set; }
        public Results WorkingResults { get; set; }
        public bool DoMore {get; set; }
        public bool Reset { get; set; }
    }

    public class Solution
    {
        static void Main(string[] args)
        {
            List<TestSet> testSets = ReadInput();
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
                    
                    set.AddPoint(new MyPoint(Convert.ToDouble(x), Convert.ToDouble(y)));
                }
                WriteOutput(CalcPoints(set).GetBestSet());
            }
            return tests;
        }

        private static void WriteOutput(ResultCollection res)
        {
            int mod = 1000000007;
            int minLines = res.MinSetSize;
            int permutations = Results.Permute(minLines) * res.ResultSet.Count;
            Console.WriteLine("{0} {1}", minLines % mod, permutations % mod);
        }

        public static ResultCollection CalcPoints(TestSet set)
        {
            ResultCollection ret = new ResultCollection();
            Results res = new Results();
            TestSet work = set.Clone();
            CallPointResult calcRes = new CallPointResult();
            //do
            //{
            //    if (calcRes.Reset)
            //    {
            //        work = set.Clone();
            //        res = new Results();
            //    }
            //    calcRes = CalcPointsRecurse2(work, res, ret);
            //    work = calcRes.WorkingSet;
            //    res = calcRes.WorkingResults;
            //}
            //while (calcRes.DoMore);
            CalcPointsRecurse2(work, res, ret);
            return ret;
        }

        public static CallPointResult CalcPointsRecurse2(TestSet set, Results res, ResultCollection ret)
        {
            int nSetPoint = set.Count;
            if (nSetPoint <= 2)
            {
                Line l = new Line();
                for (int i = 0; i < nSetPoint; ++i)
                {
                    l.AddValidPoint(set.Points[i]);
                }
                if (l.Points.Count > 0)
                {
                    res.Lines.Add(l);
                }
                ret.AddUniqueResult(res);
                return new CallPointResult() { Reset = true };
            }
            List<Line> linesFound = new List<Line>();
            for (int i = 0; i < nSetPoint - 1; i++)
            {
                for (int j = i + 1; j < nSetPoint; j++)
                {
                    if (i != j)
                    {
                        Results workRes = res.Clone();
                        TestSet workSet = set.Clone();
                        Line currentLine = new Line();

                        currentLine.AddValidPoint(set.Points[i]);
                        currentLine.AddValidPoint(set.Points[j]);

                        workSet.RemoveAt(j);
                        workSet.RemoveAt(i);

                        int nPoints = workSet.Count;

                        for (int k = nPoints - 1; k >= 0; k--)
                        {
                            if (currentLine.AddValidPoint(workSet.Points[k]))
                            {
                                workSet.RemoveAt(k);
                            }
                        }
                        workRes.Lines.Add(currentLine);
                        int n = linesFound.Count;
                        bool repeatedLine = false;
                        for (int m = 0; m < n; m++)
                        {
                            if (linesFound[m].Equals(currentLine))
                            {
                                repeatedLine = true;
                                break;
                            }
                        }
                        if (!repeatedLine)
                        {
                            linesFound.Add(currentLine);
                            if (workRes.Lines.Count <= ret.MinSetSize)
                            {
                               
                                if (workSet.Count > 0)
                                {
                                    CalcPointsRecurse2(workSet, workRes, ret);
                                   // return new CallPointResult() { DoMore = true, WorkingResults = workRes, WorkingSet = workSet, Reset = false };
                                }
                                else
                                {
                                    ret.AddUniqueResult(workRes);
                                }
                            }
                        }
                    }
                }
            }
            return new CallPointResult() { Reset = true };
        }
    }
}
