namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class WindowStrategy<T, TWrapper> : WindowStrategyBase<T> where T: DependencyObject where TWrapper: class, IWindowWrapper<T>, new()
    {
        private bool suppressClosed;

        protected override void ActivateCore()
        {
            this.Wrapper.Activate();
        }

        protected override void AfterShowDialogCore()
        {
            this.OnWindowClosed(null, EventArgs.Empty);
        }

        protected override void CloseCore()
        {
            this.Unsubscribe();
            this.Wrapper.Close();
        }

        protected virtual void ConfigureWrapper()
        {
            this.Wrapper.DataContext = base.ViewModel;
            this.Wrapper.Content ??= base.ViewModel;
            if (this.Wrapper.ContentTemplate == null)
            {
                this.Wrapper.ContentTemplateSelector ??= base.ViewSelector;
            }
            base.Owner.ConfigureChild(this.Wrapper.Target);
            this.Subscribe();
        }

        protected override void InitializeCore()
        {
            base.InitializeCore();
            TWrapper local1 = Activator.CreateInstance<TWrapper>();
            local1.Target = base.Target;
            this.Wrapper = local1;
        }

        private void OnWindowActivated(object sender, EventArgs e)
        {
            base.Owner.SelectViewModel(base.ViewModel);
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            if (!this.suppressClosed)
            {
                base.Owner.RemoveViewModel(base.ViewModel);
            }
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (!e.Cancel && !base.Owner.CanRemoveViewModel(base.ViewModel))
            {
                e.Cancel = true;
            }
        }

        protected override void ShowCore()
        {
            this.ConfigureWrapper();
            this.Wrapper.Show();
        }

        protected override MessageBoxResult ShowDialogCore()
        {
            this.ConfigureWrapper();
            this.suppressClosed = true;
            MessageBoxResult result = this.Wrapper.ShowDialog();
            this.suppressClosed = false;
            return result;
        }

        protected virtual void Subscribe()
        {
            this.Unsubscribe();
            this.Wrapper.Closing += new CancelEventHandler(this.OnWindowClosing);
            this.Wrapper.Closed += new EventHandler(this.OnWindowClosed);
            this.Wrapper.Activated += new EventHandler(this.OnWindowActivated);
        }

        protected override void UninitializeCore()
        {
            this.Unsubscribe();
            T local = default(T);
            this.Wrapper.Target = local;
            TWrapper local2 = default(TWrapper);
            this.Wrapper = local2;
            base.UninitializeCore();
        }

        protected virtual void Unsubscribe()
        {
            this.Wrapper.Closing -= new CancelEventHandler(this.OnWindowClosing);
            this.Wrapper.Closed -= new EventHandler(this.OnWindowClosed);
            this.Wrapper.Activated -= new EventHandler(this.OnWindowActivated);
        }

        protected TWrapper Wrapper { get; private set; }
    }
}

