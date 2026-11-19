namespace App.Lab4;

public class Rectangle : IGeometry
{
    private readonly Vertex bottomLeft;
    
    public double Width { get; }
    public double Height { get; }

    public int VertexCount => 4;
    
    public Rectangle(I2DVertex bottomLeft, double width, double height)
    {
        if (bottomLeft == null)
            throw new ArgumentException("Vertex cannot be null");
        
        if (!(width > 0) ||  !(height > 0))
            throw new ArgumentException("Width and height must be greater than zero");
        
        this.bottomLeft = new Vertex(bottomLeft.X, bottomLeft.Y);
        Width = width;
        Height = height;
    }

    public double CalculateArea() => Width * Height;
    
    
}