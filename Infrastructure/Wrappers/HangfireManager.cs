using Domain.Interfaces;
using Hangfire;

namespace Infrastructure.Wrappers;

public class HangfireManager : IHangfireManager
{
    private readonly IDrugRepository _drugRepository;

    public HangfireManager(IDrugRepository drugRepository)
    {
        _drugRepository = drugRepository;
    }
    
    public void Scheduler(long id, int timeSpan)
    {
        BackgroundJob.Schedule
        (
            () =>
                _drugRepository.ExpireDrug(id),
            TimeSpan.FromSeconds(timeSpan)
        );
    }
}