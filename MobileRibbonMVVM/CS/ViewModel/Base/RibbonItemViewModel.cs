using System.Collections.Generic;
using DevComponents.WPF.Controls;

namespace OptimumLap.ViewModel
{
    public class RibbonItemViewModel : ViewModelBase
    {
        private string _Header;

        public int Height { get; set; }
        public string Header
        {
            get { return _Header; }
            set { SetPropertyValue(value, ref _Header, "Header"); }
        }

        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { SetPropertyValue(value, ref _IsSelected, "IsSelected"); }
        }

        public BarViewModel BarView { get; set; }
}

    public class BarViewModel : ViewModelBase
    {
        private IList<object> _items;

        public  BarViewModel()
        {

        }

        public IList<object> Items
        {
            get
            {
                return _items; 
                
            }
            set { _items = value; }
        }
    }
}
