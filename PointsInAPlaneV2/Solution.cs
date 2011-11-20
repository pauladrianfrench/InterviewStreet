namespace PointsInAPlaneV2
{
    using System;
    using System.Collections.Generic;

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
            int minLines = ResultCollection2.BestSetSize;
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

        public static void CalcPointsRecurse(MyList<MyPoint> master, ItemSet set, Results2 res, ResultCollection2 ret)
        {
            int nSetPoint = set.Count;
            MyList<ItemSet> pars = new MyList<ItemSet>(250,20);
            for (int i = 0; i < nSetPoint - 1; i++)
            {
                for (int j = i + 1; j < nSetPoint; j++)
                {
                    int idxi = set.GetItemIndex(i);
                    int idxj = set.GetItemIndex(j);

                    long pow = ItemSet.PowerOfTwo(idxi) | ItemSet.PowerOfTwo(idxj);

                    uint map = (uint)pow;
                    if (pars.Exists(x => x.HasItems(map)))
                    {
                        continue;
                    }

                    if (idxi != idxj)
                    {
                        Results2 workRes = res.Clone();

                        ItemSet workSet = set.Clone();
                        ItemSet currentLine = new ItemSet();
                        
                        currentLine.Add(idxi);
                        currentLine.Add(idxj);
                        LineParams par = new LineParams(master[idxi], master[idxj]);

                        workSet.RemoveAt(idxi);
                        workSet.RemoveAt(idxj);

                        for (int k = 0; k < workSet.Count; ++k)
                        {
                            int id = workSet.GetItemIndex(k);
                            if (par.IsCollinear(master[id]))
                            {
                                currentLine.Add(id);
                                workSet.RemoveAt(id);
                            }
                        }
                        workRes.AddResult(currentLine.GetIndices());

                        //if (pars.Exists(x => x.Equals(currentLine)))
                        //{
                        //    continue;
                        //}

                        pars.Add(currentLine);
                        
                        if (workRes.Count < ResultCollection2.BestSetSize)
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
                                    workRes.AddResult(l.GetIndices());
                                }
                                ret.AddUniqueResult(workRes);
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
    }
}


