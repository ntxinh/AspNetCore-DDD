using System.Linq;

using DDD.Domain.Interfaces;
using DDD.Domain.Models;
using DDD.Infra.Data.Context;

using Microsoft.EntityFrameworkCore;

namespace DDD.Infra.Data.Repository;

public class CustomerRepository(ApplicationDbContext context) : Repository<Customer>(context), ICustomerRepository
{
    public Customer GetByEmail(string email) => _dbSet.AsNoTracking().FirstOrDefault(c => c.Email == email);
}
