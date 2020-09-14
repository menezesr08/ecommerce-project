using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    // We can't use a generic repository here as that is designed for EF, not redis
    public interface IBasketRepository
    {
         Task<CustomerBasket> GetBasketAsync(string basketId);
         Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
         Task<bool> DeleteBasketAsync(string basketId);
    }
}