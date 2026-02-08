using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    class InstructionsViewModel : ViewModelBase
    {
        public MainWindowViewModel MainVm { get; set; }
        public string Instructions { get; } =
@"whatever works for you.

track your runs based on your perceived effort and experience.
by this you can make your own conclusion of whatever works for you.

add test data, register runs, study and edit previous runs and browse your total stats with a number of filters.

type and rating filters include runs that match
any selected option, while water and fueling filters 
strictly exclude runs that do not meet the selected condition.";
        public InstructionsViewModel(MainWindowViewModel mainVm)
        {
            MainVm = mainVm;
        }

    }
}
