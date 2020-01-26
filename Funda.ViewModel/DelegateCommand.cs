using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Funda.ViewModel
{
    public class DelegateCommand : IAsyncCommand
    {
        private Func<Task> executeAction;
        private Func<bool> canExecute;

        public DelegateCommand(Func<Task> executeAction, Func<bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            ExecuteAsync().GetAwaiter().GetResult();
        }

        public async Task ExecuteAsync()
        {
            await executeAction();         
        }
    }
}
