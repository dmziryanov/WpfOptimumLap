using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace OptimumLap.ViewModel
{
    public class AlignmentViewModel : SelectorItemViewModel
    {
        public AlignmentViewModel(AlignmentsGalleryViewModel gallery) : base(gallery)
        {          
        }

        public TextAlignment Alignment { get; set; }        
    }

    public class AlignmentsGalleryViewModel : ItemSelectorViewModel
    {
        public AlignmentsGalleryViewModel()
        {
            ValuePath = "Alignment";

            var items = new List<AlignmentViewModel> {
                new AlignmentViewModel(this)
                {
                    Alignment = TextAlignment.Left,
                    Content = "Left",
                    ImageSource = Images.Current.GetImage(ImageId.AlignLeftIcon)
                },
                new AlignmentViewModel(this)
                {
                    Alignment = TextAlignment.Center,
                    Content = "Center",
                    ImageSource = Images.Current.GetImage(ImageId.AlignCenterIcon)
                },
                new AlignmentViewModel(this)
                {
                    Alignment = TextAlignment.Right,
                    Content = "Right",
                    ImageSource = Images.Current.GetImage(ImageId.AlignRightIcon)
                },
                new AlignmentViewModel(this)
                {
                    Alignment = TextAlignment.Justify,
                    Content = "Justify",
                    ImageSource = Images.Current.GetImage(ImageId.JustifyIcon)
                }
            };

            items[0].IsSelected = true;
            Items = items;
        }

        protected override void OnSelectedItemChanged(object oldSelectedItem, object newSelectedItem)
        {
            base.OnSelectedItemChanged(oldSelectedItem, newSelectedItem);
            var alignment = newSelectedItem as AlignmentViewModel;
            if(alignment != null)
                Value = alignment.Alignment;
        }

        protected override void OnValueChanged(object oldValue, object newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            var items = Items as List<AlignmentViewModel>;
            if (items != null)
                items.First(a => a.Alignment == (TextAlignment)newValue).IsSelected = true;
        }
    }

}
