using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_WEWFY_Presentation.ViewModels
{
    public class EditPreviousWorkoutWrapper : ViewModelBase
    {
        public PreviousWorkoutsViewModel PreviousVM { get; }
        public RegisterWorkoutViewModel RegisterVM { get; }

        public EditPreviousWorkoutWrapper(PreviousWorkoutsViewModel previous, RegisterWorkoutViewModel register)
        {
            PreviousVM = previous;
            RegisterVM = register;
        }
    }
}
