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
            GetTopTenCommand = new DelegateCommand(GetTopTenCommandAction);
            TopTenEstateAgents = new ObservableCollection<string>();
            HasGarden = true;
        }

        private void GetTopTenCommandAction()
        {
            TopTenEstateAgents.Clear();
            try
            {
                //In future we want to make this one awaited and UI responsive
                var topTenEstateAgents = topEstateAgentsService.GetTopTenEstateAgentElements(HasGarden).GetAwaiter().GetResult();
                foreach (var topTenEstateAgent in topTenEstateAgents)
                    TopTenEstateAgents.Add(topTenEstateAgent.ToString());
            }
            catch( RequestLimitExceededException ex)
            {
                ErrorText = ex.Message;
            }
            catch (Exception)
            {
                ErrorText = "Something went wrong";
            }

            PropertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorText)));
        }
    }
}
