using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>
        (
            options => options.UseInMemoryDatabase("Drug_DB")
        );

        services.AddScoped<IDrugRepository, DrugRepository>();
    }
}