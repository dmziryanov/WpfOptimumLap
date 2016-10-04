using DevComponents.WPF.Controls;

namespace OptimumLap.ViewModel
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
