namespace PointsInAPlaneV2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ResultCollection2
    {
        public ResultCollection2()
        {
            this.ResultSet = new MyList<Results2>(1000, 50);
            BestSetSize = Int32.MaxValue;
        }
        public static int BestSetSize { get; set; }
        public MyList<Results2> ResultSet { get; set; }
        
        public bool AddUniqueResult(Results2 res)
        {
            int rCount = res.Lines.Count;
            if (rCount <= 0)
            {
                return false;
            }
            int nRes = ResultSet.Count;
            for (int i = nRes -1; i >= 0; --i)
            {
                int rsCount = ResultSet[i].Lines.Count;
                
                if (rsCount < rCount || res.Equals(ResultSet[i]))
                {
                    return false;
                }
                else if (rCount < rsCount)
                {
                    ResultSet.RemoveAt(i);
                }
                
            }
            ResultSet.Add(res);
            BestSetSize = rCount;
            return true;
        }

        public ResultCollection2 GetBestSet()
        {
            ResultCollection2 ret = new ResultCollection2();

            int nRes = this.ResultSet.Count;
            for (int i = 0; i < nRes; i++)
            {
                if (this.ResultSet[i].Lines.Count == ResultCollection2.BestSetSize)
                {
                    ret.ResultSet.Add(this.ResultSet[i]);
                }
            }

            return ret;
        }
    }
}
