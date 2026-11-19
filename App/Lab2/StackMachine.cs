using System.Text;

namespace App.Lab2;

public static class StackMachine
{
    private const string PushCommand = "push ";
    private const string PopCommand = "pop ";
    
    public static string CalculateString(string[] codeLines)
    {
        var result = new StringBuilder();

        foreach (var line in codeLines) 
        {
            if (line.StartsWith(PushCommand)) 
            {
                result.Append(line, PushCommand.Length, line.Length - PushCommand.Length);
            } 
            else 
            {
                var count = int.Parse(line[PopCommand.Length..]);
                /*
                 * Вычитал, что даже если мы за границей оставим большую строку, то после
                 * return-а наш билдер будет полностью удалён сборщиком мусора и у
                 * нас не будет проблемы постоянного нахождения ненужных данных в памяти.
                 */
                result.Length -= count;
            }
        }

        return result.ToString();
    }
}