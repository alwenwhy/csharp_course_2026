namespace App.Lab4;

public class Triangle : IGeometry
{
    private readonly Vertex a;
    private readonly Vertex b;
    private readonly Vertex c;
    
    public int VertexCount => 3;

    public Triangle(I2DVertex a, I2DVertex b, I2DVertex c)
    {
        if (a == null || b == null || c == null)
            throw new ArgumentException("Vertex cannot be null.");
        
        this.a = new Vertex(a.X, a.Y);
        this.b = new Vertex(b.X, b.Y);
        this.c = new Vertex(c.X, c.Y);
        
        // если площадь равна нулю, то треугольник либо схлопнут в отрезок, или это вообще одна точка получилась
        if (CalculateArea() == 0)
            throw new ArgumentException("The area is zero. Maybe, the vertexes lie on a single line?");
    }

    public double CalculateArea()
    {
        var vectorProduct = (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        return Math.Abs(vectorProduct) / 2;
    }

    public I2DVertex[] GetVertexes()
    {
        return
        [
            new Vertex(a.X, a.Y),
            new Vertex(b.X, b.Y),
            new Vertex(c.X, c.Y)
        ];
    }
}