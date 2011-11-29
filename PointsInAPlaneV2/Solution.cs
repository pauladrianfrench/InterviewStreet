namespace PointsInAPlaneV2
{
    using System;
    using System.Collections.Generic;

    public class MyPoint
    {
        public MyPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(MyPoint p)
        {
            return p.X == this.X && p.Y == this.Y;
        }
    }
   
    public class Results2
    {
        uint[] lins;
        int count;
      
        public Results2(int size = 10)
        {
            lins = new uint[size];
            count = 0;
        }

        public int Count { get { return count; } }

        public Results2 Clone()
        {
            Results2 ret = new Results2();
            int len = this.count;
            for (int i = 0; i < len; i++)
            {
                ret.lins[i] = this.lins[i];
            }
            ret.count = this.count;
            
            return ret;
        }

        public void AddResult(uint set)
        {
            uint stashThis = 0;
            uint stashTemp = 0;
            bool shift = false;

            for (int i = 0; i < count; i++)
            {
                if (shift)
                {
                    stashTemp = stashThis;
                    stashThis = lins[i];
                    lins[i] = stashTemp;
                }
                else if (set <= lins[i])
                {
                    shift = true;
                    stashThis = lins[i];
                    lins[i] = set;
                }
            }

            lins[count++] = (shift) ? stashThis : set;
        }

        public uint GetFullLine(uint line)
        {
            int i = 0;

            if (count >= 20)
            {
                int inc = count >> 2;
                if (line < lins[inc])
                {
                    i = 0;
                }
                else if (line < lins[inc + inc])
                {
                    i = inc;
                }
                else if (line < lins[inc + inc + inc])
                {
                    i = inc + inc;
                }
                else
                {
                    i = inc + inc + inc;
                }
            }
           
            for (; i < count; i++)
            {
                if ((lins[i] & line) == line)
                {
                    return lins[i];
                }
            }
            return 0;
        }

        public uint GetLine(int index)
        {
            return lins[index];
        }

        public bool Equals(Results2 other)
        {
            //return this.count == other.count && this.values == other.values;
            int len = this.count;
            int oLen = other.count;
            if (len != oLen)
            {
                return false;
            }

            for (int i = 0; i < len; i++)
            {
                if (lins[i] != other.lins[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int Permutations { get { return Permute(count); } }

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
    public class MyList<T>
    {
        T[] items;
        int count;
        int increment;
        int size;

        public delegate bool MyDelegate(T param);

        public MyList()
        {
            size = 17;
            this.items = new T[size];
            count = 0;
            increment = 5;
        }

        public MyList(int initialSize, int increment = 10)
        {
            this.size = initialSize;
            this.items = new T[size];
            this.count = 0;
            this.increment = increment;
        }

        public int Count { get { return count; } }

        public T this[int i]
        {
            get
            {
                return items[i];
            }
            set
            {
                items[i] = value;
            }
        }

        public void Add(T p)
        {
            if (count + 1 == size)
            {
                size += increment;
                T[] np = new T[size];
                for (int i = 0; i < count; i++)
                {
                    np[i] = this.items[i];
                }
                this.items = np;
            }
            items[count++] = p;
        }

        public void RemoveAt(int pos)
        {
            for (int i = pos; i < count; i++)
            {
                this.items[i] = this.items[i + 1];
            }
            count--;
        }

        public MyList<T> Clone()
        {
            MyList<T> ret = new MyList<T>(this.size, this.increment);
            ret.count = this.count;

            for (int i = 0; i < this.count; i++)
            {
                ret[i] = this[i];
            }
            return ret;
        }

        public bool Exists(MyDelegate searchFunc)
        {
            for (int i = 0; i < Count; i++)
            {
                if (searchFunc(items[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class ResultCollection2
    {
        public static int Used = 0;

        public ResultCollection2()
        {
            this.ResultSet = new MyList<Results2>(100, 50);
            BestSetSize = Int32.MaxValue;
        }
        public int BestSetSize { get; set; }
        public MyList<Results2> ResultSet { get; set; }

        public bool AddUniqueResult(Results2 res)
        {
            
            int rCount = res.Count;

            if (rCount <= 0)
            {
                return false;
            }
            int nRes = ResultSet.Count;
            for (int i = nRes - 1; i >= 0; --i)
            {
                
                int rsCount = ResultSet[i].Count;

                if (rsCount < rCount || res.Equals(ResultSet[i]))
                {
                    return false;
                }
                else if (rCount < rsCount)
                {
                    ResultSet.RemoveAt(i);
                }
            }

            if (rCount < BestSetSize)
            {
                BestSetSize = rCount;
            }

            ResultSet.Add(res);
            return true;
        }

        public ResultCollection2 GetBestSet()
        {
            return GetBestSet(BestSetSize);
        }

        public ResultCollection2 GetBestSet(int lineCount)
        {
            ResultCollection2 ret = new ResultCollection2();

            int nRes = this.ResultSet.Count;
            for (int i = 0; i < nRes; i++)
            {
                if (this.ResultSet[i].Count == lineCount)
                {
                    ret.ResultSet.Add(this.ResultSet[i]);
                }
            }

            return ret;
        }
    }

    public class LineParams
    {
        MyPoint aPoint;
        int rise;
        int run;
        
        public LineParams(MyPoint a, MyPoint b)
        {
            aPoint = a;
            rise = b.Y - a.Y;
            run = b.X - a.X;
        }

        public bool IsCollinear(MyPoint c)
        {
            if (IsVert)
            {
                return c.X == aPoint.X;
            }
            else
            {
                return rise * (c.X - aPoint.X) == run * (c.Y - aPoint.Y);
            }
        }

        public bool IsSameLine(LineParams other)
        {
            return other.rise * this.run == other.run * this.rise && this.IsCollinear(other.aPoint);
        }
        public bool IsVert { get { return run == 0; } }
    }

    public class Solution
    {
        static void Main(string[] args)
        {
            ReadInput();
        }

        private static void ReadInput()
        {
            int nTest = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < nTest; i++)
            {
                MyList<MyPoint> set = new MyList<MyPoint>();
                int nPoint = Convert.ToInt32(Console.ReadLine());
                for (int j = 0; j < nPoint; j++)
                {
                    string point = (i + 1 == nTest && j + 1 == nPoint) ? Console.In.ReadToEnd() : Console.ReadLine();
                    string x = point.Split(' ')[0];
                    string y = point.Split(' ')[1];

                    set.Add(new MyPoint(Convert.ToInt32(x), Convert.ToInt32(y)));
                }
                WriteOutput(CalculatePoints(set));
            }
            return;
        }

        private static void WriteOutput(ResultCollection2 res)
        {
            int mod = 1000000007;
            int minLines = res.BestSetSize;
            int permutations = Results2.Permute(minLines) * res.ResultSet.Count;
            Console.WriteLine("{0} {1}", minLines % mod, permutations % mod);
        }

        public static ResultCollection2 CalculatePoints(MyList<MyPoint> master)
        {
            ResultCollection2 ret = new ResultCollection2();
            Results2 res = new Results2();
            Results2 allLines = FindLongestLines(master);

            int mc = master.Count;
            uint start = 0;
            for (int i = 0; i < mc; i++)
            {
                start|= Solution.PowOfTwo[i];
            }
            CalcPointsRecurseNew(start, res, ret, allLines);
            return ret;
        }

        public static Results2 FindLongestLines(MyList<MyPoint> master)
        {
            int nSetPoint = master.Count;
            
            Results2 ret = new Results2(nSetPoint * nSetPoint);

            MyList<LineParams> lineSet = new MyList<LineParams>();
            for (int i = 0; i < nSetPoint - 1; i++)
            {
                for (int j = i + 1; j < nSetPoint; j++)
                {
                    if (i != j)
                    {
                        LineParams par = new LineParams(master[i], master[j]);
                        if (lineSet.Exists(p => p.IsSameLine(par)))
                        {
                            continue;
                        }
                        lineSet.Add(par);
                        uint currentLine = 0;
                      
                        currentLine |= PowOfTwo[i]; //ItemSet.Add()
                        currentLine |= PowOfTwo[j];

                        int counter = nSetPoint;
                        for (int k = 0; k < counter; ++k)
                        {
                            if (k != i && k != j)
                            {
                                if (par.IsCollinear(master[k]))
                                {
                                    currentLine |= PowOfTwo[k];
                                }
                            }
                        }
                        ret.AddResult(currentLine);
                    }
                }
            }
            return ret;
        }

        public static void CalcPointsRecurseNew(uint set, Results2 res, ResultCollection2 ret, Results2 allLines)
        {
            int nSetPoint = CountBits(set); 
            for (int i = 0; i < nSetPoint - 1; i++)
            {
                for (int j = i + 1; j < nSetPoint; j++)
                {
                    int idxi = GetItemIndex(set, i);
                    int idxj = GetItemIndex(set, j);

                    if (idxi != idxj)
                    {
                        uint currentLine = 0;
                        
                        currentLine |= PowOfTwo[idxi]; // ItemSet.Add(i)
                        currentLine |= PowOfTwo[idxj];

                        uint whole = allLines.GetFullLine(currentLine);
                        currentLine = whole & set;

                        uint workSet = set;
                        workSet &= (~currentLine); // ItemSet.RemoveItems(currentLine)

                        Results2 workRes = res.Clone();

                        workRes.AddResult(currentLine);

                        int nWorkSet = CountBits(workSet);
                        if (workRes.Count < ret.BestSetSize)
                        {
                            if (nWorkSet <= 2)
                            {
                                uint l = 0;
                                for (int m = 0; m < nWorkSet; ++m)
                                {
                                    l |= PowOfTwo[GetItemIndex(workSet, m)];
                                }
                                if (l > 0)
                                {
                                    workRes.AddResult(l);
                                }
                                ret.AddUniqueResult(workRes);
                            }
                            else
                            {
                                CalcPointsRecurseNew(workSet, workRes, ret, allLines);
                            }
                        }
                    }
                }
            }
            return;
        }

        public static int GetItemIndex(uint indx, int index)
        {
            uint n = indx;

            int c; // c accumulates the total bits set in v
            uint alreadyRemoved = 0;
            for (c = 0; n > 0; c++)
            {
                n &= n - 1; // clear the least significant bit set
                uint removeThisCycle = indx - (indx & (n | alreadyRemoved));
                if (c == index)
                {
                    int idx = LogBase2(removeThisCycle);
                    return idx;
                }
                alreadyRemoved |= removeThisCycle;
            }
            throw new Exception("Item out of array bounds");
        }

        public static int LogBase2(uint mask)
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

        public static int CountBits(uint indx)
        {
            uint n = indx;
            int c; // c accumulates the total bits set in v
            for (c = 0; n > 0; c++)
            {
                n &= n - 1; // clear the least significant bit set
            }
            return c;
        }

        public T GetItem<T>(MyList<T> set, uint indices, int index)
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
                    return set[idx];
                }
                alreadyRemoved |= removeThisCycle;
            }

            throw new Exception("Item out of array bounds");
        }

        public static uint[] PowOfTwo = new uint[32] 
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
}


