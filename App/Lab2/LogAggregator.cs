using System.Globalization;
using System.Text;

namespace App.Lab2;

public static class LogAggregator
{
    private static readonly string TimestampFormat = "yyyy-MM-dd HH:mm:ss";
    
    public static string MergeLogs(params string[] logLines)
    {
        if (logLines == null || logLines.Length == 0)
            return "";
        
        var groups = new Dictionary<string, LogGroup>();

        foreach (var logLine in logLines)
        {
            if (!TryParseLog(logLine, out var log))
                continue;
            
            if (!groups.ContainsKey(log.TraceId))
                groups[log.TraceId] = new LogGroup(log.TraceId);
            
            groups[log.TraceId].Logs.Add(log);
        }
        
        if (groups.Count == 0)
            return "";
        
        var sortedGroups = groups.Values.ToList();

        foreach (var group in sortedGroups)
        {
            group.Logs.Sort(CompareLogs);
        }
        
        sortedGroups.Sort(CompareGroups);
        
        return FormatLogs(sortedGroups);
    }

    private static bool TryParseLog(string logLine, out LogEntry log)
    {
        //Формат входной строки лога: [YYYY-MM-DD HH:mm:ss] [TraceId] ServiceName: Message
        //т. е. я буду считать, что только такой формат правильный, а всё остальное
        //(с лишними символами в т. ч.) - мусор 
        log = null;
        
        if (string.IsNullOrWhiteSpace(logLine))
            return false;
        
        //time
        var timestampEnd = logLine.IndexOf(']');
        
        if (timestampEnd < 0)
            return false;
        
        var timestampString = logLine.Substring(1, timestampEnd - 1);

        if (!DateTime.TryParseExact(
                timestampString, 
                TimestampFormat, 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None,
                out var timestamp))
            return false;
        
        //trace
        var traceStart = timestampEnd + 2;
        
        if (traceStart >= logLine.Length || logLine[traceStart - 1] != ' ' ||  logLine[traceStart] != '[')
            return false;
        
        var traceEnd = logLine.IndexOf(']', traceStart);
        
        if (traceEnd < 0)
            return false;
        
        var traceId = logLine.Substring(traceStart + 1, traceEnd - traceStart - 1);
        
        if (string.IsNullOrWhiteSpace(traceId))
            return false;

        //service & message
        var serviceStart = traceEnd + 2;
        
        if (serviceStart >= logLine.Length || logLine[serviceStart - 1] != ' ')
            return false;
        
        var messageSeparator = logLine.IndexOf(':', serviceStart);
        
        if (messageSeparator < 0)
            return false;
        
        var serviceName = logLine.Substring(serviceStart, messageSeparator - serviceStart).Trim();
        
        if (serviceName.Length == 0)
            return false;
        
        var message = logLine.Substring(messageSeparator + 1).TrimStart();

        //result
        log = new LogEntry(timestamp, traceId, serviceName, message);
        
        return true;
    }

    private static int CompareLogs(LogEntry x, LogEntry y)
    {
        var comparison = x.Timestamp.CompareTo(y.Timestamp);
        
        if (comparison != 0)
            return comparison;
        
        comparison = string.CompareOrdinal(x.ServiceName,  y.ServiceName);
        
        return comparison != 0 ? comparison : string.CompareOrdinal(x.Message, y.Message);
    }

    private static int CompareGroups(LogGroup x, LogGroup y)
    {
        var comparison = x.Logs[0].Timestamp.CompareTo(y.Logs[0].Timestamp);
        
        return comparison != 0 ? comparison : string.CompareOrdinal(x.TraceId, y.TraceId);
    }

    private static string FormatLogs(List<LogGroup> groups)
    {
        /*
         * Хоть в первом задании я не пользовался билдером (да и после ревью я так понял, что всё хорошо),
         * здесь уже работа с изменяемой строкой как будто потяжелее, оттого для эффективности буду им пользоваться.
         */
        var result = new StringBuilder();
        
        for (var i = 0; i < groups.Count; i++)
        {
            var group = groups[i];
            
            if (i > 0)
                result.AppendLine();
            
            result.AppendLine($"--- TraceId: [{group.TraceId}] ---");

            foreach (var log in group.Logs)
            {
                result.AppendLine(
                    $"[{log.Timestamp.ToString(TimestampFormat, CultureInfo.InvariantCulture)}] " 
                    + $"{log.ServiceName}: {log.Message}"
                );
            }
        }
        
        result.Remove(result.Length - 1, 1);
        //а можно ещё сделать result.Length--;
        //но тогда меня скорее всего заподозрят в программировании со светлой темой.
        return result.ToString();
    }
    
    private class LogEntry(
        DateTime timestamp,
        string traceId,
        string serviceName,
        string message)
    {
        public DateTime Timestamp { get; } = timestamp;
        public string TraceId { get; } = traceId;
        public string ServiceName { get; } = serviceName;
        public string Message { get; } = message;
    }
    
    private class LogGroup(string traceId)
    {
        public string TraceId { get; } = traceId;
        public List<LogEntry> Logs { get; } = new List<LogEntry>();
    }
}