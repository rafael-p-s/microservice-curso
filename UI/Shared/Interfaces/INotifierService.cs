namespace Shared.Interfaces;

public interface INotifierService
{
    void AddLog(string message);
    string? GetLog();
    bool HasMessages();
}
