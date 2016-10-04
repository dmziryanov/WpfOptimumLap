using Microsoft.Practices.Prism.UnityExtensions;
using OptimumLap.Infrastructure;

namespace OptimumLap
{
    public partial class App
    {
        protected override UnityBootstrapper GetBootstrapper()
        {
            return new Bootstrapper();
        }
    }
}
