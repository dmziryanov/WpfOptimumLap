using System;
using System.ComponentModel;
using System.Windows.Input;


namespace OptimumLap
{

    public interface IShellInteraction
    {
        event CancelEventAction OnShellClosing;
        event Action OnShellClosed;
    }

    public delegate void CancelEventAction(CancelEventArgs e);
}
