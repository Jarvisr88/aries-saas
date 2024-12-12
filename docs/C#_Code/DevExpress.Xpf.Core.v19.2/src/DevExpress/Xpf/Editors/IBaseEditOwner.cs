namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;

    public interface IBaseEditOwner
    {
        HorizontalAlignment DefaultHorizontalAlignment { get; }

        bool HasTextDecorations { get; }

        bool? AllowDefaultButton { get; }

        IDisplayTextProvider DisplayTextProvider { get; }
    }
}

