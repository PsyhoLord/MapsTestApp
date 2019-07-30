using System;
using System.Threading.Tasks;
using MapsTestApp.Models;
using MapsTestApp.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MapsTestApp.ViewModels
{
    public class MainActivityViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;
        private readonly ILocationService _locationService;
        private MvxObservableCollection<AddressModel> _favoritesList;
        private bool _isFavoritesPresent;
        public Action RefreshMapMarkers;

        public MainActivityViewModel(IMvxNavigationService navigationService, ILocationService locationService)
        {
            _navigationService = navigationService;
            _locationService = locationService;

            AddAddressCommand = new MvxAsyncCommand(AddAddress);
            FavoritesList = new MvxObservableCollection<AddressModel>(_locationService.GetAddressModels());
        }

        private async Task AddAddress()
        {
            await _navigationService.Navigate<AddressViewModel>();
        }

        public IMvxCommand AddAddressCommand { get; }

        public bool IsFavoritesPresent
        {
            get => _isFavoritesPresent;
            set => SetProperty(ref _isFavoritesPresent, value);
        }

        public MvxObservableCollection<AddressModel> FavoritesList
        {
            get => _favoritesList;
            set
            {
                SetProperty(ref _favoritesList, value);
                IsFavoritesPresent = value != null && value.Count != 0;
                RefreshMapMarkers?.Invoke();
            }
        }

        public void RefreshFavorites()
        {
            FavoritesList = new MvxObservableCollection<AddressModel>(_locationService.GetAddressModels());
        }
    }
}
