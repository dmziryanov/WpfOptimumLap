using DevComponents.WpfRibbon;
using OptimumLap;

namespace OptimumLap
{ 
    interface IShell : IShellInteraction
    {
        ShellRibbonWindow View { get; }
    }
}