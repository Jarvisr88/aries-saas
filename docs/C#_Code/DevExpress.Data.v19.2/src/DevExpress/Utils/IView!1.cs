namespace DevExpress.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IView<in TPresenter>
    {
        event EventHandler Cancel;

        event EventHandler Ok;

        void BeginUpdate();
        void EndUpdate();
        void RegisterPresenter(TPresenter presenter);
        void Start();
        void Stop();
        void Warning(string message);
    }
}

