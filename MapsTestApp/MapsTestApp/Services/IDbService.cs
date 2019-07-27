using System.Collections.Generic;
using MapsTestApp.Models;

namespace MapsTestApp.Services
{
    public interface IDbService
    {
        int SaveAddress(AddressDbModel address);
        List<AddressDbModel> GetAddresses();
        AddressDbModel GetAddress(int id);
    }

}
