namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [DXToolboxBrowsable(false)]
    public abstract class DockHintElement : psvControl
    {
        public static readonly DependencyProperty DockTypeProperty;
        public static readonly DependencyProperty ShowShadowProperty;

        static DockHintElement()
        {
            DependencyPropertyRegistrator<DockHintElement> registrator = new DependencyPropertyRegistrator<DockHintElement>();
            registrator.RegisterAttachedInherited<DockType>("DockType", ref DockTypeProperty, DockType.None, null, null);
            registrator.Register<bool>("ShowShadow", ref ShowShadowProperty, true, null, null);
        }

        public DockHintElement(DockVisualizerElement type)
        {
            this.Type = type;
        }

        public void Arrange(DockingHintAdornerBase adorner, DockingHintAdornerBase.ElementInfo info)
        {
            if (this.UpdateVisibility(adorner))
            {
                Rect finalRect = info.CalcPlacement(this.CalcBounds(adorner), VisualizerAdornerHelper.GetAdornerWindowIndent(adorner));
                if (!finalRect.IsEmpty)
                {
                    base.Arrange(finalRect);
                }
            }
        }

        protected abstract Rect CalcBounds(DockingHintAdornerBase adorner);
        private bool CalcHotTrack(DockHintButton button, DockHintButton hotButton) => 
            button.IsAvailable && ReferenceEquals(button, hotButton);

        protected abstract bool CalcVisibleState(DockingHintAdornerBase adorner);
        public static DockType GetDockType(DependencyObject obj) => 
            (DockType) obj.GetValue(DockTypeProperty);

        public static void SetDockType(DependencyObject obj, DockType value)
        {
            obj.SetValue(DockTypeProperty, value);
        }

        public virtual void UpdateAvailableState(DockingHintAdornerBase adorner)
        {
        }

        public virtual void UpdateAvailableState(bool dock, bool hide, bool fill)
        {
        }

        public virtual void UpdateEnabledState(DockingHintAdornerBase adorner)
        {
        }

        public virtual void UpdateHotTrack(DockHintButton hotButton)
        {
        }

        protected void UpdateHotTrack(DockHintButton button, DockHintButton hotButton)
        {
            if (button != null)
            {
                button.IsHot = this.CalcHotTrack(button, hotButton);
            }
        }

        public virtual void UpdateState(DockingHintAdornerBase adorner)
        {
        }

        protected bool UpdateVisibility(DockingHintAdornerBase adorner)
        {
            bool flag = this.CalcVisibleState(adorner);
            this.Visibility = flag ? Visibility.Visible : Visibility.Collapsed;
            return flag;
        }

        public bool ShowShadow
        {
            get => 
                (bool) base.GetValue(ShowShadowProperty);
            set => 
                base.SetValue(ShowShadowProperty, value);
        }

        public DockVisualizerElement Type { get; private set; }
    }
}

