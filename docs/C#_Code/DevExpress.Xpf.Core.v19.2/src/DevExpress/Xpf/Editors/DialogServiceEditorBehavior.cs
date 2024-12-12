namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DialogServiceEditorBehavior : DialogServiceEditorBehaviorBase
    {
        public static readonly DependencyProperty CommandsProperty;

        static DialogServiceEditorBehavior()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DialogServiceEditorBehavior), "owner");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            CommandsProperty = DependencyPropertyRegistrator.Register<DialogServiceEditorBehavior, DevExpress.Xpf.Editors.UICommandContainerCollection>(System.Linq.Expressions.Expression.Lambda<Func<DialogServiceEditorBehavior, DevExpress.Xpf.Editors.UICommandContainerCollection>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DialogServiceEditorBehavior.get_Commands)), parameters), null, (settings, value, newValue) => settings.CommandsChanged(value, newValue));
        }

        public DialogServiceEditorBehavior()
        {
            base.SetCurrentValue(CommandsProperty, new DevExpress.Xpf.Editors.UICommandContainerCollection());
        }

        private void CommandsChanged(DevExpress.Xpf.Editors.UICommandContainerCollection oldValue, DevExpress.Xpf.Editors.UICommandContainerCollection newValue)
        {
            ILogicalChildrenContainer2 logicalOwner = this.GetLogicalOwner();
            if (logicalOwner != null)
            {
                oldValue.Do<DevExpress.Xpf.Editors.UICommandContainerCollection>(delegate (DevExpress.Xpf.Editors.UICommandContainerCollection x) {
                    Action<DevExpress.Xpf.Editors.UICommandContainer> <>9__1;
                    Action<DevExpress.Xpf.Editors.UICommandContainer> action = <>9__1;
                    if (<>9__1 == null)
                    {
                        Action<DevExpress.Xpf.Editors.UICommandContainer> local1 = <>9__1;
                        action = <>9__1 = child => logicalOwner.RemoveLogicalChild(child);
                    }
                    x.ForEach<DevExpress.Xpf.Editors.UICommandContainer>(action);
                });
            }
            oldValue.Do<DevExpress.Xpf.Editors.UICommandContainerCollection>(delegate (DevExpress.Xpf.Editors.UICommandContainerCollection x) {
                x.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.CommandsCollectionChanged);
            });
            if (logicalOwner != null)
            {
                newValue.Do<DevExpress.Xpf.Editors.UICommandContainerCollection>(delegate (DevExpress.Xpf.Editors.UICommandContainerCollection x) {
                    Action<DevExpress.Xpf.Editors.UICommandContainer> <>9__4;
                    Action<DevExpress.Xpf.Editors.UICommandContainer> action = <>9__4;
                    if (<>9__4 == null)
                    {
                        Action<DevExpress.Xpf.Editors.UICommandContainer> local1 = <>9__4;
                        action = <>9__4 = child => logicalOwner.AddLogicalChild(child);
                    }
                    x.ForEach<DevExpress.Xpf.Editors.UICommandContainer>(action);
                });
            }
            newValue.Do<DevExpress.Xpf.Editors.UICommandContainerCollection>(delegate (DevExpress.Xpf.Editors.UICommandContainerCollection x) {
                x.CollectionChanged += new NotifyCollectionChangedEventHandler(this.CommandsCollectionChanged);
            });
        }

        private void CommandsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (base.IsAttached)
            {
                ILogicalChildrenContainer2 logicalOwner = this.GetLogicalOwner();
                if (logicalOwner != null)
                {
                    logicalOwner.ProcessChildrenChanged(e);
                }
            }
        }

        private DialogService CreateDefaultDialogService() => 
            new DialogService { 
                SetWindowOwner = true,
                DialogWindowStartupLocation = WindowStartupLocation.CenterScreen,
                Title = base.Title
            };

        protected internal virtual IEnumerable<UICommand> CreateUICommands()
        {
            Func<DevExpress.Xpf.Editors.UICommandContainerCollection, IEnumerable<UICommand>> evaluator = <>c.<>9__11_0;
            if (<>c.<>9__11_0 == null)
            {
                Func<DevExpress.Xpf.Editors.UICommandContainerCollection, IEnumerable<UICommand>> local1 = <>c.<>9__11_0;
                evaluator = <>c.<>9__11_0 = delegate (DevExpress.Xpf.Editors.UICommandContainerCollection command) {
                    Func<DevExpress.Xpf.Editors.UICommandContainer, UICommand> selector = <>c.<>9__11_1;
                    if (<>c.<>9__11_1 == null)
                    {
                        Func<DevExpress.Xpf.Editors.UICommandContainer, UICommand> local1 = <>c.<>9__11_1;
                        selector = <>c.<>9__11_1 = x => new UICommand(x.Id, x.Caption, x.Command, x.IsDefault, x.IsCancel, x.Tag, true, Dock.Right, DialogButtonAlignment.Right);
                    }
                    return command.Select<DevExpress.Xpf.Editors.UICommandContainer, UICommand>(selector).ToList<UICommand>();
                };
            }
            return this.Commands.Return<DevExpress.Xpf.Editors.UICommandContainerCollection, IEnumerable<UICommand>>(evaluator, new Func<IEnumerable<UICommand>>(Enumerable.Empty<UICommand>));
        }

        private ILogicalChildrenContainer2 GetLogicalOwner()
        {
            Func<ILogicalChildrenContainerProvider, ILogicalChildrenContainer2> evaluator = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<ILogicalChildrenContainerProvider, ILogicalChildrenContainer2> local1 = <>c.<>9__8_0;
                evaluator = <>c.<>9__8_0 = x => x.LogicalChildrenContainer;
            }
            return (base.AssociatedObject as ILogicalChildrenContainerProvider).With<ILogicalChildrenContainerProvider, ILogicalChildrenContainer2>(evaluator);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            ILogicalChildrenContainer2 logicalOwner = this.GetLogicalOwner();
            if (logicalOwner != null)
            {
                this.Commands.ForEach<DevExpress.Xpf.Editors.UICommandContainer>(x => logicalOwner.AddLogicalChild(x));
            }
        }

        protected override void OnDetaching()
        {
            ILogicalChildrenContainer2 logicalOwner = this.GetLogicalOwner();
            if (logicalOwner != null)
            {
                this.Commands.ForEach<DevExpress.Xpf.Editors.UICommandContainer>(x => logicalOwner.RemoveLogicalChild(x));
                base.OnDetaching();
            }
        }

        protected override void ProcessClickForFrameworkElementInternal(FrameworkElement owner, object editValue)
        {
            DialogService service = base.DialogServiceTemplate.Return<DataTemplate, DialogService>(new Func<DataTemplate, DialogService>(TemplateHelper.LoadFromTemplate<DialogService>), new Func<DialogService>(this.CreateDefaultDialogService));
            if (service != null)
            {
                AssignableServiceHelper2<DependencyObject, IDialogService>.DoServiceAction(owner, service, delegate (IDialogService service) {
                    int num1;
                    UITypeEditorValue editableValue = this.GetEditableValue(owner, editValue);
                    UICommand dialogResult = service.ShowDialog(this.CreateUICommands(), this.Title, editableValue);
                    AfterDialogServiceDialogClosedEventArgs args = this.RaiseDialogClosed(editableValue, dialogResult);
                    if (editableValue.ShouldPost())
                    {
                        Func<UICommand, bool> evaluator = <>c.<>9__12_1;
                        if (<>c.<>9__12_1 == null)
                        {
                            Func<UICommand, bool> local1 = <>c.<>9__12_1;
                            evaluator = <>c.<>9__12_1 = x => x.IsDefault;
                        }
                        if (dialogResult.If<UICommand>(evaluator).ReturnSuccess<UICommand>())
                        {
                            num1 = 1;
                            goto TR_0002;
                        }
                    }
                    if (args.PostValue == null)
                    {
                        num1 = 0;
                    }
                    else
                    {
                        Func<bool?, bool> evaluator = <>c.<>9__12_2;
                        if (<>c.<>9__12_2 == null)
                        {
                            Func<bool?, bool> local2 = <>c.<>9__12_2;
                            evaluator = <>c.<>9__12_2 = x => x.Value;
                        }
                        num1 = args.PostValue.Return<bool, bool>(evaluator, <>c.<>9__12_3 ??= () => false);
                    }
                TR_0002:
                    if (num1 != 0)
                    {
                        this.PostEditValue(owner, args.Value.Value);
                    }
                });
            }
        }

        public DevExpress.Xpf.Editors.UICommandContainerCollection Commands
        {
            get => 
                (DevExpress.Xpf.Editors.UICommandContainerCollection) base.GetValue(CommandsProperty);
            set => 
                base.SetValue(CommandsProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DialogServiceEditorBehavior.<>c <>9 = new DialogServiceEditorBehavior.<>c();
            public static Func<ILogicalChildrenContainerProvider, ILogicalChildrenContainer2> <>9__8_0;
            public static Func<DevExpress.Xpf.Editors.UICommandContainer, UICommand> <>9__11_1;
            public static Func<DevExpress.Xpf.Editors.UICommandContainerCollection, IEnumerable<UICommand>> <>9__11_0;
            public static Func<UICommand, bool> <>9__12_1;
            public static Func<bool?, bool> <>9__12_2;
            public static Func<bool> <>9__12_3;

            internal void <.cctor>b__1_0(DialogServiceEditorBehavior settings, DevExpress.Xpf.Editors.UICommandContainerCollection value, DevExpress.Xpf.Editors.UICommandContainerCollection newValue)
            {
                settings.CommandsChanged(value, newValue);
            }

            internal IEnumerable<UICommand> <CreateUICommands>b__11_0(DevExpress.Xpf.Editors.UICommandContainerCollection command)
            {
                Func<DevExpress.Xpf.Editors.UICommandContainer, UICommand> selector = <>9__11_1;
                if (<>9__11_1 == null)
                {
                    Func<DevExpress.Xpf.Editors.UICommandContainer, UICommand> local1 = <>9__11_1;
                    selector = <>9__11_1 = x => new UICommand(x.Id, x.Caption, x.Command, x.IsDefault, x.IsCancel, x.Tag, true, Dock.Right, DialogButtonAlignment.Right);
                }
                return command.Select<DevExpress.Xpf.Editors.UICommandContainer, UICommand>(selector).ToList<UICommand>();
            }

            internal UICommand <CreateUICommands>b__11_1(DevExpress.Xpf.Editors.UICommandContainer x) => 
                new UICommand(x.Id, x.Caption, x.Command, x.IsDefault, x.IsCancel, x.Tag, true, Dock.Right, DialogButtonAlignment.Right);

            internal ILogicalChildrenContainer2 <GetLogicalOwner>b__8_0(ILogicalChildrenContainerProvider x) => 
                x.LogicalChildrenContainer;

            internal bool <ProcessClickForFrameworkElementInternal>b__12_1(UICommand x) => 
                x.IsDefault;

            internal bool <ProcessClickForFrameworkElementInternal>b__12_2(bool? x) => 
                x.Value;

            internal bool <ProcessClickForFrameworkElementInternal>b__12_3() => 
                false;
        }
    }
}

