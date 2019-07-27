﻿using System.Threading.Tasks;
using MapsTestApp.ViewModels;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace MapsTestApp
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService)
            : base(app, mvxNavigationService)
        {
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<MainActivityViewModel>();
        }
    }
}
