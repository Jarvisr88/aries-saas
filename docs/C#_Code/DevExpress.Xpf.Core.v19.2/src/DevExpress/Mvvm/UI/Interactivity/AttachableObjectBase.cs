namespace DevExpress.Mvvm.UI.Interactivity
{
    using DevExpress.Mvvm.UI.Interactivity.Internal;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media.Animation;

    public abstract class AttachableObjectBase : Animatable, IAttachableObject, INotifyPropertyChanged
    {
        private Type associatedType;
        private DependencyObject associatedObject;
        private PropertyChangedEventHandler propertyChanged;

        internal event EventHandler AssociatedObjectChanged;

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                this.propertyChanged += value;
            }
            remove
            {
                this.propertyChanged -= value;
            }
        }

        internal AttachableObjectBase(Type type)
        {
            this.associatedType = type;
        }

        public void Attach(DependencyObject obj)
        {
            if (!ReferenceEquals(this.AssociatedObject, obj))
            {
                if (this.AssociatedObject != null)
                {
                    throw new InvalidOperationException("Cannot attach this object twice");
                }
                Type c = obj.GetType();
                if (!this.AssociatedType.IsAssignableFrom(c))
                {
                    throw new InvalidOperationException($"This object cannot be attached to a {c.ToString()} object");
                }
                this.AssociatedObject = obj;
                this.IsAttached = true;
                this.OnAttached();
            }
        }

        protected override Freezable CreateInstanceCore() => 
            (Freezable) Activator.CreateInstance(base.GetType());

        public void Detach()
        {
            this.OnDetaching();
            this.AssociatedObject = null;
            this.IsAttached = false;
        }

        protected override bool FreezeCore(bool isChecking) => 
            false;

        protected void NotifyChanged()
        {
            base.WritePostscript();
        }

        protected virtual void OnAttached()
        {
        }

        protected virtual void OnDetaching()
        {
        }

        protected void RaisePropertyChanged(string name)
        {
            if (this.propertyChanged != null)
            {
                this.propertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        protected void VerifyRead()
        {
            base.ReadPreamble();
        }

        protected void VerifyWrite()
        {
            base.WritePreamble();
        }

        public bool IsAttached { get; private set; }

        internal bool _AllowAttachInDesignMode =>
            this.AllowAttachInDesignMode;

        protected virtual bool AllowAttachInDesignMode =>
            InteractionHelper.GetBehaviorInDesignMode(this) == InteractionBehaviorInDesignMode.AsWellAsNotInDesignMode;

        protected virtual Type AssociatedType
        {
            get
            {
                this.VerifyRead();
                return this.associatedType;
            }
        }

        public DependencyObject AssociatedObject
        {
            get
            {
                this.VerifyRead();
                return this.associatedObject;
            }
            private set
            {
                this.VerifyRead();
                if (!ReferenceEquals(this.associatedObject, value))
                {
                    this.VerifyWrite();
                    this.associatedObject = value;
                    this.NotifyChanged();
                    EventHandler associatedObjectChanged = this.AssociatedObjectChanged;
                    if (associatedObjectChanged != null)
                    {
                        associatedObjectChanged(this, EventArgs.Empty);
                    }
                    this.RaisePropertyChanged("AssociatedObject");
                }
            }
        }
    }
}

