using Domain.Entities;

namespace Domain.Interfaces;

public interface IDrugRepository
{
    int Create(Drug drug);
    List<Drug> GetAll();
    int ExpireDrug(long id);
}