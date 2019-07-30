using Android.App;
using Android.OS;
using Android.Widget;
using MapsTestApp.Driod.Views.RecycleView;
using MapsTestApp.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

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

            var searchEditText = FindViewById<EditText>(Resource.Id.search_edittext);
            var recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.address_recycler_view);

            var set = this.CreateBindingSet<AddressView, AddressViewModel>();
            set.Bind(searchEditText).To(vm => vm.SearchText);

            var adapter = new AddressAdapter((IMvxAndroidBindingContext)this.BindingContext);
            recyclerView.Adapter = adapter;

            set.Bind(adapter).For(x => x.ItemsSource).To(x => x.AddressList);
            set.Bind(recyclerView).For(v => v.ItemClick).To(vm => vm.AddressSelectedCommand);
            set.Apply();
        }
    }
}