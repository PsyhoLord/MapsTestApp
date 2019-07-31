using System.Collections.Generic;
using System.Threading.Tasks;
using MapsTestApp.Models;
using SQLite;

namespace MapsTestApp.Services
{
    public class DbService : IDbService
    {
        private readonly SQLiteAsyncConnection _db = null;

        public DbService()
        {
            _db = new SQLiteAsyncConnection(Constants.DbFilePath);//Connection();
            _db.CreateTableAsync<AddressDbModel>();
        }

        public async Task<int> SaveAddressAsync(AddressDbModel address)
        {
            await _db.InsertOrReplaceAsync(address);
            return address.Id;
        }

        public async Task<List<AddressDbModel>> GetAddressesAsync()
        {
            return await _db.QueryAsync<AddressDbModel>("select * from Address");
        }

        public async Task<AddressDbModel> GetAddressAsync(int id)
        {
            return await _db.GetAsync<AddressDbModel>(p => p.Id == id);
        }
    }
}
