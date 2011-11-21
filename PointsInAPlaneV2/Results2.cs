namespace PointsInAPlaneV2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Results2
    {
        uint[] lins;
        int count;
        int values;
        public Results2()
        {
            lins = new uint[10];
            count = 0;
            values = 0;
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
            ret.values = this.values;
            return ret;
        }

        public void AddResult(uint set)
        {
            //count++;
            //values += (int)set * (int)set;
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
            if (shift)
            {
                lins[count++] = stashThis;
            }
            else
            {
                lins[count++] = set;
            }
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
}
