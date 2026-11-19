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
}