using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

namespace MobileRibbonMVVMSample
{
    /// <summary>
    /// Базовая реализация презентаторов
    /// </summary>
    public class PresenterBase<TView, TViewModel> : IPresenter<IView> where TView : IView 
    {
        readonly IRegionManager _regionManager;
        TView _view;
        TViewModel _viewModel;

        protected PresenterBase()
        {
            _regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            //AccessChecker = ServiceLocator.Current.GetInstance<IPermissionChecker>();
            View = ServiceLocator.Current.GetInstance<TView>();
            ViewModel = ServiceLocator.Current.GetInstance<TViewModel>();
            View.DataContext = ViewModel;
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
        /// Регистрация представления в регионе
        /// </summary>
        /// <param name="regionName">Имя региона</param>
        protected void RegisterViewWithRegion(string regionName)
        {
            if (_regionManager.Regions.ContainsRegionWithName(regionName))
                _regionManager.Regions[regionName].Add(View);
        }
    }
}
