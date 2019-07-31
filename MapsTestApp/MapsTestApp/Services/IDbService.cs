using System.Collections.Generic;
using System.Threading.Tasks;
using MapsTestApp.Models;

namespace MapsTestApp.Services
{
    public interface IDbService
    {
        Task<int> SaveAddressAsync(AddressDbModel address);
        Task<List<AddressDbModel>> GetAddressesAsync();
        Task<AddressDbModel> GetAddressAsync(int id);
    }

}
