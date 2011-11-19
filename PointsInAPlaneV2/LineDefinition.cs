namespace PointsInAPlaneV2
{
    public class LineDefinition
    {
        public LineParams LineParams { get; set; }
        public ItemSet Points { get; set; }

        public LineDefinition Clone()
        {
            return new LineDefinition() { LineParams = this.LineParams, Points = this.Points.Clone() };
        }

        public bool Equals(LineDefinition other)
        {
            return this.LineParams.IsSameLine(other.LineParams);
        }
    }
}
