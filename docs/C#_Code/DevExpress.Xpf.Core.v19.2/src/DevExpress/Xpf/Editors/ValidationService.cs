namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;

    public class ValidationService : DependencyObject
    {
        public static readonly DependencyProperty IsValidationContainerProperty;
        private static readonly DependencyPropertyKey ValidationServicePropertyKey;
        public static readonly DependencyProperty ValidationServiceProperty;
        private static readonly DependencyPropertyKey HasValidationErrorPropertyKey;
        public static readonly DependencyProperty HasValidationErrorProperty;
        private static readonly DependencyPropertyKey ValidationErrorsPropertyKey;
        public static readonly DependencyProperty ValidationErrorsProperty;
        private WeakReference owner;
        private readonly EditorsCache editorsCache = new EditorsCache();

        static ValidationService()
        {
            Type ownerType = typeof(ValidationService);
            IsValidationContainerProperty = DependencyPropertyManager.RegisterAttached("IsValidationContainer", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback(ValidationService.IsValidationContainerPropertyChanged)));
            ValidationServicePropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("ValidationService", typeof(ValidationService), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(ValidationService.ValidationServicePropertyChanged)));
            ValidationServiceProperty = ValidationServicePropertyKey.DependencyProperty;
            HasValidationErrorPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("HasValidationError", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, new PropertyChangedCallback(ValidationService.HasValidationErrorPropertyChanged)));
            HasValidationErrorProperty = HasValidationErrorPropertyKey.DependencyProperty;
            ValidationErrorsPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("ValidationErrors", typeof(ReadOnlyObservableCollection<BaseValidationError>), ownerType, new FrameworkPropertyMetadata(null));
            ValidationErrorsProperty = ValidationErrorsPropertyKey.DependencyProperty;
        }

        public void AddEditor(DependencyObject d)
        {
            if (BaseEdit.GetHasValidationError(d))
            {
                this.editorsCache.Add(d);
                this.UpdateErrors();
            }
        }

        public static bool GetHasValidationError(DependencyObject d) => 
            (bool) d.GetValue(HasValidationErrorProperty);

        public static bool GetIsValidationContainer(DependencyObject d) => 
            (bool) d.GetValue(IsValidationContainerProperty);

        public static ReadOnlyObservableCollection<BaseValidationError> GetValidationErrors(DependencyObject d) => 
            (ReadOnlyObservableCollection<BaseValidationError>) d.GetValue(ValidationErrorsProperty);

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public static ValidationService GetValidationService(DependencyObject d) => 
            (ValidationService) DependencyObjectHelper.GetValueWithInheritance(d, ValidationServiceProperty);

        private static void HasValidationErrorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
        }

        private static void IsValidationContainerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!((bool) e.NewValue))
            {
                d.ClearValue(ValidationServicePropertyKey);
            }
            else
            {
                ValidationService service = new ValidationService();
                service.Owner = d;
                SetValidationService(d, service);
            }
        }

        public void RemoveEditor(DependencyObject d)
        {
            this.editorsCache.Remove(d);
            this.UpdateErrors();
        }

        private static void SetHasValidationError(DependencyObject d, bool value)
        {
            d.SetValue(HasValidationErrorPropertyKey, value);
        }

        public static void SetIsValidationContainer(DependencyObject d, bool service)
        {
            d.SetValue(IsValidationContainerProperty, service);
        }

        internal static void SetValidationErrors(DependencyObject d, ReadOnlyObservableCollection<BaseValidationError> value)
        {
            d.SetValue(ValidationErrorsPropertyKey, value);
        }

        private static void SetValidationService(DependencyObject d, ValidationService service)
        {
            d.SetValue(ValidationServicePropertyKey, service);
        }

        public void UpdateEditor(DependencyObject d)
        {
            this.editorsCache.Remove(d);
            this.AddEditor(d);
        }

        private void UpdateErrors()
        {
            if (this.Owner != null)
            {
                if (this.editorsCache.HasElements)
                {
                    SetHasValidationError(this.Owner, this.editorsCache.HasElements);
                    SetValidationErrors(this.Owner, this.editorsCache.GetErrors());
                }
                else
                {
                    this.Owner.ClearValue(HasValidationErrorPropertyKey);
                    this.Owner.ClearValue(ValidationErrorsPropertyKey);
                }
            }
        }

        private static void ValidationServicePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                ((ValidationService) e.OldValue).RemoveEditor(d);
            }
            if (e.NewValue != null)
            {
                ((ValidationService) e.NewValue).AddEditor(d);
            }
        }

        private DependencyObject Owner
        {
            get => 
                (DependencyObject) this.owner.Target;
            set => 
                this.owner = new WeakReference(value);
        }

        private class EditorsCache
        {
            private List<WeakReference> editors = new List<WeakReference>();

            public void Add(DependencyObject d)
            {
                if (!this.Contains(d))
                {
                    this.editors.Add(new WeakReference(d));
                }
            }

            public bool Contains(DependencyObject d) => 
                this.Find(d) != null;

            public WeakReference Find(DependencyObject d)
            {
                this.Flush();
                return this.editors.FirstOrDefault<WeakReference>(reference => (reference.Target == d));
            }

            public void Flush()
            {
                for (int i = this.editors.Count - 1; i >= 0; i--)
                {
                    if (this.editors[i].Target == null)
                    {
                        this.editors.RemoveAt(i);
                    }
                }
            }

            public ReadOnlyObservableCollection<BaseValidationError> GetErrors()
            {
                ObservableCollection<BaseValidationError> list = new ObservableCollection<BaseValidationError>();
                foreach (WeakReference reference in this.editors)
                {
                    DependencyObject target = (DependencyObject) reference.Target;
                    if (target != null)
                    {
                        list.Add(BaseEdit.GetValidationError(target));
                    }
                }
                return new ReadOnlyObservableCollection<BaseValidationError>(list);
            }

            public void Remove(DependencyObject d)
            {
                WeakReference item = this.Find(d);
                if (item != null)
                {
                    this.editors.Remove(item);
                }
            }

            public bool HasElements =>
                this.editors.Count > 0;
        }
    }
}

