namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using System.Windows.Media;

    public sealed class MenuItem : NamedMenuItem
    {
        internal MenuItem(string displayName, ImageSource icon, Action command) : this(displayName, icon, null, command)
        {
        }

        internal MenuItem(string displayName, ImageSource icon, FormatConditionBase formatCondition, Action command) : base(displayName, icon)
        {
            this.<FormatCondition>k__BackingField = formatCondition;
            this.<Command>k__BackingField = new DelegateCommand(command);
        }

        public FormatConditionBase FormatCondition { get; }

        public ICommand Command { get; }
    }
}

