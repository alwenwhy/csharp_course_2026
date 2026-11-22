using App.Lab4;

namespace AppTests.Lab4;

public class RectangleTests
{
    [Test]
    public void Constructor_Throws_Exception_When_VertexNull()
    {
        Assert.Throws<ArgumentException>(() => new Rectangle(null, 4, 3));
    }

    [Test]
    public void Constructor_Throws_Exception_When_WidthOrHeightInvalid()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.Throws<ArgumentException>(() => new Rectangle(new Vertex(0, 0), 0, 3));
            Assert.Throws<ArgumentException>(() => new Rectangle(new Vertex(0, 0), -1, 3));
            Assert.Throws<ArgumentException>(() => new Rectangle(new Vertex(0, 0), 3, 0));
            Assert.Throws<ArgumentException>(() => new Rectangle(new Vertex(0, 0), 0, -1));
            Assert.Throws<ArgumentException>(() => new Rectangle(new Vertex(0, 0), double.NaN, 3));
        }
    }

    [Test]
    public void VertexCount_Returns_FourVertexes_When_RectangleCreated()
    {
        var rectangle = new Rectangle(new Vertex(0, 0), 4, 3);
        Assert.That(rectangle.VertexCount, Is.EqualTo(4));
    }

    [TestCase(4, 3, 12)]
    [TestCase(1, 1, 1)]
    [TestCase(0.5, 0.2, 0.1)]
    [TestCase(1000000, 0.001, 1000)]
    public void CalculateArea_Returns_ExpectedArea_When_SizeGiven(double width, double height, double expectedArea)
    {
        var rectangle = new Rectangle(new Vertex(0, 0), width, height);
        Assert.That(rectangle.CalculateArea(), Is.EqualTo(expectedArea));
    }

    [Test]
    public void GetVertexes_Returns_CornersCounterclockwise_When_RectangleCreated()
    {
        var rectangle = new Rectangle(new Vertex(1, 1), 4, 3);
        var vertexes = rectangle.GetVertexes();

        using (Assert.EnterMultipleScope())
        {
            using (Assert.EnterMultipleScope())
            {
                Assert.That(vertexes, Has.Length.EqualTo(rectangle.VertexCount));
                Assert.That(vertexes[0].X, Is.EqualTo(1.0));
                Assert.That(vertexes[0].Y, Is.EqualTo(1.0));
                Assert.That(vertexes[1].X, Is.EqualTo(5.0));
                Assert.That(vertexes[1].Y, Is.EqualTo(1.0));
                Assert.That(vertexes[2].X, Is.EqualTo(5.0));
                Assert.That(vertexes[2].Y, Is.EqualTo(4.0));
                Assert.That(vertexes[3].X, Is.EqualTo(1.0));
                Assert.That(vertexes[3].Y, Is.EqualTo(4.0));
            }
        }
    }

    [Test]
    public void GetVertexes_Returns_InitialVertexes_When_SourceVertexChangedAfterCreation()
    {
        var bottomLeft = new Vertex(1, 1);
        var rectangle = new Rectangle(bottomLeft, 4, 3);

        bottomLeft.X = 100;

        Assert.That(rectangle.GetVertexes()[0].X, Is.EqualTo(1.0));
    }

    [Test]
    public void GetVertexes_Returns_Copies_When_ReturnedVertexChanged()
    {
        var rectangle = new Rectangle(new Vertex(1, 1), 4, 3);

        rectangle.GetVertexes()[0].X = 100;

        Assert.That(rectangle.GetVertexes()[0].X, Is.EqualTo(1.0));
    }
}