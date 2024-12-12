namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using System;

    public interface ISupportManager
    {
        BaseEditUnit CreateEditUnit();

        bool AllowUserCustomization { get; }
    }
}

