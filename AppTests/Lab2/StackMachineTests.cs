using App.Lab2;

namespace AppTests.Lab2;

public class StackMachineTests
{
    [TestCase(
        new[]
        {
            "push Привет! Это снова я! Пока!", "pop 5", "push Как твои успехи? Плохо?", "push qwertyuiop",
            "push 1234567890", "pop 27"
        }, "Привет! Это снова я! Как твои успехи?")]
    [TestCase(new[] { "push Hello, ", "push World!", "pop 7" }, "Hello,")]
    [TestCase(new[] { "push abcdef", "pop 2", "push XY", "pop 3" }, "abc")]
    public void CalculateString_Returns_ExpectedString_When_PushAndPopMixed(string[] codeLines, string expected)
    {
        Assert.That(StackMachine.CalculateString(codeLines), Is.EqualTo(expected));
    }

    [TestCase(new[] { "push Hello," }, "Hello,")]
    [TestCase(new[] { "push Hello,", "push World!" }, "Hello,World!")]
    [TestCase(new[] { "push a", "push b", "push c" }, "abc")]
    public void CalculateString_Returns_ConcatStrings_When_OnlyPush(string[] codeLines, string expected)
    {
        Assert.That(StackMachine.CalculateString(codeLines), Is.EqualTo(expected));
    }

    [TestCase(new[] { "push abc", "pop 1" }, "ab")]
    [TestCase(new[] { "push abcdef", "pop 2", "pop 2" }, "ab")]
    [TestCase(new[] { "push Hello", "pop 0" }, "Hello")]
    public void CalculateString_Returns_StringWithoutLastSymbols_When_OnlyPop(string[] codeLines, string expected)
    {
        Assert.That(StackMachine.CalculateString(codeLines), Is.EqualTo(expected));
    }

    [TestCase(new[] { "push a", "pop 1" }, "")]
    [TestCase(new[] { "push abc", "pop 3" }, "")]
    [TestCase(new[] { "push Hello", "push World", "pop 10" }, "")]
    public void CalculateString_Returns_EmptyString_When_OnlyRemovesEverything(string[] codeLines, string expected)
    {
        Assert.That(StackMachine.CalculateString(codeLines), Is.EqualTo(expected));
    }

    [Test]
    public void CalculateString_Returns_EmptyString_When_NoCommands()
    {
        Assert.That(StackMachine.CalculateString([]), Is.EqualTo(""));
    }
}