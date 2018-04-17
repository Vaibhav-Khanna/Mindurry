using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.DataModels
{
    public class CheckablePerson : Person
    {
        private bool isChecked;
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                ViewModels.StaticViewModel.OnSelectionChanged();
            }
        }
    }
}
