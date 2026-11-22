namespace App.Lab4;

// Counterclockwise = Ccw
public class CcwComparer : IComparer<I2DVertex>
{
    private readonly Vertex center;

    public CcwComparer(I2DVertex center)
    {
        if (center == null)
            throw new ArgumentException("center cannot be null");

        this.center = new Vertex(center.X, center.Y);
    }

    public CcwComparer() : this(new Vertex(0, 0))
    {
    }

    public int Compare(I2DVertex x, I2DVertex y)
    {
        if (x == null)
            return y == null ? 0 : -1;

        if (y == null)
            return 1;

        var angleComparison = GetAngle(x).CompareTo(GetAngle(y));
        return angleComparison != 0 ? angleComparison : GetDistanceSquared(x).CompareTo(GetDistanceSquared(y));
    }

    private double GetAngle(I2DVertex vertex)
    {
        var angle = Math.Atan2(vertex.Y - center.Y, vertex.X - center.X);

        if (angle < 0)
            angle += 2 * Math.PI;

        return angle;
    }

    private double GetDistanceSquared(I2DVertex vertex)
    {
        var dx = vertex.X - center.X;
        var dy = vertex.Y - center.Y;

        return dx * dx + dy * dy;
    }
}