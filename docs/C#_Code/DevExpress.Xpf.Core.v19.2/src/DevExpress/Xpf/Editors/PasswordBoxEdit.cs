namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.EditStrategy;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    [DXToolboxBrowsable(DXToolboxItemKind.Free), LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class PasswordBoxEdit : TextEditBase
    {
        internal static readonly DependencyPropertyKey PasswordStrengthPropertyKey;
        public static readonly DependencyProperty PasswordStrengthProperty;
        public static readonly DependencyProperty PasswordCharProperty;
        public static readonly DependencyProperty PasswordProperty;
        public static readonly RoutedEvent CustomPasswordStrengthEvent;
        public static readonly DependencyProperty ShowCapsLockWarningToolTipProperty;
        public static readonly DependencyProperty CapsLockWarningToolTipTemplateProperty;
        private bool shouldHandle;

        public event CustomPasswordStrengthEventHandler CustomPasswordStrength
        {
            add
            {
                base.AddHandler(CustomPasswordStrengthEvent, value);
            }
            remove
            {
                base.RemoveHandler(CustomPasswordStrengthEvent, value);
            }
        }

        static PasswordBoxEdit()
        {
            Type ownerType = typeof(PasswordBoxEdit);
            PasswordCharProperty = DependencyPropertyManager.Register("PasswordChar", typeof(char), ownerType, new FrameworkPropertyMetadata('●', FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(PasswordBoxEdit.PasswordCharPropertyChanged)));
            PasswordProperty = DependencyPropertyManager.Register("Password", typeof(string), ownerType, new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(PasswordBoxEdit.PasswordPropertyChanged), new CoerceValueCallback(PasswordBoxEdit.CoercePasswordProperty)));
            PasswordStrengthPropertyKey = DependencyPropertyManager.RegisterReadOnly("PasswordStrength", typeof(DevExpress.Xpf.Editors.PasswordStrength), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.PasswordStrength.Weak));
            PasswordStrengthProperty = PasswordStrengthPropertyKey.DependencyProperty;
            CustomPasswordStrengthEvent = EventManager.RegisterRoutedEvent("CustomPasswordStrength", RoutingStrategy.Direct, typeof(CustomPasswordStrengthEventHandler), ownerType);
            ShowCapsLockWarningToolTipProperty = DependencyPropertyManager.Register("ShowCapsLockWarningToolTip", typeof(bool), ownerType, new FrameworkPropertyMetadata(false));
            CapsLockWarningToolTipTemplateProperty = DependencyPropertyManager.Register("CapsLockWarningToolTipTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null));
        }

        public PasswordBoxEdit()
        {
            this.SetDefaultStyleKey(typeof(PasswordBoxEdit));
        }

        private static object CoercePasswordProperty(DependencyObject d, object value) => 
            ((PasswordBoxEdit) d).EditStrategy.CoercePassword(value);

        protected override ActualPropertyProvider CreateActualPropertyProvider() => 
            new TextEditPropertyProvider(this);

        protected override EditBoxWrapper CreateEditBoxWrapper() => 
            new PasswordBoxWrapper(this);

        protected override EditStrategyBase CreateEditStrategy() => 
            new PasswordBoxEditStrategy(this);

        protected override string GetExportText() => 
            (base.DisplayText == null) ? string.Empty : new string(this.PasswordChar, base.DisplayText.Length);

        protected override void OnEditBoxMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            base.OnEditBoxMouseLeftButtonUp(sender, e);
            this.EditStrategy.MouseUp(e);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            e.Handled = this.shouldHandle;
        }

        private void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            this.EditStrategy.SyncWithEditor();
        }

        protected virtual void PasswordChanged(string oldValue, string value)
        {
            this.EditStrategy.PasswordChanged(oldValue, value);
        }

        protected virtual void PasswordCharChanged(char value)
        {
            this.EditStrategy.PasswordCharChanged(value);
        }

        private static void PasswordCharPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PasswordBoxEdit) d).PasswordCharChanged((char) e.NewValue);
        }

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((PasswordBoxEdit) d).PasswordChanged((string) e.OldValue, (string) e.NewValue);
        }

        protected override void ProcessFocusEditCore(MouseButtonEventArgs e)
        {
            this.shouldHandle = this.FocusEditCore();
        }

        protected override void SubscribeEditEventsCore()
        {
            base.SubscribeEditEventsCore();
            if (this.PasswordBox != null)
            {
                this.PasswordBox.AddHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnEditBoxMouseLeftButtonUp), true);
                this.PasswordBox.PasswordChanged += new RoutedEventHandler(this.PasswordBoxPasswordChanged);
            }
        }

        protected override void UnsubscribeEditEventsCore()
        {
            base.UnsubscribeEditEventsCore();
            if (this.PasswordBox != null)
            {
                this.PasswordBox.RemoveHandler(UIElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(this.OnEditBoxMouseLeftButtonUp));
                this.PasswordBox.PasswordChanged -= new RoutedEventHandler(this.PasswordBoxPasswordChanged);
            }
        }

        [Description("Gets the password strength. This is a dependency property.")]
        public DevExpress.Xpf.Editors.PasswordStrength PasswordStrength
        {
            get => 
                (DevExpress.Xpf.Editors.PasswordStrength) base.GetValue(PasswordStrengthProperty);
            internal set => 
                base.SetValue(PasswordStrengthPropertyKey, value);
        }

        [Description("Gets or sets whether to display a warning tooltip when the Caps Lock is turned on. This is a dependency property.")]
        public bool ShowCapsLockWarningToolTip
        {
            get => 
                (bool) base.GetValue(ShowCapsLockWarningToolTipProperty);
            set => 
                base.SetValue(ShowCapsLockWarningToolTipProperty, value);
        }

        [Description("Gets or sets the template used to represent a warning tooltip displayed when the Caps Lock is turned on. This is a dependency property.")]
        public DataTemplate CapsLockWarningToolTipTemplate
        {
            get => 
                (DataTemplate) base.GetValue(CapsLockWarningToolTipTemplateProperty);
            set => 
                base.SetValue(CapsLockWarningToolTipTemplateProperty, value);
        }

        [Description("Gets or sets the masking character for the password. This is a dependency property.")]
        public char PasswordChar
        {
            get => 
                (char) base.GetValue(PasswordCharProperty);
            set => 
                base.SetValue(PasswordCharProperty, value);
        }

        [Description("Gets or sets the current password. This is a dependency property.")]
        public string Password
        {
            get => 
                (string) base.GetValue(PasswordProperty);
            set => 
                base.SetValue(PasswordProperty, value);
        }

        internal System.Windows.Controls.PasswordBox PasswordBox =>
            base.EditCore as System.Windows.Controls.PasswordBox;

        protected PasswordBoxEditStrategy EditStrategy =>
            (PasswordBoxEditStrategy) base.EditStrategy;
    }
}

