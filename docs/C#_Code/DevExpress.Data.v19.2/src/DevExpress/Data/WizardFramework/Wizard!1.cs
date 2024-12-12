namespace DevExpress.Data.WizardFramework
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class Wizard<TWizardModel> where TWizardModel: IWizardModel
    {
        private readonly IWizardView view;
        private readonly List<IWizardPage<TWizardModel>> pageList;
        private readonly IWizardPageFactory<TWizardModel> pageFactory;
        private readonly TimeMachine<TWizardModel> timeMachine;
        private readonly Semaphore semaphore;
        private TWizardModel resultModel;
        private IWizardPage<TWizardModel> currentPage;
        [CompilerGenerated]
        private EventHandler<WizardNextPageShowingEventArgs<TWizardModel>> NextPageShowing;
        [CompilerGenerated]
        private EventHandler Completed;
        [CompilerGenerated]
        private EventHandler Cancelled;

        public event EventHandler Cancelled;

        public event EventHandler Completed;

        public event EventHandler<WizardNextPageShowingEventArgs<TWizardModel>> NextPageShowing;

        public Wizard(IWizardView view, TWizardModel model, IWizardPageFactory<TWizardModel> pageFactory);
        private void currentPage_Changed(object sender, EventArgs e);
        private void currentPage_Error(object sender, WizardPageErrorEventArgs e);
        private IWizardPage<TWizardModel> GetNextPage();
        public TWizardModel GetResultModel();
        private void MoveToPage(IWizardPage<TWizardModel> page, Action moveTimeMachine, bool addToList);
        protected virtual void RefreshView();
        public void SetStartPage(Type pageType);
        private bool ValidatePage(IWizardPage<TWizardModel> page);
        private void view_Cancel(object sender, EventArgs e);
        private void view_Finish(object sender, EventArgs e);
        private void view_Next(object sender, EventArgs e);
        private void view_Previous(object sender, EventArgs e);

        public IWizardView View { get; }

        public IWizardPage<TWizardModel> CurrentPage { get; }

        internal bool ShouldMoveToTheEndOfHistory { get; set; }
    }
}

