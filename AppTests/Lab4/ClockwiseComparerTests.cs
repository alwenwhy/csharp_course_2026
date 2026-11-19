using App.Lab4;

namespace AppTests.Lab4;

public class ClockwiseComparerTests
{
    [Test]
    public void Sort_Returns_CounterclockwiseOrder_When_CenterGiven()
    {
        var vertexes = ToVertexes(new double[] { 1, 1, 5, 4, 5, 1, 1, 4 });

        Array.Sort(vertexes, new ClockwiseComparer(new Vertex(3, 2.5)));

        Assert.That(ToCoordinates(vertexes), Is.EqualTo(new double[] { 5, 4, 1, 4, 1, 1, 5, 1 }));
    }

    [TestCase(new double[] { 0, -1, -1, 0, 1, 1, 1, 0, 0, 1, -1, -1, 1, -1 },
        new double[] { 1, 0, 1, 1, 0, 1, -1, 0, -1, -1, 0, -1, 1, -1 })]
    [TestCase(new double[] { 0, 1, 1, 0 }, new double[] { 1, 0, 0, 1 })]
    [TestCase(new double[] { 3, 3, 1, 1, 2, 2 }, new double[] { 1, 1, 2, 2, 3, 3 })]
    public void Sort_Returns_CounterclockwiseOrder_When_CenterIsOrigin(double[] input, double[] expected)
    {
        var vertexes = ToVertexes(input);

        Array.Sort(vertexes, new ClockwiseComparer());

        Assert.That(ToCoordinates(vertexes), Is.EqualTo(expected));
    }

    private static Vertex[] ToVertexes(double[] coordinates)
    {
        var vertexes = new Vertex[coordinates.Length / 2];

        for (var i = 0; i < vertexes.Length; i++)
            vertexes[i] = new Vertex(coordinates[i * 2], coordinates[i * 2 + 1]);

        return vertexes;
    }

    private static double[] ToCoordinates(I2DVertex[] vertexes)
    {
        var coordinates = new double[vertexes.Length * 2];

        for (var i = 0; i < vertexes.Length; i++)
        {
            coordinates[2 * i] = vertexes[i].X;
            coordinates[2 * i + 1] = vertexes[i].Y;
        }

        return coordinates;
    }
}