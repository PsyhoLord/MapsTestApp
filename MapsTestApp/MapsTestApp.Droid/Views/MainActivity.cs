using Android;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Widget;
using MapsTestApp.Driod.Views.RecycleView;
using MapsTestApp.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using MvvmCross.Plugin.Visibility;

namespace MapsTestApp.Driod.Views
{
    [MvxActivityPresentation]
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : BaseAppCompatActivity<MainActivityViewModel>, IOnMapReadyCallback
    {
        private MapFragment _mapFragment;
        private GoogleMap _googleMap;

        protected override int LayoutResource => Resource.Layout.MainActivity;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetPageAsRoot();
            base.OnCreate(savedInstanceState);

            ApplyBinding();
            SetupTabControl();
            SetupMaps();

            RequestPermissions();
        }

        protected override void ApplyBinding()
        {
            var addButton = FindViewById<Button>(Resource.Id.AddButton);
            var recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.address_recycler_view);
            var noContentView = FindViewById<AppCompatTextView>(Resource.Id.no_content_view);

            var set = this.CreateBindingSet<MainActivity, MainActivityViewModel>();
            set.Bind(addButton).To(vm => vm.AddAddressCommand);

            var adapter = new AddressAdapter((IMvxAndroidBindingContext)this.BindingContext);
            recyclerView.Adapter = adapter;

            set.Bind(adapter).For(x => x.ItemsSource).To(x => x.FavoritesList);
            set.Bind(recyclerView).For(v => v.Visibility).To(vm => vm.IsFavoritesPresent).WithConversion<MvxVisibilityValueConverter>();
            set.Bind(noContentView).For(v => v.Visibility).To(vm => vm.IsFavoritesPresent).WithConversion<MvxInvertedVisibilityValueConverter>();

            set.Apply();
        }

        private void SetupTabControl()
        {
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
        }

        private void SetupMaps()
        {
            _mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            _mapFragment.GetMapAsync(this);
            ViewModel.RefreshMapMarkers += RefreshMap;
        }

        private void RequestPermissions()
        {
            ActivityCompat.RequestPermissions(this,
                new[] {Manifest.Permission.AccessFineLocation, Manifest.Permission.WriteExternalStorage}, 1);
        }

        private void RefreshMap()
        {
            if (_googleMap == null) return;

            _googleMap.Clear();

            var locList = ViewModel.FavoritesList;

            foreach (var addressModel in locList)
            {
                var markerOption = new MarkerOptions();

                markerOption.SetPosition(new LatLng(addressModel.Latitude,
                    addressModel.Longitude));

                markerOption.SetTitle(addressModel.Caption);
                _googleMap.AddMarker(markerOption);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            ViewModel?.RefreshFavorites();
        }

        protected override void OnDestroy()
        {
            ViewModel.RefreshMapMarkers -= RefreshMap;
            base.OnDestroy();
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _googleMap = googleMap;
            _googleMap.UiSettings.ZoomControlsEnabled = true;
            _googleMap.UiSettings.CompassEnabled = true;
        }
    }
}

