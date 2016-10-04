using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using OptimumLap.ViewModel;
using DelegateCommand = Microsoft.Practices.Prism.Commands.DelegateCommand;

namespace OptimumLap
{
    class ShellPresenter : PresenterBase<ShellRibbonWindow, MainViewModel>, IShell
    {
        readonly IEventAggregator _eventAggregator;
        readonly IUnityContainer _container;
        readonly HashSet<CancelEventAction> _closingHandlerSet = new HashSet<CancelEventAction>();
        //To Inject here
        //readonly IApplicationSettings _appSettings;
        //readonly IMessenger _messenger;
        //readonly IUser _user;

        //readonly ILogger _logger;
        //readonly MenuAssistantPresenter _menuAssistantPresenter = new MenuAssistantPresenter();
        //readonly List<IWindowMessageHandler> _messageHandlerList = new List<IWindowMessageHandler>();
        //readonly UpdateManager _updateManager;

        //Here to inject ILogger logger etc
        public ShellPresenter(IEventAggregator eventAggregator, IUnityContainer container)
        {
            _eventAggregator = eventAggregator;
            _container = container;
            
        }

        public event CancelEventAction OnShellClosing
        {
            add { _closingHandlerSet.Add(value); }
            remove { _closingHandlerSet.Remove(value); }
        }

        /*public event KeyEventHandler PreviewKeyDown
        {
            add { View.PreviewKeyDown += value; }
            remove { View.PreviewKeyDown -= value; }
        }*/


        /*
                event KeyEventHandler IShellInteraction.PreviewKeyDown
                {
                    add { throw new NotImplementedException(); }
                    remove { throw new NotImplementedException(); }
                }
        */

        public event Action OnShellClosed;


        public void Execute()
        {
        //    SubscribeEvents();
            InitializeViewModel();
            InitializeView();

        }

      
        void InitializeView()
        {
           
        }

        void InitializeViewModel()
        {

            /*ViewModel.IsFullScreen = false;
            ViewModel.IsMainRibbonMinimized = false;
            ViewModel.IsServiceMessagePanelVisible = true;
            ViewModel.IsStatusBarVisible = true;*/
        }

        void OnViewClosing(object sender, CancelEventArgs e)
        {
            foreach (CancelEventAction action in _closingHandlerSet)
            {
                action(e);
                if (e.Cancel)
                    return;
            }
        }

        void OnViewClosed(object sender, EventArgs e)
        {
            
            
            var handler = OnShellClosed;
            if (handler != null)
            {
                OnShellClosed();
            }
        }
    }
}