using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MobileRibbonMVVMSample.ViewModel;

namespace MobileRibbonMVVMSample.ViewModel
{
    public class NamedListDescription
    {
        public ListDescription ListDescription { get; set; }
        public Uri ImageSource { get; set; }
        public string Name { get; set; }
    }

    public class ListGalleryPopupButtonViewModel : PopupButtonViewModel
    {
        public ListDescriptionGalleryViewModel Gallery { get; set; }
        public List<NamedListDescription> GalleryItems { get; set; }
    }

    public class ListDescriptionGalleryViewModel : ItemSelectorViewModel
    {
        public ListDescriptionGalleryViewModel()
        {
            ValuePath = "ListDescription";

            var none = new NamedListDescription
            {
                ListDescription = ListDescription.None,
                ImageSource = Images.Current.GetImage(ImageId.ListNone)
            };

            SelectedItem = none;

            BulletedListDescriptions = new List<NamedListDescription>
            {
                none,
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.Disc),
                    ImageSource = Images.Current.GetImage(ImageId.BulletSolid)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.Circle),
                    ImageSource = Images.Current.GetImage(ImageId.BulletOpen)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.Box),
                    ImageSource = Images.Current.GetImage(ImageId.BulletSquare)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.Box),
                    ImageSource = Images.Current.GetImage(ImageId.BulletDiamonds)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.Box),
                    ImageSource = Images.Current.GetImage(ImageId.BulletArrow)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.Box),
                    ImageSource = Images.Current.GetImage(ImageId.BulletCheck)
                },
            };

            NumberedListDescriptions = new List<NamedListDescription>
            {
                none,
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.Decimal),
                    ImageSource = Images.Current.GetImage(ImageId.NumberedListArabicPeriod)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.Decimal),
                    ImageSource = Images.Current.GetImage(ImageId.NumberedListArabicParenth)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.UpperRoman),
                    ImageSource = Images.Current.GetImage(ImageId.NumberedListRomanUpperPeriod)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.UpperLatin),
                    ImageSource = Images.Current.GetImage(ImageId.NumberedListLetterUpperPeriod)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.LowerLatin),
                    ImageSource = Images.Current.GetImage(ImageId.NumberedListLetterLowerPeriod)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.LowerLatin),
                    ImageSource = Images.Current.GetImage(ImageId.NumberedListLetterLowerParenth)
                },
                new NamedListDescription
                {
                    ListDescription = new ListDescription(TextMarkerStyle.LowerRoman),
                    ImageSource = Images.Current.GetImage(ImageId.NumberedListRomanLowerPeriod)
                },
            };
        }

        public List<NamedListDescription> NumberedListDescriptions { get; private set; }
        public List<NamedListDescription> BulletedListDescriptions { get; private set; }        
    }
}
