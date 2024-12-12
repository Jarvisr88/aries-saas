namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class ConditionalCellAppearanceUpdater : ConditionalClientAppearanceUpdaterBase
    {
        private readonly GridCellData cellData;

        public ConditionalCellAppearanceUpdater(GridCellData cellData)
        {
            if (cellData == null)
            {
                throw new ArgumentNullException("cellData");
            }
            this.cellData = cellData;
        }

        protected override bool CanAccessAnimationProvider() => 
            (this.Editor != null) && !this.Editor.IsEditorVisible;

        protected override IConditionalFormattingClientBase GetConditionalFormattingClient()
        {
            Func<CellEditorBase, IConditionalFormattingClientBase> evaluator = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<CellEditorBase, IConditionalFormattingClientBase> local1 = <>c.<>9__7_0;
                evaluator = <>c.<>9__7_0 = x => x.GetConditionalFormattingClient();
            }
            return this.Editor.With<CellEditorBase, IConditionalFormattingClientBase>(evaluator);
        }

        protected override VersionedFormatInfoProvider GetProvider()
        {
            Func<RowData, VersionedFormatInfoProvider> evaluator = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<RowData, VersionedFormatInfoProvider> local1 = <>c.<>9__11_0;
                evaluator = <>c.<>9__11_0 = x => x.FormatInfoProvider;
            }
            return this.CellData.RowData.With<RowData, VersionedFormatInfoProvider>(evaluator);
        }

        protected override DataViewBase GetView() => 
            this.CellData.View;

        protected override void StartAnimation(IList<IList<AnimationTimeline>> animations)
        {
            this.CellData.StartAnimation(animations);
        }

        protected override void UpdateClientAppearance()
        {
            if (this.Editor != null)
            {
                this.Editor.OnDataChanged();
            }
        }

        public GridCellData CellData =>
            this.cellData;

        private CellEditorBase Editor =>
            this.CellData.Editor;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ConditionalCellAppearanceUpdater.<>c <>9 = new ConditionalCellAppearanceUpdater.<>c();
            public static Func<CellEditorBase, IConditionalFormattingClientBase> <>9__7_0;
            public static Func<RowData, VersionedFormatInfoProvider> <>9__11_0;

            internal IConditionalFormattingClientBase <GetConditionalFormattingClient>b__7_0(CellEditorBase x) => 
                x.GetConditionalFormattingClient();

            internal VersionedFormatInfoProvider <GetProvider>b__11_0(RowData x) => 
                x.FormatInfoProvider;
        }
    }
}

