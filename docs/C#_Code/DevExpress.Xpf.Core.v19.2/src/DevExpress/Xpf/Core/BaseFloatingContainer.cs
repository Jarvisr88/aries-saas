namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Content")]
    public abstract class BaseFloatingContainer : FrameworkElement
    {
        public static readonly DependencyProperty FloatLocationProperty;
        public static readonly DependencyProperty FloatSizeProperty;
        public static readonly DependencyProperty IsOpenProperty;
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty ContainerTemplateProperty;
        public static readonly DependencyProperty ShowContentOnlyProperty;
        public static readonly DependencyProperty OwnerProperty;
        private int lockUpdateCounter;
        private bool fHierarchyCreated;
        protected FlowDirection storedFlowDirrection;

        static BaseFloatingContainer()
        {
            Type ownerType = typeof(BaseFloatingContainer);
            FloatLocationProperty = DependencyProperty.Register("FloatLocation", typeof(Point), ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(BaseFloatingContainer.OnFloatingBoundsChanged)));
            FloatSizeProperty = DependencyProperty.Register("FloatSize", typeof(Size), ownerType, new FrameworkPropertyMetadata(new PropertyChangedCallback(BaseFloatingContainer.OnFloatingBoundsChanged)));
            IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(BaseFloatingContainer.OnIsOpenChanged)));
            ContentProperty = DependencyProperty.Register("Content", typeof(object), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(BaseFloatingContainer.OnContentChanged)));
            ContainerTemplateProperty = DependencyProperty.Register("ContainerTemplate", typeof(DataTemplate), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (d, e) => ((BaseFloatingContainer) d).OnContainerTemplateChanged((DataTemplate) e.NewValue)));
            ShowContentOnlyProperty = DependencyProperty.Register("ShowContentOnly", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((BaseFloatingContainer) d).UpdatePresenterContentTemplate()));
            OwnerProperty = DependencyProperty.Register("Owner", typeof(FrameworkElement), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        }

        public BaseFloatingContainer()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
        }

        protected abstract void AddDecoratorToContentContainer(NonLogicalDecorator decorator);
        public void BeginUpdate()
        {
            this.lockUpdateCounter++;
        }

        public void CheckBoundsInContainer()
        {
            this.OnFloatingBoundsChanged(new Rect(this.FloatLocation, this.FloatSize));
        }

        public void CheckIsOpen()
        {
            this.OnIsOpenChanged(this.IsOpen);
        }

        protected abstract UIElement CreateContentContainer();
        protected virtual ManagedContentPresenter CreateContentPresenter() => 
            new ManagedContentPresenter(this);

        protected virtual NonLogicalDecorator CreateNonLogicalDecorator() => 
            new NonLogicalDecorator();

        public void EndUpdate()
        {
            int num = this.lockUpdateCounter - 1;
            this.lockUpdateCounter = num;
            if (num == 0)
            {
                this.UpdateContainer(this.Content);
            }
        }

        protected void EnsureContainerHierarchy(FrameworkElement owner)
        {
            if (owner != null)
            {
                this.storedFlowDirrection = owner.FlowDirection;
            }
            if (!this.fHierarchyCreated)
            {
                this.fHierarchyCreated = true;
                this.ContentContainer = this.CreateContentContainer();
                this.Decorator = this.CreateNonLogicalDecorator();
                this.Presenter = this.CreateContentPresenter();
                this.AddDecoratorToContentContainer(this.Decorator);
                this.Decorator.Child = this.Presenter;
                base.AddLogicalChild(this.Presenter);
                this.OnHierarchyCreated();
            }
        }

        protected virtual void NotifyIsOpenChanged(bool isOpen)
        {
        }

        protected virtual void OnContainerTemplateChanged(DataTemplate newValue)
        {
            this.UpdatePresenterContentTemplate();
        }

        protected virtual void OnContentChanged(object content)
        {
            this.UpdateContainer(content);
        }

        private static void OnContentChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            ((BaseFloatingContainer) dObj).OnContentChanged(e.NewValue);
        }

        protected abstract void OnContentUpdated(object content, FrameworkElement owner);
        protected virtual void OnFloatingBoundsChanged(Rect bounds)
        {
            if (this.IsAlive)
            {
                this.UpdateFloatingBoundsCore(bounds);
            }
        }

        private static void OnFloatingBoundsChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            BaseFloatingContainer container = dObj as BaseFloatingContainer;
            container.OnFloatingBoundsChanged(new Rect(container.FloatLocation, container.FloatSize));
        }

        protected virtual void OnHierarchyCreated()
        {
        }

        protected virtual void OnIsOpenChanged(bool isOpen)
        {
            if (this.IsAlive)
            {
                this.UpdateIsOpenCore(isOpen);
            }
        }

        private static void OnIsOpenChanged(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
        {
            BaseFloatingContainer container = dObj as BaseFloatingContainer;
            container.OnIsOpenChanged((bool) e.NewValue);
            container.NotifyIsOpenChanged((bool) e.NewValue);
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.CheckIsOpen();
            this.CheckBoundsInContainer();
        }

        protected virtual void OnUnloaded(object sender, RoutedEventArgs e)
        {
        }

        protected void UpdateContainer(object content)
        {
            if (!this.IsUpdateLocked)
            {
                this.EnsureContainerHierarchy(this.Owner);
                this.Presenter.Content = content;
                this.UpdatePresenterContentTemplate();
                if ((this.Owner != null) && this.Owner.IsLoaded)
                {
                    this.CheckIsOpen();
                    this.CheckBoundsInContainer();
                }
                this.OnContentUpdated(content, this.Owner);
            }
        }

        protected abstract void UpdateFloatingBoundsCore(Rect bounds);
        protected abstract void UpdateIsOpenCore(bool isOpen);
        protected virtual void UpdatePresenterContentTemplate()
        {
            if (this.Presenter != null)
            {
                this.Presenter.ContentTemplate = this.ShowContentOnly ? null : this.ContainerTemplate;
            }
        }

        public Point FloatLocation
        {
            get => 
                (Point) base.GetValue(FloatLocationProperty);
            set => 
                base.SetValue(FloatLocationProperty, value);
        }

        public Size FloatSize
        {
            get => 
                (Size) base.GetValue(FloatSizeProperty);
            set => 
                base.SetValue(FloatSizeProperty, value);
        }

        public bool IsOpen
        {
            get => 
                (bool) base.GetValue(IsOpenProperty);
            set => 
                base.SetValue(IsOpenProperty, value);
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        public DataTemplate ContainerTemplate
        {
            get => 
                (DataTemplate) base.GetValue(ContainerTemplateProperty);
            set => 
                base.SetValue(ContainerTemplateProperty, value);
        }

        public bool ShowContentOnly
        {
            get => 
                (bool) base.GetValue(ShowContentOnlyProperty);
            set => 
                base.SetValue(ShowContentOnlyProperty, value);
        }

        public FrameworkElement Owner
        {
            get => 
                (FrameworkElement) base.GetValue(OwnerProperty);
            set => 
                base.SetValue(OwnerProperty, value);
        }

        public bool UseActiveStateOnly { get; set; }

        public bool IsUpdateLocked =>
            this.lockUpdateCounter > 0;

        protected abstract bool IsAlive { get; }

        protected ManagedContentPresenter Presenter { get; private set; }

        protected NonLogicalDecorator Decorator { get; private set; }

        protected UIElement ContentContainer { get; private set; }

        protected override IEnumerator LogicalChildren
        {
            get
            {
                List<object> list = new List<object>();
                if (this.Presenter != null)
                {
                    list.Add(this.Presenter);
                }
                return list.GetEnumerator();
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseFloatingContainer.<>c <>9 = new BaseFloatingContainer.<>c();

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseFloatingContainer) d).OnContainerTemplateChanged((DataTemplate) e.NewValue);
            }

            internal void <.cctor>b__7_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((BaseFloatingContainer) d).UpdatePresenterContentTemplate();
            }
        }

        public class ManagedContentPresenter : ContentPresenter
        {
            public ManagedContentPresenter(BaseFloatingContainer container)
            {
                this.Container = container;
            }

            protected internal virtual void UpdatePresenter()
            {
            }

            public BaseFloatingContainer Container { get; private set; }
        }
    }
}

