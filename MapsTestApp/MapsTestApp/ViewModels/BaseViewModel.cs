using MvvmCross.ViewModels;

namespace MapsTestApp.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        protected BaseViewModel()
        {
        }
    }

    public abstract class BaseViewModel<TParameter, TResult> : MvxViewModel<TParameter, TResult>
        where TParameter : class
        where TResult : class
    {
        protected BaseViewModel()
        {
        }
    }
}
