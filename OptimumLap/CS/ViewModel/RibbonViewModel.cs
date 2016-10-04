﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DevComponents.WPF.Controls;

namespace MobileRibbonMVVMSample.ViewModel
{
    public class RibbonViewModel : ViewModelBase
    {
        public RibbonViewModel(MainViewModel mainViewModel)
        {
            Backstage = new BackstageViewModel(mainViewModel);
            ToolBarItems = CreateToolBarItems();            
            RibbonItems = new List<RibbonItemViewModel>
            {
                (Home = new HomeRibbonItemViewModel()),
                (Insert = new InsertRibbonItemViewModel()),
                (Layout = new LayoutRibbonItemViewModel()),
                (Review  = new ReviewRibbonItemViewModel()),
                (View  = new ViewRibbonItemViewModel())
            };
        }

        public string BackstageButtonContent
        {
            get { return Strings.Current.GetString(StringId.File); }
        }

        public BackstageViewModel Backstage { get; set; }

        public List<RibbonItemViewModel> RibbonItems { get; set; }
        public ObservableCollection<ButtonViewModel> ToolBarItems { get; set; }

        public HomeRibbonItemViewModel Home { get; set; }
        public InsertRibbonItemViewModel Insert { get; set; }
        public LayoutRibbonItemViewModel Layout { get; set; }
        public ReviewRibbonItemViewModel Review { get; set; }
        public ViewRibbonItemViewModel View { get; set; }

        private ObservableCollection<ButtonViewModel> CreateToolBarItems()
        {
            return new ObservableCollection<ButtonViewModel>
            {
                new ButtonViewModel
                {
                    ToolTip = Strings.Current.GetString(StringId.Feedback),
                    ImageSource = Images.Current.GetImage(ImageId.Smiley),                    
                    Command = Commands.SendMail,
                },
                new ButtonViewModel
                {
                    ToolTip = Strings.Current.GetString(StringId.Undo),
                    ImageSource = Images.Current.GetImage(ImageId.Undo),                    
                    Command = ApplicationCommands.Undo,
                },
                new ButtonViewModel
                {
                    ToolTip = Strings.Current.GetString(StringId.Redo),
                    ImageSource = Images.Current.GetImage(ImageId.Redo),
                    Command = ApplicationCommands.Redo,
                },
                new ButtonViewModel
                {
                    ToolTip = Strings.Current.GetString(StringId.Help),
                    ImageSource = Images.Current.GetImage(ImageId.Question),
                    Command = Commands.LaunchHelp,
                },                
            };
        }        
    }
    
    public class LayoutRibbonItemViewModel : RibbonItemViewModel
    {
        public LayoutRibbonItemViewModel()
        {
            Header = Strings.Current.GetString(StringId.Layout);
        }
    }

    public class ReviewRibbonItemViewModel : RibbonItemViewModel
    {
        public ReviewRibbonItemViewModel()
        {
            Header = Strings.Current.GetString(StringId.Review);
        }
    }
 }
