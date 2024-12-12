namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Automation;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Input;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider)), DXToolboxBrowsable(DXToolboxItemKind.Free)]
    public class MemoEdit : PopupBaseEdit
    {
        public static readonly DependencyProperty MemoTextWrappingProperty;
        public static readonly DependencyProperty MemoHorizontalScrollBarVisibilityProperty;
        public static readonly DependencyProperty MemoVerticalScrollBarVisibilityProperty;
        public static readonly DependencyProperty MemoAcceptsReturnProperty;
        public static readonly DependencyProperty MemoAcceptsTabProperty;
        public static readonly DependencyProperty ShowIconProperty;
        protected internal const double popupMinHeightConstant = 100.0;
        private TextEdit memo;

        static MemoEdit()
        {
            Type ownerType = typeof(MemoEdit);
            MemoTextWrappingProperty = DependencyPropertyManager.Register("MemoTextWrapping", typeof(TextWrapping), ownerType, new FrameworkPropertyMetadata(TextWrapping.NoWrap));
            MemoHorizontalScrollBarVisibilityProperty = DependencyPropertyManager.Register("MemoHorizontalScrollBarVisibility", typeof(ScrollBarVisibility), ownerType, new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
            MemoVerticalScrollBarVisibilityProperty = DependencyPropertyManager.Register("MemoVerticalScrollBarVisibility", typeof(ScrollBarVisibility), ownerType, new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
            MemoAcceptsReturnProperty = DependencyPropertyManager.Register("MemoAcceptsReturn", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ShowIconProperty = DependencyPropertyManager.Register("ShowIcon", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            MemoAcceptsTabProperty = DependencyPropertyManager.Register("MemoAcceptsTab", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            PopupBaseEdit.PopupFooterButtonsProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(PopupFooterButtons.OkCancel));
            PopupBaseEdit.ShowSizeGripProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(true));
            PopupBaseEdit.PopupMinHeightProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(100.0));
        }

        public MemoEdit()
        {
            this.SetDefaultStyleKey(typeof(MemoEdit));
        }

        protected override void AcceptPopupValue()
        {
            base.AcceptPopupValue();
            if (!base.IsReadOnly)
            {
                this.SyncWithMemo();
            }
        }

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new MemoEditPropertyProvider(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new MemoEditStrategy(this);

        protected TextEdit GetMemo()
        {
            Predicate<FrameworkElement> predicate = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__21_0;
                predicate = <>c.<>9__21_0 = element => (element is TextEdit) && (element.Name == "PART_PopupContent");
            }
            return (TextEdit) LayoutHelper.FindElement(base.Popup.Child as FrameworkElement, predicate);
        }

        protected override bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers) => 
            (key == Key.Return) && ((!ModifierKeysHelper.IsCtrlPressed(modifiers) || !this.MemoAcceptsReturn) ? (ModifierKeysHelper.NoModifiers(modifiers) && !this.MemoAcceptsReturn) : true);

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new MemoEditAutomationPeer(this);

        protected override void OnPopupClosed()
        {
            base.OnPopupClosed();
            this.Memo = null;
        }

        protected override void OnPopupOpened()
        {
            this.Memo = this.GetMemo();
            base.OnPopupOpened();
            this.EditStrategy.OnPopupOpened();
        }

        private void SyncMemoWithEditor(TextEdit memo)
        {
            this.EditStrategy.SyncMemoWithEditor(memo);
        }

        private void SyncWithMemo()
        {
            this.EditStrategy.SyncWithMemo();
        }

        protected internal void UpdateOkButtonIsEnabled(bool isTextModified)
        {
            base.PropertyProvider.PopupViewModel.OkButtonIsEnabled = isTextModified;
        }

        public override FrameworkElement PopupElement =>
            this.Memo?.EditCore;

        protected internal override Type StyleSettingsType =>
            typeof(MemoEditStyleSettings);

        protected internal TextEdit Memo
        {
            get => 
                this.memo;
            set
            {
                if (!ReferenceEquals(value, this.memo))
                {
                    this.memo = value;
                    if (this.memo != null)
                    {
                        this.SyncMemoWithEditor(this.memo);
                    }
                }
            }
        }

        protected MemoEditStrategy EditStrategy
        {
            get => 
                base.EditStrategy as MemoEditStrategy;
            set => 
                base.EditStrategy = value;
        }

        [Category("Behavior"), Description("Gets or sets whether the text is wrapped when it reaches the edge of the memo box. This is a dependency property.")]
        public TextWrapping MemoTextWrapping
        {
            get => 
                (TextWrapping) base.GetValue(MemoTextWrappingProperty);
            set => 
                base.SetValue(MemoTextWrappingProperty, value);
        }

        [Description("Gets or sets whether an end-user can insert return characters into a text. This is a dependency property."), Category("Behavior")]
        public bool MemoAcceptsReturn
        {
            get => 
                (bool) base.GetValue(MemoAcceptsReturnProperty);
            set => 
                base.SetValue(MemoAcceptsReturnProperty, value);
        }

        [Description("Gets or sets whether an end-user can insert tabulation characters into a text. This is a dependency property."), Category("Behavior")]
        public bool MemoAcceptsTab
        {
            get => 
                (bool) base.GetValue(MemoAcceptsTabProperty);
            set => 
                base.SetValue(MemoAcceptsTabProperty, value);
        }

        [Description("Gets or sets whether a horizontal scroll bar is shown. This is a dependency property."), Category("Behavior")]
        public ScrollBarVisibility MemoHorizontalScrollBarVisibility
        {
            get => 
                (ScrollBarVisibility) base.GetValue(MemoHorizontalScrollBarVisibilityProperty);
            set => 
                base.SetValue(MemoHorizontalScrollBarVisibilityProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether a vertical scroll bar is shown. This is a dependency property.")]
        public ScrollBarVisibility MemoVerticalScrollBarVisibility
        {
            get => 
                (ScrollBarVisibility) base.GetValue(MemoVerticalScrollBarVisibilityProperty);
            set => 
                base.SetValue(MemoVerticalScrollBarVisibilityProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether an identification icon is displayed within the editor's edit region.")]
        public bool ShowIcon
        {
            get => 
                (bool) base.GetValue(ShowIconProperty);
            set => 
                base.SetValue(ShowIconProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly MemoEdit.<>c <>9 = new MemoEdit.<>c();
            public static Predicate<FrameworkElement> <>9__21_0;

            internal bool <GetMemo>b__21_0(FrameworkElement element) => 
                (element is TextEdit) && (element.Name == "PART_PopupContent");
        }
    }
}

