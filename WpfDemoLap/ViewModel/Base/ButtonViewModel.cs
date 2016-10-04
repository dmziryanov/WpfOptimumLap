using System;
using System.Windows.Input;
using DevComponents.WPF.Mobile;

namespace OptimumLap.ViewModel
{
    public class ButtonViewModel : ControlBaseViewModel
    {
        // Making ImageSource an Object instead of a Uri silences binding 
        // errors from ImageSourceConverter when the value is null...
        public object ImageSource { get; set; }
        public object Content { get; set; }
        public object Icon { get; set; }
        public ICommand Command { get; set; }
        public RibbonButtonDisplayOption DisplayOption { get; set; }

        private object _CommandParameter;
        public object CommandParameter
        {
            get { return _CommandParameter; }
            set { SetPropertyValue(value, ref _CommandParameter, "CommandParameter"); }
        }
    }

    public class ToggleButtonViewModel : ButtonViewModel
    {
        private bool? _IsChecked = false;
        public bool? IsChecked
        {
            get { return _IsChecked; }
            set { SetPropertyValue(value, ref _IsChecked, "IsChecked"); }
        }
    }

    public class RadioButtonViewModel : ToggleButtonViewModel
    {

    }

    public class CheckBoxViewModel : ToggleButtonViewModel
    {
        
    }
}
