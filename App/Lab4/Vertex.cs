namespace App.Lab4;

public class Vertex : I2DVertex
{
    public double X { get; set; }
    public double Y { get; set; }

    public Vertex(double x, double y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"(x = {X:F3}, y = {Y:F3})";

    public static Vertex[] FromCoordinates(double[] coordinates)
    {
        if (coordinates == null || coordinates.Length % 2 != 0)
            throw new ArgumentException("Coordinates must contain pairs of numbers");

        var vertexes = new Vertex[coordinates.Length / 2];

        for (var i = 0; i < vertexes.Length; i++)
            vertexes[i] = new Vertex(coordinates[i * 2], coordinates[i * 2 + 1]);

        return vertexes;
    }

    public static double[] ToCoordinates(I2DVertex[] vertexes)
    {
        if (vertexes == null)
            throw new ArgumentException("Vertexes cannot be null");

        var coordinates = new double[vertexes.Length * 2];

        for (var i = 0; i < vertexes.Length; i++)
        {
            coordinates[2 * i] = vertexes[i].X;
            coordinates[2 * i + 1] = vertexes[i].Y;
        }

        return coordinates;
    }
}