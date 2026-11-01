namespace App.Lab2;

public class Frequency
{
    private static readonly char[] SentenceSeparators = { '.', '!', '?' };
    private static readonly char[] WordSeparators = { ' ', ',', ':', '-' };
    
    public static Dictionary<string, string> FrequencyAnalysis(string inputString)
    {
        var frequencies = new Dictionary<string, Dictionary<string, int>>();
        var sentences = inputString
            .ToLower()
            .Split(SentenceSeparators, StringSplitOptions.RemoveEmptyEntries);

        foreach (var sentence in sentences)
        {
            var words = sentence.Split(WordSeparators, StringSplitOptions.RemoveEmptyEntries);
            
            AddNGramFrequencies(words, frequencies);
        }

        return GetMostFrequentContinuations(frequencies);
    }

    private static void AddNGramFrequencies(string[] words, Dictionary<string, Dictionary<string, int>> frequencies)
    {
        //bigram
        for (var i = 0; i < words.Length - 1; i++)
        {
            AddFrequency(words[i], words[i + 1], frequencies);
        }

        //trigram
        for (var i = 0; i < words.Length - 2; i++)
        {
            var key = words[i] + " " + words[i + 1];

            AddFrequency(key, words[i + 2], frequencies);
        }
    }

    private static void AddFrequency(
        string key, 
        string continuation,
        Dictionary<string, Dictionary<string, int>> frequencies)
    {
        if (!frequencies.ContainsKey(key))
            frequencies[key] = new Dictionary<string, int>();

        if (!frequencies[key].ContainsKey(continuation))
            frequencies[key][continuation] = 0;
        
        frequencies[key][continuation]++;
    }

    private static Dictionary<string, string> GetMostFrequentContinuations(
        Dictionary<string, Dictionary<string, int>> frequencies)
    {
        var result = new Dictionary<string, string>();

        foreach (var frequency in frequencies)
        {
            result[frequency.Key] = GetMostFrequentContinuation(frequency.Value);
        }

        return result;
    }

    private static string GetMostFrequentContinuation(Dictionary<string, int> continuations)
    {
        var mostFrequentWord = "";
        var maxFrequency = 0;

        foreach (var continuation in continuations)
        {
            //я не уверен, но является ли такая "разгрузка" условий в C# верной? 
            if (continuation.Value < maxFrequency)
                continue;
            
            if (continuation.Value == maxFrequency && string.CompareOrdinal(continuation.Key, mostFrequentWord) >= 0)
                continue;
            
            mostFrequentWord = continuation.Key;
            maxFrequency = continuation.Value;
        }

        return mostFrequentWord;
    }
}