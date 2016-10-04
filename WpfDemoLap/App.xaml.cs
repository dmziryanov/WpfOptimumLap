using Microsoft.Practices.Prism.UnityExtensions;
using OptimumLap.Infrastructure;

namespace OptimumLap
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
