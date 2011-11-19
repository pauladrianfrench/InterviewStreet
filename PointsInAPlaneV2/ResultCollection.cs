namespace PointsInAPlaneV2
{
    using System;
    using System.Collections.Generic;

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
}
