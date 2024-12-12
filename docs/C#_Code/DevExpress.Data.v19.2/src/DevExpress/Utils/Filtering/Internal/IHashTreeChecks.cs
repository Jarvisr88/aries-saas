namespace DevExpress.Utils.Filtering.Internal
{
    using System;

    public interface IHashTreeChecks
    {
        void Initialize(ICheckedGroup value);
        void Initialize(object[] level, int key);
        bool Invert();
        bool? IsChecked(int key, int valueKey);
        bool Reset();
        void Toggle(int key, int valueKey);
        bool ToggleAll();
    }
}

