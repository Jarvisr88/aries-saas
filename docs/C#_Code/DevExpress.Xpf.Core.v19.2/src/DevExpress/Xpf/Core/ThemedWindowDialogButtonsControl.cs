namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;

    [ContentProperty("DialogItems")]
    public class ThemedWindowDialogButtonsControl : ItemsControl
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty IWindowServiceProperty;
        private static readonly DependencyPropertyKey DialogItemsPropertyKey;
        public static readonly DependencyProperty DialogItemsProperty;
        private CollectionViewSource collection;

        static ThemedWindowDialogButtonsControl()
        {
            object obj1;
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindowDialogButtonsControl), new FrameworkPropertyMetadata(typeof(ThemedWindowDialogButtonsControl)));
            FieldInfo field = typeof(System.Windows.Window).GetField("IWindowServiceProperty", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            if (field != null)
            {
                obj1 = field.GetValue(null);
            }
            else
            {
                FieldInfo local1 = field;
                obj1 = null;
            }
            IWindowServiceProperty = obj1 as DependencyProperty;
            if (IWindowServiceProperty == null)
            {
                DependencyProperty iWindowServiceProperty = IWindowServiceProperty;
            }
            else
            {
                IWindowServiceProperty.OverrideMetadata(typeof(ThemedWindowDialogButtonsControl), new FrameworkPropertyMetadata((d, e) => ((ThemedWindowDialogButtonsControl) d).OnWindowServiceChanged(e.OldValue, e.NewValue)));
            }
            DialogItemsPropertyKey = DependencyProperty.RegisterReadOnly("DialogItems", typeof(ObservableCollection<object>), typeof(ThemedWindowDialogButtonsControl), new FrameworkPropertyMetadata(null));
            DialogItemsProperty = DialogItemsPropertyKey.DependencyProperty;
        }

        public ThemedWindowDialogButtonsControl()
        {
            this.CreateDialogItemsCollection();
            this.CreateCollectionViewSource();
            this.AddGroupStyle();
        }

        private void AddGroupStyle()
        {
            ThemedWindowThemeKeyExtension resourceKey = new ThemedWindowThemeKeyExtension();
            resourceKey.ResourceKey = ThemedWindowThemeKeys.DialogButtonsControlGroupStyle;
            GroupStyle item = (GroupStyle) base.FindResource(resourceKey);
            base.GroupStyle.Add(item);
        }

        private void CreateCollectionViewSource()
        {
            this.collection = new CollectionViewSource();
            PropertyGroupDescription description1 = new PropertyGroupDescription();
            description1.PropertyName = "ActualAlignment";
            PropertyGroupDescription item = description1;
            this.collection.GroupDescriptions.Add(item);
        }

        private void CreateDialogItemsCollection()
        {
            base.SetValue(DialogItemsPropertyKey, new ObservableCollection<object>());
        }

        internal UICommand FindUICommand(MessageBoxResult uiCommandResult)
        {
            if (base.ItemsSource == null)
            {
                return null;
            }
            object obj2 = base.ItemsSource.Cast<object>().FirstOrDefault<object>(x => IsCommandIdEqualToDialogResult(uiCommandResult, x));
            return (!(obj2 is UICommand) ? (!(obj2 is ThemedWindowDialogButton) ? null : ((ThemedWindowDialogButton) obj2).UICommand) : ((UICommand) obj2));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            ThemedWindowDialogButton button1 = new ThemedWindowDialogButton();
            button1.Margin = new Thickness(2.0, 0.0, 2.0, 0.0);
            return button1;
        }

        private static bool IsCommandIdEqualToDialogResult(MessageBoxResult uiCommandResult, object x)
        {
            UICommand command = x as UICommand;
            if (command != null)
            {
                return ((command.Id != null) && command.Id.Equals(uiCommandResult));
            }
            ThemedWindowDialogButton button = x as ThemedWindowDialogButton;
            return ((button != null) && button.DialogResult.Equals(uiCommandResult));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.SetDialogButtons();
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            (this.Window != null) ? (this.Window.ShowDialogFooter ? new ThemedDialogControlAutomationPeer(this) : base.OnCreateAutomationPeer()) : base.OnCreateAutomationPeer();

        protected virtual void OnWindowServiceChanged(object oldValue, object newValue)
        {
            this.SetDialogButtons();
        }

        private void SetDialogButtons()
        {
            if (this.UseCustomDialogButtons())
            {
                this.Window.DialogButtons.Clear();
                foreach (object obj2 in this.DialogItems)
                {
                    this.Window.DialogButtons.Add(obj2);
                }
                this.Window.UpdateActualDialogButtons();
            }
            Binding binding = new Binding {
                Source = this.Window,
                Path = new PropertyPath("ActualDialogButtons", new object[0])
            };
            BindingOperations.SetBinding(this.collection, CollectionViewSource.SourceProperty, binding);
            Binding binding1 = new Binding();
            binding1.Source = this.collection;
            base.SetBinding(ItemsControl.ItemsSourceProperty, binding1);
        }

        private bool UseCustomDialogButtons() => 
            (this.Window != null) && (ThemedWindowOptions.GetUseCustomDialogFooter(this.Window) && !this.DialogItems.IsNullOrEmpty());

        public ObservableCollection<object> DialogItems =>
            (ObservableCollection<object>) base.GetValue(DialogItemsProperty);

        protected ThemedWindow Window =>
            base.GetValue(IWindowServiceProperty) as ThemedWindow;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemedWindowDialogButtonsControl.<>c <>9 = new ThemedWindowDialogButtonsControl.<>c();

            internal void <.cctor>b__3_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ThemedWindowDialogButtonsControl) d).OnWindowServiceChanged(e.OldValue, e.NewValue);
            }
        }
    }
}

