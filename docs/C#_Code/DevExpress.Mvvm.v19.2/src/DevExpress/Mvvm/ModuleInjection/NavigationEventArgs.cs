namespace DevExpress.Mvvm.ModuleInjection
{
    using System;
    using System.Runtime.CompilerServices;

    public class NavigationEventArgs : EventArgs
    {
        public NavigationEventArgs(string regionName, object oldVM, object newVM, string oldVMKey, string newVMKey)
        {
            this.RegionName = regionName;
            this.OldViewModel = oldVM;
            this.NewViewModel = newVM;
            this.OldViewModelKey = oldVMKey;
            this.NewViewModelKey = newVMKey;
        }

        public string RegionName { get; private set; }

        public object OldViewModel { get; private set; }

        public string OldViewModelKey { get; private set; }

        public object NewViewModel { get; private set; }

        public string NewViewModelKey { get; private set; }
    }
}

