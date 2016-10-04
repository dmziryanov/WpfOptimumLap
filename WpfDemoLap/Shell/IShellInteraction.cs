using System;
using System.ComponentModel;
using System.Windows.Input;
/*using ERP.Infrastructure.CrossCutting.AggregationEvents;
using ERP.Infrastructure.CrossCutting.Commands;*/

namespace OptimumLap
{
    /// <summary>
    /// Интерфейс презентатора главного окна приложения
    /// </summary>
    public interface IShellInteraction
    {
        /// <summary>
        /// Событие возникает перед закрытием главного окна, с возможностью отмены
        /// </summary>
        event CancelEventAction OnShellClosing;

        /// <summary>
        /// Событие возникает после закрытия главного окна
        /// </summary>
        event Action OnShellClosed;

        //DevComponenents window somehow don't contain this member, but must
        //event KeyEventHandler PreviewKeyDown;        
    }

    public delegate void CancelEventAction(CancelEventArgs e);
}
