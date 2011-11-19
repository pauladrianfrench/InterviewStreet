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
                List<MyPoint> set1 = new List<MyPoint>();
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
                // WriteOutput(CalcPoints(set).GetBestSet());
                // WriteOutput(CalcPoints2(set1).GetBestSet());
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

        public static ResultCollection CalcPoints(MyList<MyPoint> set)
        {
            ResultCollection ret = new ResultCollection();
            Results res = new Results();
            CalcPointsRecurse2(set, res, ret);
            return ret;
        }

        public static ResultCollection2 CalcPoints2(MyList<MyPoint> master)
        {
            ResultCollection2 ret = new ResultCollection2();
            Results2 res = new Results2();

            ItemSet start = new ItemSet();
            start.SetUp(master.Count);
            CalcPointsRecurseWithPointSet(master, start, res, ret);
            return ret;
        }

        public static void CalcPointsRecurse2(MyList<MyPoint> set, Results res, ResultCollection ret)
        {
            int nSetPoint = set.Count;
            
            for (int i = 0; i < nSetPoint - 1; i++)
            {
                for (int j = i + 1; j < nSetPoint; j++)
                {
                    if (i != j)
                    {
                        Results workRes = res.Clone();

                        Line currentLine = new Line();

                        currentLine.AddValidPoint(set[i]);
                        currentLine.AddValidPoint(set[j]);

                        MyList<MyPoint> workSet = set.Clone();

                        workSet.RemoveAt(j);
                        workSet.RemoveAt(i);

                        int nPoints = workSet.Count;

                        for (int k = nPoints - 1; k >= 0; k--)
                        {
                            if (currentLine.AddValidPoint(workSet[k]))
                            {
                                workSet.RemoveAt(k);
                            }
                        }

                        workRes.Lines.Add(currentLine);

                        if (workRes.Lines.Count < ret.MinSetSize)
                        {
                            if (nSetPoint <= 2)
                            {
                                Line l = new Line();
                                for (int m = 0; m < nSetPoint; ++m)
                                {
                                    l.AddValidPoint(set[m]);
                                }
                                if (l.Points.Count > 0)
                                {
                                    res.Lines.Add(l);
                                }
                                ret.AddUniqueResult(res);
                            }
                            else
                            {
                                CalcPointsRecurse2(workSet, workRes, ret);
                            }
                        }
                    }
                }
            }
            return;
        }

        public static void CalcPointsRecurseWithPointSet(MyList<MyPoint> master, ItemSet set, Results2 res, ResultCollection2 ret)
        {
            int nSetPoint = set.Count;
            MyList<ItemSet> pars = new MyList<ItemSet>(1000,20);
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
                        LineParams par = new LineParams(master[idxi], master[idxj]);

                        workRes.AddResult(currentLine);

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

                        if (pars.Exists(x => x.Equals(currentLine)))
                        {
                            continue;
                        }

                        pars.Add(currentLine);
                        
                        if (workRes.Lines.Count < ResultCollection2.BestSetSize)
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
                                    workRes.AddResult(l);
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


