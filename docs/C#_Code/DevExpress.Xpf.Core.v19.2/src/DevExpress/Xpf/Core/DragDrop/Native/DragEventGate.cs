namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class DragEventGate : IDragEventListener, IDragDataTransferListener
    {
        private readonly IDragStateMachine dataModificationController;
        private readonly IDragDropInfoFactory dragInfoFactory;
        private readonly DropMarkerData dropData;
        private DragPointClickInfo startDragPosition;
        private double minimumHorizontalDragDistanceCore = SystemParameters.MinimumHorizontalDragDistance;
        private double minimumVerticalDragDistanceCore = SystemParameters.MinimumVerticalDragDistance;
        private IDropMarkerDisplayer dropMarkerDisplayer = new EmptyDropMarkerDisplayer();
        private ICustomDragService customDragService = new EmptyCustomDragService();
        private IDragDataTransferListener customDataTransferListener = new EmptyDragDataTransferListener();
        private IDropPositionCalculator dropPositionCalculator = new DevExpress.Xpf.Core.DragDrop.Native.DropPositionCalculator();
        private IDragElementController dragElementController = new DefaultDragElementController();
        private DragDataTransferService dataTransferService = new DragDataTransferService();
        private IDragScrollService scrollService = new DefaultDragScrollService();
        private bool autoProcessExternalDropData = true;
        private IAutoExpandService autoExpandService = new DefaultAutoExpandService();
        private DevExpress.Xpf.Core.DragDrop.Native.DropTargetValidatorFactory dropTargetValidatorFactory = new DevExpress.Xpf.Core.DragDrop.Native.DropTargetValidatorFactory();
        private IDropTargetValidator currentDropTargetValidator = new DefaultDropTargetValidator();
        private DragInfo activedragInfo;
        private Locker dragLeaveLocker = new Locker();
        private DragDropEffects? lastUsedDragOverEffects;
        private bool wasCanceled;

        public DragEventGate(IDragStateMachine dataModificationController, IDragDropInfoFactory dragInfoFactory, DropMarkerData dropData)
        {
            Guard.ArgumentNotNull(dataModificationController, "dataModificationController");
            Guard.ArgumentNotNull(dragInfoFactory, "dragInfoFactory");
            Guard.ArgumentNotNull(dropData, "dropData");
            this.dataModificationController = dataModificationController;
            this.dragInfoFactory = dragInfoFactory;
            this.dropData = dropData;
            Func<FrameworkElement, double> func1 = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<FrameworkElement, double> local1 = <>c.<>9__8_0;
                func1 = <>c.<>9__8_0 = _ => 0.0;
            }
            this.CalcLeftIndent = func1;
        }

        public static void Clear()
        {
            GlobalDragHintStatus.Clear();
        }

        private Lazy<IDataObject> CreateDefaultDataObject(object[] data) => 
            new Lazy<IDataObject>(delegate {
                DataObject obj2 = new DataObject();
                obj2.SetData(DataObjectFormat, new RecordDragDropData(data));
                return obj2;
            });

        private DragInfo CreateDragInfo(object source) => 
            this.DragInfoFactory.CreateDragInfo(source);

        private DropInfo CreateDropInfo(object source) => 
            this.DragInfoFactory.CreateDropInfo(source, this.ActivedragInfo);

        private void DoDragDropAction<T>(IRoutedEventArgs mouseArg, Func<object, T> infoFactory, Action<T> insideDragAreaAction, Action outsideDragAreaAction = null) where T: DragDropInfoBase
        {
            T local = infoFactory(mouseArg.OriginalSource);
            if (local != null)
            {
                insideDragAreaAction(local);
            }
            else if (outsideDragAreaAction != null)
            {
                outsideDragAreaAction();
            }
        }

        private DragDropEffects GetAutoCalculatedDragOverEffects(DragDropEffects currentEffects, MoveValidationState validationState, DropInfo dropInfo)
        {
            bool flag = currentEffects == DragDropEffects.None;
            bool flag3 = validationState != MoveValidationState.Valid;
            return (((flag | (!this.AutoProcessExternalDropData && this.IsExternalDropInfo(dropInfo))) | flag3) ? DragDropEffects.None : DragDropEffects.Move);
        }

        private static Size GetSize(DropInfo dropInfo) => 
            new Size(dropInfo.Element.ActualWidth, dropInfo.Element.ActualHeight);

        private static int GetValidationLevel(MoveValidationState state)
        {
            switch (state)
            {
                case MoveValidationState.Valid:
                    return 2;

                case MoveValidationState.Warning:
                    return 1;

                case MoveValidationState.Fail:
                    return 0;
            }
            return -2147483648;
        }

        private void HideDragOverVisualFeedback()
        {
            this.DropMarkerDisplayer.Hide();
            Action<DragDropHintData> action = <>c.<>9__82_0;
            if (<>c.<>9__82_0 == null)
            {
                Action<DragDropHintData> local1 = <>c.<>9__82_0;
                action = <>c.<>9__82_0 = delegate (DragDropHintData x) {
                    x.DropPosition = null;
                    x.TargetRecord = null;
                };
            }
            this.GlobalDragDropHintData.Do<DragDropHintData>(action);
        }

        private bool IsDragGesture(IMouseEventArgs e)
        {
            if ((this.startDragPosition == null) || (e.LeftButton == MouseButtonState.Released))
            {
                return false;
            }
            Point position = e.GetPosition(this.startDragPosition.Info.Element);
            double num2 = Math.Abs((double) (this.startDragPosition.Point.Y - position.Y));
            return ((Math.Abs((double) (this.startDragPosition.Point.X - position.X)) > this.MinimumHorizontalDragDistance) || (num2 > this.MinimumVerticalDragDistance));
        }

        private bool IsExternalDropInfo(DropInfo dropInfo) => 
            !this.IsDraggingInsideOwnerControl || (this.ActivedragInfo.Owner != dropInfo.Owner);

        private void OnActivedragInfoChanged()
        {
            this.CustomDragService.InsideControl = this.IsDraggingInsideOwnerControl;
            this.wasCanceled = false;
            this.UpdateCurrentDropTargetValidator();
        }

        public void OnDragEnter(object sender, IDragEventArgs e)
        {
            this.dragLeaveLocker.LockOnce();
            this.RestoreDragEffect(e);
        }

        public void OnDragLeave(object sender, IDragEventArgs e)
        {
            this.RestoreDragEffect(e);
            this.dragLeaveLocker.Unlock();
            Action method = delegate {
                this.dragLeaveLocker.DoIfNotLocked(new Action(this.HideDragOverVisualFeedback));
                this.dragLeaveLocker.Unlock();
            };
            Func<DispatcherObject, Dispatcher> evaluator = <>c.<>9__78_1;
            if (<>c.<>9__78_1 == null)
            {
                Func<DispatcherObject, Dispatcher> local1 = <>c.<>9__78_1;
                evaluator = <>c.<>9__78_1 = x => x.Dispatcher;
            }
            Dispatcher dispatcher = (sender as DispatcherObject).With<DispatcherObject, Dispatcher>(evaluator);
            if (dispatcher != null)
            {
                dispatcher.BeginInvoke(method, new object[0]);
            }
            else
            {
                method();
            }
        }

        public void OnDragOver(object sender, IDragEventArgs e)
        {
            UIElement dropMarkerAdornedElement = null;
            this.ScrollService.Update(e);
            this.DoDragDropAction<DropInfo>(e, new Func<object, DropInfo>(this.CreateDropInfo), delegate (DropInfo d) {
                this.AutoExpandService.Update(d);
                DropPositionCalculationResult rawPosition = this.DropPositionCalculator.CalcPosition(e.GetPosition(d.Element), GetSize(d));
                Tuple<DropPosition, MoveValidationState> tuple = this.ValidateRawDragOverPosition(d, rawPosition);
                DropPosition position = tuple.Item1;
                MoveValidationState validationState = tuple.Item2;
                e.Effects = this.GetAutoCalculatedDragOverEffects(e.Effects, validationState, d);
                if (validationState != MoveValidationState.Fail)
                {
                    DragOverEventSource eventSource = new DragOverEventSource();
                    eventSource.Args = e;
                    eventSource.DropInfo = d;
                    eventSource.Position = position;
                    eventSource.Ratio = rawPosition.Ratio;
                    position = this.CustomDragService.CustomDragOver(eventSource);
                    this.UpdateDropData(position, d);
                    dropMarkerAdornedElement = d.Element;
                    this.GlobalDragDropHintData.Do<DragDropHintData>(delegate (DragDropHintData x) {
                        x.DropPosition = new DropPosition?(position);
                        x.TargetRecord = d.Data;
                    });
                }
            }, () => e.Effects = DragDropEffects.None);
            e.Handled = true;
            this.lastUsedDragOverEffects = new DragDropEffects?(e.Effects);
            if ((e.Effects != DragDropEffects.None) && (dropMarkerAdornedElement != null))
            {
                this.DropMarkerDisplayer.Show(dropMarkerAdornedElement);
            }
            else
            {
                this.HideDragOverVisualFeedback();
            }
        }

        public void OnDrop(object sender, IDragEventArgs e)
        {
            this.DoDragDropAction<DropInfo>(e, new Func<object, DropInfo>(this.CreateDropInfo), delegate (DropInfo d) {
                CustomDropResult result = this.CustomDragService.CustomDrop(e, d, this.DropData.Position) ?? new CustomDropResult();
                IDataObject dataObject = result.DataObject ?? e.Data;
                if (!result.IsCustom)
                {
                    object[] objects = this.ParseDataObject(dataObject);
                    if (objects != null)
                    {
                        this.DataModificationController.Drop(objects, new DropPointer(d.RowPointer, this.DropData.Position));
                    }
                    else
                    {
                        e.Effects = DragDropEffects.None;
                        this.DataModificationController.Cancel();
                    }
                    e.Handled = true;
                }
            }, null);
            this.DropMarkerDisplayer.Hide();
        }

        private void OnEndDrag(DragDropEffects effects)
        {
            bool isCustom = false;
            DragDropEffects effects2 = effects;
            bool wasCanceled = this.wasCanceled;
            if (!this.wasCanceled && ((effects == DragDropEffects.None) && Keyboard.IsKeyDown(Key.Escape)))
            {
                wasCanceled = true;
            }
            Func<DragInfo, object[]> evaluator = <>c.<>9__97_0;
            if (<>c.<>9__97_0 == null)
            {
                Func<DragInfo, object[]> local1 = <>c.<>9__97_0;
                evaluator = <>c.<>9__97_0 = x => x.Data;
            }
            object[] local2 = this.ActivedragInfo.With<DragInfo, object[]>(evaluator);
            object[] dragObjects = local2;
            if (local2 == null)
            {
                object[] local3 = local2;
                dragObjects = new object[0];
            }
            CustomCompleteDragResult result = this.CustomDragService.CustomCompleteDragDrop(effects, dragObjects, wasCanceled);
            if (result != null)
            {
                isCustom = result.IsCustom;
                effects2 = result.Effects;
            }
            if ((effects2 == DragDropEffects.None) | isCustom)
            {
                this.DataModificationController.Cancel();
            }
            else
            {
                this.DataModificationController.EndDrag();
            }
            this.ActivedragInfo = null;
            this.CustomDragService.ActiveDataObject = null;
            this.DragElementController.Hide();
            Clear();
        }

        public void OnGiveFeedback(object sender, IGiveFeedbackEventArgs args)
        {
            this.DragElementController.Data.Do<DragDropHintData>(x => x.Effects = args.Effects);
            this.CustomDataTransferListener.OnGiveFeedback(sender, args);
        }

        public void OnMouseDown(object sender, IMouseButtonEventArgs e)
        {
            this.DoDragDropAction<DragInfo>(e, new Func<object, DragInfo>(this.CreateDragInfo), delegate (DragInfo d) {
                if (e.ChangedButton == MouseButton.Left)
                {
                    this.startDragPosition = new DragPointClickInfo(e.GetPosition(d.Element), d);
                }
            }, null);
        }

        public void OnMouseLeave(object sender, IMouseEventArgs args)
        {
            this.startDragPosition = null;
        }

        public void OnMouseMove(object sender, IMouseEventArgs e)
        {
            if (this.IsDragGesture(e))
            {
                this.OnStartDrag();
            }
        }

        public void OnMouseUp(object sender, IMouseButtonEventArgs e)
        {
            this.startDragPosition = null;
        }

        public void OnQueryContinueDrag(object sender, IQueryContinueDragEventArgs args)
        {
            this.DragElementController.UpdatePosition();
            this.CustomDataTransferListener.OnQueryContinueDrag(sender, args);
            this.wasCanceled = args.Action == DragAction.Cancel;
        }

        private void OnStartDrag()
        {
            DragInfo dragInfo = this.startDragPosition.Info;
            CustomStartDragResult result = this.CustomDragService.CustomStartDrag(dragInfo, this.CreateDefaultDataObject(dragInfo.Data));
            if (result != null)
            {
                IDataObject dataObject = result.DataObject;
                this.lastUsedDragOverEffects = null;
                this.startDragPosition = null;
                if (dataObject != null)
                {
                    this.ActivedragInfo = dragInfo;
                    this.DragElementController.Show(dragInfo.Data);
                    GlobalDragHintStatus.Instance.DragDropHintData = this.DragElementController.Data;
                    this.DataModificationController.StartDrag(dragInfo.RowPointers);
                    this.DataTransferService.CallBack = new Action<DragDropEffects>(this.OnEndDrag);
                    this.DataTransferService.DataObject = dataObject;
                    this.DataTransferService.AllowedEffects = new DragDropEffects?(result.Effects);
                    this.CustomDragService.ActiveDataObject = dataObject;
                    this.DataTransferService.Transfer();
                }
            }
        }

        private object[] ParseDataObject(IDataObject dataObject)
        {
            if ((dataObject != null) && dataObject.GetDataPresent(DataObjectFormat))
            {
                RecordDragDropData data = (RecordDragDropData) dataObject.GetData(DataObjectFormat);
                if ((data.Records != null) && (data.Records.Length != 0))
                {
                    return data.Records;
                }
            }
            return null;
        }

        private void RestoreDragEffect(IDragEventArgs e)
        {
            e.Effects = (this.lastUsedDragOverEffects != null) ? this.lastUsedDragOverEffects.Value : DragDropEffects.None;
            e.Handled = true;
        }

        private void UpdateCurrentDropTargetValidator()
        {
            this.CurrentDropTargetValidator = this.IsDraggingInsideOwnerControl ? this.DropTargetValidatorFactory.CreateInternalValidator(this.ActivedragInfo) : this.DropTargetValidatorFactory.CreateExternalValidator();
        }

        private void UpdateDropData(DropPosition position, DropInfo dropInfo)
        {
            this.DropData.Position = position;
            this.DropData.Padding = new Thickness(this.CalcLeftIndent(dropInfo.Element), 0.0, 0.0, 0.0);
        }

        private Tuple<DropPosition, MoveValidationState> ValidateRawDragOverPosition(DropInfo dropInfo, DropPositionCalculationResult rawPosition)
        {
            IEnumerable<DropPosition> positions;
            DropPosition append = DropPosition.Append;
            MoveValidationState fail = MoveValidationState.Fail;
            if (dropInfo.StaticPosition == null)
            {
                positions = rawPosition.Positions;
            }
            else
            {
                DropPosition[] positionArray1 = new DropPosition[] { dropInfo.StaticPosition.Value };
                positions = positionArray1;
            }
            foreach (DropPosition position2 in positions)
            {
                MoveValidationState state = this.CurrentDropTargetValidator.Validate(new DropPointer(dropInfo.RowPointer, position2));
                if (GetValidationLevel(state) > GetValidationLevel(fail))
                {
                    append = position2;
                    fail = state;
                }
                if (fail == MoveValidationState.Valid)
                {
                    break;
                }
            }
            return new Tuple<DropPosition, MoveValidationState>(append, fail);
        }

        private static Type DataObjectFormat =>
            typeof(RecordDragDropData);

        public double MinimumHorizontalDragDistance
        {
            get => 
                this.minimumHorizontalDragDistanceCore;
            set => 
                this.minimumHorizontalDragDistanceCore = value;
        }

        public double MinimumVerticalDragDistance
        {
            get => 
                this.minimumVerticalDragDistanceCore;
            set => 
                this.minimumVerticalDragDistanceCore = value;
        }

        public IDropMarkerDisplayer DropMarkerDisplayer
        {
            get => 
                this.dropMarkerDisplayer;
            set => 
                this.dropMarkerDisplayer = value;
        }

        public ICustomDragService CustomDragService
        {
            get => 
                this.customDragService;
            set => 
                this.customDragService = value;
        }

        public IDragDataTransferListener CustomDataTransferListener
        {
            get => 
                this.customDataTransferListener;
            set => 
                this.customDataTransferListener = value;
        }

        public IDropPositionCalculator DropPositionCalculator
        {
            get => 
                this.dropPositionCalculator;
            set => 
                this.dropPositionCalculator = value;
        }

        public DropMarkerData DropData =>
            this.dropData;

        public IDragElementController DragElementController
        {
            get => 
                this.dragElementController;
            set => 
                this.dragElementController = value;
        }

        private DragDropHintData GlobalDragDropHintData =>
            this.DragElementController.Data ?? GlobalDragHintStatus.Instance.DragDropHintData;

        public DragDataTransferService DataTransferService
        {
            get => 
                this.dataTransferService;
            set => 
                this.dataTransferService = value;
        }

        private IDragStateMachine DataModificationController =>
            this.dataModificationController;

        public IDragScrollService ScrollService
        {
            get => 
                this.scrollService;
            set => 
                this.scrollService = value;
        }

        public bool AutoProcessExternalDropData
        {
            get => 
                this.autoProcessExternalDropData;
            set => 
                this.autoProcessExternalDropData = value;
        }

        public IAutoExpandService AutoExpandService
        {
            get => 
                this.autoExpandService;
            set => 
                this.autoExpandService = value;
        }

        public DevExpress.Xpf.Core.DragDrop.Native.DropTargetValidatorFactory DropTargetValidatorFactory
        {
            get => 
                this.dropTargetValidatorFactory;
            set
            {
                if (!ReferenceEquals(this.dropTargetValidatorFactory, value))
                {
                    this.dropTargetValidatorFactory = value;
                    this.UpdateCurrentDropTargetValidator();
                }
            }
        }

        private IDropTargetValidator CurrentDropTargetValidator
        {
            get => 
                this.currentDropTargetValidator;
            set => 
                this.currentDropTargetValidator = value;
        }

        private DragInfo ActivedragInfo
        {
            get => 
                this.activedragInfo;
            set
            {
                if (!ReferenceEquals(this.activedragInfo, value))
                {
                    this.activedragInfo = value;
                    this.OnActivedragInfoChanged();
                }
            }
        }

        public Func<FrameworkElement, double> CalcLeftIndent { get; set; }

        private bool IsDraggingInsideOwnerControl =>
            this.ActivedragInfo != null;

        private IDragDropInfoFactory DragInfoFactory =>
            this.dragInfoFactory;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DragEventGate.<>c <>9 = new DragEventGate.<>c();
            public static Func<FrameworkElement, double> <>9__8_0;
            public static Func<DispatcherObject, Dispatcher> <>9__78_1;
            public static Action<DragDropHintData> <>9__82_0;
            public static Func<DragInfo, object[]> <>9__97_0;

            internal double <.ctor>b__8_0(FrameworkElement _) => 
                0.0;

            internal void <HideDragOverVisualFeedback>b__82_0(DragDropHintData x)
            {
                x.DropPosition = null;
                x.TargetRecord = null;
            }

            internal Dispatcher <OnDragLeave>b__78_1(DispatcherObject x) => 
                x.Dispatcher;

            internal object[] <OnEndDrag>b__97_0(DragInfo x) => 
                x.Data;
        }

        private class DragPointClickInfo
        {
            public DragPointClickInfo(System.Windows.Point point, DragInfo info)
            {
                this.Point = point;
                this.Info = info;
            }

            public System.Windows.Point Point { get; private set; }

            public DragInfo Info { get; private set; }
        }

        private class GlobalDragHintStatus
        {
            private static DragEventGate.GlobalDragHintStatus instance;

            private GlobalDragHintStatus()
            {
            }

            public static void Clear()
            {
                instance = null;
            }

            public static DragEventGate.GlobalDragHintStatus Instance
            {
                get
                {
                    instance ??= new DragEventGate.GlobalDragHintStatus();
                    return instance;
                }
            }

            public DevExpress.Xpf.Core.DragDropHintData DragDropHintData { get; set; }
        }
    }
}

