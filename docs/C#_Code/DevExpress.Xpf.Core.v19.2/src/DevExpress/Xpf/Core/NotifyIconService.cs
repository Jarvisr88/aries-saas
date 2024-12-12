namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Bars.Native;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("ContextMenu")]
    public class NotifyIconService : ServiceBase, INotifyIconService
    {
        public static readonly DependencyProperty ContextMenuProperty;
        public static readonly DependencyProperty IconProperty;
        public static readonly DependencyProperty LeftClickCommandProperty;
        public static readonly DependencyProperty LeftDoubleClickCommandProperty;
        public static readonly DependencyProperty ThemeNameProperty;
        public static readonly DependencyProperty StatesProperty;
        public static readonly DependencyProperty TooltipProperty;

        static NotifyIconService()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(NotifyIconService), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<NotifyIconService> registrator1 = DependencyPropertyRegistrator<NotifyIconService>.New().Register<PopupMenu>(System.Linq.Expressions.Expression.Lambda<Func<NotifyIconService, PopupMenu>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NotifyIconService.get_ContextMenu)), parameters), out ContextMenuProperty, null, (x, oldValue, newValue) => x.OnContextMenuChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NotifyIconService), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<NotifyIconService> registrator2 = registrator1.Register<ICommand>(System.Linq.Expressions.Expression.Lambda<Func<NotifyIconService, ICommand>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NotifyIconService.get_LeftClickCommand)), expressionArray2), out LeftClickCommandProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NotifyIconService), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<NotifyIconService> registrator3 = registrator2.Register<ICommand>(System.Linq.Expressions.Expression.Lambda<Func<NotifyIconService, ICommand>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NotifyIconService.get_LeftDoubleClickCommand)), expressionArray3), out LeftDoubleClickCommandProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NotifyIconService), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<NotifyIconService> registrator4 = registrator3.Register<ImageSource>(System.Linq.Expressions.Expression.Lambda<Func<NotifyIconService, ImageSource>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NotifyIconService.get_Icon)), expressionArray4), out IconProperty, null, (x, oldValue, newValue) => x.OnIconChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NotifyIconService), "x");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<NotifyIconService> registrator5 = registrator4.Register<string>(System.Linq.Expressions.Expression.Lambda<Func<NotifyIconService, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NotifyIconService.get_ThemeName)), expressionArray5), out ThemeNameProperty, "Office2013", (x, oldValue, newValue) => x.OnThemeChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NotifyIconService), "x");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<NotifyIconService> registrator6 = registrator5.Register<ObservableCollection<NotifyIconState>>(System.Linq.Expressions.Expression.Lambda<Func<NotifyIconService, ObservableCollection<NotifyIconState>>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NotifyIconService.get_States)), expressionArray6), out StatesProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(NotifyIconService), "x");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator6.Register<string>(System.Linq.Expressions.Expression.Lambda<Func<NotifyIconService, string>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(NotifyIconService.get_Tooltip)), expressionArray7), out TooltipProperty, string.Empty, (x, oldValue, newValue) => x.OnTooltipChanged(oldValue, newValue), frameworkOptions);
        }

        public NotifyIconService()
        {
            this.States = new ObservableCollection<NotifyIconState>();
        }

        private void AddPopupInLogicalTree(PopupMenu newValue)
        {
            if ((newValue != null) && (base.AssociatedObject != null))
            {
                LogicalTreeWrapper.AddLogicalChild(base.AssociatedObject, newValue, null, false);
            }
        }

        void INotifyIconService.ResetStatusIcon()
        {
            this.UpdateNotifyIcon(this.Icon.GetIcon());
        }

        void INotifyIconService.SetStatusIcon(object icon)
        {
            System.Drawing.Icon icon2 = null;
            if (icon is string)
            {
                icon2 = this.DoIfString((string) icon);
            }
            if (icon is System.Drawing.Icon)
            {
                icon2 = this.DoIfIcon((System.Drawing.Icon) icon);
            }
            if (icon2 != null)
            {
                this.UpdateNotifyIcon(icon2);
            }
        }

        private System.Drawing.Icon DoIfIcon(System.Drawing.Icon icon) => 
            icon;

        private System.Drawing.Icon DoIfString(string icon)
        {
            System.Drawing.Icon icon2;
            using (IEnumerator<NotifyIconState> enumerator = this.States.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        NotifyIconState current = enumerator.Current;
                        if (current.Name != icon)
                        {
                            continue;
                        }
                        icon2 = current.Icon.GetIcon();
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return icon2;
        }

        private void InitializeWpfNotifyIcon()
        {
            this.Window = System.Windows.Window.GetWindow(base.AssociatedObject);
            this.UpdateNotifyIcon(this.Icon.GetIcon());
            WpfNotifyIcon.PushAttachedWindow(this.Window);
            this.SetClickCommands();
            if (this.Window != null)
            {
                this.Window.Closing += new CancelEventHandler(this.OnWindowClosing);
            }
            this.SetActualContextMenu();
            if (this.ContextMenu != null)
            {
                ThemeManager.SetThemeName(this.ContextMenu, this.ThemeName);
            }
        }

        private void MergeContextMenus()
        {
            ((ILinksHolder) WpfNotifyIcon.ContextMenu).Merge(this.ContextMenu);
        }

        private void OnAssociatedObjectInitialized(object sender, EventArgs e)
        {
            this.InitializeWpfNotifyIcon();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            PresentationSource.AddSourceChangedHandler(base.AssociatedObject, new SourceChangedEventHandler(this.OnWindowSourceChanged));
            this.AddPopupInLogicalTree(this.ContextMenu);
            this.SubscribeToEvents();
        }

        protected virtual void OnContextMenuChanged(PopupMenu oldValue, PopupMenu newValue)
        {
            this.AddPopupInLogicalTree(newValue);
            this.RemovePopupFromLogicalTree(oldValue);
        }

        protected override void OnDetaching()
        {
            this.UnsubscribeToEvents();
            PresentationSource.RemoveSourceChangedHandler(base.AssociatedObject, new SourceChangedEventHandler(this.OnWindowSourceChanged));
            this.RemovePopupFromLogicalTree(this.ContextMenu);
            base.OnDetaching();
        }

        private void OnIconChanged(ImageSource oldValue, ImageSource newValue)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                WpfNotifyIcon.PushIcon(this, newValue.GetIcon());
            }
        }

        private void OnThemeChanged(string oldValue, string newValue)
        {
        }

        private void OnTooltipChanged(string oldValue, string newValue)
        {
            WpfNotifyIcon.PushTooltip(this, newValue);
        }

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                WpfNotifyIcon.OnClosingParentWindow();
            }
        }

        private void OnWindowSourceChanged(object sender, SourceChangedEventArgs e)
        {
            if (e.NewSource != null)
            {
                this.InitializeWpfNotifyIcon();
            }
            else
            {
                WpfNotifyIcon.OnClosingParentWindow();
                this.UnsubscribeToEvents();
            }
        }

        private void RemovePopupFromLogicalTree(PopupMenu oldValue)
        {
            if ((oldValue != null) && (base.AssociatedObject != null))
            {
                LogicalTreeWrapper.RemoveLogicalChild(base.AssociatedObject, oldValue, null, false);
            }
        }

        private void SetActualContextMenu()
        {
            if (WpfNotifyIcon.ContextMenu != null)
            {
                this.MergeContextMenus();
            }
            else
            {
                WpfNotifyIcon.ContextMenu = this.ContextMenu;
            }
        }

        private void SetClickCommands()
        {
            if (this.LeftClickCommand != null)
            {
                WpfNotifyIcon.LeftClickCommand = this.LeftClickCommand;
            }
            if (this.LeftDoubleClickCommand != null)
            {
                WpfNotifyIcon.LeftDoubleClickCommand = this.LeftDoubleClickCommand;
            }
        }

        private void SubscribeToEvents()
        {
            base.AssociatedObject.Initialized += new EventHandler(this.OnAssociatedObjectInitialized);
        }

        private void UnsubscribeToEvents()
        {
            base.AssociatedObject.Initialized -= new EventHandler(this.OnAssociatedObjectInitialized);
            if (this.Window != null)
            {
                this.Window.Closing -= new CancelEventHandler(this.OnWindowClosing);
            }
        }

        private void UpdateNotifyIcon(System.Drawing.Icon icon)
        {
            WpfNotifyIcon.PushTooltip(this, this.Tooltip);
            WpfNotifyIcon.PushIcon(this, icon);
        }

        public System.Windows.Window Window { get; private set; }

        public PopupMenu ContextMenu
        {
            get => 
                (PopupMenu) base.GetValue(ContextMenuProperty);
            set => 
                base.SetValue(ContextMenuProperty, value);
        }

        public ImageSource Icon
        {
            get => 
                (ImageSource) base.GetValue(IconProperty);
            set => 
                base.SetValue(IconProperty, value);
        }

        public string ThemeName
        {
            get => 
                (string) base.GetValue(ThemeNameProperty);
            set => 
                base.SetValue(ThemeNameProperty, value);
        }

        public ICommand LeftClickCommand
        {
            get => 
                (ICommand) base.GetValue(LeftClickCommandProperty);
            set => 
                base.SetValue(LeftClickCommandProperty, value);
        }

        public ICommand LeftDoubleClickCommand
        {
            get => 
                (ICommand) base.GetValue(LeftDoubleClickCommandProperty);
            set => 
                base.SetValue(LeftDoubleClickCommandProperty, value);
        }

        public ObservableCollection<NotifyIconState> States
        {
            get => 
                (ObservableCollection<NotifyIconState>) base.GetValue(StatesProperty);
            set => 
                base.SetValue(StatesProperty, value);
        }

        public string Tooltip
        {
            get => 
                (string) base.GetValue(TooltipProperty);
            set => 
                base.SetValue(TooltipProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly NotifyIconService.<>c <>9 = new NotifyIconService.<>c();

            internal void <.cctor>b__7_0(NotifyIconService x, PopupMenu oldValue, PopupMenu newValue)
            {
                x.OnContextMenuChanged(oldValue, newValue);
            }

            internal void <.cctor>b__7_1(NotifyIconService x, ImageSource oldValue, ImageSource newValue)
            {
                x.OnIconChanged(oldValue, newValue);
            }

            internal void <.cctor>b__7_2(NotifyIconService x, string oldValue, string newValue)
            {
                x.OnThemeChanged(oldValue, newValue);
            }

            internal void <.cctor>b__7_3(NotifyIconService x, string oldValue, string newValue)
            {
                x.OnTooltipChanged(oldValue, newValue);
            }
        }
    }
}

