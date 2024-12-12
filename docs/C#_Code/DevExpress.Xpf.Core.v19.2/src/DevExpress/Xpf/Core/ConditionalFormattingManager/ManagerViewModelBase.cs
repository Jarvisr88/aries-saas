namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class ManagerViewModelBase
    {
        public ManagerViewModelBase(IDialogContext context)
        {
            this.Context = context;
        }

        protected string GetLocalizedString(ConditionalFormattingStringId id) => 
            ConditionalFormattingLocalizer.GetString(id);

        protected internal IDialogContext Context { get; private set; }

        public abstract string Description { get; }
    }
}

