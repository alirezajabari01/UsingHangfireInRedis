using Domain.Entities;
using Domain.Interfaces;
using ServiceStack.Redis;

namespace Infrastructure.Wrappers;

public class RedisManager : IRedisManager
{
    private readonly IRedisClient _redisClient;

    public RedisManager(IRedisClientsManager redisClientsManager)
    {
        using IRedisClient redisClient = redisClientsManager.GetClient();
        this._redisClient = redisClient;
    }
    public bool Set(Drug drug)
    {
        return _redisClient.Set(drug.Name, drug);
    }
}