using System;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Widget;
using MapsTestApp.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace MapsTestApp.Driod.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : MvxAppCompatActivity<MainActivityViewModel>, IOnMapReadyCallback
    {
        private MapFragment _mapFragment;
        private MainActivityViewModel _activityViewModel;
        private GoogleMap _googleMap;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var tabHost = FindViewById<TabHost>(Resource.Id.tabhost);
            tabHost.Setup();

            var tabDrive = tabHost.NewTabSpec("tag1");
            tabDrive.SetIndicator("Favorites");
            tabDrive.SetContent(Resource.Id.tab1_content);
            tabHost.AddTab(tabDrive);

            var tabWork = tabHost.NewTabSpec("tag2");
            tabWork.SetIndicator("Map");
            tabWork.SetContent(Resource.Id.tab2_content);
            tabHost.AddTab(tabWork);

            tabHost.CurrentTab = 0;

            _mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            _mapFragment.GetMapAsync(this);

            _activityViewModel = BindingContext.DataContext as MainActivityViewModel;
            _activityViewModel.RefreshMapMarkers += RefreshMap;
        }

        private void RefreshMap()
        {
            if (_googleMap == null) return;

            _googleMap.Clear();

            if (!(BindingContext.DataContext is MainActivityViewModel vm)) return;

            var locList = vm.FavoritesList;

            foreach (var addressModel in locList)
            {
                var markerOpt1 = new MarkerOptions();

                markerOpt1.SetPosition(new LatLng(addressModel.MapsCandidate.geometry.location.lat,
                    addressModel.MapsCandidate.geometry.location.lng));

                markerOpt1.SetTitle(addressModel.Caption);
                _googleMap.AddMarker(markerOpt1);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            _activityViewModel?.RefreshFavorites();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _activityViewModel.RefreshMapMarkers -= RefreshMap;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _googleMap = googleMap;
            _googleMap.UiSettings.ZoomControlsEnabled = true;
            _googleMap.UiSettings.CompassEnabled = true;
        }
    }
}

