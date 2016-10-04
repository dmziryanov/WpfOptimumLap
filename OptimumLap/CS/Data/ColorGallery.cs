using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MobileRibbonMVVMSample
{
    public class ColorGalleryStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is ColorGallery.ColorSet)
                return CategoryStyle;
            if (item.Equals(ColorGallery.AutomaticColor))
                return AutomaticColorStyle;
            return ColorItemStyle;
        }

        public Style CategoryStyle { get; set; }
        public Style ColorItemStyle { get; set; }
        public Style AutomaticColorStyle { get; set; }
    }

    public class ColorGallery : List<object>
    {
        public class NamedColor
        {
            public NamedColor(string colorString)
            {
                Name = colorString;
                Color = (Color)ColorConverter.ConvertFromString(colorString);
            }

            public string Name { get; set; }
            public Color Color { get; set; }
        }

        public class ColorSet : List<NamedColor>
        {
            public ColorSet(string displayName)
            {
                DisplayName = displayName;
            }
            public string DisplayName { get; set; }
        }

        public ColorGallery()
        {
            AutomaticColor = new NamedColor("#000001") {Name = Strings.Current.GetString(StringId.Automatic)};

            // Added in order they appear in dropdown.
            Add(ThemeColors = CreateThemeColors());
            Add(StandardColors = CreateStandardColors());
            Add(AutomaticColor);

            DefaultColor = this[Colors.Black];
        }

        public ColorSet ThemeColors { get; set; }
        public ColorSet StandardColors { get; set; }
        public static NamedColor AutomaticColor { get; set; }
        public NamedColor DefaultColor { get; set; }

        public NamedColor this[Color color]
        {
            get
            {
                var namedColor = ThemeColors.FirstOrDefault(c => c.Color == color) ?? 
                                 StandardColors.FirstOrDefault(c => c.Color == color);
                if(namedColor == null && AutomaticColor.Color == color)
                    namedColor = AutomaticColor;

                return namedColor;
            }
        }

        private static ColorSet CreateThemeColors()
        {
            return new ColorSet("Theme Colors")
            {
                // Ordered by row as they appear in the drop down.
                new NamedColor("#FFFFFF"),
                new NamedColor("#000000"),
                new NamedColor("#E7E6E6"),
                new NamedColor("#44546A"),
                new NamedColor("#5B9BD5"),
                new NamedColor("#ED7D31"),
                new NamedColor("#A5A5A5"),
                new NamedColor("#FFC000"),
                new NamedColor("#4472C4"),
                new NamedColor("#70AD47"),
                
                new NamedColor("#F2F2F2"),
                new NamedColor("#7F7F7F"),
                new NamedColor("#D0CECE"),
                new NamedColor("#D6DCE4"),
                new NamedColor("#DEEBF6"),
                new NamedColor("#FBE5D5"),
                new NamedColor("#EDEDED"),
                new NamedColor("#FFF2CC"),
                new NamedColor("#D9E2F3"),
                new NamedColor("#E2EFD9"),

                new NamedColor("#D8D8D8"),
                new NamedColor("#595959"),
                new NamedColor("#AEABAB"),
                new NamedColor("#ADB9CA"),
                new NamedColor("#BDD7EE"),
                new NamedColor("#F7CBAC"),
                new NamedColor("#DBDBDB"),
                new NamedColor("#FEE599"),
                new NamedColor("#B4C6E7"),
                new NamedColor("#C5E0B3"),
                
                new NamedColor("#BFBFBF"),
                new NamedColor("#3F3F3F"),
                new NamedColor("#757070"),
                new NamedColor("#8496B0"),
                new NamedColor("#9CC3E5"),
                new NamedColor("#F4B183"),
                new NamedColor("#C9C9C9"),
                new NamedColor("#FFD965"),
                new NamedColor("#8EAADB"),
                new NamedColor("#A8D08D"),

                new NamedColor("#A5A5A5"),
                new NamedColor("#262626"),
                new NamedColor("#3A3838"),
                new NamedColor("#323F4F"),
                new NamedColor("#2E75B5"),
                new NamedColor("#C55A11"),
                new NamedColor("#7B7B7B"),
                new NamedColor("#BF9000"),
                new NamedColor("#2F5496"),
                new NamedColor("#538135"),
                
                new NamedColor("#7F7F7F"),
                new NamedColor("#0C0C0C"),                
                new NamedColor("#171616"),
                new NamedColor("#222A35"),
                new NamedColor("#1E4E79"),
                new NamedColor("#833C0B"),
                new NamedColor("#525252"),
                new NamedColor("#7F6000"),
                new NamedColor("#1F3864"),
                new NamedColor("#375623")
            };          
        }

        private static ColorSet CreateStandardColors()
        {
            return new ColorSet("Standard Colors")
            {
                new NamedColor("#C00000"),
                new NamedColor("#FF0000"),
                new NamedColor("#FFC000"),
                new NamedColor("#FFFF00"),
                new NamedColor("#92D050"),
                new NamedColor("#00B050"),
                new NamedColor("#00B0F0"),
                new NamedColor("#0070C0"),
                new NamedColor("#002060"),
                new NamedColor("#7030A0")
            };
        } 
    }
}
