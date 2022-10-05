using Domain.Interfaces;
using Hangfire;
using Hangfire.MemoryStorage;
using Infrastructure.Wrappers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ServiceStack.Redis;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IHangfireManager, HangfireManager>();
        services.AddScoped<IRedisManager, RedisManager>();

        services.AddSingleton(typeof(IRedisClientsManager)
            , new RedisManagerPool("redis://@localhost:6379?Db=0&ConnectTimeout=5000&IdleTimeOutSecs=100"));
        services.AddSingleton<IRedisManager, RedisManager>();

        services.AddHangfire(configuration =>
        {
            configuration.UseSerializerSettings
            (
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );
        });

        GlobalConfiguration.Configuration.UseRedisStorage("localhost:6379");
        services.AddHangfireServer();
        GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute {Attempts = 5});
    }
}