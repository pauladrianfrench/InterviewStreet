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

    public class LineParams
    {
        double x;
        public LineParams(MyPoint a, MyPoint b)
        {
            double rise = b.Y - a.Y;
            double run = b.X - a.X;

            if (run != 0.0)
            {
                Gradient = rise / run;
                Intercept = a.Y - Gradient * a.X;
                IsVert = false;
            }
            else
            {
                x = a.X;
                IsVert = true;
            }

        }
        public bool IsCollinear(MyPoint c)
        {
            if (IsVert)
            {
                return c.X == this.x;
            }
            else
            {
                return c.Y == Gradient * c.X + Intercept;
            }
        }
        public double Gradient { get; private set; }
        public double Intercept { get; private set; }
        public bool IsVert { get; private set; }
    }

    public class ItemSet
    {
        uint indices;

        static long[] PowersOfTwo; 

        public ItemSet()
        {
            indices = 0;
            PowersOfTwo = new long [32]
             {
                 1,
                 2,
                 4,
                 8,
                 16,
                 32,
                 64,
                 128,
                 256,
                 512,
                 1024,
                 2048,
                 4096,
                 8192,
                 16384,
                 32768,
                 65536,
                131072,
                262144,
                524288,
                1048576,
                2097152,
                4194304,
                8388608,
                16777216,
                33554432,
                67108864,
                134217728,
                268435456,
                536870912,
                1073741824,
                2147483648
             };

        }

        public int LogBase2(uint mask)
        {
            switch (mask)
            {
                case 1: return 0;
                case 2: return 1;
                case 4: return 2;
                case 8: return 3;
                case 16: return 4;
                case 32: return 5;
                case 64: return 6;
                case 128: return 7;
                case 256: return 8;
                case 512: return 9;
                case 1024: return 10;
                case 2048: return 11;
                case 4096: return 12;
                case 8192: return 13;
                case 16384: return 14;
                case 32768: return 15;
                case 65536: return 16;
                case 131072: return 17;
                case 262144: return 18;
                case 524288: return 19;
                case 1048576: return 20;
                case 2097152: return 21;
                case 4194304: return 22;
                case 8388608: return 23;
                case 16777216: return 24;
                case 33554432: return 25;
                case 67108864: return 26;
                case 134217728: return 27;
                case 268435456: return 28;
                case 536870912: return 29;
               case 1073741824: return 30;
               case 2147483648: return 31;
            }
            return -1;
        }

        public void Reset()
        {
            indices = 0;
        }

        public void SetUp(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                Add(i);
            }
        }

        public void Add(int i)
        {
            indices |= (uint)PowersOfTwo[i];
            
        }

        public void RemoveAt(int i)
        {
            indices = indices & (uint)~PowersOfTwo[i];
            
        }

        public uint GetIndices()
        {
            return indices;
        }

        public ItemSet Clone()
        {
            return new ItemSet() { indices = this.indices };
        }

        public bool Equals(ItemSet set)
        {
            return this.indices == set.indices;
        }

        public int Count { get { return CountBits(); } }

        public int CountBits() 
        {
            uint n = indices;
            int c; // c accumulates the total bits set in v
            for (c = 0; n > 0; c++)
            {
                n &= n - 1; // clear the least significant bit set
            }
            return c;
        }

        public MyPoint GetItem(TestSet set, int index)
        {
            uint n = indices;
            
            int c; // c accumulates the total bits set in v
            uint alreadyRemoved = 0;
            for (c = 0; n > 0; c++)
            {
                n &= n - 1; // clear the least significant bit set
                uint removeThisCycle = indices - (indices & (n | alreadyRemoved));
                if (c == index)
                {
                    int idx = LogBase2(removeThisCycle);
                    return set.Points[idx];
                }
                alreadyRemoved |= removeThisCycle;
            }
            
            throw new Exception("Item out of array bounds");
        }

        public List<T> GetList<T>(List<T> masterList)
        {
            List<T> ret = new List<T>();
            uint mask = 0x01;

            int nList = masterList.Count;
            int count = 0;
            for (int i = 0; i < nList; ++i)
            {
                if ((mask & indices) != 0)
                {
                    ret.Add(masterList[i]);
                    count++;
                }
                mask = mask << 1;
            }

            return ret;
        }

        public int GetItemIndex(int index)
        {
            uint n = indices;

            int c; // c accumulates the total bits set in v
            uint alreadyRemoved = 0;
            for (c = 0; n > 0; c++)
            {
                n &= n - 1; // clear the least significant bit set
                uint removeThisCycle = indices - (indices & (n | alreadyRemoved));
                if (c == index)
                {
                    int idx = LogBase2(removeThisCycle);
                    return idx;
                }
                alreadyRemoved |= removeThisCycle;
            }

            throw new Exception("Item out of array bounds");
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
            int pCount = this.Points.Count;
            if (pCount == 1)
            {
                if (pCount == other.Points.Count)
                {
                    return this.Points[0] == other.Points[0];
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return this.m == other.m && this.c == other.c && this.isVert == other.isVert;
            }
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

    public class Results2
    {
        public Results2()
        {
            this.Lines = new List<ItemSet>();
        }
        public List<ItemSet> Lines { get; set; }

        public Results2 Clone()
        {
            Results2 ret = new Results2();
            int len = this.Lines.Count;
            for (int i = 0; i < len; i++)
            {
                ret.Lines.Add(this.Lines[i].Clone());
            }
            return ret;
        }

        public bool Equals(Results2 other)
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

    public class ResultCollection2
    {
        public ResultCollection2()
        {
            this.ResultSet = new List<Results2>();
        }

        public List<Results2> ResultSet { get; private set; }
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
        public bool AddUniqueResult(Results2 res)
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

        public ResultCollection2 GetBestSet()
        {
            ResultCollection2 ret = new ResultCollection2();

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
                List<MyPoint> set1 = new List<MyPoint>();
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
                // WriteOutput(CalcPoints(set).GetBestSet());
                // WriteOutput(CalcPoints2(set1).GetBestSet());
                // Console.WriteLine("2 6");
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
            CalcPointsRecurse2(set, res, ret);
            return ret;
        }

        public static ResultCollection2 CalcPoints2(TestSet master)
        {
            ResultCollection2 ret = new ResultCollection2();
            Results2 res = new Results2();

            ItemSet start = new ItemSet();
            start.SetUp(master.Count);
            CalcPointsRecurseWithPointSet(master, start, res, ret);
            return ret;
        }

        public static void CalcPointsRecurse2(TestSet set, Results res, ResultCollection ret)
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
                ret.ResultSet.Add(res);
                return;
            }

            for (int i = 0; i < nSetPoint - 1; i++)
            {
                for (int j = i + 1; j < nSetPoint; j++)
                {
                    if (i != j)
                    {
                        Results workRes = res.Clone();

                        Line currentLine = new Line();

                        currentLine.AddValidPoint(set.Points[i]);
                        currentLine.AddValidPoint(set.Points[j]);

                        TestSet workSet = set.Clone();

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

                        if (workRes.Lines.Count < ret.MinSetSize)
                        {
                            if (workSet.Count > 0)
                            {
                                CalcPointsRecurse2(workSet, workRes, ret);
                            }
                            else
                            {
                                // ret.ResultSet.Add(workRes);
                                ret.AddUniqueResult(workRes);
                            }
                        }
                    }
                }
            }
            return;
        }

        public static void CalcPointsRecurseWithPointSet(TestSet master, ItemSet set, Results2 res, ResultCollection2 ret)
        {
            int nSetPoint = set.Count;
            for (int i = 0; i < nSetPoint - 1; i++)
            {
                for (int j = i + 1; j < nSetPoint; j++)
                {
                    int idxi = set.GetItemIndex(i);
                    int idxj = set.GetItemIndex(j);
                    if (idxi != idxj)
                    {
                        Results2 workRes = res.Clone();

                        ItemSet workSet = set.Clone();
                        ItemSet currentLine = new ItemSet();

                        currentLine.Add(idxi);
                        currentLine.Add(idxj);
                        LineParams par = new LineParams(master.Points[idxi], master.Points[idxj]);
                        
                        workSet.RemoveAt(idxi);
                        workSet.RemoveAt(idxj);

                        for (int k = 0; k < workSet.Count; ++k)
                        {
                            int id = workSet.GetItemIndex(k);
                            if (par.IsCollinear(master.Points[id]))
                            {
                                currentLine.Add(id);
                                workSet.RemoveAt(id);
                            }
                        }

                        workRes.Lines.Add(currentLine);
                        if (workRes.Lines.Count < ret.MinSetSize)
                        {
                            int nWorkSet = workSet.Count;
                            if (nWorkSet <= 2)
                            {
                                ItemSet l = new ItemSet();
                                for (int m = 0; m < nWorkSet; ++m)
                                {
                                    l.Add(workSet.GetItemIndex(m));
                                }
                                if (l.Count > 0)
                                {
                                    workRes.Lines.Add(l);
                                }
                                ret.AddUniqueResult(workRes);
                                // ret.ResultSet.Add(workRes);
                            }
                            else
                            {
                                CalcPointsRecurseWithPointSet(master, workSet, workRes, ret);
                            }
                        }
                    }
                }
            }
            return;
        }
    }
}

