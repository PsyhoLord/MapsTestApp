using System.Collections.Generic;
using System.Threading.Tasks;
using MapsTestApp.Models;

namespace MapsTestApp.Services
{
    public interface ILocationService
    {
        Task<MapsResponseModel> SearchForStuff(string keyword);
        List<AddressModel> GetAddressModels();
        Task AddToFavoritesAsync(AddressModel addressModel);
        void RemoveFromFavorites(AddressModel addressModel);
        Task LoadFavoritesFromDbAsync();
    }
}