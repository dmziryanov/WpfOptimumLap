using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevComponents.WPF.Mobile;

namespace MobileRibbonMVVMSample.ViewModel
{   
    public partial class HomeRibbonItemViewModel
    {        
        public GroupBarViewModel CreateParagraphFormattingGroup()
        {
            ListDescriptions = new ListDescriptionGalleryViewModel();
            Alignments = new AlignmentsGalleryViewModel();
            SpecialIndents = new SpecialIndentsGalleryViewModel();

            var bulletsButton = new ListGalleryPopupButtonViewModel
            {
                OverflowIndex = 12,
                ImageSource = Images.Current.GetImage(ImageId.BulletedListIcon),
                ToolTip = Strings.Current.GetString(StringId.Bullets),
                PopupTitle = Strings.Current.GetString(StringId.Bullets),
                Content = Strings.Current.GetString(StringId.Bullets),
                Gallery = ListDescriptions,
                GalleryItems = ListDescriptions.BulletedListDescriptions,
            };

            var numberingButton = new ListGalleryPopupButtonViewModel
            {
                OverflowIndex = 12,
                ImageSource = Images.Current.GetImage(ImageId.NumberedListIcon),
                Content = Strings.Current.GetString(StringId.Numbering),
                ToolTip = Strings.Current.GetString(StringId.Numbering),
                PopupTitle = Strings.Current.GetString(StringId.Numbering),
                Gallery = ListDescriptions,
                GalleryItems = ListDescriptions.NumberedListDescriptions,
            };

            var decreaseIndentButton = new ButtonViewModel
            {
                OverflowIndex = 1,
                SharedOverflowRow = "indent",
                ImageSource = Images.Current.GetImage(ImageId.IndentLeftIcon),
                ToolTip = Strings.Current.GetString(StringId.DecreaseIndent),
                Command = Commands.DecreaseIndentation,
            };

            var increaseIndentButton = new ButtonViewModel
            {
                OverflowIndex = 1,
                SharedOverflowRow = "indent",
                ImageSource = Images.Current.GetImage(ImageId.IndentRightIcon),
                ToolTip = Strings.Current.GetString(StringId.IncreaseIndent),
                Command = Commands.IncreaseIndentation,
            };

            var alignmentPopupButton = new PopupButtonViewModel
            {
                OverflowIndex = 10,
                ItemsPanelOption = ItemsPanelOption.HorizontalStack,
                ItemsDisplayOption = RibbonPopupButtonItemsDisplayOption.ShowItemsInGroupBarOverflow,
                ImageSource = Images.Current.GetImage(ImageId.AlignLeftIcon),
                ToolTip = Strings.Current.GetString(StringId.Alignment),
                PopupTitle = Strings.Current.GetString(StringId.Alignment),
                Items = Alignments.Items,
            };
            
            var specialIndentPopupButton = new PopupButtonViewModel
            {
                OverflowIndex = 0,
                ImageSource = Images.Current.GetImage(ImageId.SpecialIndentIcon),
                ItemsPanelOption = ItemsPanelOption.VerticalStack,
                Content = Strings.Current.GetString(StringId.SpecialIndent),
                ToolTip = Strings.Current.GetString(StringId.SpecialIndent),
                PopupTitle = Strings.Current.GetString(StringId.SpecialIndent),
                Items = SpecialIndents.Items,
            };

            LineSpacings = new ComboBoxViewModel
            {
                Items = new List<double> {1.0, 1.15, 1.5, 2.0, 2.5, 3.0},
                Header = Strings.Current.GetString(StringId.LineSpacing),
                Value = 1.15,
                IsEditable = true,
            };

            var lineAndParagraphSpacingPopupButton = new PopupButtonViewModel
            {
                OverflowIndex = 2,
                ItemsDisplayOption = RibbonPopupButtonItemsDisplayOption.ShowItemsInGroupBarOverflow,
                ImageSource = Images.Current.GetImage(ImageId.LineSpacingIcon),
                ToolTip = Strings.Current.GetString(StringId.LineAndParagraphSpacing),
                Content = Strings.Current.GetString(StringId.LineAndParagraphSpacing),
                PopupTitle = Strings.Current.GetString(StringId.LineAndParagraphSpacing),
                Items = new List<object>
                {
                    LineSpacings,
                    new ButtonViewModel
                    {
                        ImageSource = Images.Current.GetImage(ImageId.RemoveSpaceIcon),
                        Content = Strings.Current.GetString(StringId.RemoveSpaceAfterParagraph),
                        ToolTip = Strings.Current.GetString(StringId.RemoveSpaceAfterParagraph),
                        Command = Commands.AddSpacingAfterParagraph,
                        CommandParameter = -6d
                    },
                    new ButtonViewModel
                    {
                        ImageSource = Images.Current.GetImage(ImageId.AddSpaceIcon),
                        Content = Strings.Current.GetString(StringId.AddSpaceBeforeParagraph),
                        ToolTip = Strings.Current.GetString(StringId.AddSpaceBeforeParagraph),
                        Command = Commands.RemoveSpacingAfterParagraph,
                        CommandParameter = 6d
                    },
                }
            };

            return new GroupBarViewModel
            {
                ToolTip = Strings.Current.GetString(StringId.ParagraphFormatting),
                PopupTitle = Strings.Current.GetString(StringId.Paragraph),
                ImageSource = Images.Current.GetImage(ImageId.ParagraphFormattingIcon),
                Items = new ObservableCollection<object>
                {
                    bulletsButton,
                    numberingButton,
                    new SeparatorViewModel(),
                    decreaseIndentButton,
                    increaseIndentButton,
                    specialIndentPopupButton,
                    new SeparatorViewModel(),
                    alignmentPopupButton,
                    new SeparatorViewModel(),
                    lineAndParagraphSpacingPopupButton
                }
            };
        }

        public ComboBoxViewModel LineSpacings { get; private set; }
        public ListDescriptionGalleryViewModel ListDescriptions { get; private set; }
        public ItemSelectorViewModel Alignments { get; private set; }
        public ItemSelectorViewModel SpecialIndents { get; private set; }        
    }
}
