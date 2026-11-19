using App.Lab2;

namespace AppTests.Lab2;

public class LogAggregatorTests
{
    [Test]
    public void MergeLogs_OnlyGarbage_ReturnsEmptyString()
    {
        var result = LogAggregator.MergeLogs("garbage", "[invalid]", null);
        Assert.That(result, Is.EqualTo(""));
    }
    
    [Test]
    public void MergeLogs_ValidLog_ReturnsFormattedLog()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:01] [abc-123] AuthService: User logged in"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [abc-123] ---",
            "[2023-10-25 14:05:01] AuthService: User logged in"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_SingleGroup_SortsByTime()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:02] [trace-A] AuthService: Second",
            "[2023-10-25 14:05:01] [trace-A] AuthService: First"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: First",
            "[2023-10-25 14:05:02] AuthService: Second"
        });

        Assert.That(result, Is.EqualTo(expected));
    }
    
        [Test]
    public void MergeLogs_MultipleGroups_SortsGroupsByFirstEvent()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:05] [trace-A] AuthService: Second",
            "[2023-10-25 14:05:00] [trace-B] Gateway: First",
            "[2023-10-25 14:05:01] [trace-A] AuthService: First"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-B] ---",
            "[2023-10-25 14:05:00] Gateway: First",
            "",
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: First",
            "[2023-10-25 14:05:05] AuthService: Second"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_SameTimestamp_SortsByServiceName()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:01] [trace-A] Gateway: Request received",
            "[2023-10-25 14:05:01] [trace-A] AuthService: User logged in",
            "[2023-10-25 14:05:01] [trace-A] BillingService: Payment started"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: User logged in",
            "[2023-10-25 14:05:01] BillingService: Payment started",
            "[2023-10-25 14:05:01] Gateway: Request received"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_SameFirstEventTime_SortsGroupsByTraceId()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:01] [trace-B] Gateway: Request received",
            "[2023-10-25 14:05:01] [trace-A] AuthService: User logged in"
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: User logged in",
            "",
            "--- TraceId: [trace-B] ---",
            "[2023-10-25 14:05:01] Gateway: Request received"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_MixedValidAndInvalid_ProcessesOnlyValid()
    {
        var result = LogAggregator.MergeLogs(
            "garbage",
            "[invalid]",
            "[2023-10-25 14:05:01] [trace-A] AuthService: User logged in",
            "[2023-15-45 14:05:01] [trace-B] Gateway: Invalid date",
            null
        );

        var expected = string.Join(Environment.NewLine, new[]
        {
            "--- TraceId: [trace-A] ---",
            "[2023-10-25 14:05:01] AuthService: User logged in"
        });

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MergeLogs_EmptyTraceId_ReturnsEmptyString()
    {
        var result = LogAggregator.MergeLogs(
            "[2023-10-25 14:05:01] [] AuthService: User logged in"
        );

        Assert.That(result, Is.EqualTo(""));
    }
}