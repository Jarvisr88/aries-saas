namespace DevExpress.Data.WizardFramework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public abstract class WizardPageBase<TView, TModel> : IWizardPage<TModel> where TModel: IWizardModel
    {
        private readonly TView view;
        private TModel model;
        [CompilerGenerated]
        private EventHandler Changed;
        [CompilerGenerated]
        private EventHandler<WizardPageErrorEventArgs> Error;

        public event EventHandler Changed;

        public event EventHandler<WizardPageErrorEventArgs> Error;

        protected WizardPageBase(TView view);
        public abstract void Begin();
        public abstract void Commit();
        public virtual Type GetNextPageType();
        protected void RaiseChanged();
        protected void RaiseError(string errorMessage);
        public virtual bool Validate(out string errorMessage);

        protected TView View { get; }

        public TModel Model { get; set; }

        public virtual bool MoveNextEnabled { get; }

        public virtual bool FinishEnabled { get; }

        public object PageContent { get; }
    }
}

