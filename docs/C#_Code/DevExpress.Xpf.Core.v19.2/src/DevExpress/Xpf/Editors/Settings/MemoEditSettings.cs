namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public class MemoEditSettings : PopupBaseEditSettings
    {
        public static readonly DependencyProperty MemoTextWrappingProperty;
        public static readonly DependencyProperty MemoHorizontalScrollBarVisibilityProperty;
        public static readonly DependencyProperty MemoVerticalScrollBarVisibilityProperty;
        public static readonly DependencyProperty MemoAcceptsReturnProperty;
        public static readonly DependencyProperty MemoAcceptsTabProperty;
        public static readonly DependencyProperty ShowIconProperty;

        static MemoEditSettings()
        {
            Type ownerType = typeof(MemoEditSettings);
            MemoTextWrappingProperty = DependencyPropertyManager.Register("MemoTextWrapping", typeof(TextWrapping), ownerType, new FrameworkPropertyMetadata(TextWrapping.NoWrap));
            MemoHorizontalScrollBarVisibilityProperty = DependencyPropertyManager.Register("MemoHorizontalScrollBarVisibility", typeof(ScrollBarVisibility), ownerType, new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
            MemoVerticalScrollBarVisibilityProperty = DependencyPropertyManager.Register("MemoVerticalScrollBarVisibility", typeof(ScrollBarVisibility), ownerType, new FrameworkPropertyMetadata(ScrollBarVisibility.Hidden));
            MemoAcceptsReturnProperty = DependencyPropertyManager.Register("MemoAcceptsReturn", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ShowIconProperty = DependencyPropertyManager.Register("ShowIcon", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            MemoAcceptsTabProperty = DependencyPropertyManager.Register("MemoAcceptsTab", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            ButtonEditSettings.IsTextEditableProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(null));
            PopupBaseEditSettings.PopupFooterButtonsProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(PopupFooterButtons.OkCancel));
            PopupBaseEditSettings.ShowSizeGripProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(true));
            PopupBaseEditSettings.PopupMinHeightProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(100.0));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            MemoEdit editor = edit as MemoEdit;
            if (editor != null)
            {
                base.SetValueFromSettings(MemoTextWrappingProperty, () => editor.MemoTextWrapping = this.MemoTextWrapping);
                base.SetValueFromSettings(MemoAcceptsReturnProperty, () => editor.MemoAcceptsReturn = this.MemoAcceptsReturn);
                base.SetValueFromSettings(MemoHorizontalScrollBarVisibilityProperty, () => editor.MemoHorizontalScrollBarVisibility = this.MemoHorizontalScrollBarVisibility);
                base.SetValueFromSettings(MemoVerticalScrollBarVisibilityProperty, () => editor.MemoVerticalScrollBarVisibility = this.MemoVerticalScrollBarVisibility);
                base.SetValueFromSettings(ShowIconProperty, () => editor.ShowIcon = this.ShowIcon);
                base.SetValueFromSettings(MemoAcceptsTabProperty, () => editor.MemoAcceptsTab = this.MemoAcceptsTab);
            }
        }

        [Description("Gets or sets whether the text is wrapped when it reaches the edge of the text box. This is a dependency property."), Category("Behavior")]
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

        [Category("Behavior"), Description("Gets or sets a value specifying whether an identification icon is displayed within the editor's edit region.")]
        public bool ShowIcon
        {
            get => 
                (bool) base.GetValue(ShowIconProperty);
            set => 
                base.SetValue(ShowIconProperty, value);
        }
    }
}

