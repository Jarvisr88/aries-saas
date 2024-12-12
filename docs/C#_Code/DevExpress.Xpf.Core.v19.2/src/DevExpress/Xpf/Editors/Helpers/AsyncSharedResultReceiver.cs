namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Async;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AsyncSharedResultReceiver : IAsyncResultReceiver, IAsyncCommandVisitor
    {
        private readonly IAsyncResultReceiver mainReceiver;
        private readonly IList<IAsyncResultReceiver> additionalReceivers;

        public AsyncSharedResultReceiver(IAsyncResultReceiver mainReceiver)
        {
            this.mainReceiver = mainReceiver;
            this.additionalReceivers = new List<IAsyncResultReceiver>();
        }

        public void AddReceiver(IAsyncResultReceiver receiver)
        {
            if ((receiver != null) && !this.additionalReceivers.Contains(receiver))
            {
                this.additionalReceivers.Add(receiver);
            }
        }

        public void BusyChanged(bool busy)
        {
            this.mainReceiver.BusyChanged(busy);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.BusyChanged(busy));
        }

        public void Canceled(Command command)
        {
            this.mainReceiver.Canceled(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Canceled(command));
        }

        public void Notification(NotificationExceptionThrown exception)
        {
            this.mainReceiver.Notification(exception);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Notification(exception));
        }

        public void Notification(NotificationInconsistencyDetected notification)
        {
            this.mainReceiver.Notification(notification);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Notification(notification));
        }

        public void PropertyDescriptorsRenewed()
        {
            this.mainReceiver.PropertyDescriptorsRenewed();
            Action<IAsyncResultReceiver> action = <>c.<>9__20_0;
            if (<>c.<>9__20_0 == null)
            {
                Action<IAsyncResultReceiver> local1 = <>c.<>9__20_0;
                action = <>c.<>9__20_0 = x => x.PropertyDescriptorsRenewed();
            }
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(action);
        }

        public void Refreshing(CommandRefresh refreshCommand)
        {
            this.mainReceiver.Refreshing(refreshCommand);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Refreshing(refreshCommand));
        }

        public void Visit(CommandApply command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        public void Visit(CommandFindIncremental command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        public void Visit(CommandGetAllFilteredAndSortedRows command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        public void Visit(CommandGetGroupInfo command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        public void Visit(CommandGetRow command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        public void Visit(CommandGetRowIndexByKey command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        public void Visit(CommandGetTotals command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        public void Visit(CommandGetUniqueColumnValues command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        public void Visit(CommandLocateByValue command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        public void Visit(CommandPrefetchRows command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        public void Visit(CommandRefresh command)
        {
            this.mainReceiver.Visit(command);
            this.additionalReceivers.ForEach<IAsyncResultReceiver>(x => x.Visit(command));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AsyncSharedResultReceiver.<>c <>9 = new AsyncSharedResultReceiver.<>c();
            public static Action<IAsyncResultReceiver> <>9__20_0;

            internal void <PropertyDescriptorsRenewed>b__20_0(IAsyncResultReceiver x)
            {
                x.PropertyDescriptorsRenewed();
            }
        }
    }
}

