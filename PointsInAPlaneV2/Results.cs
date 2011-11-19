namespace PointsInAPlaneV2
{
    using System.Collections.Generic;

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
}
