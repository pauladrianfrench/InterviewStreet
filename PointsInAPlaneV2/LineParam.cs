namespace PointsInAPlaneV2
{
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
        public bool IsVert { get { return run == 0; }  }
    }
}
