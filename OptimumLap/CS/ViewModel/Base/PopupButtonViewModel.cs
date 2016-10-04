using System;
using System.Collections;
using System.Windows.Input;
using DevComponents.WPF.Mobile;

namespace MobileRibbonMVVMSample.ViewModel
{
    public class PopupButtonGalleryViewModel : PopupButtonViewModel
    {
        public ItemSelectorViewModel Gallery { get; set; }
    }

    public class PopupButtonViewModel : ControlBaseViewModel
    {
        public PopupButtonViewModel()
        {
            MaximumPopupWidth = double.PositiveInfinity;
        }

        public double MaximumPopupWidth { get; set; }
        public RibbonPopupButtonItemsDisplayOption ItemsDisplayOption { get; set; }
        public Uri ImageSource { get; set; }
        public string PopupTitle { get; set; }
        public bool IsSplitButton { get; set; }
        public ICommand Command { get; set; }
        public object Content { get; set; }

        private object _CommandParameter;
        public object CommandParameter
        {
            get { return _CommandParameter; }
            set { SetPropertyValue(value, ref _CommandParameter, "CommandParameter"); }
        }

        public ItemsPanelOption ItemsPanelOption { get; set; }
        public IEnumerable Items { get; set; }

        private object _Icon;
        public object Icon
        {
            get { return _Icon; }
            set { SetPropertyValue(value, ref _Icon, "Icon"); }
        }
    }
}
