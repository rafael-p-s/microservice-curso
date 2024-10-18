using Shared.Interfaces;

namespace Shared.Services;

public class NotifierService : INotifierService
{
    protected readonly List<string> Messages = new();

    public void AddLog(string message)
    {
        Messages.Add(message);
    }

    public string? GetLog()
    {
        return Messages.Any() ? string.Join("; ", Messages) : string.Empty;
    }

    public bool HasMessages()
    {
        return Messages.Any();
    }
}
