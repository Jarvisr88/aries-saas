namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomDragEventProcessor : ICustomDragService, IDragDataTransferListener
    {
        private readonly IDragEventFactory eventFactory;
        private readonly Action<RoutedEventArgs> raiser;

        public CustomDragEventProcessor(IDragEventFactory eventFactory, Action<RoutedEventArgs> raiser)
        {
            Guard.ArgumentNotNull(eventFactory, "eventFactory");
            Guard.ArgumentNotNull(raiser, "raiser");
            this.eventFactory = eventFactory;
            this.raiser = raiser;
        }

        public CustomCompleteDragResult CustomCompleteDragDrop(DragDropEffects effects, object[] dragObjects, bool canceled)
        {
            CompleteRecordDragDropEventArgs args = this.eventFactory.CreateCustomCompleteDragDrop();
            if (args == null)
            {
                return new CustomCompleteDragResult(false, effects);
            }
            args.Effects = effects;
            args.Records = dragObjects;
            args.Canceled = canceled;
            this.raiser(args);
            return new CustomCompleteDragResult(args.Handled, args.Effects);
        }

        public DropPosition CustomDragOver(DragOverEventSource eventSource)
        {
            DragRecordOverEventArgs args = this.eventFactory.CreateCustomDragOver();
            if (args == null)
            {
                return eventSource.Position;
            }
            this.PrepareCustomDragEventArgs(args, eventSource.DropInfo, eventSource.Position);
            args.DropPositionRelativeCoefficient = eventSource.Ratio;
            this.ProcessDragEventWrapper(args, eventSource.Args);
            return args.DropPosition;
        }

        public CustomDropResult CustomDrop(IDragEventArgs args, DropInfo dropInfo, DropPosition position)
        {
            DropRecordEventArgs args2 = this.eventFactory.CreateCustomDrop();
            if (args2 == null)
            {
                return new CustomDropResult();
            }
            this.PrepareCustomDragEventArgs(args2, dropInfo, position);
            this.ProcessDragEventWrapper(args2, args);
            return new CustomDropResult(args2.Handled, args2.Data);
        }

        public CustomStartDragResult CustomStartDrag(DragInfo dragInfo, Lazy<IDataObject> dataObject)
        {
            StartRecordDragEventArgs args = this.eventFactory.CreateCustomStartDrag();
            if (args == null)
            {
                return new CustomStartDragResult(dataObject.Value);
            }
            args.DragElement = dragInfo.OriginalSource;
            args.Records = dragInfo.Data;
            args.SetDataObjectInitializer(dataObject);
            args.AllowDrag = true;
            this.raiser(args);
            return (!args.Handled ? new CustomStartDragResult(DragDropEffects.Link | DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll, dataObject.Value) : (args.AllowDrag ? new CustomStartDragResult(args.AllowedEffects, args.Data) : null));
        }

        public void OnGiveFeedback(object sender, IGiveFeedbackEventArgs args)
        {
            this.ProcessMouseEventWrapper<GiveRecordDragFeedbackEventArgs, IGiveFeedbackEventArgs>(delegate (GiveRecordDragFeedbackEventArgs c, IGiveFeedbackEventArgs n) {
                c.Effects = n.Effects;
                c.UseDefaultCursors = n.UseDefaultCursors;
                c.Data = this.ActiveDataObject;
            }, (c, n) => this.SyncNativeArgsIfNeeded<IGiveFeedbackEventArgs>(n, () => n.UseDefaultCursors = c.UseDefaultCursors, () => c.Handled), args, this.eventFactory.CreateCustomGiveFeedback());
        }

        public void OnQueryContinueDrag(object sender, IQueryContinueDragEventArgs args)
        {
            this.ProcessMouseEventWrapper<ContinueRecordDragEventArgs, IQueryContinueDragEventArgs>(delegate (ContinueRecordDragEventArgs c, IQueryContinueDragEventArgs n) {
                c.EscapePressed = n.EscapePressed;
                c.KeyStates = n.KeyStates;
                c.Action = n.EscapePressed ? DragAction.Cancel : n.Action;
                c.Data = this.ActiveDataObject;
            }, (c, n) => this.SyncNativeArgsIfNeeded<IQueryContinueDragEventArgs>(n, () => n.Action = c.Action, () => c.Handled), args, this.eventFactory.CreateCustomQueryContinueDrag());
        }

        private void PrepareCustomDragEventArgs(DragEventArgsBase args, DropInfo dropInfo, DropPosition position)
        {
            args.TargetRecord = dropInfo.Data;
            args.DropPosition = position;
            args.IsFromOutside = !this.InsideControl;
            Func<RowPointer, int> evaluator = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<RowPointer, int> local1 = <>c.<>9__15_0;
                evaluator = <>c.<>9__15_0 = x => x.Handle;
            }
            args.TargetRowHandle = dropInfo.RowPointer.Return<RowPointer, int>(evaluator, <>c.<>9__15_1 ??= () => -2147483648);
        }

        private DragEventArgsBase ProcessDragEventWrapper(DragEventArgsBase customArgs, IDragEventArgs args)
        {
            Action<DragEventArgsBase, IDragEventArgs> initCustomArgs = <>c.<>9__16_0;
            if (<>c.<>9__16_0 == null)
            {
                Action<DragEventArgsBase, IDragEventArgs> local1 = <>c.<>9__16_0;
                initCustomArgs = <>c.<>9__16_0 = delegate (DragEventArgsBase c, IDragEventArgs n) {
                    c.Effects = n.Effects;
                    c.AllowedEffects = n.AllowedEffects;
                    c.KeyStates = n.KeyStates;
                    c.Data = n.Data;
                };
            }
            this.ProcessMouseEventWrapper<DragEventArgsBase, IDragEventArgs>(initCustomArgs, delegate (DragEventArgsBase c, IDragEventArgs n) {
                Func<bool> isHandleNeeded = <>c.<>9__16_3;
                if (<>c.<>9__16_3 == null)
                {
                    Func<bool> local1 = <>c.<>9__16_3;
                    isHandleNeeded = <>c.<>9__16_3 = () => true;
                }
                this.SyncNativeArgsIfNeeded<IDragEventArgs>(n, () => n.Effects = c.Effects, isHandleNeeded);
            }, args, customArgs);
            return customArgs;
        }

        private void ProcessMouseEventWrapper<T, U>(Action<T, U> initCustomArgs, Action<T, U> syncNativeArgs, U nativeArgs, T customArgs) where T: RoutedEventArgs where U: IRoutedEventArgs
        {
            if (customArgs != null)
            {
                initCustomArgs(customArgs, nativeArgs);
                this.raiser(customArgs);
                syncNativeArgs(customArgs, nativeArgs);
            }
        }

        private void SyncNativeArgsIfNeeded<U>(U nativeArgs, Action syncNativeArgs, Func<bool> isHandleNeeded) where U: IRoutedEventArgs
        {
            if (isHandleNeeded())
            {
                syncNativeArgs();
                nativeArgs.Handled = true;
            }
        }

        public bool InsideControl { get; set; }

        public IDataObject ActiveDataObject { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomDragEventProcessor.<>c <>9 = new CustomDragEventProcessor.<>c();
            public static Func<RowPointer, int> <>9__15_0;
            public static Func<int> <>9__15_1;
            public static Action<DragEventArgsBase, IDragEventArgs> <>9__16_0;
            public static Func<bool> <>9__16_3;

            internal int <PrepareCustomDragEventArgs>b__15_0(RowPointer x) => 
                x.Handle;

            internal int <PrepareCustomDragEventArgs>b__15_1() => 
                -2147483648;

            internal void <ProcessDragEventWrapper>b__16_0(DragEventArgsBase c, IDragEventArgs n)
            {
                c.Effects = n.Effects;
                c.AllowedEffects = n.AllowedEffects;
                c.KeyStates = n.KeyStates;
                c.Data = n.Data;
            }

            internal bool <ProcessDragEventWrapper>b__16_3() => 
                true;
        }
    }
}

