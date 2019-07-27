using System.Collections.Generic;
using MapsTestApp.Models;
using SQLite;

namespace MapsTestApp.Services
{
    public class DbService : IDbService
    {
        private readonly SQLiteConnection _db = null;

        public DbService()
        {
            _db = new SQLiteConnection(Constants.DbFilePath);
            _db.CreateTable<AddressDbModel>();
        }

        public int SaveAddress(AddressDbModel address)
        {
            _db.Insert(address);
            return address.Id;
        }

        public List<AddressDbModel> GetAddresses()
        {
            return _db.Query<AddressDbModel>("select * from Address");
        }

        public AddressDbModel GetAddress(int id)
        {
            return _db.Get<AddressDbModel>(p => p.Id == id);
        }
    }
}
