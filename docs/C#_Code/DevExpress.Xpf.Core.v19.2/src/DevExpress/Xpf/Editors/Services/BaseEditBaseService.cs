namespace DevExpress.Xpf.Editors.Services
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;

    public abstract class BaseEditBaseService
    {
        protected BaseEditBaseService(BaseEdit editor)
        {
            this.OwnerEdit = editor;
        }

        protected BaseEdit OwnerEdit { get; private set; }

        protected EditStrategyBase EditStrategy =>
            this.OwnerEdit.EditStrategy;

        protected ActualPropertyProvider PropertyProvider =>
            this.OwnerEdit.PropertyProvider;
    }
}

