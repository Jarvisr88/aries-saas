namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    internal class ConditionalRowAppearanceUpdater : ConditionalClientAppearanceUpdaterBase
    {
        private readonly DevExpress.Xpf.Grid.RowData rowData;

        public ConditionalRowAppearanceUpdater(DevExpress.Xpf.Grid.RowData rowData)
        {
            if (rowData == null)
            {
                throw new ArgumentNullException("rowData");
            }
            this.rowData = rowData;
        }

        protected override bool CanAccessAnimationProvider() => 
            !this.RowData.IsDirty;

        protected override IConditionalFormattingClientBase GetConditionalFormattingClient() => 
            this.RowData.GetConditionalFormattingClient();

        protected override VersionedFormatInfoProvider GetProvider() => 
            this.RowData.FormatInfoProvider;

        protected override DataViewBase GetView() => 
            this.RowData.View;

        protected override void StartAnimation(IList<IList<AnimationTimeline>> animations)
        {
            this.RowData.StartAnimation(animations);
        }

        protected override void UpdateClientAppearance()
        {
            this.RowData.UpdateClientAppearance();
        }

        public DevExpress.Xpf.Grid.RowData RowData =>
            this.rowData;
    }
}

