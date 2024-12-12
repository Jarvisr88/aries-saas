namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Interactivity.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DependencyPropertyBehavior : Behavior<DependencyObject>
    {
        private const BindingFlags bindingFlags = (BindingFlags.Public | BindingFlags.Instance);
        public static readonly DependencyProperty BindingProperty;
        private EventTriggerEventSubscriber EventHelper;

        static DependencyPropertyBehavior()
        {
            FrameworkPropertyMetadata defaultMetadata = new FrameworkPropertyMetadata(null, (d, e) => ((DependencyPropertyBehavior) d).OnBindingPropertyChanged());
            defaultMetadata.BindsTwoWayByDefault = true;
            BindingProperty = DependencyProperty.RegisterAttached("Binding", typeof(object), typeof(DependencyPropertyBehavior), defaultMetadata);
        }

        public DependencyPropertyBehavior()
        {
            this.EventHelper = new EventTriggerEventSubscriber(new Action<object, object>(this.OnEvent));
        }

        protected virtual object GetAssociatedObjectForName(string name)
        {
            object obj3;
            if (base.AssociatedObject == null)
            {
                return null;
            }
            char[] separator = new char[] { '.' };
            string[] source = name.Split(separator);
            object associatedObject = base.AssociatedObject;
            using (IEnumerator<string> enumerator = source.Take<string>((source.Length - 1)).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        string current = enumerator.Current;
                        if (associatedObject != null)
                        {
                            associatedObject = associatedObject.GetType().GetProperty(current, BindingFlags.Public | BindingFlags.Instance).GetValue(associatedObject, null);
                            continue;
                        }
                        obj3 = null;
                    }
                    else
                    {
                        return associatedObject;
                    }
                    break;
                }
            }
            return obj3;
        }

        protected virtual string GetShortName(string name)
        {
            char[] separator = new char[] { '.' };
            return name.Split(separator).Last<string>();
        }

        protected override void OnAttached()
        {
            if (this.Binding != null)
            {
                this.OnBindingPropertyChanged();
            }
            if (!string.IsNullOrEmpty(this.EventName))
            {
                this.EventHelper.UnsubscribeFromEvent(this.EventAssociatedObject, this.ShortEventName);
                this.EventHelper.SubscribeToEvent(this.EventAssociatedObject, this.ShortEventName);
            }
        }

        private void OnBindingPropertyChanged()
        {
            if (((this.PropertyAssociatedObject != null) && !Equals(this.PropertyInfo.GetValue(this.PropertyAssociatedObject, null), this.Binding)) && this.PropertyInfo.CanWrite)
            {
                this.PropertyInfo.SetValue(this.PropertyAssociatedObject, Convert.ChangeType(this.Binding, this.PropertyInfo.PropertyType, null), null);
            }
        }

        protected override void OnDetaching()
        {
            if (!string.IsNullOrEmpty(this.EventName))
            {
                this.EventHelper.UnsubscribeFromEvent(this.EventAssociatedObject, this.ShortEventName);
            }
        }

        private void OnEvent(object sender, object eventArgs)
        {
            this.Binding = (this.PropertyAssociatedObject != null) ? this.PropertyInfo.GetValue(this.PropertyAssociatedObject, null) : null;
        }

        public object Binding
        {
            get => 
                base.GetValue(BindingProperty);
            set => 
                base.SetValue(BindingProperty, value);
        }

        public string PropertyName { get; set; }

        public string EventName { get; set; }

        protected System.Reflection.PropertyInfo PropertyInfo =>
            this.PropertyAssociatedObject.GetType().GetProperty(this.ShortPropertyName, BindingFlags.Public | BindingFlags.Instance);

        protected object PropertyAssociatedObject =>
            this.GetAssociatedObjectForName(this.PropertyName);

        protected object EventAssociatedObject =>
            this.GetAssociatedObjectForName(this.EventName);

        protected string ShortEventName =>
            this.GetShortName(this.EventName);

        protected string ShortPropertyName =>
            this.GetShortName(this.PropertyName);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DependencyPropertyBehavior.<>c <>9 = new DependencyPropertyBehavior.<>c();

            internal void <.cctor>b__31_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DependencyPropertyBehavior) d).OnBindingPropertyChanged();
            }
        }
    }
}

