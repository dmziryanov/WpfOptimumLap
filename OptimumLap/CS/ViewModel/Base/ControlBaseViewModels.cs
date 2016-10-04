using DevComponents.WPF.Controls;

namespace MobileRibbonMVVMSample.ViewModel
{
    public class ControlBaseViewModel : ViewModelBase
    {
        public ControlBaseViewModel()
        {
            OverflowIndex = short.MaxValue;
        }

        public int OverflowIndex { get; set; }
        public object SharedOverflowRow { get; set; }
        public string ToolTip { get; set; }
    }
}
