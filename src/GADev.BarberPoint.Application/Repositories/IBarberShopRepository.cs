using System.Collections.Generic;
using System.Threading.Tasks;
using GADev.BarberPoint.Domain.Entities;

namespace GADev.BarberPoint.Application.Repositories
{
    public interface IBarberShopRepository
    {
        Task<BarberShop> GetBarberShopById(int id);
    }
}