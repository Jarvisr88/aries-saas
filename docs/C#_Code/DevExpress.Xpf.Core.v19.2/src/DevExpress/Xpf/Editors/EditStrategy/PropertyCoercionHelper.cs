namespace DevExpress.Xpf.Editors.EditStrategy
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public class PropertyCoercionHelper
    {
        private Locker setSyncValueLocker = new Locker();
        private readonly NullableContainer syncValue = new NullableContainer();
        [IgnoreDependencyPropertiesConsistencyChecker]
        private DependencyProperty syncProperty;

        public PropertyCoercionHelper(BaseEdit editor)
        {
            this.Editor = editor;
            this.GetBaseValueHandlers = new Dictionary<object, PropertyCoercionHandler>();
            this.GetPropertyValueHandlers = new Dictionary<object, PropertyCoercionHandler>();
            this.UpdaterHandlers = new Dictionary<object, PropertyUpdaterHandler>();
        }

        private object GetPreviousValue(object property)
        {
            DependencyProperty dependencyProperty;
            if (property is DependencyProperty)
            {
                dependencyProperty = (DependencyProperty) property;
            }
            else
            {
                if (!(property is DependencyPropertyKey))
                {
                    throw new ArgumentException("property");
                }
                dependencyProperty = ((DependencyPropertyKey) property).DependencyProperty;
            }
            return this.Editor.GetValue(dependencyProperty);
        }

        private bool IsTwoWayBound(DependencyProperty dp)
        {
            BindingExpression bindingExpression = this.Editor.GetBindingExpression(dp);
            if (bindingExpression == null)
            {
                return false;
            }
            Binding parentBinding = bindingExpression.ParentBinding;
            return ((parentBinding != null) ? ((parentBinding.Mode == BindingMode.TwoWay) || (parentBinding.Mode != BindingMode.OneWayToSource)) : false);
        }

        private void RaiseValueChangedEvents(object oldBaseValue, object newBaseValue)
        {
            try
            {
                this.setSyncValueLocker.Lock();
                this.EditStrategy.RaiseValueChangedEvents(oldBaseValue, newBaseValue);
            }
            finally
            {
                this.setSyncValueLocker.Unlock();
            }
            if (this.SyncValueUpdateMode == DevExpress.Xpf.Editors.EditStrategy.SyncValueUpdateMode.Update)
            {
                this.Update(this.SyncProperty, this.SyncValue);
            }
            this.SyncValueUpdateMode = DevExpress.Xpf.Editors.EditStrategy.SyncValueUpdateMode.Default;
        }

        public void Register(DependencyProperty dp, PropertyCoercionHandler getBaseValueHandler, PropertyCoercionHandler getPropertyValueHandler)
        {
            this.GetBaseValueHandlers[dp] = getBaseValueHandler;
            this.GetPropertyValueHandlers[dp] = getPropertyValueHandler;
        }

        public void Register(DependencyPropertyKey dpk, PropertyCoercionHandler getBaseValueHandler, PropertyCoercionHandler getPropertyValueHandler)
        {
            this.GetBaseValueHandlers[dpk] = getBaseValueHandler;
            this.GetPropertyValueHandlers[dpk] = getPropertyValueHandler;
        }

        public void Register(DependencyProperty dp, PropertyCoercionHandler getBaseValueHandler, PropertyCoercionHandler getPropertyValueHandler, PropertyUpdaterHandler updater)
        {
            this.UpdaterHandlers[dp] = updater;
            this.GetBaseValueHandlers[dp] = getBaseValueHandler;
            this.GetPropertyValueHandlers[dp] = getPropertyValueHandler;
        }

        public void Register(DependencyPropertyKey dpk, PropertyCoercionHandler getBaseValueHandler, PropertyCoercionHandler getPropertyValueHandler, PropertyUpdaterHandler updater)
        {
            this.GetBaseValueHandlers[dpk] = getBaseValueHandler;
            this.GetPropertyValueHandlers[dpk] = getPropertyValueHandler;
            this.UpdaterHandlers[dpk] = updater;
        }

        public void ResetSyncValue()
        {
            this.SetSyncValue(null, null, DevExpress.Xpf.Editors.EditStrategy.SyncValueUpdateMode.Default);
        }

        public void SetSyncValue(DependencyProperty dp, object value, DevExpress.Xpf.Editors.EditStrategy.SyncValueUpdateMode syncValueUpdateMode)
        {
            if (syncValueUpdateMode == DevExpress.Xpf.Editors.EditStrategy.SyncValueUpdateMode.Default)
            {
                this.SyncProperty = dp;
                this.syncValue.SetValue(value);
            }
            else if ((syncValueUpdateMode == DevExpress.Xpf.Editors.EditStrategy.SyncValueUpdateMode.Update) && this.setSyncValueLocker.IsLocked)
            {
                this.SyncProperty = dp;
                this.syncValue.SetValue(value);
                this.SyncValueUpdateMode = syncValueUpdateMode;
            }
        }

        private void SetValue(object property, object value)
        {
            PropertyUpdaterHandler handler;
            if (this.UpdaterHandlers.TryGetValue(property, out handler))
            {
                handler(value);
            }
            else
            {
                object previousValue = this.GetPreviousValue(property);
                if (!Equals(value, previousValue))
                {
                    if (property is DependencyProperty)
                    {
                        BaseEditHelper.SetCurrentValue(this.Editor, (DependencyProperty) property, value);
                    }
                    else if (property is DependencyPropertyKey)
                    {
                        this.Editor.SetValue((DependencyPropertyKey) property, value);
                    }
                }
            }
        }

        private bool ShouldUpdateProperty(object dp)
        {
            DependencyProperty property = dp as DependencyProperty;
            return ((property != null) ? (this.Editor.AllowUpdateTwoWayBoundPropertiesOnSynchronization || !this.IsTwoWayBound(property)) : false);
        }

        public void Update()
        {
            object cachedSyncValue = (this.SyncProperty != null) ? this.GetBaseValueHandlers[this.SyncProperty](this.SyncValue) : this.SyncValue;
            if (this.Editor.PropertyProvider.SuppressFeatures)
            {
                this.UpdateProperty(this.SyncProperty, cachedSyncValue);
            }
            else
            {
                foreach (object obj3 in this.GetPropertyValueHandlers.Keys)
                {
                    this.UpdateProperty(obj3, cachedSyncValue);
                }
            }
        }

        public void Update(DependencyProperty dp, object baseValue)
        {
            object obj2 = (dp != null) ? this.GetBaseValueHandlers[dp](baseValue) : baseValue;
            foreach (object obj3 in this.GetPropertyValueHandlers.Keys)
            {
                if (!ReferenceEquals(dp, obj3))
                {
                    this.SetValue(obj3, this.GetPropertyValueHandlers[obj3](obj2));
                }
            }
        }

        public void Update(DependencyProperty dp, object oldValue, object newValue)
        {
            if (dp == null)
            {
                throw new ArgumentException("dp");
            }
            object obj2 = this.GetBaseValueHandlers[dp](oldValue);
            object obj3 = this.GetBaseValueHandlers[dp](newValue);
            this.SetSyncValue(dp, newValue, DevExpress.Xpf.Editors.EditStrategy.SyncValueUpdateMode.Default);
            if ((obj2 != obj3) && !this.Editor.PropertyProvider.SuppressFeatures)
            {
                if (this.EditStrategy.RaiseValueChangingEvents(obj2, obj3))
                {
                    this.Update(dp, newValue);
                    this.RaiseValueChangedEvents(obj2, obj3);
                }
                else
                {
                    this.EditStrategy.ResetOnValueChanging();
                    this.SetSyncValue(null, obj2, DevExpress.Xpf.Editors.EditStrategy.SyncValueUpdateMode.Default);
                    this.Update(null, obj2);
                }
            }
        }

        public void UpdateProperty(object property, object cachedSyncValue)
        {
            if (this.ShouldUpdateProperty(property))
            {
                this.SetValue(property, this.GetPropertyValueHandlers[property](cachedSyncValue));
            }
        }

        private BaseEdit Editor { get; set; }

        private EditStrategyBase EditStrategy =>
            this.Editor.EditStrategy;

        private Dictionary<object, PropertyCoercionHandler> GetBaseValueHandlers { get; set; }

        private Dictionary<object, PropertyCoercionHandler> GetPropertyValueHandlers { get; set; }

        private Dictionary<object, PropertyUpdaterHandler> UpdaterHandlers { get; set; }

        public bool HasSyncValue =>
            this.syncValue.HasValue;

        public object SyncValue =>
            this.HasSyncValue ? this.syncValue.Value : this.Editor.EditValue;

        public DevExpress.Xpf.Editors.EditStrategy.SyncValueUpdateMode SyncValueUpdateMode { get; private set; }

        public DependencyProperty SyncProperty
        {
            get => 
                this.syncProperty;
            private set => 
                this.syncProperty = value;
        }
    }
}

