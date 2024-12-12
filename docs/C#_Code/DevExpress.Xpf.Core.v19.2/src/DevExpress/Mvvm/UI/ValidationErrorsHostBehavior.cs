namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.UI.Interactivity;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TargetType(typeof(FrameworkElement))]
    public class ValidationErrorsHostBehavior : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty ErrorsProperty;
        public static readonly DependencyProperty HasErrorsProperty;

        static ValidationErrorsHostBehavior()
        {
            ErrorsProperty = DependencyProperty.Register("Errors", typeof(IList<ValidationError>), typeof(ValidationErrorsHostBehavior), new PropertyMetadata(null, (d, e) => ((ValidationErrorsHostBehavior) d).OnErrorsChanged(e), (CoerceValueCallback) ((d, v) => (v ?? new ObservableCollection<ValidationError>()))));
            HasErrorsProperty = DependencyProperty.Register("HasErrors", typeof(bool), typeof(ValidationErrorsHostBehavior), new PropertyMetadata(false));
        }

        public ValidationErrorsHostBehavior()
        {
            this.Errors = new ObservableCollection<ValidationError>();
        }

        private void OnAssociatedObjectError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                this.Errors.Add(e.Error);
            }
            else
            {
                this.Errors.Remove(e.Error);
            }
            e.Handled = true;
            this.UpdateHasErrors();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            Validation.AddErrorHandler(base.AssociatedObject, new EventHandler<ValidationErrorEventArgs>(this.OnAssociatedObjectError));
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            Validation.RemoveErrorHandler(base.AssociatedObject, new EventHandler<ValidationErrorEventArgs>(this.OnAssociatedObjectError));
        }

        protected virtual void OnErrorsChanged(DependencyPropertyChangedEventArgs e)
        {
            INotifyCollectionChanged oldValue = e.OldValue as INotifyCollectionChanged;
            INotifyCollectionChanged newValue = e.NewValue as INotifyCollectionChanged;
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnErrorsCollectionChanged);
            }
            if (newValue != null)
            {
                newValue.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnErrorsCollectionChanged);
            }
            this.UpdateHasErrors();
        }

        private void OnErrorsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateHasErrors();
        }

        protected void UpdateHasErrors()
        {
            this.HasErrors = this.Errors.Count != 0;
        }

        public IList<ValidationError> Errors
        {
            get => 
                (IList<ValidationError>) base.GetValue(ErrorsProperty);
            set => 
                base.SetValue(ErrorsProperty, value);
        }

        public bool HasErrors
        {
            get => 
                (bool) base.GetValue(HasErrorsProperty);
            set => 
                base.SetValue(HasErrorsProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ValidationErrorsHostBehavior.<>c <>9 = new ValidationErrorsHostBehavior.<>c();

            internal void <.cctor>b__15_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((ValidationErrorsHostBehavior) d).OnErrorsChanged(e);
            }

            internal object <.cctor>b__15_1(DependencyObject d, object v) => 
                v ?? new ObservableCollection<ValidationError>();
        }
    }
}

