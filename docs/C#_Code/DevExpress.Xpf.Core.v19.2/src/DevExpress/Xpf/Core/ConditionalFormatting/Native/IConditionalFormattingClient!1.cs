namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using System;

    public interface IConditionalFormattingClient<T> : IConditionalFormattingClientBase where T: FrameworkElement, IConditionalFormattingClient<T>
    {
        FormatValueProvider? GetValueProvider(string fieldName);
        void UpdateBackground();
        void UpdateCustomAppearance(CustomAppearanceEventArgs args);
        void UpdateDataBarFormatInfo(DataBarFormatInfo info);

        ConditionalFormattingHelper<T> FormattingHelper { get; }

        bool IsSelected { get; }

        bool IsReady { get; }

        bool HasCustomAppearance { get; }

        DevExpress.Xpf.Core.Locker Locker { get; }
    }
}

