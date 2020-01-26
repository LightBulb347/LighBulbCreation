using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Funda.ViewModel
{
    public class DelegateCommand : ICommand
    {
        private Action executeAction;

        public DelegateCommand(Action executeAction)
        {
            this.executeAction = executeAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction.Invoke();
        }
    }
}
