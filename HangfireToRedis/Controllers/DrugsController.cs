using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HangfireToRedis.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DrugsController : ControllerBase
{
    private readonly IDrugRepository _drugRepository;
    private readonly IHangfireManager _hangfireManager;
    private readonly IRedisManager _redisManager;

    public DrugsController(IDrugRepository drugRepository, IHangfireManager hangfireManager, IRedisManager redisManager)
    {
        _drugRepository = drugRepository;
        _hangfireManager = hangfireManager;
        _redisManager = redisManager;
    }

    [HttpPost]
    public IActionResult Create()
    {
        Drug drug = new Drug
        {
            Name = "ali",
            Status = Status.Pending,
            ExpirationDateTime = DateTime.Now + TimeSpan.FromSeconds(10)
        };

        _redisManager.Set(drug);

        int result = _drugRepository.Create(drug);

        int timeSpan = drug.ExpirationDateTime.Second - DateTime.Now.Second;

        _hangfireManager.Scheduler(drug.Id, timeSpan);

        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_drugRepository.GetAll());
    }
}