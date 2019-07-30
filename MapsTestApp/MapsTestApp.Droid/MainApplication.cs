using System;
using Android.App;
using Android.Runtime;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace MapsTestApp.Driod
{
    [Application]
    public class MainApplication : MvxAppCompatApplication<Setup, App>
    {
        public MainApplication()
        {
        }

        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Microsoft.AppCenter.AppCenter.Start("6d5dd9fc-9e89-4654-b379-8e0ea73a9342", typeof(Analytics), typeof(Crashes));
        }
    }
}