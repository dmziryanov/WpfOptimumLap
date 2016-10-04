using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OptimumLap.ViewModel
{    
    public class InsertRibbonItemViewModel : RibbonItemViewModel
    {
        public InsertRibbonItemViewModel()
        {
            Header = Strings.Current.GetString(StringId.Design);
            IsSelected = false;

        
        }

        public InsertGroupBarViewModel Insert { get; set; }

        public class InsertGroupBarViewModel : GroupBarViewModel
        {
            public InsertGroupBarViewModel()
            {
                ToolTip = Strings.Current.GetString(StringId.Insert);
                PopupTitle = Strings.Current.GetString(StringId.Insert);
                

                Items = new ObservableCollection<object>
                {
                    new ButtonViewModel
                    {
                        Content = Strings.Current.GetString(StringId.Table),
                        ImageSource = Images.Current.GetImage(ImageId.TableIcon),
                    },

                    new ButtonViewModel
                    {
                        OverflowIndex = 7,
                        Content = Strings.Current.GetString(StringId.Pictures),
                        ImageSource = Images.Current.GetImage(ImageId.PictureIcon),
                    },

                    new ButtonViewModel
                    {
                        OverflowIndex = 6,
                        Content = Strings.Current.GetString(StringId.Shapes),
                        ImageSource = Images.Current.GetImage(ImageId.ShapesIcon),
                    },

                    new ButtonViewModel
                    {
                        OverflowIndex = 5,
                        Content = Strings.Current.GetString(StringId.TextBox),
                        ImageSource = Images.Current.GetImage(ImageId.TextBoxIcon),
                    },

                    new SeparatorViewModel(),

                    new ButtonViewModel
                    {
                        OverflowIndex = 4,
                        Content = Strings.Current.GetString(StringId.Link),
                        ImageSource = Images.Current.GetImage(ImageId.LinkIcon),
                    },

                    new SeparatorViewModel(),
                    new ButtonViewModel
                    {
                        OverflowIndex = 3,
                        Content = Strings.Current.GetString(StringId.Comment),
                        ImageSource = Images.Current.GetImage(ImageId.CommentIcon),
                    },

                    new SeparatorViewModel(),

                    new ButtonViewModel
                    {
                        OverflowIndex = 2,
                        Content = Strings.Current.GetString(StringId.HeaderAndFooter),
                        ImageSource = Images.Current.GetImage(ImageId.HeadersFootersIcon),
                    },

                     new ButtonViewModel
                    {
                        OverflowIndex = 1,
                        Content = Strings.Current.GetString(StringId.PageNumber),
                        ImageSource = Images.Current.GetImage(ImageId.PageNumberIcon),
                    },

                    new SeparatorViewModel(),

                    new ButtonViewModel
                    {
                        OverflowIndex = 0,
                        Content = Strings.Current.GetString(StringId.Footnote),
                        ImageSource = Images.Current.GetImage(ImageId.FootnoteIcon),
                    },

                    new ButtonViewModel
                    {
                        OverflowIndex = 0,
                        Content = Strings.Current.GetString(StringId.Endnote),
                        ImageSource = Images.Current.GetImage(ImageId.EndnoteIcon),
                    },
                };
            }
        }
    }
}
