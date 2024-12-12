namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Markup;

    public class DXFrameworkContentElement : FrameworkContentElement
    {
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsSealed =>
            base.IsSealed;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Input.Cursor Cursor
        {
            get => 
                base.Cursor;
            set => 
                base.Cursor = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Input.InputScope InputScope
        {
            get => 
                base.InputScope;
            set => 
                base.InputScope = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowDrop
        {
            get => 
                base.AllowDrop;
            set => 
                base.AllowDrop = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Windows.Data.BindingGroup BindingGroup
        {
            get => 
                base.BindingGroup;
            set => 
                base.BindingGroup = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public System.Windows.Controls.ContextMenu ContextMenu
        {
            get => 
                base.ContextMenu;
            set => 
                base.ContextMenu = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool Focusable
        {
            get => 
                base.Focusable;
            set => 
                base.Focusable = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Style FocusVisualStyle
        {
            get => 
                base.FocusVisualStyle;
            set => 
                base.FocusVisualStyle = value;
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ForceCursor
        {
            get => 
                base.ForceCursor;
            set => 
                base.ForceCursor = value;
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEnabled
        {
            get => 
                base.IsEnabled;
            set => 
                base.IsEnabled = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool OverridesDefaultStyle
        {
            get => 
                base.OverridesDefaultStyle;
            set => 
                base.OverridesDefaultStyle = value;
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ResourceDictionary Resources =>
            base.Resources;

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object ToolTip
        {
            get => 
                base.ToolTip;
            set => 
                base.ToolTip = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public CommandBindingCollection CommandBindings =>
            base.CommandBindings;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasAnimatedProperties =>
            base.HasAnimatedProperties;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public InputBindingCollection InputBindings =>
            base.InputBindings;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsFocused =>
            base.IsFocused;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsInputMethodEnabled =>
            base.IsInputMethodEnabled;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsKeyboardFocused =>
            base.IsKeyboardFocused;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsKeyboardFocusWithin =>
            base.IsKeyboardFocusWithin;

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsMouseCaptured =>
            base.IsMouseCaptured;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool IsMouseCaptureWithin =>
            base.IsMouseCaptureWithin;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsMouseDirectlyOver =>
            base.IsMouseDirectlyOver;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsMouseOver =>
            base.IsMouseOver;

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsStylusCaptured =>
            base.IsStylusCaptured;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStylusCaptureWithin =>
            base.IsStylusCaptureWithin;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStylusDirectlyOver =>
            base.IsStylusDirectlyOver;

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsStylusOver =>
            base.IsFocused;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public XmlLanguage Language =>
            base.Language;
    }
}

