
using DevComponents.WpfRibbon;
using OptimumLap;

namespace OptimumLap
{ 
    interface IShell : IShellInteraction
    {
        MyRibbonWindow View { get; }
    }
}