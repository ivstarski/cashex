// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActionCommand .cs" company="Gazprom Space Systems">
//   Developer: Ivan Starski
// </copyright>
// <summary>
//   The command base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Cashex.support
{
    using System;
    using System.Windows.Input;

    public class ActionCommand : ICommand
    {
        private readonly Action<object> actionWithPar;
        private readonly Func<bool> canExecute;
        private readonly Action action;

        public ActionCommand(Action action, Func<bool> canExecute = null)
        {
            if( action == null )
                throw new ArgumentNullException(nameof(action));
            this.canExecute = canExecute;
            this.action = action;
        }

        public ActionCommand(Action<object> actionWithPar, Func<bool> canExecute = null)
        {
            if( actionWithPar == null )
                throw new ArgumentNullException(nameof(actionWithPar));
            this.actionWithPar = actionWithPar;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            if( actionWithPar != null )
            {
                if( parameter == null )
                    return;
                actionWithPar(parameter);
            }
            else
                action();
        }

        public bool CanExecute(object parameter) => canExecute == null || canExecute();

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}