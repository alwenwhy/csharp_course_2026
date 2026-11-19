using App.Lab4;

namespace AppTests.Lab4;

public class LoggerTests
{
    [Test]
    public void LogGeometry_Returns_Square_When_SquareGiven()
    {
        var square = new Square(new Vertex(0, 0), 3);
        
        Assert.That(Logger.LogGeometry(square), Is.EqualTo("Квадрат"));
    }
    
    [Test]
    public void LogGeometry_Returns_Rectangle_When_RectangleGiven()
    {
        var rectangle = new Rectangle(new Vertex(0, 0), 4, 3);
        
        Assert.That(Logger.LogGeometry(rectangle), Is.EqualTo("Прямоугольник"));
    }
    
    [Test]
    public void LogGeometry_Returns_Triangle_When_TriangleGiven()
    {
        var triangle = new Triangle(new Vertex(0, 0), new Vertex(1, 1), new Vertex(-1, 1));
        
        Assert.That(Logger.LogGeometry(triangle), Is.EqualTo("Треугольник"));
    }

    [TestCase]
    public void LogGeometry_Returns_Unknown_When_NullOrOtherGeometry()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(Logger.LogGeometry(null), Is.EqualTo("Неизвестная геометрия"));
            Assert.That(Logger.LogGeometry(new Circle()), Is.EqualTo("Неизвестная геометрия"));
        }
    }
    
    private class Circle : IGeometry
    {
        public int VertexCount => 0;
 
        public double CalculateArea() => Math.PI;
    }
}