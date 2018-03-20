using System;
using System.Collections.Generic;
using System.Text;

namespace Mindurry.ViewModels
{
    public static class StaticViewModel
    {
        static public event EventHandler SelectionChanged;

        public static void OnSelectionChanged()
        {
            SelectionChanged?.Invoke(new object(), new EventArgs());
        }
    }
}
