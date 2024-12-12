namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using System;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PdfControllerBehavior : ControllerBehavior
    {
        protected override void OnActionsChanged(ObservableCollection<IControllerAction> oldValue)
        {
            base.OnActionsChanged(oldValue);
            oldValue.Do<ObservableCollection<IControllerAction>>(delegate (ObservableCollection<IControllerAction> x) {
                x.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
            });
            Func<ObservableCollection<IControllerAction>, bool> evaluator = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                Func<ObservableCollection<IControllerAction>, bool> local1 = <>c.<>9__0_1;
                evaluator = <>c.<>9__0_1 = x => x.Count > 0;
            }
            if (base.Actions.Return<ObservableCollection<IControllerAction>, bool>(evaluator, <>c.<>9__0_2 ??= () => false))
            {
                Func<DependencyObject, FrameworkElement> func2 = <>c.<>9__0_3;
                if (<>c.<>9__0_3 == null)
                {
                    Func<DependencyObject, FrameworkElement> local3 = <>c.<>9__0_3;
                    func2 = <>c.<>9__0_3 = x => x as FrameworkElement;
                }
                Func<FrameworkElement, bool> func3 = <>c.<>9__0_4;
                if (<>c.<>9__0_4 == null)
                {
                    Func<FrameworkElement, bool> local4 = <>c.<>9__0_4;
                    func3 = <>c.<>9__0_4 = x => x.IsLoaded;
                }
                if (base.AssociatedObject.With<DependencyObject, FrameworkElement>(func2).Return<FrameworkElement, bool>(func3, <>c.<>9__0_5 ??= () => false))
                {
                    base.Execute(null);
                }
            }
            base.Actions.Do<ObservableCollection<IControllerAction>>(delegate (ObservableCollection<IControllerAction> x) {
                x.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnCollectionChanged);
            });
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.Execute(null);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfControllerBehavior.<>c <>9 = new PdfControllerBehavior.<>c();
            public static Func<ObservableCollection<IControllerAction>, bool> <>9__0_1;
            public static Func<bool> <>9__0_2;
            public static Func<DependencyObject, FrameworkElement> <>9__0_3;
            public static Func<FrameworkElement, bool> <>9__0_4;
            public static Func<bool> <>9__0_5;

            internal bool <OnActionsChanged>b__0_1(ObservableCollection<IControllerAction> x) => 
                x.Count > 0;

            internal bool <OnActionsChanged>b__0_2() => 
                false;

            internal FrameworkElement <OnActionsChanged>b__0_3(DependencyObject x) => 
                x as FrameworkElement;

            internal bool <OnActionsChanged>b__0_4(FrameworkElement x) => 
                x.IsLoaded;

            internal bool <OnActionsChanged>b__0_5() => 
                false;
        }
    }
}

