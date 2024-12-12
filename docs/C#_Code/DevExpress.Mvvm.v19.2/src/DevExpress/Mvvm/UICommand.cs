namespace DevExpress.Mvvm
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class UICommand : BindableBase, IUICommand
    {
        private object id;
        private object caption;
        private ICommand command;
        private bool isDefault;
        private bool isCancel;
        private object tag;
        private bool allowCloseWindow;
        private DialogButtonAlignment alignment;
        private Dock placement;
        private EventHandler executed;

        event EventHandler IUICommand.Executed
        {
            add
            {
                this.executed += value;
            }
            remove
            {
                this.executed -= value;
            }
        }

        public UICommand()
        {
            this.allowCloseWindow = true;
            this.placement = Dock.Right;
        }

        public UICommand(object id, object caption, ICommand command, bool isDefault, bool isCancel, object tag = null, bool allowCloseWindow = true, Dock placement = 2, DialogButtonAlignment alignment = 0)
        {
            this.allowCloseWindow = true;
            this.placement = Dock.Right;
            this.id = id;
            this.caption = caption;
            this.command = command;
            this.isDefault = isDefault;
            this.isCancel = isCancel;
            this.tag = tag;
            this.allowCloseWindow = allowCloseWindow;
            this.placement = placement;
            this.alignment = alignment;
        }

        private static UICommand CreateDefaultButtonCommand(MessageResult result, bool usePlatformSpecificTag, Func<MessageResult, string> getButtonCaption)
        {
            object id = usePlatformSpecificTag ? ((object) result.ToMessageBoxResult()) : ((object) result);
            return new DefaultButtonCommand(id, getButtonCaption(result), id);
        }

        void IUICommand.RaiseExecuted()
        {
            if (this.executed != null)
            {
                this.executed(this, EventArgs.Empty);
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static List<UICommand> GenerateFromMessageBoxButton(MessageBoxButton dialogButtons, IMessageBoxButtonLocalizer buttonLocalizer, MessageBoxResult? defaultButton = new MessageBoxResult?(), MessageBoxResult? cancelButton = new MessageBoxResult?()) => 
            GenerateFromMessageBoxButton(dialogButtons, buttonLocalizer.ToMessageButtonLocalizer(), defaultButton, cancelButton);

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public static List<UICommand> GenerateFromMessageBoxButton(MessageBoxButton dialogButtons, IMessageButtonLocalizer buttonLocalizer, MessageBoxResult? defaultButton = new MessageBoxResult?(), MessageBoxResult? cancelButton = new MessageBoxResult?())
        {
            MessageResult? nullable3;
            MessageResult? nullable1;
            MessageResult? nullable4;
            if (defaultButton != null)
            {
                nullable1 = new MessageResult?(defaultButton.Value.ToMessageResult());
            }
            else
            {
                nullable3 = null;
                nullable1 = nullable3;
            }
            MessageResult? nullable = nullable1;
            if (cancelButton != null)
            {
                nullable4 = new MessageResult?(cancelButton.Value.ToMessageResult());
            }
            else
            {
                nullable3 = null;
                nullable4 = nullable3;
            }
            return GenerateFromMessageButton(dialogButtons.ToMessageButton(), true, buttonLocalizer, nullable, nullable4);
        }

        public static List<UICommand> GenerateFromMessageButton(MessageButton dialogButtons, IMessageButtonLocalizer buttonLocalizer, MessageResult? defaultButton = new MessageResult?(), MessageResult? cancelButton = new MessageResult?()) => 
            GenerateFromMessageButton(dialogButtons, false, buttonLocalizer, defaultButton, cancelButton);

        private static List<UICommand> GenerateFromMessageButton(MessageButton dialogButtons, bool usePlatformSpecificTag, IMessageButtonLocalizer buttonLocalizer, MessageResult? defaultButton, MessageResult? cancelButton)
        {
            MessageResult? nullable;
            MessageResult oK;
            List<UICommand> list = new List<UICommand>();
            if (dialogButtons == MessageButton.OK)
            {
                bool flag1;
                UICommand item = CreateDefaultButtonCommand(MessageResult.OK, usePlatformSpecificTag, new Func<MessageResult, string>(buttonLocalizer.Localize));
                if (defaultButton == null)
                {
                    flag1 = true;
                }
                else
                {
                    nullable = defaultButton;
                    oK = MessageResult.OK;
                    flag1 = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                }
                item.IsDefault = flag1;
                nullable = cancelButton;
                oK = MessageResult.OK;
                item.IsCancel = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                list.Add(item);
                return list;
            }
            if (dialogButtons == MessageButton.OKCancel)
            {
                bool flag2;
                bool flag5;
                UICommand item = CreateDefaultButtonCommand(MessageResult.OK, usePlatformSpecificTag, new Func<MessageResult, string>(buttonLocalizer.Localize));
                UICommand command3 = CreateDefaultButtonCommand(MessageResult.Cancel, usePlatformSpecificTag, new Func<MessageResult, string>(buttonLocalizer.Localize));
                if (defaultButton == null)
                {
                    flag2 = true;
                }
                else
                {
                    nullable = defaultButton;
                    oK = MessageResult.OK;
                    flag2 = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                }
                item.IsDefault = flag2;
                nullable = defaultButton;
                oK = MessageResult.Cancel;
                command3.IsDefault = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                nullable = cancelButton;
                oK = MessageResult.OK;
                item.IsCancel = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                if (cancelButton == null)
                {
                    flag5 = true;
                }
                else
                {
                    nullable = cancelButton;
                    oK = MessageResult.Cancel;
                    flag5 = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                }
                command3.IsCancel = flag5;
                list.Add(item);
                list.Add(command3);
                return list;
            }
            if (dialogButtons == MessageButton.YesNo)
            {
                bool flag3;
                bool flag6;
                UICommand item = CreateDefaultButtonCommand(MessageResult.Yes, usePlatformSpecificTag, new Func<MessageResult, string>(buttonLocalizer.Localize));
                UICommand command5 = CreateDefaultButtonCommand(MessageResult.No, usePlatformSpecificTag, new Func<MessageResult, string>(buttonLocalizer.Localize));
                if (defaultButton == null)
                {
                    flag3 = true;
                }
                else
                {
                    nullable = defaultButton;
                    oK = MessageResult.Yes;
                    flag3 = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                }
                item.IsDefault = flag3;
                nullable = defaultButton;
                oK = MessageResult.No;
                command5.IsDefault = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                nullable = cancelButton;
                oK = MessageResult.Yes;
                item.IsCancel = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                if (cancelButton == null)
                {
                    flag6 = true;
                }
                else
                {
                    nullable = cancelButton;
                    oK = MessageResult.No;
                    flag6 = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                }
                command5.IsCancel = flag6;
                list.Add(item);
                list.Add(command5);
                return list;
            }
            if (dialogButtons == MessageButton.YesNoCancel)
            {
                bool flag4;
                bool flag7;
                bool flag8;
                UICommand item = CreateDefaultButtonCommand(MessageResult.Yes, usePlatformSpecificTag, new Func<MessageResult, string>(buttonLocalizer.Localize));
                UICommand command7 = CreateDefaultButtonCommand(MessageResult.No, usePlatformSpecificTag, new Func<MessageResult, string>(buttonLocalizer.Localize));
                UICommand command8 = CreateDefaultButtonCommand(MessageResult.Cancel, usePlatformSpecificTag, new Func<MessageResult, string>(buttonLocalizer.Localize));
                if (defaultButton == null)
                {
                    flag4 = true;
                }
                else
                {
                    nullable = defaultButton;
                    oK = MessageResult.Yes;
                    flag4 = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                }
                item.IsDefault = flag4;
                nullable = defaultButton;
                oK = MessageResult.No;
                command7.IsDefault = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                nullable = defaultButton;
                oK = MessageResult.Cancel;
                command8.IsDefault = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                nullable = cancelButton;
                oK = MessageResult.Yes;
                item.IsCancel = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                if (cancelButton == null)
                {
                    flag7 = true;
                }
                else
                {
                    nullable = cancelButton;
                    oK = MessageResult.No;
                    flag7 = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                }
                command7.IsCancel = flag7;
                if (cancelButton == null)
                {
                    flag8 = true;
                }
                else
                {
                    nullable = cancelButton;
                    oK = MessageResult.Cancel;
                    flag8 = (((MessageResult) nullable.GetValueOrDefault()) == oK) ? (nullable != null) : false;
                }
                command8.IsCancel = flag8;
                list.Add(item);
                list.Add(command7);
                list.Add(command8);
            }
            return list;
        }

        public object Id
        {
            get => 
                this.id;
            set => 
                base.SetProperty<object>(ref this.id, value, System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UICommand)), (MethodInfo) methodof(UICommand.get_Id)), new ParameterExpression[0]));
        }

        public object Caption
        {
            get => 
                this.caption;
            set => 
                base.SetProperty<object>(ref this.caption, value, System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UICommand)), (MethodInfo) methodof(UICommand.get_Caption)), new ParameterExpression[0]));
        }

        public ICommand Command
        {
            get => 
                this.command;
            set => 
                base.SetProperty<ICommand>(ref this.command, value, System.Linq.Expressions.Expression.Lambda<Func<ICommand>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UICommand)), (MethodInfo) methodof(UICommand.get_Command)), new ParameterExpression[0]));
        }

        public bool IsDefault
        {
            get => 
                this.isDefault;
            set => 
                base.SetProperty<bool>(ref this.isDefault, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UICommand)), (MethodInfo) methodof(UICommand.get_IsDefault)), new ParameterExpression[0]));
        }

        public bool IsCancel
        {
            get => 
                this.isCancel;
            set => 
                base.SetProperty<bool>(ref this.isCancel, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UICommand)), (MethodInfo) methodof(UICommand.get_IsCancel)), new ParameterExpression[0]));
        }

        public object Tag
        {
            get => 
                this.tag;
            set => 
                base.SetProperty<object>(ref this.tag, value, System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UICommand)), (MethodInfo) methodof(UICommand.get_Tag)), new ParameterExpression[0]));
        }

        public bool AllowCloseWindow
        {
            get => 
                this.allowCloseWindow;
            set => 
                base.SetProperty<bool>(ref this.allowCloseWindow, value, System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UICommand)), (MethodInfo) methodof(UICommand.get_AllowCloseWindow)), new ParameterExpression[0]));
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DialogButtonAlignment ActualAlignment =>
            (this.alignment == DialogButtonAlignment.Right) ? (((this.placement == Dock.Right) || !this.placement.Equals(Dock.Left)) ? this.alignment : DialogButtonAlignment.Left) : this.alignment;

        public DialogButtonAlignment Alignment
        {
            get => 
                this.alignment;
            set => 
                base.SetProperty<DialogButtonAlignment>(ref this.alignment, value, System.Linq.Expressions.Expression.Lambda<Func<DialogButtonAlignment>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UICommand)), (MethodInfo) methodof(UICommand.get_Alignment)), new ParameterExpression[0]));
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public Dock Placement
        {
            get => 
                this.placement;
            set => 
                base.SetProperty<Dock>(ref this.placement, value, System.Linq.Expressions.Expression.Lambda<Func<Dock>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UICommand)), (MethodInfo) methodof(UICommand.get_Placement)), new ParameterExpression[0]));
        }

        private class DefaultButtonCommand : UICommand
        {
            public DefaultButtonCommand(object id, string caption, object tag)
            {
                base.id = id;
                base.caption = caption;
                base.tag = tag;
            }
        }
    }
}

