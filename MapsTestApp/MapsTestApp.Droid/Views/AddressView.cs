using Android.App;
using Android.OS;
using MapsTestApp.ViewModels;

namespace MapsTestApp.Driod.Views
{
    [Activity(Label = "Address", Theme = "@style/AppTheme")]
    public class AddressView : BaseAppCompatActivity<AddressViewModel>
    {
        protected override int LayoutResource => Resource.Layout.AddressView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Title = "Address";

            //view.FindViewById<MvxRecyclerView>(Resource.Id.messages_recycler_view);
        }
    }
}