using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DotNetBay.WPF.ViewModel
{
    public class CommandWrapper<TCommandParameter> : ICommand
    {

        private Action<TCommandParameter> executionAction;
        private Func<TCommandParameter, bool> canExecute;

        public CommandWrapper(Action<TCommandParameter> executionAction, Func<TCommandParameter, bool> canExecute)
        {
            this.executionAction = executionAction;
            this.canExecute = canExecute;
        }

        public CommandWrapper(Action<TCommandParameter> executionAction) : this(executionAction, null)
        {
            
        } 


        public bool CanExecute(object parameter)
        {
            if (this.canExecute != null)
            {
                return this.canExecute((TCommandParameter) parameter);
            }
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.executionAction((TCommandParameter)parameter);
        }
    }

    public class CommandWrapper : CommandWrapper<object>
    {
        public CommandWrapper(Action executeAction, Func<bool> canExecute)
            : base(o => executeAction(), o => canExecute())
        {
        }
        public CommandWrapper(Action executeAction)
            : base(o => executeAction(), null)
        {
        }
    }
}
