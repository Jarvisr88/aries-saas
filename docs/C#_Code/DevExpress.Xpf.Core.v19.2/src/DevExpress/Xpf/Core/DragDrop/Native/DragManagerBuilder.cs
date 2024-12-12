namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DragManagerBuilder
    {
        private System.Windows.Controls.Orientation orientation;

        public DragManagerBuilder()
        {
            Func<DataTemplate> func1 = <>c.<>9__60_0;
            if (<>c.<>9__60_0 == null)
            {
                Func<DataTemplate> local1 = <>c.<>9__60_0;
                func1 = <>c.<>9__60_0 = (Func<DataTemplate>) (() => null);
            }
            this.GetDragDropHintTemplate = func1;
            Func<DataTemplate> func2 = <>c.<>9__60_1;
            if (<>c.<>9__60_1 == null)
            {
                Func<DataTemplate> local2 = <>c.<>9__60_1;
                func2 = <>c.<>9__60_1 = (Func<DataTemplate>) (() => null);
            }
            this.GetDropMarkerTemplate = func2;
            this.CreateDropPositionCalculator = delegate {
                DropPositionCalculator calculator1 = new DropPositionCalculator();
                calculator1.Orientation = this.Orientation;
                calculator1.AllowInsideDrop = this.AllowInsideDrop;
                return calculator1;
            };
            Func<DragDataTransferService> func3 = <>c.<>9__60_3;
            if (<>c.<>9__60_3 == null)
            {
                Func<DragDataTransferService> local3 = <>c.<>9__60_3;
                func3 = <>c.<>9__60_3 = () => new DragDataTransferService();
            }
            this.CreateDataTransferService = func3;
            Func<IDataModifier> func4 = <>c.<>9__60_4;
            if (<>c.<>9__60_4 == null)
            {
                Func<IDataModifier> local4 = <>c.<>9__60_4;
                func4 = <>c.<>9__60_4 = () => new EmptyDataModifier();
            }
            this.CreateDataModifier = func4;
            Func<IDragDropInfoFactory> func5 = <>c.<>9__60_5;
            if (<>c.<>9__60_5 == null)
            {
                Func<IDragDropInfoFactory> local5 = <>c.<>9__60_5;
                func5 = <>c.<>9__60_5 = () => new EmptyDragDropInfoFactory();
            }
            this.CreateDragDropInfoFactory = func5;
            Func<IDragEventFactory> func6 = <>c.<>9__60_6;
            if (<>c.<>9__60_6 == null)
            {
                Func<IDragEventFactory> local6 = <>c.<>9__60_6;
                func6 = <>c.<>9__60_6 = () => new EmptyDragEventFactory();
            }
            this.CreateDragEventFactory = func6;
            Func<bool> func7 = <>c.<>9__60_7;
            if (<>c.<>9__60_7 == null)
            {
                Func<bool> local7 = <>c.<>9__60_7;
                func7 = <>c.<>9__60_7 = () => false;
            }
            this.GetShowTargetInfoInDragDropHint = func7;
        }

        public NativeDragManager Build(FrameworkElement owner)
        {
            CustomDragEventProcessor processor = new CustomDragEventProcessor(this.CreateDragEventFactory(), delegate (RoutedEventArgs e) {
                owner.RaiseEvent(e);
            });
            DragDataTransferService service = this.CreateDataTransferService();
            DropMarkerData data1 = new DropMarkerData();
            data1.Orientation = this.Orientation;
            DropMarkerData dropData = data1;
            DragEventGate dragEventGate = new DragEventGate(new DataModificationController(this.CreateDataModifier()), this.CreateDragDropInfoFactory(), dropData) {
                DataTransferService = service
            };
            service.Listener = dragEventGate;
            if (this.ShowDragDropHint)
            {
                DragElementPopupController controller1 = new DragElementPopupController(this.GetDragDropHintTemplate, owner);
                controller1.GetShowTargetInfoInDragDropHint = this.GetShowTargetInfoInDragDropHint;
                dragEventGate.DragElementController = controller1;
            }
            dragEventGate.DropMarkerDisplayer = new AdornedMarkerDisplayer(dropData, this.GetDropMarkerTemplate);
            dragEventGate.CustomDragService = processor;
            dragEventGate.CustomDataTransferListener = processor;
            dragEventGate.DropPositionCalculator = this.CreateDropPositionCalculator();
            this.SetOptionalDependency<IDragScrollService>(this.CreateScrollService, delegate (IDragScrollService x) {
                dragEventGate.ScrollService = x;
            });
            this.SetOptionalDependency<IAutoExpandService>(this.CreateAutoExpandService, delegate (IAutoExpandService x) {
                dragEventGate.AutoExpandService = x;
            });
            this.SetOptionalDependency<DropTargetValidatorFactory>(this.CreateDropTargetValidatorFactory, delegate (DropTargetValidatorFactory x) {
                dragEventGate.DropTargetValidatorFactory = x;
            });
            this.SetOptionalDependency<Func<FrameworkElement, double>>(this.CreateLeftIndentCalculator, delegate (Func<FrameworkElement, double> x) {
                dragEventGate.CalcLeftIndent = x;
            });
            dragEventGate.AutoProcessExternalDropData = false;
            return new NativeDragManager(owner, new DragSubscriptionService(dragEventGate, owner));
        }

        private void SetOptionalDependency<T>(Func<T> retrieveDependency, Action<T> setAction)
        {
            if (retrieveDependency != null)
            {
                setAction(retrieveDependency());
            }
        }

        public Func<DataTemplate> GetDragDropHintTemplate { get; set; }

        public Func<DataTemplate> GetDropMarkerTemplate { get; set; }

        public Func<IDataModifier> CreateDataModifier { get; set; }

        public Func<IDragDropInfoFactory> CreateDragDropInfoFactory { get; set; }

        public Func<IDragEventFactory> CreateDragEventFactory { get; set; }

        public Func<IDropPositionCalculator> CreateDropPositionCalculator { get; set; }

        public Func<DragDataTransferService> CreateDataTransferService { get; set; }

        public Func<IDragScrollService> CreateScrollService { get; set; }

        public Func<IAutoExpandService> CreateAutoExpandService { get; set; }

        public Func<DropTargetValidatorFactory> CreateDropTargetValidatorFactory { get; set; }

        public Func<Func<FrameworkElement, double>> CreateLeftIndentCalculator { get; set; }

        public Func<bool> GetShowTargetInfoInDragDropHint { get; set; }

        public System.Windows.Controls.Orientation Orientation
        {
            get => 
                this.orientation;
            set => 
                this.orientation = value;
        }

        public bool AllowInsideDrop { get; set; }

        public bool ShowDragDropHint { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DragManagerBuilder.<>c <>9 = new DragManagerBuilder.<>c();
            public static Func<DataTemplate> <>9__60_0;
            public static Func<DataTemplate> <>9__60_1;
            public static Func<DragDataTransferService> <>9__60_3;
            public static Func<IDataModifier> <>9__60_4;
            public static Func<IDragDropInfoFactory> <>9__60_5;
            public static Func<IDragEventFactory> <>9__60_6;
            public static Func<bool> <>9__60_7;

            internal DataTemplate <.ctor>b__60_0() => 
                null;

            internal DataTemplate <.ctor>b__60_1() => 
                null;

            internal DragDataTransferService <.ctor>b__60_3() => 
                new DragDataTransferService();

            internal IDataModifier <.ctor>b__60_4() => 
                new EmptyDataModifier();

            internal IDragDropInfoFactory <.ctor>b__60_5() => 
                new EmptyDragDropInfoFactory();

            internal IDragEventFactory <.ctor>b__60_6() => 
                new EmptyDragEventFactory();

            internal bool <.ctor>b__60_7() => 
                false;
        }
    }
}

