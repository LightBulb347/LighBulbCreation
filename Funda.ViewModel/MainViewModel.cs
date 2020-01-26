using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.ViewModel
{
    public class MainViewModel
    {
        public TopEstateAgentsViewModel TopEstateAgentsViewModel { get; }

        public MainViewModel()
        {
            TopEstateAgentsViewModel = new TopEstateAgentsViewModel();
        }
    }
}
