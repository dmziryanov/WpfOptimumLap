using System;
using System.Windows.Input;
using DevComponents.WPF.Controls;

namespace OptimumLap.ViewModel
{
    public class SelectorItemViewModel : ViewModelBase
    {
        private readonly ItemSelectorViewModel _ParentSelector;
        public SelectorItemViewModel(ItemSelectorViewModel parentSelector = null)
        {
            _ParentSelector = parentSelector;
        }

        public event EventHandler IsSelectedChanged;

        public Uri ImageSource { get; set; }
        public object Content { get; set; }
        public object Header { get; set; }
        public ICommand Command { get; set; }
        public object CommandParameter { get; set; }
        public bool IsSelectable { get; set; }

        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if(SetPropertyValue(value, ref _IsSelected, "IsSelected"))
                    OnIsSelectedChanged();
            }
        }

        protected virtual void OnIsSelectedChanged()
        {
            if(_ParentSelector != null && IsSelected)
                _ParentSelector.SelectedItem = this;

            var h = IsSelectedChanged;
            if(h != null)
                h(this, EventArgs.Empty);
        }
    }

    public class BackstageItemViewModel : SelectorItemViewModel
    {
        public bool ClosesBackstage { get; set; }
    }

    public class SettingsFlyoutItemViewModel : SelectorItemViewModel
    {
        public bool ClosesFlyout { get; set; }
    }
}
