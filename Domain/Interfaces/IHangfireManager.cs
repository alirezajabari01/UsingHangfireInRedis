namespace Domain.Interfaces;

public interface IHangfireManager
{
    void Scheduler(long id, int timeSpan);
}