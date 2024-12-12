namespace DevExpress.Mvvm.UI.Interactivity
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI.Interactivity.Internal;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Data;

    public class EventTriggerBase<T> : TriggerBase<T> where T: DependencyObject
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty EventNameProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty EventProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SourceNameProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty SourceObjectProperty;
        private object source;
        private EventTriggerEventSubscriber EventHelper;

        static EventTriggerBase()
        {
            EventTriggerBase<T>.EventNameProperty = DependencyProperty.Register("EventName", typeof(string), typeof(EventTriggerBase<T>), new PropertyMetadata("Loaded", (d, e) => ((EventTriggerBase<T>) d).OnEventNameChanged((string) e.OldValue, (string) e.NewValue)));
            EventTriggerBase<T>.EventProperty = DependencyProperty.Register("Event", typeof(RoutedEvent), typeof(EventTriggerBase<T>), new PropertyMetadata(null, (d, e) => ((EventTriggerBase<T>) d).OnEventChanged((RoutedEvent) e.OldValue, (RoutedEvent) e.NewValue)));
            EventTriggerBase<T>.SourceNameProperty = DependencyProperty.Register("SourceName", typeof(string), typeof(EventTriggerBase<T>), new PropertyMetadata(null, (d, e) => ((EventTriggerBase<T>) d).OnSourceNameChanged()));
            EventTriggerBase<T>.SourceObjectProperty = DependencyProperty.Register("SourceObject", typeof(object), typeof(EventTriggerBase<T>), new PropertyMetadata(null, (d, e) => ((EventTriggerBase<T>) d).OnSourceObjectChanged()));
        }

        public EventTriggerBase()
        {
            this.EventHelper = new EventTriggerEventSubscriber(new Action<object, object>(this.OnEvent));
        }

        private void AssociatedObjectLayoutUpdated(object sender, object e)
        {
            this.OnAssociatedObjectUpdated(sender, EventArgs.Empty);
        }

        private void AssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            this.OnAssociatedObjectUpdated(sender, EventArgs.Empty);
        }

        private void AssociatedObjectSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.OnAssociatedObjectUpdated(sender, EventArgs.Empty);
        }

        private static DependencyObject FindObject(DependencyObject root, string elementName, bool useVisualTree)
        {
            if (EventTriggerBase<T>.GetObjectName(root) == elementName)
            {
                return root;
            }
            DependencyObject obj2 = null;
            FrameworkElement treeRoot = root as FrameworkElement;
            FrameworkElement parent = treeRoot.Parent as FrameworkElement;
            FrameworkElement logicalTreeNode = parent ?? treeRoot;
            try
            {
                obj2 = LogicalTreeHelper.FindLogicalNode(logicalTreeNode, elementName);
            }
            catch
            {
            }
            if (obj2 != null)
            {
                return obj2;
            }
            FrameworkContentElement element4 = root as FrameworkContentElement;
            obj2 = (element4 != null) ? ((DependencyObject) element4.FindName(elementName)) : null;
            if (obj2 != null)
            {
                return obj2;
            }
            obj2 = (logicalTreeNode != null) ? ((DependencyObject) logicalTreeNode.FindName(elementName)) : null;
            if (obj2 != null)
            {
                return obj2;
            }
            if (useVisualTree)
            {
                obj2 = (parent != null) ? LayoutHelper.FindElementByName(parent, elementName) : null;
                if (obj2 != null)
                {
                    return obj2;
                }
                obj2 = (treeRoot != null) ? LayoutHelper.FindElementByName(treeRoot, elementName) : null;
                if (obj2 != null)
                {
                    return obj2;
                }
            }
            return null;
        }

        private static BindingExpression GetBindingExp(DependencyObject d, DependencyProperty dp) => 
            BindingOperations.GetBindingExpression(d, dp);

        private static string GetObjectName(object obj)
        {
            FrameworkElement element = obj as FrameworkElement;
            if (element != null)
            {
                return element.Name;
            }
            FrameworkContentElement element2 = obj as FrameworkContentElement;
            return element2?.Name;
        }

        private void OnAssociatedObjectUpdated(object sender, EventArgs e)
        {
            bool? useVisualTree = null;
            this.ResolveSource(false, useVisualTree);
            FrameworkElement associatedObject = base.AssociatedObject as FrameworkElement;
            if ((associatedObject != null) && (LayoutHelper.IsElementLoaded(associatedObject) || (this.Source != null)))
            {
                this.UnsubscribeAssociatedObject();
                if (this.Source == null)
                {
                    this.ResolveSource(false, true);
                }
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            this.EventHelper.UnsubscribeFromEvent(this.Source, this.Event);
            this.EventHelper.UnsubscribeFromEvent(this.Source, this.EventName);
            this.EventHelper.SubscribeToEvent(this.Source, this.EventName);
            this.EventHelper.SubscribeToEvent(this.Source, this.Event);
            bool? useVisualTree = null;
            this.ResolveSource(false, useVisualTree);
            base.Dispatcher.BeginInvoke(delegate {
                bool? nullable = null;
                base.ResolveSource(false, nullable);
            }, new object[0]);
            this.SubsribeAssociatedObject();
        }

        protected override void OnDetaching()
        {
            this.UnsubscribeAssociatedObject();
            this.EventHelper.UnsubscribeFromEvent(this.Source, this.EventName);
            this.EventHelper.UnsubscribeFromEvent(this.Source, this.Event);
            this.Source = null;
            base.OnDetaching();
        }

        protected virtual void OnEvent(object sender, object eventArgs)
        {
        }

        protected virtual void OnEventChanged(RoutedEvent oldRoutedEvent, RoutedEvent newRoutedEvent)
        {
            if (newRoutedEvent != null)
            {
                this.EventName = null;
            }
            if (base.IsAttached)
            {
                this.EventHelper.UnsubscribeFromEvent(this.Source, oldRoutedEvent);
                this.EventHelper.SubscribeToEvent(this.Source, newRoutedEvent);
            }
        }

        protected virtual void OnEventNameChanged(string oldEventName, string newEventName)
        {
            if (newEventName != null)
            {
                this.Event = null;
            }
            if (base.IsAttached)
            {
                this.EventHelper.UnsubscribeFromEvent(this.Source, oldEventName);
                this.EventHelper.SubscribeToEvent(this.Source, newEventName);
            }
        }

        protected virtual void OnSourceChanged(object oldSource, object newSource)
        {
            this.EventHelper.UnsubscribeFromEvent(oldSource, this.Event);
            this.EventHelper.UnsubscribeFromEvent(oldSource, this.EventName);
            this.EventHelper.SubscribeToEvent(newSource, this.EventName);
            this.EventHelper.SubscribeToEvent(newSource, this.Event);
        }

        private void OnSourceNameChanged()
        {
            bool? useVisualTree = null;
            this.ResolveSource(true, useVisualTree);
        }

        private void OnSourceObjectChanged()
        {
            bool? useVisualTree = null;
            this.ResolveSource(true, useVisualTree);
        }

        private void ResolveSource(bool forceResolving, bool? useVisualTree = new bool?())
        {
            if (((!ViewModelBase.IsInDesignMode || InteractionHelper.GetEnableBehaviorsInDesignTime(base.AssociatedObject)) && base.IsAttached) && ((this.Source == null) || forceResolving))
            {
                if (this.SourceObject != null)
                {
                    this.Source = this.SourceObject;
                }
                else
                {
                    bool? nullable = useVisualTree;
                    bool flag = (nullable != null) ? nullable.GetValueOrDefault() : false;
                    BindingExpression bindingExp = EventTriggerBase<T>.GetBindingExp(this, EventTriggerBase<T>.SourceObjectProperty);
                    if (bindingExp == null)
                    {
                        BindingExpression expression2 = EventTriggerBase<T>.GetBindingExp(this, EventTriggerBase<T>.SourceNameProperty);
                        if (string.IsNullOrEmpty(this.SourceName) && (expression2 == null))
                        {
                            this.Source = base.AssociatedObject;
                        }
                        else
                        {
                            this.Source = EventTriggerBase<T>.FindObject(base.AssociatedObject, this.SourceName, flag);
                        }
                    }
                    else
                    {
                        string elementName = null;
                        if (bindingExp.ParentBinding != null)
                        {
                            elementName = bindingExp.ParentBinding.ElementName;
                        }
                        if (!string.IsNullOrEmpty(elementName))
                        {
                            this.Source = EventTriggerBase<T>.FindObject(base.AssociatedObject, elementName, flag);
                        }
                    }
                }
            }
        }

        private void SubsribeAssociatedObject()
        {
            this.UnsubscribeAssociatedObject();
            FrameworkElement associatedObject = base.AssociatedObject as FrameworkElement;
            if (associatedObject != null)
            {
                associatedObject.Initialized += new EventHandler(this.OnAssociatedObjectUpdated);
                associatedObject.LayoutUpdated += new EventHandler(this.AssociatedObjectLayoutUpdated);
                associatedObject.SizeChanged += new SizeChangedEventHandler(this.AssociatedObjectSizeChanged);
                associatedObject.Loaded += new RoutedEventHandler(this.AssociatedObjectLoaded);
            }
            else
            {
                FrameworkContentElement element2 = base.AssociatedObject as FrameworkContentElement;
                if (element2 != null)
                {
                    element2.Initialized += new EventHandler(this.OnAssociatedObjectUpdated);
                    element2.Loaded += new RoutedEventHandler(this.OnAssociatedObjectUpdated);
                }
            }
        }

        private void UnsubscribeAssociatedObject()
        {
            FrameworkElement associatedObject = base.AssociatedObject as FrameworkElement;
            if (associatedObject != null)
            {
                associatedObject.Initialized -= new EventHandler(this.OnAssociatedObjectUpdated);
                associatedObject.LayoutUpdated -= new EventHandler(this.AssociatedObjectLayoutUpdated);
                associatedObject.SizeChanged -= new SizeChangedEventHandler(this.AssociatedObjectSizeChanged);
                associatedObject.Loaded -= new RoutedEventHandler(this.AssociatedObjectLoaded);
            }
            FrameworkContentElement element2 = base.AssociatedObject as FrameworkContentElement;
            if (element2 != null)
            {
                element2.Initialized -= new EventHandler(this.OnAssociatedObjectUpdated);
                element2.Loaded -= new RoutedEventHandler(this.OnAssociatedObjectUpdated);
            }
        }

        public string EventName
        {
            get => 
                (string) base.GetValue(EventTriggerBase<T>.EventNameProperty);
            set => 
                base.SetValue(EventTriggerBase<T>.EventNameProperty, value);
        }

        public RoutedEvent Event
        {
            get => 
                (RoutedEvent) base.GetValue(EventTriggerBase<T>.EventProperty);
            set => 
                base.SetValue(EventTriggerBase<T>.EventProperty, value);
        }

        public string SourceName
        {
            get => 
                (string) base.GetValue(EventTriggerBase<T>.SourceNameProperty);
            set => 
                base.SetValue(EventTriggerBase<T>.SourceNameProperty, value);
        }

        public object SourceObject
        {
            get => 
                base.GetValue(EventTriggerBase<T>.SourceObjectProperty);
            set => 
                base.SetValue(EventTriggerBase<T>.SourceObjectProperty, value);
        }

        public object Source
        {
            get
            {
                base.VerifyRead();
                return this.source;
            }
            private set
            {
                base.VerifyRead();
                if (this.source != value)
                {
                    base.VerifyWrite();
                    object source = this.source;
                    this.source = value;
                    base.NotifyChanged();
                    this.OnSourceChanged(source, this.source);
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EventTriggerBase<T>.<>c <>9;

            static <>c()
            {
                EventTriggerBase<T>.<>c.<>9 = new EventTriggerBase<T>.<>c();
            }

            internal void <.cctor>b__40_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EventTriggerBase<T>) d).OnEventNameChanged((string) e.OldValue, (string) e.NewValue);
            }

            internal void <.cctor>b__40_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EventTriggerBase<T>) d).OnEventChanged((RoutedEvent) e.OldValue, (RoutedEvent) e.NewValue);
            }

            internal void <.cctor>b__40_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EventTriggerBase<T>) d).OnSourceNameChanged();
            }

            internal void <.cctor>b__40_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((EventTriggerBase<T>) d).OnSourceObjectChanged();
            }
        }
    }
}

