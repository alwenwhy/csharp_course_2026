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

    [Test]
    public void FromCoordinates_Returns_VertexesFromPairs_When_EvenNumberOfCoordinates()
    {
        var vertexes = Vertex.FromCoordinates([1, 2, -3, 4.5]);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(vertexes, Has.Length.EqualTo(2));
            Assert.That(vertexes[0].X, Is.EqualTo(1.0));
            Assert.That(vertexes[0].Y, Is.EqualTo(2.0));
            Assert.That(vertexes[1].X, Is.EqualTo(-3.0));
            Assert.That(vertexes[1].Y, Is.EqualTo(4.5));
        }
    }

    [Test]
    public void FromCoordinates_Returns_EmptyArray_When_NoCoordinates()
    {
        Assert.That(Vertex.FromCoordinates([]), Is.Empty);
    }

    [Test]
    public void FromCoordinates_Throws_Exception_When_OddNumberOfCoordinates()
    {
        Assert.Throws<ArgumentException>(() => Vertex.FromCoordinates([1, 2, 3]));
    }

    [Test]
    public void ToCoordinates_Returns_SameCoordinates_When_ConvertedBackAndForth()
    {
        var coordinates = new double[] { 1, 2, -3, 4.5, 0, 0 };

        Assert.That(Vertex.ToCoordinates(Vertex.FromCoordinates(coordinates)), Is.EqualTo(coordinates));
    }
}