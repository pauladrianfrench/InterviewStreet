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

    public class ItemSet
    {
        uint indices;

        public ItemSet()
        {
            indices = 0;
            this.Count = 0;
        }

        public static long PowerOfTwo(int index)
        {
            switch (index)
            {
                case 0: return 1;
                case 1: return 2;
                case 2: return 4;
                case 3: return 8;
                case 4: return 16;
                case 5: return 32;
                case 6: return 64;
                case 7: return 128;
                case 8: return 256;
                case 9: return 512;
                case 10: return 1024;
                case 11: return 2048;
                case 12: return 4096;
                case 13: return 8192;
                case 14: return 16384;
                case 15: return 32768;
                case 16: return 65536;
                case 17: return 131072;
                case 18: return 262144;
                case 19: return 524288;
                case 20: return 1048576;
                case 21: return 2097152;
                case 22: return 4194304;
                case 23: return 8388608;
                case 24: return 16777216;
                case 25: return 33554432;
                case 26: return 67108864;
                case 27: return 134217728;
                case 28: return 268435456;
                case 29: return 536870912;
                case 30: return 1073741824;
                case 31: return 2147483648;
            }
            return -1;
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

        public bool HasItems(uint itms)
        {
            return (indices & itms) == itms;
        }

        public void Reset()
        {
            indices = 0;
            this.Count = 0;

        }

        public void SetUp(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                Add(i);
            }
            Count = count;
        }

        public void Add(int i)
        {
            indices |= (uint)PowerOfTwo(i);
            this.Count++;

        }

        public void RemoveAt(int i)
        {
            indices = indices & (uint)~PowerOfTwo(i);
            this.Count--;
        }

        public uint GetIndices()
        {
            return indices;
        }

        public void SetIndices(uint ind)
        {
            indices = ind;
            Count = CountBits();
        }

        public ItemSet Clone()
        {
            ItemSet ret = new ItemSet();
            ret.indices = this.indices;
            ret.Count = this.Count;
            return ret;
        }

        public bool Equals(ItemSet set)
        {
            return this.indices == set.indices;
        }

        public int Count { get; set; }

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

        public T GetItem<T>(MyList<T> set, int index)
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

        public static ItemSet operator &(ItemSet set1, ItemSet set2)
        {
            ItemSet ret = new ItemSet();
            ret.SetIndices(set1.GetIndices() & set2.GetIndices());
            return ret;
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

        public ItemSet GetLine(int index)
        {
            ItemSet ret = new ItemSet();
            ret.SetIndices(lins[index]);
            return ret;
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
            string testCount = Console.ReadLine();

            int nTest = Convert.ToInt32(testCount);

            for (int i = 0; i < nTest; i++)
            {
                MyList<MyPoint> set = new MyList<MyPoint>();
                string pointCount = Console.ReadLine();
                int nPoint = Convert.ToInt32(pointCount);
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

            ItemSet start = new ItemSet();
            start.SetUp(master.Count);
            CalcPointsRecurse(master, start, res, ret);
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
                        ItemSet currentLine = new ItemSet();
                        currentLine.Add(i);
                        currentLine.Add(j);

                        int counter = nSetPoint - 2;
                        for (int k = 0; k < counter; ++k)
                        {
                            if (k != i && k != j)
                            {
                                if (par.IsCollinear(master[k]))
                                {
                                    currentLine.Add(k);
                                }
                            }
                        }
                        ret.AddResult(currentLine.GetIndices());
                    }
                }
            }
            return ret;
        }

        public static void CalcPointsRecurse(MyList<MyPoint> master, ItemSet set, Results2 res, ResultCollection2 ret)
        {
            int nSetPoint = set.Count;

            MyList<LineParams> lineSet = new MyList<LineParams>();
            for (int i = 0; i < nSetPoint-1; i++)
            {
                for (int j = i+1; j < nSetPoint; j++)
                {
                    int idxi = set.GetItemIndex(i);
                    int idxj = set.GetItemIndex(j);

                    if (idxi != idxj)
                    {
                        LineParams par = new LineParams(master[idxi], master[idxj]);
                        if (lineSet.Exists(p => p.IsSameLine(par)))
                        {
                            continue;
                        }
                        lineSet.Add(par);
                        
                        Results2 workRes = res.Clone();

                        ItemSet workSet = set.Clone();
                        ItemSet currentLine = new ItemSet();
                        
                        currentLine.Add(idxi);
                        currentLine.Add(idxj);

                        workSet.RemoveAt(idxi);
                        workSet.RemoveAt(idxj);
                        int counter = workSet.Count;
                        for (int k = counter - 1; k >= 0; --k)
                        {
                            
                            int id = workSet.GetItemIndex(k);
                            if (par.IsCollinear(master[id]))
                            {
                                ResultCollection2.Used++;
                                currentLine.Add(id);
                                workSet.RemoveAt(id);
                            }
                        }

                        workRes.AddResult(currentLine.GetIndices());
                        
                        int nWorkSet = workSet.Count;
                        if (workRes.Count < ret.BestSetSize)
                        {
                            if (nWorkSet <= 2)
                            {
                                ItemSet l = new ItemSet();
                                for (int m = 0; m < nWorkSet; ++m)
                                {
                                    l.Add(workSet.GetItemIndex(m));
                                }
                                if (l.Count > 0)
                                {
                                    workRes.AddResult(l.GetIndices());
                                }
                                ret.AddUniqueResult(workRes);
                                // ret.ResultSet.Add(workRes);
                            }
                            else
                            {
                                CalcPointsRecurse(master, workSet, workRes, ret);
                            }
                        }
                    }
                }
            }
            return;
        }

        public static void CalcPointsRecurseNew(MyList<MyPoint> master, ItemSet set, Results2 res, ResultCollection2 ret, Results2 allLines)
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
                        LineParams par = new LineParams(master[idxi], master[idxj]);

                        Results2 workRes = res.Clone();

                        ItemSet workSet = set.Clone();
                        ItemSet currentLine = new ItemSet();

                        currentLine.Add(idxi);
                        currentLine.Add(idxj);

                        workSet.RemoveAt(idxi);
                        workSet.RemoveAt(idxj);
                        int counter = workSet.Count;
                        for (int k = counter - 1; k >= 0; --k)
                        {

                            int id = workSet.GetItemIndex(k);
                            if (par.IsCollinear(master[id]))
                            {
                                ResultCollection2.Used++;
                                currentLine.Add(id);
                                workSet.RemoveAt(id);
                            }
                        }

                        workRes.AddResult(currentLine.GetIndices());

                        int nWorkSet = workSet.Count;
                        if (workRes.Count < ret.BestSetSize)
                        {
                            if (nWorkSet <= 2)
                            {
                                ItemSet l = new ItemSet();
                                for (int m = 0; m < nWorkSet; ++m)
                                {
                                    l.Add(workSet.GetItemIndex(m));
                                }
                                if (l.Count > 0)
                                {
                                    workRes.AddResult(l.GetIndices());
                                }
                                ret.AddUniqueResult(workRes);
                            }
                            else
                            {
                                CalcPointsRecurseNew(master, workSet, workRes, ret, allLines);
                            }
                        }
                    }
                }
            }
            return;
        }
    }
}


