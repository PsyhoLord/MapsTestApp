using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MapsTestApp.Models;
using Newtonsoft.Json;

namespace MapsTestApp.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRequestService _requestService;
        private readonly IDbService _dbService;
        private const string ServerUrl = "https://maps.googleapis.com";
        private const string QuerySearchEndpoint = "/maps/api/place/findplacefromtext/json";
        private const string ApiKey = "AIzaSyCpD8epFirHDJfTxRdc_VuYeegE0HenqLs";
        private List<AddressModel> _favoriteList;

        public LocationService(IRequestService requestService, IDbService dbService)
        {
            _requestService = requestService;
            _dbService = dbService;
        }

        public async Task<MapsResponseModel> SearchForStuff(string keyword)
        {
            var response = await _requestService.Request(ServerUrl, QuerySearchEndpoint, new Dictionary<string, string>
            {
                {"input", Uri.EscapeDataString(keyword)},
                {"inputtype", "textquery"},
                {"fields", "formatted_address,name,geometry"},
                {"key", ApiKey},
            }, HttpMethod.Get);

            if (string.IsNullOrEmpty(response)) return null;

            var responseModel = JsonConvert.DeserializeObject<MapsResponseModel>(response);
            return responseModel;
        }

        public List<AddressModel> GetAddressModels()
        {
            return _favoriteList;
        }

        public async Task LoadFavoritesFromDbAsync()
        {
            var records = await _dbService.GetAddressesAsync();

            _favoriteList = new List<AddressModel>();

            foreach (var addressDbModel in records)
            {
                _favoriteList.Add(new AddressModel(addressDbModel.Name)
                {
                    Latitude = addressDbModel.lat,
                    Longitude = addressDbModel.lng
                });
            }
        }

        public async Task SaveFavoritesToDbAsync(AddressModel model)
        {
            await _dbService.SaveAddressAsync(new AddressDbModel
            {
                Name = model.Caption,
                lng = model.Longitude,
                lat = model.Latitude
            });
        }

        public async Task AddToFavoritesAsync(AddressModel addressModel)
        {
            if (_favoriteList == null)
                _favoriteList = new List<AddressModel>();

            _favoriteList.Add(addressModel);
            await SaveFavoritesToDbAsync(addressModel);
        }

        public void RemoveFromFavorites(AddressModel addressModel)
        {
            throw new NotImplementedException();
        }

    }

}
