using System.Collections.Generic;
using System.IO;
using DevComponents.WPF.Controls;
using Microsoft.Win32;

namespace MobileRibbonMVVMSample.ViewModel
{
    public interface ISamplePresenter
    {
         
    }

    public class MainViewModel : ViewModelBase
    {
        private string _DocumentFileTitle;
        private string _DocumentFilePath;
        private ISamplePresenter Presenter;

        public MainViewModel()
        {
            TitleBarItems = CreateTitleBarItem();
            Settings = new SettingsFlyoutViewModel();
            Ribbon = new RibbonViewModel(this);
            DocumentFileTitle = "New Document";
        }

        public List<ButtonViewModel> TitleBarItems { get; set; }

        public SettingsFlyoutViewModel Settings { get; set; }
        public RibbonViewModel Ribbon { get; set; }

        public string DocumentFileTitle
        {
            get { return _DocumentFileTitle; }
            set
            {
                if(SetPropertyValue(value, ref _DocumentFileTitle, "DocumentFileTitle"))
                    RenameDocument(value);
            }
        }

        public string DocumentFilePath
        {
            get { return _DocumentFilePath; }
            set
            {
                _DocumentFilePath = value;
                DocumentFileTitle = Path.GetFileNameWithoutExtension(value);
            }
        }

        private void RenameDocument(string newFileTitle)
        {
            // ToDo
        }

        public void OpenDocument()
        {
            var dialog = new OpenFileDialog();
            dialog.ShowDialog();            
        }

        public void SaveDocument(bool showFilePicker)
        {
            var dialog = new SaveFileDialog();
            dialog.ShowDialog();
        }

        private List<ButtonViewModel> CreateTitleBarItem()
        {
            return new List<ButtonViewModel>
            {
                new ButtonViewModel { Content = "Jane Doe" }
            };
        }
    }
}
