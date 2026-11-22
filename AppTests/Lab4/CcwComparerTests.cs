using App.Lab4;

namespace AppTests.Lab4;

public class ClockwiseComparerTests
{
    [Test]
    public void Sort_Returns_CounterclockwiseOrder_When_CenterGiven()
    {
        var vertexes = Vertex.FromCoordinates([1, 1, 5, 4, 5, 1, 1, 4]);

        Array.Sort(vertexes, new CcwComparer(new Vertex(3, 2.5)));


        Assert.That(Vertex.ToCoordinates(vertexes), Is.EqualTo(new double[] { 5, 4, 1, 4, 1, 1, 5, 1 }));
    }

    [TestCase(new double[] { 0, -1, -1, 0, 1, 1, 1, 0, 0, 1, -1, -1, 1, -1 }, new double[] { 1, 0, 1, 1, 0, 1, -1, 0, -1, -1, 0, -1, 1, -1 })]
    [TestCase(new double[] { 0, 1, 1, 0 }, new double[] { 1, 0, 0, 1 })]
    [TestCase(new double[] { 3, 3, 1, 1, 2, 2 }, new double[] { 1, 1, 2, 2, 3, 3 })]
    public void Sort_Returns_CounterclockwiseOrder_When_CenterIsOrigin(double[] input, double[] expected)
    {
        var vertexes = Vertex.FromCoordinates(input);

        Array.Sort(vertexes, new CcwComparer());

        Assert.That(Vertex.ToCoordinates(vertexes), Is.EqualTo(expected));
    }
}