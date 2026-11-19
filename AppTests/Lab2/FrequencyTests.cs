using App.Lab2;

namespace AppTests.Lab2;

public class FrequencyTests
{
    [TestCase("She stood up. Then she left.",
        new[] { "stood:up", "then:she", "she:left", "she stood:up", "then she:left" })]
    [TestCase("a b c d. b c d. e b c a d.", new[] { "a:b", "b:c", "c:d", "e:b", "a b:c", "b c:d", "e b:c", "c a:d" })]
    public void FrequencyAnalysis_Returns_ExpectedDictionary_When_TextFromTask(string inputText,
        string[] dictionaryLines)
    {
        Assert.That(Frequency.FrequencyAnalysis(inputText), Is.EqualTo(ToDictionary(dictionaryLines)));
    }

    [TestCase("hello. world!", new string[0])]
    [TestCase("", new string[0])]
    public void FrequencyAnalysis_Returns_EmptyDictionary_When_NoWordPairs(string inputText, string[] dictionaryLines)
    {
        Assert.That(Frequency.FrequencyAnalysis(inputText), Is.Empty);
    }

    [TestCase("a b. c d.", new[] { "a:b", "c:d" })]
    [TestCase("a b! c d? e f.", new[] { "a:b", "c:d", "e:f" })]
    public void FrequencyAnalysis_Returns_PairsOnly_When_SentencesTooSmall(string inputText, string[] dictionaryLines)
    {
        Assert.That(Frequency.FrequencyAnalysis(inputText), Is.EqualTo(ToDictionary(dictionaryLines)));
    }

    [TestCase("a b. a c. a b.", new[] { "a:b" })]
    [TestCase("x z. x y. x z. x y. x z.", new[] { "x:z" })]
    public void FrequencyAnalysis_Returns_MostFrequentCombination_When_FrequenciesDeffer(string inputText,
        string[] dictionaryLines)
    {
        Assert.That(Frequency.FrequencyAnalysis(inputText), Is.EqualTo(ToDictionary(dictionaryLines)));
    }

    [TestCase("a z. a y.", new[] { "a:y" })]
    [TestCase("a b x. a b y.", new[] { "a:b", "b:x", "a b:x" })]
    [TestCase("a B. a a.", new[] { "a:a" })]
    public void FrequencyAnalysis_Returns_LexicographicallySmallest_When_FrequenciesEqual(string text,
        string[] expected)
    {
        Assert.That(Frequency.FrequencyAnalysis(text), Is.EqualTo(ToDictionary(expected)));
    }

    [TestCase("a,b c. a:d e. a-f g.", new[] { "a:b", "b:c", "d:e", "f:g", "a b:c", "a d:e", "a f:g" })]
    [TestCase("a   b.", new[] { "a:b" })]
    public void FrequencyAnalysis_Returns_WordsWithoutSeparators_When_TextHasPunctuation(string text, string[] expected)
    {
        Assert.That(Frequency.FrequencyAnalysis(text), Is.EqualTo(ToDictionary(expected)));
    }

    [TestCase("A B. a C.", new[] { "a:b" })]
    [TestCase("Hello World. hello world.", new[] { "hello:world" })]
    public void FrequencyAnalysis_Returns_LowercaseKeys_When_TextHasUppercase(string text, string[] expected)
    {
        Assert.That(Frequency.FrequencyAnalysis(text), Is.EqualTo(ToDictionary(expected)));
    }

    private static Dictionary<string, string> ToDictionary(string[] lines)
    {
        var result = new Dictionary<string, string>();

        foreach (var line in lines)
        {
            var parts = line.Split(':');
            result[parts[0]] = parts[1];
        }

        return result;
    }
}