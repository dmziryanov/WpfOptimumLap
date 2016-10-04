using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace OptimumLap.ViewModel
{
    public class GalleryViewModel : ItemSelectorViewModel
    {
        
    }

    public class ItemSelectorViewModel : ControlBaseViewModel
    {
        public event EventHandler SelectedItemChanged;
        public event EventHandler ValueChanged;

        private object _SelectedItem;
        public object SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                var oldValue = _SelectedItem;
                if(SetPropertyValue(value, ref _SelectedItem, "SelectedItem"))
                {
                    OnSelectedItemChanged(oldValue, value);
                }
            }
        }

        private object _Value;
        public object Value
        {
            get { return _Value; }
            set
            {
                var old = _Value;
                if(SetPropertyValue(value, ref _Value, "Value"))
                    OnValueChanged(old, value);
            }
        }
        public string ValuePath { get; set; }

        private IList _Items;
        public IList Items
        {
            get { return _Items ?? (_Items = new ObservableCollection<object>()); }
            set { _Items = value; }
        }

        protected virtual void OnSelectedItemChanged(object oldSelectedItem, object newSelectedItem)
        {
            if(SelectedItemChanged != null)
                SelectedItemChanged.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnValueChanged(object oldValue, object newValue)
        {
            if(ValueChanged != null)
                ValueChanged.Invoke(this, EventArgs.Empty);
        }
    }

}
