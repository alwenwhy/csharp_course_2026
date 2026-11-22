using App.Lab4;

namespace AppTests.Lab4;

public class SquareTests
{
    [TestCase(4)]
    [TestCase(0.5)]
    public void Constructor_Returns_SquareWithEqualWidthAndHeight_When_SideGiven(double side)
    {
        var square = new Square(new Vertex(0, 0), side);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(square.Width, Is.EqualTo(side));
            Assert.That(square.Height, Is.EqualTo(side));
        }
    }

    [TestCase(4, 16)]
    [TestCase(1, 1)]
    [TestCase(0.5, 0.25)]
    public void CalculateArea_Returns_SquaredSide_When_SideGiven(double side, double expectedArea)
    {
        var square = new Square(new Vertex(0, 0), side);

        Assert.That(square.CalculateArea(), Is.EqualTo(expectedArea));
    }

    [Test]
    public void VertexCount_Returns_FourVertexes_When_SquareCreated()
    {
        var square = new Square(new Vertex(0, 0), 4);
        Assert.That(square.VertexCount, Is.EqualTo(4));
    }

    [Test]
    public void CalculateArea_Returns_SameArea_When_SquareUsedAsGeometry()
    {
        IGeometry square = new Square(new Vertex(0, 0), 4);
        Assert.That(square.CalculateArea(), Is.EqualTo(16));
    }

    [TestCase(0)]
    [TestCase(-4)]
    [TestCase(double.NaN)]
    public void Constructor_Throws_Exception_When_SideNotPositive(double side)
    {
        Assert.Throws<ArgumentException>(() => new Square(new Vertex(0, 0), side));
    }

    [Test]
    public void GetVertexes_Returns_FourCornersOfSquare_When_SquareCreated()
    {
        var square = new Square(new Vertex(2, 3), 4);
        var vertexes = square.GetVertexes();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(vertexes, Has.Length.EqualTo(4));
            Assert.That(vertexes[0].X, Is.EqualTo(2.0));
            Assert.That(vertexes[0].Y, Is.EqualTo(3.0));
            Assert.That(vertexes[2].X, Is.EqualTo(6.0));
            Assert.That(vertexes[2].Y, Is.EqualTo(7.0));
        }
    }
}