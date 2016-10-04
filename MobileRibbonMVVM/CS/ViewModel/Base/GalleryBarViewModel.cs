using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OptimumLap.ViewModel
{
    public class GalleryBarViewModel : ItemSelectorViewModel
    {
        public string PopupTitle { get; set; }
        public Uri ImageSource { get; set; }

        private IList<int> _QuickAccessOverflowIndexes;
        public IList<int> QuickAccessOverflowIndexes
        {
            get { return _QuickAccessOverflowIndexes ?? (_QuickAccessOverflowIndexes = new List<int>()); }
            set { _QuickAccessOverflowIndexes = value; }
        }

        private IEnumerable<object> _QuickAccessItems;
        public IEnumerable<object> QuickAccessItems
        {
            get { return _QuickAccessItems ?? (_QuickAccessItems = new ObservableCollection<object>()); }
            set { _QuickAccessItems = value; }
        }
    }
}
