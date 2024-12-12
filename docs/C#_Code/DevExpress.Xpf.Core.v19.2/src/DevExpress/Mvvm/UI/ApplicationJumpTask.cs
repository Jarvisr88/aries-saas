namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    public class ApplicationJumpTask : ApplicationJumpItem, IClickable, IApplicationJumpTaskInfoSource, IApplicationJumpItemInfoSource, IApplicationJumpTask, IApplicationJumpItem
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ApplicationJumpTask), new PropertyMetadata(null));
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(ApplicationJumpTask), new PropertyMetadata(null));

        public event EventHandler Click;

        public ApplicationJumpTask() : this(new ApplicationJumpTaskInfo())
        {
        }

        public ApplicationJumpTask(ApplicationJumpTaskInfo itemInfo) : base(itemInfo)
        {
        }

        public ApplicationJumpTask Clone() => 
            (ApplicationJumpTask) base.Clone();

        protected override void CloneCore(ApplicationJumpItem clone)
        {
            base.CloneCore(clone);
            ApplicationJumpTask task = (ApplicationJumpTask) clone;
            task.Click = this.Click;
            task.Command = this.Command;
            task.CommandParameter = this.CommandParameter;
        }

        protected override ApplicationJumpItem CreateInstanceCore() => 
            new ApplicationJumpTask();

        private void RaiseClickAndExecuteCommand()
        {
            if (base.Dispatcher.CheckAccess())
            {
                this.RaiseClickAndExecuteCommandCore();
            }
            else
            {
                base.Dispatcher.BeginInvoke(new Action(this.RaiseClickAndExecuteCommandCore), new object[0]);
            }
        }

        private void RaiseClickAndExecuteCommandCore()
        {
            if (this.Click != null)
            {
                this.Click(this, EventArgs.Empty);
            }
            if ((this.Command != null) && this.Command.CanExecute(this.CommandParameter))
            {
                this.Command.Execute(this.CommandParameter);
            }
        }

        public string Title
        {
            get => 
                this.ItemInfo.Title;
            set => 
                this.ItemInfo.Title = value;
        }

        public ImageSource Icon
        {
            get => 
                this.ItemInfo.Icon;
            set => 
                this.ItemInfo.Icon = value;
        }

        public string IconResourcePath
        {
            get => 
                this.ItemInfo.IconResourcePath;
            set => 
                this.ItemInfo.IconResourcePath = value;
        }

        public int IconResourceIndex
        {
            get => 
                this.ItemInfo.IconResourceIndex;
            set => 
                this.ItemInfo.IconResourceIndex = value;
        }

        public string Description
        {
            get => 
                this.ItemInfo.Description;
            set => 
                this.ItemInfo.Description = value;
        }

        public string ApplicationPath
        {
            get => 
                this.ItemInfo.ApplicationPath;
            set => 
                this.ItemInfo.ApplicationPath = value;
        }

        public string Arguments
        {
            get => 
                this.ItemInfo.Arguments;
            set => 
                this.ItemInfo.Arguments = value;
        }

        public string WorkingDirectory
        {
            get => 
                this.ItemInfo.WorkingDirectory;
            set => 
                this.ItemInfo.WorkingDirectory = value;
        }

        public string CommandId
        {
            get => 
                this.ItemInfo.CommandId;
            set => 
                this.ItemInfo.CommandId = value;
        }

        public ICommand Command
        {
            get => 
                (ICommand) base.GetValue(CommandProperty);
            set => 
                base.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => 
                base.GetValue(CommandParameterProperty);
            set => 
                base.SetValue(CommandParameterProperty, value);
        }

        protected ApplicationJumpTaskInfo ItemInfo =>
            (ApplicationJumpTaskInfo) base.ItemInfo;

        Action IApplicationJumpTaskInfoSource.Action =>
            new Action(this.RaiseClickAndExecuteCommand);

        Action IApplicationJumpTask.Action
        {
            get => 
                this.ItemInfo.Action;
            set => 
                this.ItemInfo.Action = value;
        }
    }
}

