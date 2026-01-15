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
@"the idea:
track your runs based on your perceived effort and experience.
by this you can make your own conclusion of whatever works for you.



functions:
add test data,
register runs,
study and edit previous runs
and browse your total stats with a number of filters";
        public InstructionsViewModel(MainWindowViewModel mainVm)
        {
            MainVm = mainVm;
        }

    }
}
