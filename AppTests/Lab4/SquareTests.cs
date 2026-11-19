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
}