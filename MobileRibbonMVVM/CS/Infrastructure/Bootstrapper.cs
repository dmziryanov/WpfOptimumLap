using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using DevComponents.DotNetBar;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;

namespace OptimumLap.Infrastructure
{
    public class Bootstrapper : UnityBootstrapper
    {
        ShellPresenter _shellPresenter;

        //Here to implement
        //IEnumerable<DefaultView> _defaultViews;
        //AccessRightsService _accessRightsService;

        protected override DependencyObject CreateShell()
        {

            /*    Container.RegisterInstance<IAccessRightsService>(_accessRightsService);
                Container.RegisterInstance<IPermissionChecker>(_accessRightsService);
                Container.RegisterInstance<IUser>(_accessRightsService.CurrentUser);*/
            _shellPresenter = Container.Resolve<ShellPresenter>();
            Container.RegisterInstance<IShell>(_shellPresenter);
            Container.RegisterInstance<IShellInteraction>(_shellPresenter);
            _shellPresenter.Execute();
            return _shellPresenter.View;
        }

        protected override void InitializeShell()
        {
            System.Windows.Application.Current.MainWindow = (Window)Shell;
            System.Windows.Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //Register types
            //Container.RegisterType<IGenericRepository, GenericRepository>().

        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();
            if (mappings != null)
            {
                /*mappings.RegisterMapping(typeof(BarManager), Container.Resolve<BarManagerRegionAdapter>());
                mappings.RegisterMapping(typeof(RibbonControl), Container.Resolve<RibbonRegionAdapter>());
                mappings.RegisterMapping(typeof(LayoutPanel), Container.Resolve<LayoutPanelAdapter>());
                mappings.RegisterMapping(typeof(DocumentGroup), Container.Resolve<DocumentGroupAdapter>());
                mappings.RegisterMapping(typeof(LayoutControl), Container.Resolve<LayoutControlAdapter>());
                mappings.RegisterMapping(typeof(Grid), Container.Resolve<WindowHelperAdapter>());*/
            }
            return mappings;
        }

        protected override void ConfigureModuleCatalog()
        {

        }

        protected override void InitializeModules()
        {
            /* To include another modules here */

        }

    }
}
