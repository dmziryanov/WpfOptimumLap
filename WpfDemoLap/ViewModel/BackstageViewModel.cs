using System.Collections.Generic;
using System.Windows.Input;
using DevComponents.WPF.Controls;

namespace OptimumLap.ViewModel
{    
    public class CreateNewBackstageItemViewModel : BackstageItemViewModel
    {
        public CreateNewBackstageItemViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;
            Header = Strings.Current.GetString(StringId.New);
        }

        public MainViewModel MainViewModel { get; set; }
    }

    public class OpenFileBackstageItemViewModel : BackstageItemViewModel
    {
        public OpenFileBackstageItemViewModel()
        {
            IsSelected = true;               
            Header = Strings.Current.GetString(StringId.Open);
            BrowseText = Strings.Current.GetString(StringId.Browse);                        
        }

        public string BrowseText { get; set; }
        public RelayCommand BrowseCommand { get; set; }
    }

    public class BackstageViewModel : ItemSelectorViewModel
    {
        private bool _IsOpen;

        public BackstageViewModel(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            Items = new List<BackstageItemViewModel>
            {
                new CreateNewBackstageItemViewModel(mainViewModel),
                new OpenFileBackstageItemViewModel
                {
                    BrowseCommand = new RelayCommand(p =>
                    {
                        IsOpen = false;
                        MainViewModel.OpenDocument();
                    })
                },
                new BackstageItemViewModel
                {
                    Header = Strings.Current.GetString(StringId.Save),
                    ClosesBackstage = true,
                    Command = new RelayCommand(p => MainViewModel.SaveDocument(false))
                },
                new BackstageItemViewModel
                {
                    Header = Strings.Current.GetString(StringId.SaveAs),
                    ClosesBackstage = true,         
                    Command = new RelayCommand(p => MainViewModel.SaveDocument(true))           
                },
                new BackstageItemViewModel
                {
                    Header = Strings.Current.GetString(StringId.Print),
                    ClosesBackstage = true,
                },
                new BackstageItemViewModel
                {
                    Header = Strings.Current.GetString(StringId.Close),
                    Command = ApplicationCommands.Close,
                },
                new BackstageItemViewModel
                {
                    Header = Strings.Current.GetString(StringId.Settings),
                    ClosesBackstage = true,
                    Command = new RelayCommand(p => MainViewModel.Settings.IsOpen = true)
                }
            };
        }

        public MainViewModel MainViewModel { get; set; }

        public bool IsOpen
        {
            get { return _IsOpen; }
            set { SetPropertyValue(value, ref _IsOpen, "IsOpen"); }
        }
    }
}
