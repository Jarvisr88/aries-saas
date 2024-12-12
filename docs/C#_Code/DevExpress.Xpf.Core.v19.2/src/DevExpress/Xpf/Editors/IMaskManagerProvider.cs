namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Mask;
    using System;

    public interface IMaskManagerProvider
    {
        MaskManager CreateNew();
        void LocalEditActionPerformed();
        void SetMaskManagerValue(object editValue);
        void UpdateRequired();

        WpfMaskManager Instance { get; }
    }
}

