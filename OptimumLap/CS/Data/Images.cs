using System;

namespace MobileRibbonMVVMSample
{
    public class FileTitleAttribute : Attribute
    {
        public FileTitleAttribute(string fileTitle)
        {
            FileTitle = fileTitle;
        }
        public string FileTitle { get; set; }
    }

    public enum ImageId
    {
        FontFormattingIcon,
        ParagraphFormattingIcon,
        StylesIcon,

        BoldIcon,
        ClearFormattingIcon,
        HighlightIcon,
        ItalicIcon,
        UnderlineIcon,
        StrikethroughIcon,
        SubscriptIcon,
        SuperscriptIcon,
        SearchIcon,

        BulletNone,
        BulletSolid,
        BulletOpen,
        BulletSquare,
        BulletDiamonds,
        BulletArrow,
        BulletCheck,

        AlignLeftIcon,
        AlignCenterIcon,
        AlignRightIcon,
        JustifyIcon,

        BulletedListIcon,
        NumberedListIcon,
        IndentLeftIcon,
        IndentRightIcon,
        SpecialIndentIcon,
        LineSpacingIcon,
        RemoveSpaceIcon,
        AddSpaceIcon,
        NumberedListArabicPeriod,
        NumberedListArabicParenth,
        NumberedListRomanUpperPeriod,
        NumberedListLetterUpperPeriod,
        NumberedListLetterLowerPeriod,
        NumberedListLetterLowerParenth,
        NumberedListRomanLowerPeriod,
        ListNone,

        TableIcon,
        ShapesIcon,
        TextBoxIcon,
        PictureIcon,
        LinkIcon,
        CommentIcon,
        HeadersFootersIcon,
        PageNumberIcon,
        FootnoteIcon,
        EndnoteIcon,

        EditModeIcon,
        ReadModeIcon,
        ZoomIcon,
        [FileTitle("100PercentZoomIcon")]
        OneHundredPercentZoomIcon,
        ShowHideIcon,
        Smiley,
        Undo,
        Redo,
        Question
    }

    public class Images
    {
        private static readonly Images _Current = new Images();
        public static Images Current
        {
            get { return _Current; }
        }

        public Uri GetImage(ImageId id)
        {
            var fileNameAttributes = typeof(ImageId).GetField(id.ToString()).GetCustomAttributes(typeof(FileTitleAttribute), false);
            var fileTitle = fileNameAttributes.Length > 0 ? ((FileTitleAttribute)fileNameAttributes[0]).FileTitle : id.ToString();
            return new Uri(string.Format("Images/{0}.png", fileTitle), UriKind.Relative);
        }
    }
}
