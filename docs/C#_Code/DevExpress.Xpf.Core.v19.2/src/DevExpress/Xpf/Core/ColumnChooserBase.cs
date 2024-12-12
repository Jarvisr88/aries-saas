namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Interop;

    public class ColumnChooserBase : DependencyObject, IColumnChooser
    {
        public static readonly DependencyProperty CaptionProperty = DependencyPropertyManager.Register("Caption", typeof(string), typeof(ColumnChooserBase), new PropertyMetadata(string.Empty));
        private bool isStateApplied;
        private FloatingContainer container;
        private ILogicalOwner owner;

        public ColumnChooserBase(ILogicalOwner owner)
        {
            if (!(owner is Control))
            {
                throw new ArgumentException("owner should be a Control class descendant");
            }
            this.owner = owner;
            this.container = FloatingContainerFactory.Create(FloatingContainerFactory.CheckPopupHost((Control) this.Owner));
            this.container.UseActiveStateOnly = true;
            this.Owner.AddChild(this.Container);
            this.Container.Owner = (Control) this.Owner;
            this.Container.LogicalOwner = (Control) this.Owner;
            this.Container.Content = this.CreateContentControl();
            this.Container.Hidden += new RoutedEventHandler(this.OnContainerHidden);
            Binding binding = new Binding("Caption");
            binding.Source = this;
            BindingOperations.SetBinding(this.Container, FloatingContainer.CaptionProperty, binding);
        }

        public void ApplyState(IColumnChooserState istate)
        {
            istate ??= DefaultColumnChooserState.DefaultState;
            DefaultColumnChooserState state = istate as DefaultColumnChooserState;
            if (state != null)
            {
                if (!this.Owner.IsLoaded)
                {
                    if (this.ApplyStateParameter == null)
                    {
                        this.Owner.Loaded += new RoutedEventHandler(this.OnApplyingStateOwnerLoaded);
                    }
                    this.ApplyStateParameter = state;
                }
                else
                {
                    this.isStateApplied = true;
                    Size size = state.Size;
                    double minWidth = state.MinWidth;
                    double minHeight = state.MinHeight;
                    if (ThemeHelper.IsTouchTheme(this.Container))
                    {
                        if (state.Size == ((Size) DefaultColumnChooserState.SizeProperty.GetDefaultValue()))
                        {
                            size = new Size(280.0, 360.0);
                        }
                        if (state.MinWidth == ((double) DefaultColumnChooserState.MinWidthProperty.GetDefaultValue()))
                        {
                            minWidth = 260.0;
                        }
                        if (state.MinHeight == ((double) DefaultColumnChooserState.MinHeightProperty.GetDefaultValue()))
                        {
                            minHeight = 260.0;
                        }
                    }
                    this.Container.FloatSize = size;
                    this.Container.FloatLocation = this.UpdateContainerLocation(new Rect(new Point(double.IsNaN(state.Location.X) ? (this.Owner.ActualWidth - size.Width) : state.Location.X, double.IsNaN(state.Location.Y) ? (this.Owner.ActualHeight - size.Height) : state.Location.Y), size));
                    this.Container.MinWidth = minWidth;
                    this.Container.MaxWidth = state.MaxWidth;
                    this.Container.MinHeight = minHeight;
                    this.Container.MaxHeight = state.MaxHeight;
                    if (this.ApplyStateParameter != null)
                    {
                        this.ApplyStateParameter = state;
                    }
                }
            }
        }

        private Point CheckLocationForXBAP(Point location)
        {
            location.X = (location.X < 0.0) ? 0.0 : location.X;
            location.Y = (location.Y < 0.0) ? 0.0 : location.Y;
            return location;
        }

        protected virtual Control CreateContentControl() => 
            new Control();

        public void Destroy()
        {
            this.Destroy(false);
        }

        protected void Destroy(bool force)
        {
            this.Owner.RemoveChild(this.Container);
            this.DestroyContentControl();
            if (this.IsFullDestoy | force)
            {
                this.Container.Close();
                this.IsFullDestoy = false;
            }
        }

        protected virtual void DestroyContentControl()
        {
        }

        public virtual void Hide()
        {
            this.Container.IsOpen = false;
        }

        protected virtual void OnApplyingStateOwnerLoaded(object sender, RoutedEventArgs e)
        {
            this.ApplyState(this.ApplyStateParameter);
            this.Owner.Loaded -= new RoutedEventHandler(this.OnApplyingStateOwnerLoaded);
            this.ApplyStateParameter = null;
        }

        protected virtual void OnContainerHidden(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnShowingOwnerLoaded(object sender, RoutedEventArgs e)
        {
            this.Show();
            this.Owner.Loaded -= new RoutedEventHandler(this.OnShowingOwnerLoaded);
        }

        public void SaveState(IColumnChooserState istate)
        {
            if (this.isStateApplied)
            {
                DefaultColumnChooserState state = istate as DefaultColumnChooserState;
                if (state != null)
                {
                    state.Location = this.Container.FloatLocation;
                    state.Size = this.Container.FloatSize;
                }
            }
        }

        public virtual void Show()
        {
            if (!this.Owner.IsLoaded)
            {
                this.Owner.Loaded += new RoutedEventHandler(this.OnShowingOwnerLoaded);
            }
            else
            {
                this.Container.IsOpen = true;
                this.IsFullDestoy = true;
            }
        }

        internal Point UpdateContainerLocation(Rect rect)
        {
            if (BrowserInteropHelper.IsBrowserHosted)
            {
                return this.CheckLocationForXBAP(rect.Location);
            }
            if ((this.container != null) && (this.container.FlowDirection == FlowDirection.RightToLeft))
            {
                rect.X += rect.Width;
            }
            rect.Location = ScreenHelper.GetScreenLocation(rect.Location, (FrameworkElement) this.Owner);
            ScreenHelper.UpdateContainerLocation(rect);
            return ScreenHelper.GetClientLocation(ScreenHelper.UpdateContainerLocation(rect), (FrameworkElement) this.Owner);
        }

        public string Caption
        {
            get => 
                (string) base.GetValue(CaptionProperty);
            set => 
                base.SetValue(CaptionProperty, value);
        }

        public FloatingContainer Container =>
            this.container;

        protected virtual ILogicalOwner Owner =>
            this.owner;

        protected IColumnChooserState ApplyStateParameter { get; private set; }

        private bool IsFullDestoy { get; set; }

        public UIElement TopContainer =>
            (UIElement) this.Container.Content;
    }
}

