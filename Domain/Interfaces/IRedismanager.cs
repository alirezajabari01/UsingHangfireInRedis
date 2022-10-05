using Domain.Entities;

namespace Domain.Interfaces;

public interface IRedisManager
{
    bool Set(Drug drug);
}