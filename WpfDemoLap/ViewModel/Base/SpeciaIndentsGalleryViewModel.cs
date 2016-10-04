using System;
using System.Collections.Generic;
using System.Linq;

namespace OptimumLap.ViewModel
{
    public class SpecialIndentViewModel : SelectorItemViewModel
    {
        public SpecialIndentViewModel(SpecialIndentsGalleryViewModel parentGallery) : base(parentGallery)
        { }

        public SpecialIndent SpecialIndent { get; set; }
    }


    public class SpecialIndentsGalleryViewModel : ItemSelectorViewModel
    {
        public SpecialIndentsGalleryViewModel()
        {
            var items = new List<SpecialIndentViewModel>
            {
                new SpecialIndentViewModel(this)
                {
                    SpecialIndent = SpecialIndent.None,
                    ImageSource = Images.Current.GetImage(ImageId.AlignLeftIcon),
                    Content = Strings.Current.GetString(StringId.SpecialIndentNone)
                },
                new SpecialIndentViewModel(this)
                {
                    SpecialIndent = SpecialIndent.FirstLine,
                    ImageSource = Images.Current.GetImage(ImageId.AlignCenterIcon),
                    Content = Strings.Current.GetString(StringId.SpecialIndentFirstLine)
                },
                new SpecialIndentViewModel(this)
                {
                    SpecialIndent = SpecialIndent.Hanging,
                    ImageSource = Images.Current.GetImage(ImageId.AlignRightIcon),
                    Content = Strings.Current.GetString(StringId.SpecialIndentHanging)
                }
            };

            SelectedItem = items[0];
            Items = items;
        }

        protected override void OnSelectedItemChanged(object oldSelectedItem, object newSelectedItem)
        {
            base.OnSelectedItemChanged(oldSelectedItem, newSelectedItem);
            var alignment = newSelectedItem as AlignmentViewModel;
            if (alignment != null)
                Value = alignment.Alignment;
        }

        protected override void OnValueChanged(object oldValue, object newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            var items = Items as List<SpecialIndentViewModel>;
            if (items != null)
                items.First(s => s.SpecialIndent == (SpecialIndent)newValue).IsSelected = true;
        }
    }
}
