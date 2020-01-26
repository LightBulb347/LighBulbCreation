using System.Threading.Tasks;
using System.Windows.Input;

namespace Funda.ViewModel
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
    }
}
