namespace App.Lab2;

public static class StackMachine
{
    public static string CalculateString(string[] codeLines)
    {
        var result = "";

        foreach (var line in codeLines) 
        {
            if (line.StartsWith("push ")) 
            {
                var value = line[5..];
                result += value;
            } 
            else 
            {
                var count = int.Parse(line[4..]);
                result = result[..^count];
            }
        }

        return result;
    }
}