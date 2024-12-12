namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Specialized;
    using System.Windows;

    public interface IFormatConditionCollectionOwner
    {
        void OnFormatConditionCollectionChanged(FormatConditionChangeType changeType);
        void SyncFormatConditionCollectionWithDetails(NotifyCollectionChangedEventArgs e);
        void SyncFormatConditionPropertyWithDetails(FormatConditionBase item, DependencyPropertyChangedEventArgs e);

        FormatInfoCollection PredefinedIconSetFormats { get; }
    }
}

