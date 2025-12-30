using DDD.Domain.Interfaces;
using DDD.Infra.Data.Context;

namespace DDD.Infra.Data.UoW;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

    public bool Commit() => _context.SaveChanges() > 0;

    public void Dispose() => _context.Dispose();
}
