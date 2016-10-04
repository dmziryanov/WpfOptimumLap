using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Threading;
using DevComponents.WPF.Controls;
using DevComponents.WPF.Mobile;

namespace OptimumLap.ViewModel
{
    public class FontComboBoxViewModel : ComboBoxViewModel
    {
        public FontComboBoxViewModel()
        {
            var fonts = new FontGallery();
            Items = fonts;
            SelectedItem = fonts.ThemeFonts[0];
            ValuePath = "Name";
            IsEditable = true;
            AutoCompleteOption = AutoCompleteOptions.FreeText;
        }

        protected override void OnValueChanged(object oldValue, object newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ContextIdle,
                (Action)delegate
                {
                    SelectedItem = ((FontGallery)Items).UpdateRecentFonts(Value as string);
                });
        }   
    }

    public class FontSizeComboBoxViewModel : ComboBoxViewModel
    {
        public FontSizeComboBoxViewModel()
        {
            Items = new double[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };            
            IsEditable = true;
            Value = 14.0;
            AutoCompleteOption = AutoCompleteOptions.FreeText;
        }
    }
    
    public class ColorGalleryPopupButtonViewModel : PopupButtonViewModel
    {
        private ColorGallery.NamedColor _SelectedColor;

        public ColorGalleryPopupButtonViewModel()
        {
            ColorGallery = new ColorGallery();
            _SelectedColor = ColorGallery.DefaultColor;

            IsSplitButton = true;            
            Content = Strings.Current.GetString(StringId.FontColor);
            ToolTip = Strings.Current.GetString(StringId.FontColor);
            PopupTitle = Strings.Current.GetString(StringId.FontColor);
            CommandParameter = ColorGallery.DefaultColor.Color;

            SelectedColor = ColorGallery[Colors.Black];
        }

        public ColorGallery ColorGallery { get; set; }

        public ColorGallery.NamedColor SelectedColor
        {
            get { return _SelectedColor; }
            set
            {
                if(SetPropertyValue(value, ref _SelectedColor, "SelectedColor"))
                    OnPropertyChanged("SelectedBrush");
             }
        }

        public SolidColorBrush SelectedBrush
        {
            get { return new SolidColorBrush(_SelectedColor.Color); }
            set { SelectedColor = ColorGallery[value.Color]; }
        }
    }

    public partial class HomeRibbonItemViewModel
    {
        private readonly SolidColorBrush _HighlightBrush = Brushes.Yellow;
        private ToggleButtonViewModel _HighlightedToggle;

        public GroupBarViewModel CreateFontFormattingGroup()
        {            
            Fonts = new FontComboBoxViewModel {OverflowIndex = 7, SharedOverflowRow = "fontcombo"};
            FontSizes = new FontSizeComboBoxViewModel {OverflowIndex = 7, SharedOverflowRow = "fontcombo"};
            
            BoldToggle = new ButtonViewModel()
            {

                Content = Strings.Current.GetString(StringId.Bold),
                ImageSource = Images.Current.GetImage(ImageId.BoldIcon),
            };

            return new GroupBarViewModel
            {
                ToolTip = Strings.Current.GetString(StringId.FontFormatting),
                PopupTitle = Strings.Current.GetString(StringId.Font),
                ImageSource = Images.Current.GetImage(ImageId.FontFormattingIcon),

                Items = new ObservableCollection<object>
                {
           
                    BoldToggle,
           
                }
            };            
        }

        public Brush HighlightBrush
        {
            get { return _HighlightedToggle.IsChecked.GetValueOrDefault() ? _HighlightBrush : null; }
            set
            {
                _HighlightedToggle.IsChecked = value == _HighlightBrush;
                OnPropertyChanged("ActiveBackground");
            }
        }

        public FontComboBoxViewModel Fonts { get; private set; }
        public ComboBoxViewModel FontSizes { get; private set; }
        public ColorGalleryPopupButtonViewModel FontColors { get; private set; }
        public ButtonViewModel BoldToggle { get; private set; }
        public ToggleButtonViewModel ItalicToggle { get; private set; }
        public ToggleButtonViewModel UnderlineToggle { get; private set; }
        public ToggleButtonViewModel StrikethrougToggle { get; private set; }
        public ToggleButtonViewModel SubscriptToggle { get; private set; }
        public ToggleButtonViewModel SuperscriptToggle { get; private set; }        
    }
}
