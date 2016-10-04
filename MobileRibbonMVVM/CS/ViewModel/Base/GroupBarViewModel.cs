using System;
using System.Collections.ObjectModel;
using DevComponents.WPF.Controls;

namespace OptimumLap.ViewModel
{
    public class GroupBarViewModel : ViewModelBase
    {
        public string PopupTitle { get; set; }
        public string ToolTip { get; set; }
        public Uri ImageSource { get; set; }

        public ObservableCollection<object> Items { get; set; }
    }
}
