
using DevComponents.WpfRibbon;
using MobileRibbonMVVMSample;

namespace MobileRibbonMVVMSample
{ 
    interface IShell : IShellInteraction
    {
        MyRibbonWindow View { get; }
    }
}