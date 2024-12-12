namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public interface ITokenStyleSettings
    {
        bool GetFilterOutSelectedTokens();
        bool IsTokenStyleSettings();

        bool? EnableTokenWrapping { get; }

        bool? AllowEditTokens { get; }

        ControlTemplate TokenBorderTemplate { get; }

        bool? ShowTokenButtons { get; }

        DevExpress.Xpf.Editors.NewTokenPosition? NewTokenPosition { get; }

        TextTrimming? TokenTextTrimming { get; }

        double? TokenMaxWidth { get; }

        Style TokenStyle { get; }

        string NewTokenText { get; }
    }
}

