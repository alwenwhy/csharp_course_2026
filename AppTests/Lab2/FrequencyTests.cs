using App.Lab2;

namespace AppTests.Lab2;

public class FrequencyTests
{
    [TestCase("She stood up. Then she left.", new[] { "stood:up", "then:she", "she:left", "she stood:up", "then she:left" })]
    [TestCase("a b c d. b c d. e b c a d.", new[] { "a:b", "b:c", "c:d", "e:b", "a b:c", "b c:d", "e b:c", "c a:d" })]
    [TestCase("hello. world!", new string[] { })]
    [TestCase("", new string[] { })]
    [TestCase("a b. c d.", new[] { "a:b", "c:d" })]
    [TestCase("a,b c. a:d e. a-f g.", new[] { "a:b", "b:c", "d:e", "f:g", "a b:c", "a d:e", "a f:g" })]
    [TestCase("a b. a c. a b.", new[] { "a:b" })]
    [TestCase("a z. a y.", new[] { "a:y" })]
    [TestCase("a b x. a b y.", new[] { "a:b", "b:x", "a b:x" })]
    [TestCase("A B. a C.", new[] { "a:b" })]
    public void TestsForFrequencyMethod(string inputText, string[] dictionaryLines)
    {
        var result = Frequency.FrequencyAnalysis(inputText);
        
        var actual = result
            .Select(entry => $"{entry.Key}:{entry.Value}")
            .OrderBy(line => line, StringComparer.Ordinal)
            .ToArray();
        
        var expected = dictionaryLines
            .OrderBy(line => line, StringComparer.Ordinal)
            .ToArray();
        
        Assert.That(actual, Is.EqualTo(expected));
    }
}