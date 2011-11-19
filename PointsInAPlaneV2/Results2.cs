namespace PointsInAPlaneV2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Results2
    {   
        public Results2()
        {
            this.Lines = new MyList<ItemSet>();
        }
        public MyList<ItemSet> Lines { get; set; }

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

        public void AddResult(ItemSet set)
        {
            Lines.Add(set);
        }

        public bool Equals(Results2 other)
        {
            int len = this.Lines.Count;
            int oLen = other.Lines.Count;
            if (len != oLen)
            {
                return false;
            }

            for (int i = 0; i < len; i++)
            {
                bool found = false;
                for (int j = 0; j < len; j++)
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
}
