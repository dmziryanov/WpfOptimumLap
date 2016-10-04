using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;

namespace MobileRibbonMVVMSample
{
    public class Font
    {
        public Font(string name)
        {
            Name = name;
        }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class FontCollection : ObservableCollection<Font>
    {
        public FontCollection(string name, IEnumerable<Font> enumerable) : base(enumerable)
        {
            Name = name;
        }
        public FontCollection(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public Font this[string fontName]
        {
            get { return this.FirstOrDefault(f => f.Name == fontName); }
        }
    }

    public class FontGallery : List<FontCollection>
    {
        public FontGallery()
        {            
            Add(ThemeFonts = new FontCollection("Theme Fonts")
            {
                new Font("Calibri"),
                new Font("Calibri Light")
            });
            Add(RecentFonts = new FontCollection("Recent Fonts"));
            Add(AllFonts = GetAllFonts());
        }

        public FontCollection AllFonts { get; set; }
        public FontCollection ThemeFonts { get; set; }
        public FontCollection RecentFonts { get; set; }

        public bool ContainsFont(string fontName)
        {
            return AllFonts[fontName] != null || ThemeFonts[fontName] != null;
        }

        internal Font UpdateRecentFonts(string recentFontName)
        {
            if(string.IsNullOrEmpty(recentFontName))
                return null;

            var font = ThemeFonts[recentFontName];
            if(font != null)
                return font;

            font = RecentFonts[recentFontName];
            if(font != null)
            {
                RecentFonts.Move(RecentFonts.IndexOf(font), 0);
                return font;
            }

            font = new Font(recentFontName);
            RecentFonts.Insert(0, font);
            if(RecentFonts.Count > 10)
                RecentFonts.RemoveAt(RecentFonts.Count - 1);
            return font;
        }
        
        private static FontCollection GetAllFonts()
        {
            var allFonts = Fonts.SystemFontFamilies.Select(f => new Font(f.Source));
            return new FontCollection("All Fonts", allFonts.OrderBy(f => f.Name));
        }
    }
}
