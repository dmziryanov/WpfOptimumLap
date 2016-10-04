using System.Collections.Generic;

namespace OptimumLap.ViewModel
{
    public class ParagrahpStyleGalleryBarViewModel : GalleryBarViewModel
    {
        public ParagrahpStyleGalleryBarViewModel(ParagraphStyleGallery gallery)
        {
            ImageSource = Images.Current.GetImage(ImageId.StylesIcon);
            ToolTip = Strings.Current.GetString(StringId.Styles);
            PopupTitle = Strings.Current.GetString(StringId.Styles);
            Items = gallery.Styles;
            QuickAccessItems = gallery.QuickAccessStyles;
            QuickAccessOverflowIndexes = new[] {5, 6, 11};
            SelectedItem = gallery.Styles[0];
        }
    }

    public partial class HomeRibbonItemViewModel : RibbonItemViewModel
    {
        public HomeRibbonItemViewModel()
        {
            IsSelected = true;
            Header = Strings.Current.GetString(StringId.Home);
            Height = 89;

            BarView = new BarViewModel
            {
                Items = new List<object>
                {
                    CreateFontFormattingGroup(),
                    new SeparatorViewModel(),
                    CreateParagraphFormattingGroup(),
                    new SeparatorViewModel()
                }
            };

        }

        public GalleryBarViewModel Styles { get; private set; }
       
        private static ButtonViewModel CreateSearchButton()
        {
            var button = new ButtonViewModel
            {
                ToolTip = "Find",                
                ImageSource = Images.Current.GetImage(ImageId.SearchIcon),
            };

            return button;
        }
    }
}
