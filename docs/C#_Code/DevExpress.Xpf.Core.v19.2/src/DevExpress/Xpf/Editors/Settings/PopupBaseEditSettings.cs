namespace DevExpress.Xpf.Editors.Settings
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class PopupBaseEditSettings : ButtonEditSettings, IPopupContentOwner
    {
        public static readonly DependencyProperty ShowPopupIfReadOnlyProperty;
        public static readonly DependencyProperty PopupWidthProperty;
        public static readonly DependencyProperty PopupHeightProperty;
        public static readonly DependencyProperty PopupMaxWidthProperty;
        public static readonly DependencyProperty PopupMaxHeightProperty;
        public static readonly DependencyProperty PopupMinWidthProperty;
        public static readonly DependencyProperty PopupMinHeightProperty;
        public static readonly DependencyProperty PopupFooterButtonsProperty;
        public static readonly DependencyProperty ShowSizeGripProperty;
        public static readonly DependencyProperty PopupContentTemplateProperty;
        public static readonly DependencyProperty IsSharedPopupSizeProperty;
        public static readonly DependencyProperty PopupTopAreaTemplateProperty;
        public static readonly DependencyProperty PopupBottomAreaTemplateProperty;
        private FrameworkElement child;

        static PopupBaseEditSettings()
        {
            Type ownerType = typeof(PopupBaseEditSettings);
            ShowPopupIfReadOnlyProperty = DependencyPropertyManager.Register("ShowPopupIfReadOnly", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            PopupWidthProperty = DependencyPropertyManager.Register("PopupWidth", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0));
            PopupHeightProperty = DependencyPropertyManager.Register("PopupHeight", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0));
            PopupMaxWidthProperty = DependencyPropertyManager.Register("PopupMaxWidth", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0));
            PopupMaxHeightProperty = DependencyPropertyManager.Register("PopupMaxHeight", typeof(double), ownerType, new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0));
            PopupMinWidthProperty = DependencyPropertyManager.Register("PopupMinWidth", typeof(double), ownerType, new FrameworkPropertyMetadata(17.0));
            PopupMinHeightProperty = DependencyPropertyManager.Register("PopupMinHeight", typeof(double), ownerType, new FrameworkPropertyMetadata(35.0));
            PopupFooterButtonsProperty = DependencyPropertyManager.Register("PopupFooterButtons", typeof(DevExpress.Xpf.Editors.PopupFooterButtons?), ownerType, new FrameworkPropertyMetadata(null));
            ShowSizeGripProperty = DependencyPropertyManager.Register("ShowSizeGrip", typeof(bool?), ownerType, new FrameworkPropertyMetadata(null));
            PopupContentTemplateProperty = DependencyPropertyManager.Register("PopupContentTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
            IsSharedPopupSizeProperty = DependencyPropertyManager.Register("IsSharedPopupSize", typeof(bool), ownerType, new FrameworkPropertyMetadata(true));
            PopupTopAreaTemplateProperty = DependencyPropertyManager.Register("PopupTopAreaTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
            PopupBottomAreaTemplateProperty = DependencyPropertyManager.Register("PopupBottomAreaTemplate", typeof(ControlTemplate), ownerType, new FrameworkPropertyMetadata(null));
        }

        protected override void AssignToEditCore(IBaseEdit edit)
        {
            base.AssignToEditCore(edit);
            PopupBaseEdit pbe = edit as PopupBaseEdit;
            if (pbe != null)
            {
                base.SetValueFromSettings(ShowPopupIfReadOnlyProperty, () => pbe.ShowPopupIfReadOnly = this.ShowPopupIfReadOnly);
                base.SetValueFromSettings(PopupWidthProperty, () => pbe.PopupWidth = this.PopupWidth);
                base.SetValueFromSettings(PopupHeightProperty, () => pbe.PopupHeight = this.PopupHeight);
                base.SetValueFromSettings(PopupMinWidthProperty, () => pbe.PopupMinWidth = this.PopupMinWidth);
                base.SetValueFromSettings(PopupMinHeightProperty, () => pbe.PopupMinHeight = this.PopupMinHeight);
                base.SetValueFromSettings(PopupMaxWidthProperty, () => pbe.PopupMaxWidth = this.PopupMaxWidth);
                base.SetValueFromSettings(PopupMaxHeightProperty, () => pbe.PopupMaxHeight = this.PopupMaxHeight);
                base.SetValueFromSettings(PopupFooterButtonsProperty, () => pbe.PopupFooterButtons = this.PopupFooterButtons);
                base.SetValueFromSettings(ShowSizeGripProperty, () => pbe.ShowSizeGrip = this.ShowSizeGrip);
                base.SetValueFromSettings(PopupContentTemplateProperty, () => pbe.PopupContentTemplate = this.PopupContentTemplate, () => this.ClearEditorPropertyIfNeeded(pbe, PopupBaseEdit.PopupContentTemplateProperty, PopupContentTemplateProperty));
                base.SetValueFromSettings(PopupTopAreaTemplateProperty, () => pbe.PopupTopAreaTemplate = this.PopupTopAreaTemplate, () => this.ClearEditorPropertyIfNeeded(pbe, PopupBaseEdit.PopupTopAreaTemplateProperty, PopupTopAreaTemplateProperty));
                base.SetValueFromSettings(PopupBottomAreaTemplateProperty, () => pbe.PopupBottomAreaTemplate = this.PopupBottomAreaTemplate, () => this.ClearEditorPropertyIfNeeded(pbe, PopupBaseEdit.PopupBottomAreaTemplateProperty, PopupBottomAreaTemplateProperty));
            }
        }

        protected internal override ButtonInfoBase CreateDefaultButtonInfo()
        {
            ButtonInfo info1 = new ButtonInfo();
            info1.GlyphKind = GlyphKind.DropDown;
            info1.ButtonKind = ButtonKind.Toggle;
            info1.IsDefaultButton = true;
            return info1;
        }

        protected internal override bool IsActivatingKey(Key key, ModifierKeys modifiers) => 
            !this.IsTogglePopupOpenGesture(key, modifiers) ? base.IsActivatingKey(key, modifiers) : true;

        protected internal virtual bool IsTogglePopupOpenGesture(Key key, ModifierKeys modifiers) => 
            ((key != Key.Down) || !ModifierKeysHelper.IsAltPressed(modifiers)) ? ((key == Key.F4) && ModifierKeysHelper.NoModifiers(modifiers)) : true;

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if ((e.Property.Name != "PopupHeight") && (e.Property.Name != "PopupWidth"))
            {
                base.OnPropertyChanged(e);
            }
        }

        protected internal bool IsValueChangedViaPopup { get; set; }

        public bool ShowPopupIfReadOnly
        {
            get => 
                (bool) base.GetValue(ShowPopupIfReadOnlyProperty);
            set => 
                base.SetValue(ShowPopupIfReadOnlyProperty, value);
        }

        [Category("Behavior"), Description("Gets or sets whether the dropdown's size is shared between multiple popup editors created from the PopupBaseEditSettings class. This is a dependency property."), SkipPropertyAssertion]
        public bool IsSharedPopupSize
        {
            get => 
                (bool) base.GetValue(IsSharedPopupSizeProperty);
            set => 
                base.SetValue(IsSharedPopupSizeProperty, value);
        }

        [Category("Layout"), Description("Gets or sets the popup window's width.")]
        public double PopupWidth
        {
            get => 
                (double) base.GetValue(PopupWidthProperty);
            set => 
                base.SetValue(PopupWidthProperty, value);
        }

        [Category("Layout"), Description("Gets or sets the popup window's height.")]
        public double PopupHeight
        {
            get => 
                (double) base.GetValue(PopupHeightProperty);
            set => 
                base.SetValue(PopupHeightProperty, value);
        }

        [Category("Layout"), Description("Gets or sets the popup window's minimum width.")]
        public double PopupMinWidth
        {
            get => 
                (double) base.GetValue(PopupMinWidthProperty);
            set => 
                base.SetValue(PopupMinWidthProperty, value);
        }

        [Category("Layout"), Description("Gets or sets the popup window's minimum height.")]
        public double PopupMinHeight
        {
            get => 
                (double) base.GetValue(PopupMinHeightProperty);
            set => 
                base.SetValue(PopupMinHeightProperty, value);
        }

        [Category("Layout"), Description("Gets or sets the popup window's maximum width.")]
        public double PopupMaxWidth
        {
            get => 
                (double) base.GetValue(PopupMaxWidthProperty);
            set => 
                base.SetValue(PopupMaxWidthProperty, value);
        }

        [Category("Layout"), Description("Gets or sets the popup window's maximum height.")]
        public double PopupMaxHeight
        {
            get => 
                (double) base.GetValue(PopupMaxHeightProperty);
            set => 
                base.SetValue(PopupMaxHeightProperty, value);
        }

        [Description("Gets or sets which buttons are displayed within the editor's drop-down. This is a dependency property."), Category("Behavior")]
        public DevExpress.Xpf.Editors.PopupFooterButtons? PopupFooterButtons
        {
            get => 
                (DevExpress.Xpf.Editors.PopupFooterButtons?) base.GetValue(PopupFooterButtonsProperty);
            set => 
                base.SetValue(PopupFooterButtonsProperty, value);
        }

        [Category("Behavior"), TypeConverter(typeof(NullableBoolConverter)), Description("Gets or sets whether to show the size grip within the editor's drop-down.")]
        public bool? ShowSizeGrip
        {
            get => 
                (bool?) base.GetValue(ShowSizeGripProperty);
            set => 
                base.SetValue(ShowSizeGripProperty, value);
        }

        [SkipPropertyAssertion, Category("Appearance "), Description("Gets or sets a template that presents the popup window's content.")]
        public ControlTemplate PopupContentTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PopupContentTemplateProperty);
            set => 
                base.SetValue(PopupContentTemplateProperty, value);
        }

        [Description(""), Category("Behavior")]
        public ControlTemplate PopupTopAreaTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PopupTopAreaTemplateProperty);
            set => 
                base.SetValue(PopupTopAreaTemplateProperty, value);
        }

        [Description(""), Category("Behavior")]
        public ControlTemplate PopupBottomAreaTemplate
        {
            get => 
                (ControlTemplate) base.GetValue(PopupBottomAreaTemplateProperty);
            set => 
                base.SetValue(PopupBottomAreaTemplateProperty, value);
        }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                List<object> list = new List<object>();
                IEnumerator logicalChildren = base.LogicalChildren;
                if (logicalChildren != null)
                {
                    while (logicalChildren.MoveNext())
                    {
                        list.Add(logicalChildren.Current);
                    }
                }
                if (this.child != null)
                {
                    list.Add(this.child);
                }
                return list.GetEnumerator();
            }
        }

        FrameworkElement IPopupContentOwner.Child
        {
            get => 
                this.child;
            set
            {
                if (!ReferenceEquals(value, this.child))
                {
                    base.RemoveLogicalChild(this.child);
                    this.child = value;
                    base.AddLogicalChild(this.child);
                }
            }
        }

        protected internal virtual bool RestrictPopupHeight =>
            false;
    }
}

