using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OptimumLap.ViewModel
{
    public class ViewRibbonItemViewModel : RibbonItemViewModel
    {
        public ViewRibbonItemViewModel()
        {
            Header = Strings.Current.GetString(StringId.View);
            BarView = new BarViewModel();

            BarView.Items = new List<object>
            {
                new RadioButtonViewModel
                {
                    Content = Strings.Current.GetString(StringId.Read),
                    ImageSource = Images.Current.GetImage(ImageId.ReadModeIcon),
                },
                new RadioButtonViewModel
                {
                    IsChecked = true,
                    Content = Strings.Current.GetString(StringId.Edit),
                    ImageSource = Images.Current.GetImage(ImageId.EditModeIcon),
                },
                new SeparatorViewModel(),
                (ZoomLevel = new ZoomLevelGroupBarViewModel()),
                new SeparatorViewModel(),
                (ShowHide = new ShowHideGroupBarViewModel())
            };
        }

        public ZoomLevelGroupBarViewModel ZoomLevel { get; set; }
        public ShowHideGroupBarViewModel ShowHide { get; set; }

        public class ZoomLevelGroupBarViewModel : GroupBarViewModel
        {
            public ZoomLevelGroupBarViewModel()
            {
                ToolTip = Strings.Current.GetString(StringId.ChangeZoomLevel);
                PopupTitle = Strings.Current.GetString(StringId.Zoom);
                ImageSource = Images.Current.GetImage(ImageId.ZoomIcon);

                Items = new ObservableCollection<object>
                {
                    new ButtonViewModel
                    {
                        OverflowIndex = 5,
                        Content = Strings.Current.GetString(StringId.OneHundredPercent),
                        ToolTip = Strings.Current.GetString(StringId.OneHundredPercent),
                        ImageSource = Images.Current.GetImage(ImageId.OneHundredPercentZoomIcon),
                    },
                    new ButtonViewModel
                    {
                        OverflowIndex = 4,
                        Content = Strings.Current.GetString(StringId.ZoomOut),
                        ToolTip = Strings.Current.GetString(StringId.ZoomOut),
                    },
                    new ButtonViewModel
                    {
                        OverflowIndex = 3,
                        Content = Strings.Current.GetString(StringId.ZoomIn),
                        ToolTip = Strings.Current.GetString(StringId.ZoomIn),
                    },
                    new CheckBoxViewModel
                    {
                        OverflowIndex = -1,
                        Content = Strings.Current.GetString(StringId.EnableAutoZoom)
                    },
                };
            }
        }

        public class ShowHideGroupBarViewModel : GroupBarViewModel
        {
            public ShowHideGroupBarViewModel()
            {
                ToolTip = Strings.Current.GetString(StringId.ShowHide);
                PopupTitle = Strings.Current.GetString(StringId.ShowHide);
                ImageSource = Images.Current.GetImage(ImageId.ShowHideIcon);

                Items = new ObservableCollection<object>
                {
                    new CheckBoxViewModel
                    {
                        OverflowIndex = 2,
                        Content = Strings.Current.GetString(StringId.Ruler)
                    },
                    new CheckBoxViewModel
                    {
                        OverflowIndex = 1,
                        Content = Strings.Current.GetString(StringId.Grid)
                    },
                    new CheckBoxViewModel
                    {
                        OverflowIndex = 0,
                        Content = Strings.Current.GetString(StringId.NavigationPane)
                    },
                };
            }   
        }
    }
}
