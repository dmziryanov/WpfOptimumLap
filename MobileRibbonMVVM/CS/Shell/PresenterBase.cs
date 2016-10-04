using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace OptimumLap
{
    /// <summary>
    /// Basic presenter class that is responsible for fill up Views with data
    /// </summary>
    public abstract class PresenterBase<TView, TViewModel> : IPresenter<IView> where TView : IView
    {
        readonly IRegionManager _regionManager;
        TView _view;
        TViewModel _viewModel;

        protected PresenterBase()
        {
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            View = ServiceLocator.Current.GetInstance<TView>();
            ViewModel = ServiceLocator.Current.GetInstance<TViewModel>();
            View.DataContext = ViewModel;

            //Possible implement here
            //AccessChecker = ServiceLocator.Current.GetInstance<IPermissionChecker>();
        }

        public TView View
        {
            get { return _view; }
            private set
            {
                _view = value;
                OnViewSet();
            }
        }

        public TViewModel ViewModel
        {
            get { return _viewModel; }
            private set
            {
                _viewModel = value;
                OnViewModelSet();
            }
        }

        //protected IPermissionChecker AccessChecker { get; private set; }

        IView IPresenter<IView>.View { get { return View; } }

        protected virtual void OnViewSet()
        {
        }

        protected virtual void OnViewModelSet()
        {
        }

        protected bool IsPermitted(string actionUid)
        {
            return true; // AccessChecker.IsPermitted(actionUid);
        }

        /*protected bool IsPermitted(IPermissionEntity entity, AccessRightsActionType actionType)
        {
            return true; AccessChecker.IsPermitted(entity, actionType);
        }*/

        /// <summary>
        /// Region support
        /// </summary>
        /// <param name="regionName">Region name</param>
        protected void RegisterViewWithRegion(string regionName)
        {
            if (_regionManager.Regions.ContainsRegionWithName(regionName))
                _regionManager.Regions[regionName].Add(View);
        }
    }
}
