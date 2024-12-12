namespace DevExpress.Xpf.Docking.VisualElements
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    internal class VisualStateControllerBase<T> where T: FrameworkElement
    {
        protected void Attach(T owner)
        {
            this.Owner = owner;
            this.Subscribe();
            this.OnAttached();
        }

        protected void Detach(T owner)
        {
            this.Unsubscribe();
            T local = default(T);
            this.Owner = local;
        }

        protected virtual void OnAttached()
        {
        }

        private void owner_Loaded(object sender, RoutedEventArgs e)
        {
            this.UpdateVisualState(false);
        }

        private void Owner_LostMouseCapture(object sender, MouseEventArgs e)
        {
            if (!this.Owner.IsMouseOver)
            {
                this.IsMouseOver = false;
            }
            this.IsMousePressed = false;
            this.UpdateVisualState(true);
        }

        private void owner_MouseEnter(object sender, MouseEventArgs e)
        {
            this.IsMouseOver = true;
            this.UpdateVisualState(true);
        }

        private void owner_MouseLeave(object sender, MouseEventArgs e)
        {
            this.IsMouseOver = false;
            this.UpdateVisualState(true);
        }

        private void owner_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsMousePressed = true;
            this.Owner.CaptureMouse();
            this.UpdateVisualState(true);
        }

        private void owner_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Owner.ReleaseMouseCapture();
            this.IsMousePressed = false;
            this.UpdateVisualState(true);
        }

        protected void SetIsEnabled(bool isEnabled)
        {
            this.IsEnabled = isEnabled;
        }

        protected virtual void Subscribe()
        {
            this.Owner.MouseEnter += new MouseEventHandler(this.owner_MouseEnter);
            this.Owner.MouseLeave += new MouseEventHandler(this.owner_MouseLeave);
            this.Owner.MouseLeftButtonDown += new MouseButtonEventHandler(this.owner_MouseLeftButtonDown);
            this.Owner.MouseLeftButtonUp += new MouseButtonEventHandler(this.owner_MouseLeftButtonUp);
            this.Owner.LostMouseCapture += new MouseEventHandler(this.Owner_LostMouseCapture);
            this.Owner.Loaded += new RoutedEventHandler(this.owner_Loaded);
        }

        protected virtual void Unsubscribe()
        {
            this.Owner.MouseEnter -= new MouseEventHandler(this.owner_MouseEnter);
            this.Owner.MouseLeave -= new MouseEventHandler(this.owner_MouseLeave);
            this.Owner.MouseLeftButtonDown -= new MouseButtonEventHandler(this.owner_MouseLeftButtonDown);
            this.Owner.MouseLeftButtonUp -= new MouseButtonEventHandler(this.owner_MouseLeftButtonUp);
            this.Owner.LostMouseCapture -= new MouseEventHandler(this.Owner_LostMouseCapture);
            this.Owner.Loaded -= new RoutedEventHandler(this.owner_Loaded);
        }

        protected virtual void UpdateVisualState(bool useTransitions = false)
        {
        }

        protected T Owner { get; private set; }

        protected bool IsMousePressed { get; private set; }

        protected bool IsMouseOver { get; private set; }

        protected bool IsEnabled { get; private set; }
    }
}

