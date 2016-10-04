using System.Collections.Generic;
using DevComponents.WPF.Mobile;

namespace MobileRibbonMVVMSample.ViewModel
{
    public class OptionSettingsFlyoutItemViewModel : SettingsFlyoutItemViewModel
    {        
        private MobileTheme _MobileTheme = MobileTheme.BlueTheme;
        private MobileWindowChromeStyle _ChromeStyle = MobileWindowChromeStyle.ExtendedRibbon;
        private bool _HideTitle;

        public OptionSettingsFlyoutItemViewModel()
        {
            Header = Strings.Current.GetString(StringId.Options);
        }

        public string ThemeColorText
        {
            get { return Strings.Current.GetString(StringId.ThemeColor); }
        }
        public string SeedColorText
        {
            get { return Strings.Current.GetString(StringId.SeedColor); }
        }

        public MobileTheme MobileTheme
        {
            get { return _MobileTheme; }
            set { SetPropertyValue(value, ref _MobileTheme, "MobileTheme"); }
        }

        public MobileWindowChromeStyle ChromeStyle
        {
            get { return _ChromeStyle; }
            set { SetPropertyValue(value, ref _ChromeStyle, "ChromeStyle"); }
        }

        public bool HideTitle
        {
            get { return _HideTitle; }
            set { SetPropertyValue(value, ref _HideTitle, "HideTitle"); }
        }
    }

    public class SettingsFlyoutViewModel : ItemSelectorViewModel
    {
        private bool _IsOpen;

        public SettingsFlyoutViewModel()
        {
            Items = new List<object>
            {
                (Options = new OptionSettingsFlyoutItemViewModel()),
                new SettingsFlyoutItemViewModel
                {
                    Header = Strings.Current.GetString(StringId.License),
                    Content = Strings.Current.GetString(StringId.LicenseMessage)
                },
                new SettingsFlyoutItemViewModel
                {
                    Header = Strings.Current.GetString(StringId.Help),
                    ClosesFlyout = true,
                    Command = Commands.LaunchHelp,
                }
            };    
        }

        public string Header
        {
            get { return Strings.Current.GetString(StringId.SettingsHeader); }
        }
        public string Footer
        {
            get { return Strings.Current.GetString(StringId.SettingsFooter); }
        }
        
        public OptionSettingsFlyoutItemViewModel Options { get; set; }

        public bool IsOpen
        {
            get { return _IsOpen; }
            set { SetPropertyValue(value, ref _IsOpen, "IsOpen"); }
        }
    }
}
