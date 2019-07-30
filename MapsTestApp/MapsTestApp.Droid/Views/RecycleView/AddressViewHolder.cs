using Android.Views;
using Android.Widget;
using MapsTestApp.Models;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace MapsTestApp.Driod.Views.RecycleView
{
    public class AddressViewHolder : MvxRecyclerViewHolder
    {
        private readonly TextView _textView;

        public AddressViewHolder(View itemView, IMvxAndroidBindingContext context)
            : base(itemView, context)
        {
            _textView = itemView.FindViewById<TextView>(Resource.Id.caption_text);

            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<AddressViewHolder, AddressModel>();
                set.Bind(_textView).To(x => x.Caption);
                set.Apply();
            });
        }
    }
}