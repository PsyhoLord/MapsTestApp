using System.Threading.Tasks;
using MapsTestApp.Models;
using MapsTestApp.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MapsTestApp.ViewModels
{
    public class AddressViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ILocationService _locationService;
        private MvxObservableCollection<AddressModel> _addressList;
        private string _searchText;

        public AddressViewModel(IMvxNavigationService navigationService, ILocationService locationService)
        {
            _navigationService = navigationService;
            _locationService = locationService;

            AddressSelectedCommand = new MvxAsyncCommand<AddressModel>(AddressSelected);
        }

        public IMvxCommand<AddressModel> AddressSelectedCommand { get; }

        private async Task AddressSelected(AddressModel address)
        {
            _locationService.AddToFavorites(address);
            await _navigationService.Close(this);
        }

        public MvxObservableCollection<AddressModel> AddressList
        {
            get => _addressList;
            set => SetProperty(ref _addressList, value);
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                SearchLocation();
            }
        }

        private async Task SearchLocation()
        {
            if (string.IsNullOrEmpty(SearchText)) return;

            var result = await _locationService.SearchForStuff(SearchText);

            if (result != null && result.status == "OK")
            {
                FillAddressList(result);
            }
        }

        private void FillAddressList(MapsResponseModel result)
        {
            var dropDownList = new MvxObservableCollection<AddressModel>();

            foreach (var candidate in result.candidates)
            {
                dropDownList.Add(new AddressModel(candidate.name)
                {
                    MapsCandidate = candidate
                });
            }

            AddressList = dropDownList;
        }
    }
}
