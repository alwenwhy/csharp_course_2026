using App.Lab4;

namespace AppTests.Lab4;

public class SquareTests
{
    [Test]
    public void Square_Has_SameWidthAndHeight()
    {
        var square = new Square(new Vertex(0, 0), 4);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(square.Width, Is.EqualTo(square.Side));
            Assert.That(square.Height, Is.EqualTo(square.Side));
        }
    }

    [TestCase(4, 16)]
    public void CalculateArea_Returns_SquaredSide_When_SizeGiven(int side, int expectedArea)
    {
        var square = new Square(new Vertex(0, 0), side);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(square.CalculateArea(), Is.EqualTo(expectedArea));
            Assert.That(square.CalculateArea(), Is.EqualTo(square.Side * square.Side));
        }
    }
}