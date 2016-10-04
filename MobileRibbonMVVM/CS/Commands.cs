using System.Windows.Input;
using DevComponents.WPF.Controls;

namespace OptimumLap
{
    public static class Commands
    {
        public static RoutedCommand ClearSelectionFormatting = new RoutedCommand("ClearSelectionFormatting", typeof(Commands));
        public static RoutedCommand HighlightSelection = new RoutedCommand("HighlightSelection", typeof(Commands));
        public static RoutedCommand DecreaseIndentation = new RoutedCommand("DecreaseIndentation", typeof(Commands));
        public static RoutedCommand IncreaseIndentation = new RoutedCommand("IncreaseIndentation", typeof(Commands));
        public static RoutedCommand AddSpacingAfterParagraph = new RoutedCommand("AddSpacingAfterParagraph", typeof(Commands));
        public static RoutedCommand RemoveSpacingAfterParagraph = new RoutedCommand("RemoveSpacingAfterParagraph", typeof(Commands));
        public static RoutedCommand ShowHelp = new RoutedCommand("ShowHelp", typeof(Commands));

        public static RelayCommand LaunchHelp
        {
            get { return new RelayCommand(p => { System.Diagnostics.Process.Start("http://www.devcomponents.com/kb2/?p=1665"); }); }
        }

        public static RelayCommand SendMail
        {
            get { return new RelayCommand(p => { System.Diagnostics.Process.Start("mailto:support@devcomponents.com?subject=DotNetBar WPF MobileRibbon"); }); }
        }
    }
}
