using App.Lab4;

namespace AppTests.Lab4;

public class VertexTests
{
    [TestCase(1.5, -2, "(x = 1,500, y = -2,000)")]
    [TestCase(0, 0, "(x = 0,000, y = 0,000)")]
    [TestCase(1.23456, 7.8, "(x = 1,235, y = 7,800)")]
    [TestCase(10000000, 0.1, "(x = 10000000,000, y = 0,100)")]
    [TestCase(-3.0004, 0.0005, "(x = -3,000, y = 0,001)")]
    public void ToString_Returns_CoordinatesWithThreeDecimals_When_VertexGiven(double x, double y, string expected)
    {
        Assert.That(new Vertex(x, y).ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void ToString_Returns_NewCoordinates_When_VertexChanged()
    {
        var vertex = new Vertex(4, 3);

        vertex.X = 25;
        vertex.Y = -7.4212;

        Assert.That(vertex.ToString(), Is.EqualTo("(x = 25,000, y = -7,421)"));
    }
}