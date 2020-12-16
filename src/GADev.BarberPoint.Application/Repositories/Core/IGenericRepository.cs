using System.Collections.Generic;
using System.Threading.Tasks;

namespace GADev.BarberPoint.Application.Repositories.Core
{
    public interface IGenericRepository<T> where T : Domain.Entities.Core.BaseEntity
    {
        Task<T> GetAsync(int id);
        Task<int?> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}