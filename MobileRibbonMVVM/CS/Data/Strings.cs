using System;

namespace OptimumLap
{
    public class DefaultTextAttribute : Attribute
    {
        public DefaultTextAttribute(string text)
        {
            Text = text;
        }
        public string Text { get; set; }    
    }

    public enum StringId
    {
        File,
       
        Home,
        Insert,
        Layout,
        Review,
        View,  
          
        Font,        
        FontFormatting,   
        Bold,
        FontColor,
        Italic,
        Underline,
        Subscript,
        Superscript,
        Strikethrough,
        Highlight,
        ClearFormatting,
        Styles,

        ParagraphFormatting,
        Paragraph,
        Bullets,
        Numbering,
        IncreaseIndent,
        DecreaseIndent,
        SpecialIndent,
        Alignment,
        [DefaultText("Line & Paragraph Spacing")]
        LineAndParagraphSpacing,
        RemoveSpaceAfterParagraph,
        AddSpaceBeforeParagraph,
        [DefaultText("Line Spacing:")]
        LineSpacing,
        [DefaultText("None")]
        SpecialIndentNone,
        [DefaultText("First Line")]
        SpecialIndentFirstLine,
        [DefaultText("Hanging")]
        SpecialIndentHanging,
    
        Table,
        Shapes,
        TextBox,
        Pictures,
        Link,
        Comment,
        [DefaultText("Header & Footer")]
        HeaderAndFooter,
        PageNumber,
        Footnote,
        Endnote,

        Edit,
        Read,
        Zoom,
        ChangeZoomLevel,
        [DefaultText("100%")]
        OneHundredPercent,
        ZoomOut,
        ZoomIn,
        EnableAutoZoom,
        ShowHide,
        Ruler,
        Grid,
        NavigationPane,

        New,
        Open,
        SaveAs,
        Print,
        Close,
        Settings,
        Browse,
        Save,

        Help,
        Feedback,
        Undo,
        Redo,

        [DefaultText("Settings")]
        SettingsHeader,
        [DefaultText("DevComponents DotNetBar")]
        SettingsFooter,
        Options,        
        ThemeColor,
        SeedColor,
        License,
        [DefaultText("This is a trial version of DotNetBar. To purchase a license, go to DevComponents.com")]
        LicenseMessage,
        Automatic,        
    }
    
    public class Strings
    {
        private static readonly Strings _Current = new Strings();
        public static Strings Current
        {
            get { return _Current; }
        }

        public string GetString(StringId id)
        {
            var textAttributes = typeof(StringId).GetField(id.ToString()).GetCustomAttributes(typeof(DefaultTextAttribute), false);
            if(textAttributes.Length > 0)
                return ((DefaultTextAttribute)textAttributes[0]).Text;

            var text = id.ToString();
            for(var i = text.Length - 1; i > 0; i--)
            {
                if(char.IsUpper(text[i]))
                    text = text.Insert(i, " ");
            }

            return text;
        }
    }
}
