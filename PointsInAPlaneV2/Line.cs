namespace PointsInAPlaneV2
{
    using System.Collections.Generic;

    public class Line
    {
        double m;
        double c;
        bool isVert;

        public Line()
        {
            Points = new List<MyPoint>();
            m = 0;
            c = 0;
            isVert = false;
        }

        public List<MyPoint> Points { get; set; }

        public Line Clone()
        {
            Line ret = new Line();
            int len = this.Points.Count;
            for (int i = 0; i < len; i++)
            {
                ret.Points.Add(this.Points[i]);
            }
            ret.m = this.m;
            ret.c = this.c;
            ret.isVert = this.isVert;
            return ret;
        }

        public bool Equals(Line other)
        {
            int pCount = this.Points.Count;
            if (pCount == 1)
            {
                if (pCount == other.Points.Count)
                {
                    return this.Points[0] == other.Points[0];
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return this.m == other.m && this.c == other.c && this.isVert == other.isVert;
            }
        }

        public bool AddValidPoint(MyPoint p)
        {
            if (Points.Count < 2)
            {
                Points.Add(p);

                if (Points.Count == 2)
                {
                    double rise = Points[1].Y - Points[0].Y;
                    double run = Points[1].X - Points[0].X;

                    if (run != 0.0)
                    {
                        m = rise / run;
                        c = Points[0].Y - m * Points[0].X;
                    }
                    else
                    {
                        isVert = true;
                    }
                }
                return true;
            }
            else
            {
                return AddCollinearPoint(p);
            }
        }

        private bool AddCollinearPoint(MyPoint p)
        {
            if (this.PassesThroughPoint(p))
            {
                Points.Add(p);
                return true;
            }
            return false;
        }

        public bool HasPoint(MyPoint p)
        {
            int nPoint = this.Points.Count;
            for (int i = 0; i < nPoint; i++)
            {
                if (this.Points[i].Equals(p))
                {
                    return true;
                }
            }
            return false;
        }

        private bool PassesThroughPoint(MyPoint p)
        {
            if (isVert)
            {
                return p.X == Points[0].X;
            }
            else
            {
                return p.Y == m * p.X + c;
            }
        }
    }
}
