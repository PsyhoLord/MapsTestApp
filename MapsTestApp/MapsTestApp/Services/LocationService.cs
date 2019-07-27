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
            LoadFavoritesFromDb();
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


            if (!string.IsNullOrEmpty(response))
                    {
                        var jsonSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                        var responseModel = JsonConvert.DeserializeObject<MapsResponseModel>(response, jsonSettings);
                        return responseModel;
                    }

            return null;
        }

        public List<AddressModel> GetAddressModels()
        {
            return _favoriteList;
        }

        public void LoadFavoritesFromDb()
        {
            var records = _dbService.GetAddresses();

            _favoriteList = new List<AddressModel>();

            foreach (var addressDbModel in records)
            {
                _favoriteList.Add(new AddressModel(addressDbModel.Name)
                {
                    MapsCandidate = new Candidate
                    {
                        name = addressDbModel.Name,
                        geometry = new Geometry
                        {
                            location = new Location
                            {
                                lat = addressDbModel.lat,
                                lng = addressDbModel.lng
                            }
                        }
                    }
                });
            }
        }

        public void SaveFavoritesToDb(AddressModel model)
        {
            _dbService.SaveAddress(new AddressDbModel
            {
                Name = model.Caption,
                lng = model.MapsCandidate.geometry.location.lng,
                lat = model.MapsCandidate.geometry.location.lat
            });
        }

        public void AddToFavorites(AddressModel addressModel)
        {
            if (_favoriteList == null)
                _favoriteList = new List<AddressModel>();

            _favoriteList.Add(addressModel);
            SaveFavoritesToDb(addressModel);
        }

        public void RemoveFromFavorites(AddressModel addressModel)
        {
            throw new NotImplementedException();
        }

    }

}
