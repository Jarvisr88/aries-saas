namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class TouchController
    {
        private double manipulationScale;

        public TouchController(DocumentPresenterControl presenter)
        {
            this.Presenter = presenter;
        }

        public virtual void ProcessManipulationCompleted(ManipulationCompletedEventArgs e)
        {
            e.Handled = true;
        }

        public virtual void ProcessManipulationDelta(ManipulationDeltaEventArgs e)
        {
            if (!e.Handled)
            {
                if (e.Manipulators.Count<IManipulator>() != 2)
                {
                    this.manipulationScale = 0.0;
                }
                else
                {
                    this.manipulationScale += e.DeltaManipulation.Scale.X - 1.0;
                    if (Math.Abs(this.manipulationScale).GreaterThan(this.MinManipulationScale) && !this.LockManipulationZooming)
                    {
                        this.BehaviorProvider.Do<DevExpress.Xpf.DocumentViewer.BehaviorProvider>(x => x.ZoomFactor *= e.DeltaManipulation.Scale.X);
                    }
                    e.Handled = true;
                }
            }
        }

        public virtual void ProcessManipulationInertiaStarting(ManipulationInertiaStartingEventArgs e)
        {
        }

        public virtual void ProcessManipulationStarted(ManipulationStartedEventArgs e)
        {
            this.manipulationScale = 0.0;
            e.Handled = true;
        }

        public virtual void ProcessManipulationStarting(ManipulationStartingEventArgs e)
        {
            if (e.Manipulators.Count<IManipulator>() <= 2)
            {
                e.ManipulationContainer = this.PresenterDecorator;
                e.Handled = true;
            }
        }

        public virtual void ProcessStylusSystemGesture(StylusSystemGestureEventArgs e)
        {
        }

        protected DocumentPresenterControl Presenter { get; private set; }

        protected DocumentPresenterDecorator PresenterDecorator
        {
            get
            {
                Func<DocumentPresenterControl, DocumentPresenterDecorator> evaluator = <>c.<>9__5_0;
                if (<>c.<>9__5_0 == null)
                {
                    Func<DocumentPresenterControl, DocumentPresenterDecorator> local1 = <>c.<>9__5_0;
                    evaluator = <>c.<>9__5_0 = x => x.PresenterDecorator;
                }
                return this.Presenter.With<DocumentPresenterControl, DocumentPresenterDecorator>(evaluator);
            }
        }

        protected DevExpress.Xpf.DocumentViewer.BehaviorProvider BehaviorProvider
        {
            get
            {
                Func<DocumentPresenterControl, DevExpress.Xpf.DocumentViewer.BehaviorProvider> evaluator = <>c.<>9__7_0;
                if (<>c.<>9__7_0 == null)
                {
                    Func<DocumentPresenterControl, DevExpress.Xpf.DocumentViewer.BehaviorProvider> local1 = <>c.<>9__7_0;
                    evaluator = <>c.<>9__7_0 = x => x.BehaviorProvider;
                }
                return this.Presenter.With<DocumentPresenterControl, DevExpress.Xpf.DocumentViewer.BehaviorProvider>(evaluator);
            }
        }

        protected virtual double MinManipulationScale =>
            0.1;

        protected bool LockManipulationZooming { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TouchController.<>c <>9 = new TouchController.<>c();
            public static Func<DocumentPresenterControl, DocumentPresenterDecorator> <>9__5_0;
            public static Func<DocumentPresenterControl, BehaviorProvider> <>9__7_0;

            internal BehaviorProvider <get_BehaviorProvider>b__7_0(DocumentPresenterControl x) => 
                x.BehaviorProvider;

            internal DocumentPresenterDecorator <get_PresenterDecorator>b__5_0(DocumentPresenterControl x) => 
                x.PresenterDecorator;
        }
    }
}

