namespace PointsInAPlaneV2
{
    public class MyPoint
    {
        public MyPoint(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(MyPoint p)
        {
            return p.X == this.X && p.Y == this.Y;
        }
    }
}
