using Android.App;
using Android.OS;
using MapsTestApp.ViewModels;

namespace MapsTestApp.Driod.Views
{
    [Activity(Label = "Address", Theme = "@style/AppTheme.NoActionBar")]
    public class AddressView : BaseAppCompatActivity<AddressViewModel>
    {
        protected override int LayoutResource => Resource.Layout.AddressView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Title = "Address";
        }
    }
}