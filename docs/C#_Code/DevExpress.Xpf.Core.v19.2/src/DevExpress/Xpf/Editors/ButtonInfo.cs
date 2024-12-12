namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Internal;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Content")]
    public class ButtonInfo : ButtonInfoBase, ICommandSource
    {
        internal const string TemplateChildName = "PART_Item";
        internal static readonly object click = new object();
        public static readonly DependencyProperty GlyphKindProperty;
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ContentTemplateProperty;
        public static readonly DependencyProperty IsCheckedProperty;
        public static readonly DependencyProperty CommandParameterProperty;
        public static readonly DependencyProperty CommandProperty;
        public static readonly DependencyProperty CommandTargetProperty;
        public static readonly DependencyProperty ContentRenderTemplateProperty;
        public static readonly DependencyProperty ButtonKindProperty;

        [Category("Behavior")]
        public event RoutedEventHandler Click
        {
            add
            {
                base.events.AddHandler(click, value);
            }
            remove
            {
                base.events.RemoveHandler(click, value);
            }
        }

        static ButtonInfo()
        {
            Type ownerType = typeof(ButtonInfo);
            GlyphKindProperty = DependencyPropertyManager.Register("GlyphKind", typeof(DevExpress.Xpf.Editors.GlyphKind), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.GlyphKind.None, (o, args) => ((ButtonInfo) o).GlyphKindChanged((DevExpress.Xpf.Editors.GlyphKind) args.NewValue)));
            ContentProperty = DependencyPropertyManager.Register("Content", typeof(object), ownerType, new PropertyMetadata(null, (o, args) => ((ButtonInfo) o).ContentChanged(args.NewValue)));
            ContentTemplateProperty = DependencyPropertyManager.Register("ContentTemplate", typeof(DataTemplate), ownerType, new PropertyMetadata(null, (o, args) => ((ButtonInfo) o).ContentTemplateChanged((DataTemplate) args.NewValue)));
            IsCheckedProperty = DependencyPropertyManager.Register("IsChecked", typeof(bool?), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, (o, args) => ((ButtonInfo) o).IsCheckedChanged((bool?) args.NewValue)));
            ButtonKindProperty = DependencyPropertyManager.Register("ButtonKind", typeof(DevExpress.Xpf.Editors.ButtonKind), ownerType, new PropertyMetadata(DevExpress.Xpf.Editors.ButtonKind.Simple, (o, args) => ((ButtonInfo) o).ButtonKindChanged((DevExpress.Xpf.Editors.ButtonKind) args.NewValue)));
            CommandProperty = DependencyPropertyManager.Register("Command", typeof(ICommand), ownerType, new PropertyMetadata(null, (o, args) => ((ButtonInfo) o).CommandChanged((ICommand) args.NewValue)));
            CommandParameterProperty = DependencyPropertyManager.Register("CommandParameter", typeof(object), ownerType, new PropertyMetadata(null, (o, args) => ((ButtonInfo) o).CommandParameterChanged(args.NewValue)));
            ContentRenderTemplateProperty = DependencyPropertyManager.Register("ContentRenderTemplate", typeof(RenderTemplate), ownerType, new FrameworkPropertyMetadata(null));
            CommandTargetProperty = DependencyPropertyManager.Register("CommandTarget", typeof(IInputElement), ownerType, new PropertyMetadata(null, (o, args) => ((ButtonInfo) o).CommandTargetChanged((IInputElement) args.NewValue)));
            FrameworkContentElement.DefaultStyleKeyProperty.OverrideMetadata(ownerType, new FrameworkPropertyMetadata(ownerType));
        }

        protected override void ActualMarginChanged(Thickness value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.Margin = new Thickness?(value);
            }
        }

        private void AssignPropertoes(DevExpress.Xpf.Core.Native.RenderButtonContext renderButtonContext)
        {
            if (renderButtonContext != null)
            {
                RenderTemplate template = new InplaceButtonInfoTemplateSelector().SelectTemplate(renderButtonContext.ElementHost.Parent, renderButtonContext, this);
                renderButtonContext.RenderContentTemplate = template;
                renderButtonContext.Content = this;
                renderButtonContext.ContentTemplate = this.ContentTemplate;
                renderButtonContext.Visibility = new Visibility?(base.Visibility);
                renderButtonContext.IsEnabled = base.IsEnabled;
                renderButtonContext.ClickMode = new ClickMode?(base.ClickMode);
                renderButtonContext.ButtonKind = new DevExpress.Xpf.Editors.ButtonKind?(this.ButtonKind);
                renderButtonContext.IsChecked = this.IsChecked;
                renderButtonContext.Command = this.Command;
                renderButtonContext.CommandParameter = this.CommandParameter;
                renderButtonContext.CommandTarget = this.CommandTarget;
                renderButtonContext.Foreground = base.Foreground;
                renderButtonContext.Margin = new Thickness?(base.ActualMargin);
            }
        }

        protected override void AssignToCloneInternal(ButtonInfoBase clone)
        {
            base.AssignToCloneInternal(clone);
            (clone as ButtonInfo).CommandTarget = this.CommandTarget;
        }

        private void ButtonKindChanged(DevExpress.Xpf.Editors.ButtonKind value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.ButtonKind = new DevExpress.Xpf.Editors.ButtonKind?(value);
            }
        }

        protected internal Rect CalcRenderBounds()
        {
            if (this.RenderButtonContext == null)
            {
                return Rect.Empty;
            }
            Transform transform = RenderTreeHelper.TransformToRoot(this.RenderButtonContext);
            Rect rect = new Rect(transform.Value.OffsetX, transform.Value.OffsetY, this.RenderButtonContext.RenderSize.Width, this.RenderButtonContext.RenderSize.Height);
            return new Rect(ScreenHelper.GetScreenPoint(this.RenderButtonContext.ElementHost.Parent, rect.Location), rect.Size);
        }

        protected override void ClickModeChanged(ClickMode value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.ClickMode = new ClickMode?(value);
            }
        }

        private void CommandChanged(ICommand value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.Command = value;
            }
        }

        private void CommandParameterChanged(object value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.CommandParameter = value;
            }
        }

        private void CommandTargetChanged(IInputElement value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.CommandTarget = value;
            }
        }

        private void ContentChanged(object value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.Content = value;
            }
        }

        private void ContentTemplateChanged(DataTemplate value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.ContentTemplate = value;
            }
        }

        protected override ButtonInfoBase CreateClone() => 
            new ButtonInfo();

        protected override List<DependencyProperty> CreateCloneProperties()
        {
            List<DependencyProperty> list = base.CreateCloneProperties();
            list.Add(GlyphKindProperty);
            list.Add(ButtonKindProperty);
            list.Add(ButtonInfoBase.TemplateProperty);
            list.Add(ContentProperty);
            list.Add(ContentTemplateProperty);
            list.Add(CommandProperty);
            list.Add(CommandParameterProperty);
            list.Add(ContentRenderTemplateProperty);
            list.Add(ToolTipService.ShowDurationProperty);
            list.Add(ToolTipService.InitialShowDelayProperty);
            return list;
        }

        protected internal override void FindContent(RenderContentControlContext templatedParent)
        {
            base.FindContent(templatedParent);
            this.UnsubscribeFromEvents(this.RenderButtonContext);
            this.RenderButtonContext = templatedParent as DevExpress.Xpf.Core.Native.RenderButtonContext;
            this.SubscribeToEvents(this.RenderButtonContext);
            this.AssignPropertoes(this.RenderButtonContext);
        }

        protected internal override void FindContent(FrameworkElement templatedParent)
        {
            if (base.Template != null)
            {
                ButtonBase base2 = base.Template.FindName("PART_Item", templatedParent) as ButtonBase;
                if ((base2 != null) && (base.Owner != null))
                {
                    if (base.IsDefaultButton)
                    {
                        ButtonEdit owner = base.Owner;
                        base2.Click += new RoutedEventHandler(owner.OnDefaultButtonClick);
                    }
                    else
                    {
                        base2.Click += new RoutedEventHandler(this.OnButtonClick);
                    }
                }
            }
        }

        protected override void ForegroundChanged(Brush value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.Foreground = value;
            }
        }

        protected internal override AutomationPeer GetRenderAutomationPeer() => 
            new ButtonInfoAutomationPeer(this);

        private void GlyphKindChanged(DevExpress.Xpf.Editors.GlyphKind value)
        {
            DevExpress.Xpf.Core.Native.RenderButtonContext renderButtonContext = this.RenderButtonContext;
            if (renderButtonContext != null)
            {
                renderButtonContext.RenderContentTemplate = new InplaceButtonInfoTemplateSelector().SelectTemplate(renderButtonContext.ElementHost.Parent, renderButtonContext, this);
            }
        }

        private void IsCheckedChanged(bool? value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.IsChecked = value;
            }
        }

        protected override bool IsCloneInternal(ButtonInfoBase clone) => 
            base.IsCloneInternal(clone) && ((base.events[click] == null) && ReferenceEquals(clone.events[click], null));

        protected virtual void NotifyOwnerCheckChanged()
        {
            if (base.IsDefaultButton && (base.Owner != null))
            {
                bool? isChecked = this.IsChecked;
                base.Owner.RenderCheckChanged((isChecked != null) ? isChecked.GetValueOrDefault() : false);
            }
        }

        protected virtual void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.ShouldRaiseClickEvent())
            {
                this.RaiseClickEvent(sender, e);
            }
        }

        protected override void OnIsEnabledChanged(bool value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.IsEnabled = value;
            }
        }

        protected internal virtual void OnRenderButtonClick(IFrameworkRenderElementContext sender, RenderEventArgsBase args)
        {
            if (base.RaiseClickEventInInplaceInactiveMode)
            {
                if (!base.IsDefaultButton)
                {
                    this.RaiseClickEvent(this, args.OriginalEventArgs as RoutedEventArgs);
                }
                else if (base.Owner != null)
                {
                    base.Owner.OnDefaultRenderButtonClick(args);
                }
                else if (sender != null)
                {
                    Func<InplaceBaseEdit, BaseEditSettings> evaluator = <>c.<>9__62_0;
                    if (<>c.<>9__62_0 == null)
                    {
                        Func<InplaceBaseEdit, BaseEditSettings> local1 = <>c.<>9__62_0;
                        evaluator = <>c.<>9__62_0 = x => x.Settings;
                    }
                    ButtonEditSettings settings = (((FrameworkRenderElementContext) sender).ElementHost.Parent as InplaceBaseEdit).With<InplaceBaseEdit, BaseEditSettings>(evaluator) as ButtonEditSettings;
                    if (settings != null)
                    {
                        settings.RaiseDefaultButtonClick(settings, args.OriginalEventArgs as RoutedEventArgs);
                    }
                }
            }
        }

        protected virtual void RaiseClickEvent(object sender, RoutedEventArgs e)
        {
            RoutedEventHandler handler = base.events[click] as RoutedEventHandler;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        protected virtual void RenderCheckChanged(object sender, EventArgs e)
        {
            if (base.RaiseClickEventInInplaceInactiveMode)
            {
                DevExpress.Xpf.Core.Native.RenderButtonContext context = (DevExpress.Xpf.Core.Native.RenderButtonContext) sender;
                this.IsChecked = context.IsChecked;
                this.NotifyOwnerCheckChanged();
            }
        }

        private bool ShouldRaiseClickEvent() => 
            (base.Owner == null) || !base.Owner.If<ButtonEdit>(x => ((x.EditMode == EditMode.InplaceInactive) && !base.RaiseClickEventInInplaceInactiveMode)).ReturnSuccess<ButtonEdit>();

        private void SubscribeToEvents(DevExpress.Xpf.Core.Native.RenderButtonContext buttonContext)
        {
            if (buttonContext != null)
            {
                buttonContext.Click += new RenderEventHandler(this.OnRenderButtonClick);
                buttonContext.CheckChanged += new EventHandler(this.RenderCheckChanged);
            }
        }

        private void UnsubscribeFromEvents(DevExpress.Xpf.Core.Native.RenderButtonContext buttonContext)
        {
            if (buttonContext != null)
            {
                buttonContext.Click -= new RenderEventHandler(this.OnRenderButtonClick);
                buttonContext.CheckChanged -= new EventHandler(this.RenderCheckChanged);
            }
        }

        protected override void VisibilityChanged(Visibility value)
        {
            if (this.RenderButtonContext != null)
            {
                this.RenderButtonContext.Visibility = new Visibility?(value);
            }
        }

        [Description("Gets or sets the type of the button's image. This is a dependency property."), Category("Appearance")]
        public DevExpress.Xpf.Editors.GlyphKind GlyphKind
        {
            get => 
                (DevExpress.Xpf.Editors.GlyphKind) base.GetValue(GlyphKindProperty);
            set => 
                base.SetValue(GlyphKindProperty, value);
        }

        [TypeConverter(typeof(ObjectConverter)), Category("Content"), Description("Gets or sets the button's content. This is a dependency property.")]
        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        [Description("Gets or sets the data template used to present the button's content. This is a dependency property."), Browsable(false)]
        public DataTemplate ContentTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContentTemplateProperty);
            set => 
                base.SetValue(ContentTemplateProperty, value);
        }

        [Category("Common Properties"), TypeConverter(typeof(NullableBoolConverter)), Description("Gets or sets whether the button is checked. This is a dependency property.")]
        public bool? IsChecked
        {
            get => 
                (bool?) base.GetValue(IsCheckedProperty);
            set => 
                base.SetValue(IsCheckedProperty, value);
        }

        [Description("Gets or sets a command associated with the button.This is a dependency property."), Category("Action"), Localizability(LocalizationCategory.NeverLocalize)]
        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        [Description("Gets or sets a parameter to pass to the ButtonInfo.Command property.This is a dependency property."), Category("Action"), Localizability(LocalizationCategory.NeverLocalize)]
        public object CommandParameter
        {
            get => 
                base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        [Category("Action"), Description("Gets or sets the element on which to execute the associated command.This is a dependency property.")]
        public IInputElement CommandTarget
        {
            get => 
                (IInputElement) base.GetValue(CommandTargetProperty);
            set => 
                base.SetValue(CommandTargetProperty, value);
        }

        [Description("Gets or sets the template that is used to render the button contents in optimized mode. This is a dependency property."), Browsable(false)]
        public RenderTemplate ContentRenderTemplate
        {
            get => 
                (RenderTemplate) base.GetValue(ContentRenderTemplateProperty);
            set => 
                base.SetValue(ContentRenderTemplateProperty, value);
        }

        [Description("Gets a value that specifies the button's behavior. This is a dependency property."), Category("Behavior")]
        public DevExpress.Xpf.Editors.ButtonKind ButtonKind
        {
            get => 
                (DevExpress.Xpf.Editors.ButtonKind) base.GetValue(ButtonKindProperty);
            set => 
                base.SetValue(ButtonKindProperty, value);
        }

        private DevExpress.Xpf.Core.Native.RenderButtonContext RenderButtonContext { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ButtonInfo.<>c <>9 = new ButtonInfo.<>c();
            public static Func<InplaceBaseEdit, BaseEditSettings> <>9__62_0;

            internal void <.cctor>b__11_0(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfo) o).GlyphKindChanged((GlyphKind) args.NewValue);
            }

            internal void <.cctor>b__11_1(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfo) o).ContentChanged(args.NewValue);
            }

            internal void <.cctor>b__11_2(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfo) o).ContentTemplateChanged((DataTemplate) args.NewValue);
            }

            internal void <.cctor>b__11_3(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfo) o).IsCheckedChanged((bool?) args.NewValue);
            }

            internal void <.cctor>b__11_4(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfo) o).ButtonKindChanged((ButtonKind) args.NewValue);
            }

            internal void <.cctor>b__11_5(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfo) o).CommandChanged((ICommand) args.NewValue);
            }

            internal void <.cctor>b__11_6(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfo) o).CommandParameterChanged(args.NewValue);
            }

            internal void <.cctor>b__11_7(DependencyObject o, DependencyPropertyChangedEventArgs args)
            {
                ((ButtonInfo) o).CommandTargetChanged((IInputElement) args.NewValue);
            }

            internal BaseEditSettings <OnRenderButtonClick>b__62_0(InplaceBaseEdit x) => 
                x.Settings;
        }
    }
}

