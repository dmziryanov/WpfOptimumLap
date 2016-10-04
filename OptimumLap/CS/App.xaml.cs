using Microsoft.Practices.Prism.UnityExtensions;
using MobileRibbonMVVMSample.Infrastructure;

namespace MobileRibbonMVVMSample
{
    public partial class App
    {
        public App()
        {
            
        }

        protected override UnityBootstrapper GetBootstrapper()
        {
            return new Bootstrapper();
        }
    }
}
