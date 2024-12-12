namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class FormatsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public FormatsViewModel(IList<object> groups)
        {
            this.FormatConditionGroups = groups;
        }

        private void RaisePropertyChanged(string name)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public IList<object> FormatConditionGroups { get; private set; }
    }
}

