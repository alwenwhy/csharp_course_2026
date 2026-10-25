namespace App.Lab2;

public static class StackMachine
{
    public static string CalculateString(string[] codeLines)
    {
        var result = "";

        foreach (var line in codeLines) {
            if (line.StartsWith("push ")) {
                var value = line.Substring(5);
                result += value;
            } else {
                var count = int.Parse(line.Substring(4));
                result = result.Substring(0, result.Length - count);
            }
        }

        return result;
    }
}