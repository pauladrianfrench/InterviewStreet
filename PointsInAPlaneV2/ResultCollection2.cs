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
}
