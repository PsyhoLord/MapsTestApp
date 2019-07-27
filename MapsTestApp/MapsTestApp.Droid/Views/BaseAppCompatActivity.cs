using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.ViewModels;

namespace MapsTestApp.Driod.Views
{
    public abstract class BaseAppCompatActivity<TViewModel> : MvxAppCompatActivity<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get => base.ViewModel;
            set => base.ViewModel = value;
        }

        public Toolbar Toolbar { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(LayoutResource);
            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            if (Toolbar == null) return;

            SetSupportActionBar(Toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
        }

        protected abstract int LayoutResource { get; }

        protected int ActionBarIcon
        {
            set => Toolbar.SetNavigationIcon(value);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    OnBackPressed();
                    //NavUtils.NavigateUpFromSameTask(this);
                    break;
            }

            return true; //base.OnOptionsItemSelected(item);
        }
    }
}