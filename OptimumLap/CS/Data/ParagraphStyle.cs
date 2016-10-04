using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace MobileRibbonMVVMSample
{
    public class ParagraphStyle : Style
    {
        public string StyleName { get; set; }
        public int SortOrder { get; set; }

        public Brush Foreground
        {
            get { return (Brush)GetSetterValue(TextElement.ForegroundProperty); }
        }

        public FontFamily FontFamily
        {
            get { return (FontFamily)GetSetterValue(TextElement.FontFamilyProperty) ?? new FontFamily("Calibri"); }
        }

        public object FontSize
        {
            get { return (double)GetSetterValue(TextElement.FontSizeProperty); }
        }

        public FontWeight FontWeight
        {
            get { return ((FontWeight?)GetSetterValue(TextElement.FontWeightProperty)).GetValueOrDefault(); }
        }

        public FontStyle FontStyle
        {
            get { return ((FontStyle?)GetSetterValue(TextElement.FontStyleProperty)).GetValueOrDefault(); }
        }

        private object GetSetterValue(DependencyProperty property)
        {
            var setter = Setters.OfType<Setter>().FirstOrDefault(s => s.Property == property);
            if(setter == null && BasedOn != null)
                setter = BasedOn.Setters.OfType<Setter>().FirstOrDefault(s => s.Property == property);
            return setter != null ? setter.Value : null;
        }
    }
}
