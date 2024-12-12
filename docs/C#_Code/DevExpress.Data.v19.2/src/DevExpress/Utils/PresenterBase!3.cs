namespace DevExpress.Utils
{
    using System;

    public abstract class PresenterBase<TModel, TView, TPresenter> where TView: IView<TPresenter> where TPresenter: PresenterBase<TModel, TView, TPresenter>
    {
        private readonly TView view;
        private readonly TModel model;
        private bool success;

        protected PresenterBase(TModel model, TView view)
        {
            this.model = model;
            this.view = view;
        }

        protected abstract void Commit();
        public void InitView()
        {
            this.view.BeginUpdate();
            this.view.RegisterPresenter((TPresenter) this);
            this.InitViewCore();
            this.view.EndUpdate();
        }

        protected abstract void InitViewCore();
        protected virtual void OnCancel(object s, EventArgs e)
        {
            this.StopView();
            this.success = false;
        }

        protected virtual void OnOk(object s, EventArgs e)
        {
            string message = this.Validate();
            if (message != null)
            {
                this.view.Warning(message);
            }
            else
            {
                this.StopView();
                this.success = true;
            }
        }

        public virtual bool Run()
        {
            this.view.Ok += new EventHandler(this.OnOk);
            this.view.Cancel += new EventHandler(this.OnCancel);
            this.view.Start();
            if (this.success)
            {
                this.Commit();
            }
            return this.success;
        }

        private void StopView()
        {
            this.view.Ok -= new EventHandler(this.OnOk);
            this.view.Cancel -= new EventHandler(this.OnCancel);
            this.view.Stop();
        }

        protected virtual string Validate() => 
            null;

        protected TModel Model =>
            this.model;

        protected TView View =>
            this.view;
    }
}

