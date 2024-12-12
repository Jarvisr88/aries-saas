namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Windows;

    public sealed class DPValueStorage<T> : IDPValueStorage
    {
        private bool isInitialized;
        private T value;

        void IDPValueStorage.SetValue(object v)
        {
            this.value = (T) v;
            this.isInitialized = true;
        }

        public T GetValue(DependencyObject o, DependencyProperty p)
        {
            if (!this.isInitialized)
            {
                this.value = (T) o.GetValue(p);
                this.isInitialized = true;
            }
            return this.value;
        }
    }
}

