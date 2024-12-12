namespace DevExpress.Data
{
    using System;

    public class ControllerRowValuesKeeper : IDisposable
    {
        private BaseGridController controller;
        private object[] values;

        public ControllerRowValuesKeeper(BaseGridController controller);
        public void Clear();
        public void Dispose();
        public void RestoreValues();
        public void SaveValues();

        protected virtual int ControllerRow { get; }

        protected bool CanSave { get; }

        protected bool CanRestore { get; }

        protected object[] Values { get; }

        protected BaseGridController Controller { get; }
    }
}

