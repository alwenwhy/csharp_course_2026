using App.Lab4;

namespace AppTests.Lab4;

public class TriangleTests
{
    [Test]
    public void Constructor_Throws_Exception_When_VertexNull()
    {
        var a = new Vertex(0, 0);
        var b = new Vertex(4, 0);

        using (Assert.EnterMultipleScope())
        {
            Assert.Throws<ArgumentException>(() => new Triangle(null, a, b));
            Assert.Throws<ArgumentException>(() => new Triangle(a, null, b));
            Assert.Throws<ArgumentException>(() => new Triangle(a, b, null));
        }
    }

    [Test]
    public void Constructor_Throws_Exception_When_VertexesLiesOnSingleLine()
    {
        var a = new Vertex(0, 0);
        var b = new Vertex(4, 0);
        var c = new Vertex(-4, 0);

        using (Assert.EnterMultipleScope())
        {
            Assert.Throws<ArgumentException>(() => new Triangle(a, a, a));
            Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));
        }
    }

    [Test]
    public void CalculateArea_Returns_SameArea_When_VertexOrderChanged()
    {
        var a = new Vertex(0, 0);
        var b = new Vertex(4, 0);
        var c = new Vertex(0, 3);

        var counterclockwise = new Triangle(a, b, c);
        var clockwise = new Triangle(a, c, b);

        Assert.That(clockwise.CalculateArea(), Is.EqualTo(counterclockwise.CalculateArea()));
    }

    [Test]
    public void CalculateArea_Returns_SameArea_When_SourceVertexChangedAfterCreation()
    {
        var b = new Vertex(4, 0);
        var triangle = new Triangle(new Vertex(0, 0), b, new Vertex(0, 3));

        b.X = 100;

        Assert.That(triangle.CalculateArea(), Is.EqualTo(6));
    }

    [Test]
    public void VertexCount_Returns_ThreeVertexes_When_TriangleCreated()
    {
        var triangle = new Triangle(new Vertex(0, 0), new Vertex(4, 0), new Vertex(0, 3));
        Assert.That(triangle.VertexCount, Is.EqualTo(3));
    }

    [TestCase(0, 0, 4, 0, 0, 3, 6)]
    [TestCase(1, 1, 5, 1, 1, 4, 6)]
    [TestCase(0, 0, 1, 0, 0, 1, 0.5)]
    [TestCase(0, 0, 3, 0, 1, 2, 3)]
    [TestCase(-2, -1, 2, -1, 0, 3, 8)]
    public void CalculateArea_Returns_Area_When_VerticesGiven(double ax, double ay, double bx, double by, double cx,
        double cy, double expected)
    {
        var triangle = new Triangle(new Vertex(ax, ay), new Vertex(bx, by), new Vertex(cx, cy));
        Assert.That(triangle.CalculateArea(), Is.EqualTo(expected));
    }

    [Test]
    public void GetVertexes_Returns_VerticesInGivenOrder_When_TriangleCreated()
    {
        var triangle = new Triangle(new Vertex(0, 0), new Vertex(4, 0), new Vertex(0, 3));
        var vertexes = triangle.GetVertexes();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(vertexes, Has.Length.EqualTo(triangle.VertexCount));
            Assert.That(vertexes[0].X, Is.Zero);
            Assert.That(vertexes[0].Y, Is.Zero);
            Assert.That(vertexes[1].X, Is.EqualTo(4.0));
            Assert.That(vertexes[1].Y, Is.Zero);
            Assert.That(vertexes[2].X, Is.Zero);
            Assert.That(vertexes[2].Y, Is.EqualTo(3.0));
        }
    }

    [Test]
    public void GetVertexes_Returns_InitialVertexes_When_SourceVertexChangedAfterCreation()
    {
        var b = new Vertex(4, 0);
        var triangle = new Triangle(new Vertex(0, 0), b, new Vertex(0, 3));

        b.X = 100;

        Assert.That(triangle.GetVertexes()[1].X, Is.EqualTo(4.0));
    }

    [Test]
    public void GetVertexes_Returns_Copies_When_ReturnedVertexChanged()
    {
        var triangle = new Triangle(new Vertex(0, 0), new Vertex(4, 0), new Vertex(0, 3));

        triangle.GetVertexes()[1].X = 100;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(triangle.GetVertexes()[1].X, Is.EqualTo(4.0));
            Assert.That(triangle.CalculateArea(), Is.EqualTo(6.0));
        }
    }
}