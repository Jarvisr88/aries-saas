namespace DevExpress.Data.WizardFramework
{
    using System;
    using System.Runtime.CompilerServices;

    public class WizardNextPageShowingEventArgs<TWizardModel> : EventArgs where TWizardModel: IWizardModel
    {
        private Type nextPageType;

        public WizardNextPageShowingEventArgs(IWizardPage<TWizardModel> currentPage, Type nextPageType);

        public bool Cancel { get; set; }

        public Type NextPageType { get; set; }

        public IWizardPage<TWizardModel> CurrentPage { get; private set; }
    }
}

