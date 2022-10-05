using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Persistence.Repositories;

public class DrugRepository : IDrugRepository
{
    private readonly DatabaseContext _context;

    public DrugRepository(DatabaseContext context)
    {
        _context = context;
    }
    public int Create(Drug drug)
    {
        _context.Drugs.Add(drug);
        return _context.SaveChanges();
    }

    public List<Drug> GetAll()
    {
        return _context.Drugs.ToList();
    }

    public int ExpireDrug(long id)
    {
        Drug drug = _context.Drugs.SingleOrDefault(drug => drug.Id == id) ?? throw new InvalidOperationException();

        drug.Status = Status.Expired;

        _context.Drugs.Update(drug);
        return _context.SaveChanges();
    }
}