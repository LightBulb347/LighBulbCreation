using Funda.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Funda.ViewModel
{
    public class TopEstateAgentsViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<string> TopTenEstateAgents { get; private set; }
        public bool HasGarden { get;  set; }
        public string ErrorText { get; private set; }
        public ICommand GetTopTenCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ITopEstateAgentsService topEstateAgentsService;
        public TopEstateAgentsViewModel()
        {
            topEstateAgentsService = new TopEstateAgentsService();
            GetTopTenCommand = new DelegateCommand(GetTopTenCommandAction, ()=>canExecuteCommand);
            TopTenEstateAgents = new ObservableCollection<string>();
            canExecuteCommand = true;
        }

        private bool canExecuteCommand;
        private async Task GetTopTenCommandAction()
        {
            canExecuteCommand = false;
            TopTenEstateAgents.Clear();
            try
            {
                var topTenEstateAgents = await topEstateAgentsService.GetTopTenEstateAgentElements(HasGarden);
                foreach (var topTenEstateAgent in topTenEstateAgents)
                    TopTenEstateAgents.Add(topTenEstateAgent.ToString());
            }
            catch( RequestLimitExceededException ex)
            {
                ErrorText = ex.Message;
            }
            catch (Exception)
            {
                ErrorText = "Somehting went wrong";
            }

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorText)));
            canExecuteCommand = true;
        }




    }
}
