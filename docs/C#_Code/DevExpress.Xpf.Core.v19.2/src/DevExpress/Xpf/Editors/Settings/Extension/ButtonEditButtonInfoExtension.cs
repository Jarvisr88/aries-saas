namespace DevExpress.Xpf.Editors.Settings.Extension
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Markup;

    public class ButtonEditButtonInfoExtension : MarkupExtension
    {
        public ButtonEditButtonInfoExtension()
        {
            this.ClickMode = (System.Windows.Controls.ClickMode) ButtonBase.ClickModeProperty.DefaultMetadata.DefaultValue;
            this.CommandParameter = ButtonInfo.CommandParameterProperty.DefaultMetadata.DefaultValue;
            this.Command = (ICommand) ButtonInfo.CommandProperty.DefaultMetadata.DefaultValue;
            this.CommandTarget = (IInputElement) ButtonInfo.CommandTargetProperty.DefaultMetadata.DefaultValue;
            this.ButtonKind = (GlyphKind) ButtonInfo.GlyphKindProperty.DefaultMetadata.DefaultValue;
            this.IsDefaultButton = (bool) ButtonInfoBase.IsDefaultButtonProperty.DefaultMetadata.DefaultValue;
            this.Content = ButtonInfo.ContentProperty.DefaultMetadata.DefaultValue;
            this.Template = (ControlTemplate) ButtonInfoBase.TemplateProperty.DefaultMetadata.DefaultValue;
            this.ContentTemplate = (DataTemplate) ButtonInfo.ContentTemplateProperty.DefaultMetadata.DefaultValue;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            new ButtonInfo { 
                ClickMode = this.ClickMode,
                CommandParameter = this.CommandParameter,
                Command = this.Command,
                CommandTarget = this.CommandTarget,
                IsDefaultButton = this.IsDefaultButton,
                Content = this.Content,
                ContentTemplate = this.ContentTemplate
            };

        public System.Windows.Controls.ClickMode ClickMode { get; set; }

        public object CommandParameter { get; set; }

        public ICommand Command { get; set; }

        public IInputElement CommandTarget { get; set; }

        public GlyphKind ButtonKind { get; set; }

        public bool IsDefaultButton { get; set; }

        public object Content { get; set; }

        public System.Windows.Style Style { get; set; }

        public ControlTemplate Template { get; set; }

        public DataTemplate ContentTemplate { get; set; }
    }
}

