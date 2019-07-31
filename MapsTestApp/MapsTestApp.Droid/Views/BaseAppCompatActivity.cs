using Android.OS;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.ViewModels;

namespace MapsTestApp.Driod.Views
{
    public abstract class BaseAppCompatActivity<TViewModel> : MvxAppCompatActivity<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        private bool _navigationControlsEnabled = true;

        protected abstract void ApplyBinding();

        public new TViewModel ViewModel
        {
            get => base.ViewModel;
            set => base.ViewModel = value;
        }

        public Toolbar Toolbar { get; set; }

        protected void SetPageAsRoot()
        {
            _navigationControlsEnabled = false;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(LayoutResource);
            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            if (Toolbar == null) return;
            Toolbar.SetTitleTextColor(ContextCompat.GetColor(this, Resource.Color.white));

            SetSupportActionBar(Toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(_navigationControlsEnabled);
            SupportActionBar.SetHomeButtonEnabled(_navigationControlsEnabled);
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
                    break;
            }

            return true;
        }
    }
}