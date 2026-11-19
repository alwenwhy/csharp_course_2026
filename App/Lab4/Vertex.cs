namespace App.Lab4;

public class Vertex(double x, double y) : I2DVertex
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;

    public override string ToString() => $"(x = {X:F3}, y = {Y:F3})";
}