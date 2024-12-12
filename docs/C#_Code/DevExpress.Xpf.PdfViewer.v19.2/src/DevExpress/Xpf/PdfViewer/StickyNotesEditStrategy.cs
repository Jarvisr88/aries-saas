namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class StickyNotesEditStrategy : EditStrategyBase
    {
        public StickyNotesEditStrategy(StickyNotesEdit editor) : base(editor)
        {
        }

        private SuperTipControl CreateSuperTipControl()
        {
            SuperTip superTip = new SuperTip();
            if (!string.IsNullOrEmpty(this.Editor.Title))
            {
                SuperTipHeaderItem item1 = new SuperTipHeaderItem();
                item1.Content = this.Editor.Title;
                SuperTipHeaderItem item2 = item1;
                superTip.Items.Add(item2);
            }
            SuperTipItem item3 = new SuperTipItem();
            item3.Content = this.Editor.EditValue;
            SuperTipItem item = item3;
            superTip.Items.Add(item);
            return new SuperTipControl(superTip);
        }

        public override void OnLoaded()
        {
            base.OnLoaded();
            ToolTip tip1 = new ToolTip();
            tip1.Content = this.CreateSuperTipControl();
            ToolTip tip = tip1;
            this.Editor.ToolTip = tip;
            tip.IsOpen = true;
        }

        protected override void OnUnloaded()
        {
            base.OnUnloaded();
            Action<ToolTip> action = <>c.<>9__4_0;
            if (<>c.<>9__4_0 == null)
            {
                Action<ToolTip> local1 = <>c.<>9__4_0;
                action = <>c.<>9__4_0 = x => x.IsOpen = false;
            }
            (this.Editor.ToolTip as ToolTip).Do<ToolTip>(action);
        }

        private StickyNotesEdit Editor =>
            (StickyNotesEdit) base.Editor;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StickyNotesEditStrategy.<>c <>9 = new StickyNotesEditStrategy.<>c();
            public static Action<ToolTip> <>9__4_0;

            internal void <OnUnloaded>b__4_0(ToolTip x)
            {
                x.IsOpen = false;
            }
        }
    }
}

