namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows;

    internal class VisualStateController : VisualStateControllerBase<FrameworkElement>, IDisposable
    {
        public const string NormalState = "Normal";
        public const string PressedState = "Pressed";
        public const string MouseOverState = "MouseOver";
        public const string DisabledState = "Disabled";
        public static readonly DependencyProperty VisualStateControllerProperty;
        private List<FrameworkElement> VisualChildren = new List<FrameworkElement>();

        static VisualStateController()
        {
            new DependencyPropertyRegistrator<VisualStateController>().RegisterAttached<VisualStateController>("VisualStateController", ref VisualStateControllerProperty, null, null, null);
        }

        public VisualStateController(FrameworkElement owner)
        {
            base.Attach(owner);
        }

        internal void Add(FrameworkElement child)
        {
            this.VisualChildren.Add(child);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            base.SetIsEnabled(base.Owner.IsEnabled);
            this.VisualChildren.Add(base.Owner);
        }

        private void owner_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            base.SetIsEnabled((bool) e.NewValue);
            this.UpdateVisualState(true);
        }

        internal void Remove(FrameworkElement child)
        {
            this.VisualChildren.Remove(child);
        }

        protected override void Subscribe()
        {
            base.Subscribe();
            base.Owner.IsEnabledChanged += new DependencyPropertyChangedEventHandler(this.owner_IsEnabledChanged);
        }

        void IDisposable.Dispose()
        {
            this.VisualChildren.Clear();
        }

        protected override void Unsubscribe()
        {
            base.Owner.IsEnabledChanged -= new DependencyPropertyChangedEventHandler(this.owner_IsEnabledChanged);
            base.Unsubscribe();
        }

        private void UpdateChildVisualState(FrameworkElement child, bool useTransitions)
        {
            ISupportVisualStates states = child as ISupportVisualStates;
            if (states != null)
            {
                states.UpdateVisualState();
            }
            if (!base.IsEnabled)
            {
                this.UpdateVisualStateCore(child, "Disabled", false);
            }
            else
            {
                this.UpdateVisualStateCore(child, "Normal", useTransitions);
                if (base.IsMousePressed)
                {
                    this.UpdateVisualStateCore(child, "Pressed", useTransitions);
                }
                else if (base.IsMouseOver)
                {
                    this.UpdateVisualStateCore(child, "MouseOver", useTransitions);
                }
            }
        }

        public void UpdateState()
        {
            this.UpdateVisualState(false);
        }

        protected override void UpdateVisualState(bool useTransitions = false)
        {
            foreach (FrameworkElement element in this.VisualChildren)
            {
                this.UpdateChildVisualState(element, useTransitions);
            }
        }

        private void UpdateVisualStateCore(FrameworkElement child, string stateName, bool useTransitions)
        {
            VisualStateManager.GoToState(child, stateName, useTransitions);
        }
    }
}

