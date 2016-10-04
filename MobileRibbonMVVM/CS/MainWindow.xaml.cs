using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;
using OptimumLap.Resources;

namespace OptimumLap
{
    public partial class MainWindow
    {
        public MainWindow()
        {            
            CommandBindings.Add(new CommandBinding(Commands.ShowHelp, ShowHelpExecuted));
            CommandBindings.Add(new CommandBinding(Commands.ClearSelectionFormatting, ClearFormattingCommandExecuted));
            CommandBindings.Add(new CommandBinding(Commands.HighlightSelection, HighlightSelectionCommandExecuted));
            CommandBindings.Add(new CommandBinding(Commands.IncreaseIndentation, IncreaseIndentationExecuted));
            CommandBindings.Add(new CommandBinding(Commands.DecreaseIndentation, DecreaseIndentationExecuted));
            CommandBindings.Add(new CommandBinding(Commands.AddSpacingAfterParagraph, AddSpacingAfterParagraphExecuted, AddSpacingAfterParagraphCanExecute));
            CommandBindings.Add(new CommandBinding(Commands.RemoveSpacingAfterParagraph, RemoveSpacingAfterParagraphExecuted, RemoveSpacingAfterParagraphCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.SaveAs, SaveAsExecuted, SaveAsCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Print, PrintExecuted, PrintCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, UndoExecuted, UndoCanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo, RedoExecuted, RedoCanExecute));

            InitializeComponent();

            // Make "Normal" style the default paragraph style.
            
        }

        private void RedoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            
        }

        private void RedoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }

        private void UndoExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void UndoCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }

        private void ShowHelpExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.devcomponents.com/kb2/?p=1665");
        }

        private void PrintExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)delegate
            {
                MessageBox.Show(this, "Print...", "PRINT", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }, DispatcherPriority.ContextIdle);
        }

        private void PrintCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveAsExecuted(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void SaveAsCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
        }

        private void RemoveSpacingAfterParagraphCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }

        private void RemoveSpacingAfterParagraphExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void AddSpacingAfterParagraphCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
        }

        private void AddSpacingAfterParagraphExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void DecreaseIndentationExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void IncreaseIndentationExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void HighlightSelectionCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void ClearFormattingCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
        }

        private void DialogLauncherClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dialog Launcher Clicked.");
        }
    }
}
