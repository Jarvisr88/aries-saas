namespace DevExpress.Mvvm.Native
{
    using DevExpress.Data.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    internal class DataBinderCore<T>
    {
        private readonly WeakReference weakSource;
        private readonly object source;
        private readonly bool isWeakSource;
        private readonly bool isWeakSubSource;
        private readonly bool targetIsSource;
        private Subscriber<T> targetSubscriber;
        private Subscriber<T> sourceSubscriber;
        private Subscriber<T> commonSubscriber;
        private bool isTargetPropertyChanging;
        private string targetPropertyChanging;
        private bool isSourcePropertyChanging;
        private string sourcePropertyChanging;

        public DataBinderCore(T target, object source, bool isWeakSource, bool isWeakSubSource)
        {
            this.targetIsSource = target == source;
            this.isWeakSource = !this.targetIsSource && isWeakSource;
            this.isWeakSubSource = isWeakSubSource;
            if (isWeakSource)
            {
                this.weakSource = new WeakReference(source);
            }
            else
            {
                this.source = source;
            }
            this.<Target>k__BackingField = target;
            this.<Bindings>k__BackingField = new BindingCollection<T>();
        }

        public void AddBinding(BindingInfo<T> info, bool syncFirstTime)
        {
            Binding<T> binding = Binding<T>.Create((DataBinderCore<T>) this, info, syncFirstTime);
            if (binding.IsOneTime)
            {
                binding.Dispose();
            }
            else
            {
                this.Bindings.AddBinding(binding);
                this.SubscribeOnBindingAdded(binding);
            }
        }

        private void BeginSourcePropertyChanging(string prop)
        {
            if (!string.IsNullOrEmpty(prop))
            {
                this.isSourcePropertyChanging = true;
                this.sourcePropertyChanging = prop;
            }
        }

        private void BeginTargetPropertyChanging(string prop)
        {
            if (!string.IsNullOrEmpty(prop))
            {
                this.isTargetPropertyChanging = true;
                this.targetPropertyChanging = prop;
            }
        }

        public void Dispose()
        {
            this.Unsubscribe();
            this.Bindings.DisposeAll();
            this.IsDisposed = true;
        }

        private void EndSourcePropertyChanging()
        {
            this.isSourcePropertyChanging = false;
        }

        private void EndTargetPropertyChanging()
        {
            this.isTargetPropertyChanging = false;
        }

        public bool IsPropertyBound(string property) => 
            !this.TryDispose() ? this.Bindings.HasAnyBinding(property) : false;

        private void OnCommonPropertyChanged(string prop)
        {
            if ((!this.IsTargetSuppressed && (!this.isTargetPropertyChanging || (this.targetPropertyChanging != prop))) && (!this.isSourcePropertyChanging || (this.sourcePropertyChanging != prop)))
            {
                if (string.IsNullOrEmpty(prop))
                {
                    Action<Binding<T>> action = <>c<T>.<>9__42_0;
                    if (<>c<T>.<>9__42_0 == null)
                    {
                        Action<Binding<T>> local1 = <>c<T>.<>9__42_0;
                        action = <>c<T>.<>9__42_0 = delegate (Binding<T> x) {
                            if (!x.Info.IsExplicit)
                            {
                                x.FromSourceToTarget();
                                x.FromTargetToSource();
                            }
                        };
                    }
                    this.Bindings.GetAllBindings().ForEach(action);
                }
                else
                {
                    List<Binding<T>> bindingsBySourceProperty = this.Bindings.GetBindingsBySourceProperty(prop);
                    if (bindingsBySourceProperty != null)
                    {
                        Action<Binding<T>> action = <>c<T>.<>9__42_1;
                        if (<>c<T>.<>9__42_1 == null)
                        {
                            Action<Binding<T>> local2 = <>c<T>.<>9__42_1;
                            action = <>c<T>.<>9__42_1 = delegate (Binding<T> x) {
                                if (!x.Info.IsExplicit)
                                {
                                    x.FromSourceToTarget();
                                }
                            };
                        }
                        bindingsBySourceProperty.ForEach(action);
                    }
                    bindingsBySourceProperty = this.Bindings.GetBindingsByTargetProperty(prop);
                    if (bindingsBySourceProperty != null)
                    {
                        Action<Binding<T>> action = <>c<T>.<>9__42_2;
                        if (<>c<T>.<>9__42_2 == null)
                        {
                            Action<Binding<T>> local3 = <>c<T>.<>9__42_2;
                            action = <>c<T>.<>9__42_2 = delegate (Binding<T> x) {
                                if (!x.Info.IsExplicit)
                                {
                                    x.FromTargetToSource();
                                }
                            };
                        }
                        bindingsBySourceProperty.ForEach(action);
                    }
                }
            }
        }

        private void OnSourcePropertyChanged(string prop)
        {
            if (!this.isSourcePropertyChanging || (this.sourcePropertyChanging != prop))
            {
                List<Binding<T>> list = string.IsNullOrEmpty(prop) ? this.Bindings.GetAllBindings() : this.Bindings.GetBindingsBySourceProperty(prop);
                if (list != null)
                {
                    Action<Binding<T>> action = <>c<T>.<>9__44_0;
                    if (<>c<T>.<>9__44_0 == null)
                    {
                        Action<Binding<T>> local1 = <>c<T>.<>9__44_0;
                        action = <>c<T>.<>9__44_0 = delegate (Binding<T> x) {
                            if (!x.Info.IsExplicit)
                            {
                                x.FromSourceToTarget();
                            }
                        };
                    }
                    list.ForEach(action);
                }
            }
        }

        private void OnTargetPropertyChanged(string prop)
        {
            if ((!this.IsTargetSuppressed && (!this.isTargetPropertyChanging || (this.targetPropertyChanging != prop))) && !this.TryDispose())
            {
                List<Binding<T>> list = string.IsNullOrEmpty(prop) ? this.Bindings.GetAllBindings() : this.Bindings.GetBindingsByTargetProperty(prop);
                if (list != null)
                {
                    Action<Binding<T>> action = <>c<T>.<>9__43_0;
                    if (<>c<T>.<>9__43_0 == null)
                    {
                        Action<Binding<T>> local1 = <>c<T>.<>9__43_0;
                        action = <>c<T>.<>9__43_0 = delegate (Binding<T> x) {
                            if (!x.Info.IsExplicit)
                            {
                                x.FromTargetToSource();
                            }
                        };
                    }
                    list.ForEach(action);
                }
            }
        }

        public void RestoreTargetUpdates()
        {
            this.IsTargetSuppressed = false;
        }

        private void SubscribeOnBindingAdded(Binding<T> b)
        {
            if (!b.Info.IsExplicit)
            {
                if (this.targetIsSource)
                {
                    this.CommonSubscriber.Subscribe(this.Target);
                }
                else
                {
                    if (b.CanUpdateTarget)
                    {
                        this.SourceSubscriber.Subscribe(this.Source);
                    }
                    if (b.CanUpdateSource)
                    {
                        this.TargetSubscriber.Subscribe(this.Target);
                    }
                }
            }
        }

        public void SuppressTargetUpdates()
        {
            this.IsTargetSuppressed = true;
        }

        private bool TryDispose()
        {
            if (!this.IsDisposed)
            {
                if (!this.isWeakSource || this.weakSource.IsAlive)
                {
                    return false;
                }
                this.Dispose();
            }
            return true;
        }

        public void UnbindProperty(string property)
        {
            this.Bindings.Dispose(property);
        }

        private void Unsubscribe()
        {
            if (this.commonSubscriber == null)
            {
                Subscriber<T> commonSubscriber = this.commonSubscriber;
            }
            else
            {
                this.commonSubscriber.Unsubscribe(this.Target);
            }
            if (this.targetSubscriber == null)
            {
                Subscriber<T> targetSubscriber = this.targetSubscriber;
            }
            else
            {
                this.targetSubscriber.Unsubscribe(this.Target);
            }
            if (this.sourceSubscriber == null)
            {
                Subscriber<T> sourceSubscriber = this.sourceSubscriber;
            }
            else
            {
                this.sourceSubscriber.Unsubscribe(this.Source);
            }
        }

        public void UpdateSource(string property)
        {
            if (!this.IsTargetSuppressed && !this.TryDispose())
            {
                List<Binding<T>> bindingsByTargetProperty = this.Bindings.GetBindingsByTargetProperty(property);
                if (bindingsByTargetProperty == null)
                {
                    List<Binding<T>> local1 = bindingsByTargetProperty;
                }
                else
                {
                    bindingsByTargetProperty.ForEach(<>c<T>.<>9__34_0 ??= x => x.FromTargetToSource());
                }
            }
        }

        public void UpdateTarget(string property)
        {
            if (!this.TryDispose())
            {
                List<Binding<T>> bindingsByTargetProperty = this.Bindings.GetBindingsByTargetProperty(property);
                if (bindingsByTargetProperty == null)
                {
                    List<Binding<T>> local1 = bindingsByTargetProperty;
                }
                else
                {
                    bindingsByTargetProperty.ForEach(<>c<T>.<>9__33_0 ??= x => x.FromSourceToTarget());
                }
            }
        }

        private T Target { get; }

        private object Source =>
            this.isWeakSource ? this.weakSource.Target : this.source;

        private Subscriber<T> TargetSubscriber
        {
            get
            {
                Subscriber<T> targetSubscriber = this.targetSubscriber;
                if (this.targetSubscriber == null)
                {
                    Subscriber<T> local1 = this.targetSubscriber;
                    targetSubscriber = this.targetSubscriber = new Subscriber<T>(delegate (string x) {
                        base.OnTargetPropertyChanged(x);
                    }, false);
                }
                return targetSubscriber;
            }
        }

        private Subscriber<T> SourceSubscriber
        {
            get
            {
                Subscriber<T> sourceSubscriber = this.sourceSubscriber;
                if (this.sourceSubscriber == null)
                {
                    Subscriber<T> local1 = this.sourceSubscriber;
                    sourceSubscriber = this.sourceSubscriber = new Subscriber<T>(delegate (string x) {
                        base.OnSourcePropertyChanged(x);
                    }, this.isWeakSource);
                }
                return sourceSubscriber;
            }
        }

        private Subscriber<T> CommonSubscriber
        {
            get
            {
                Subscriber<T> commonSubscriber = this.commonSubscriber;
                if (this.commonSubscriber == null)
                {
                    Subscriber<T> local1 = this.commonSubscriber;
                    commonSubscriber = this.commonSubscriber = new Subscriber<T>(delegate (string x) {
                        base.OnCommonPropertyChanged(x);
                    }, false);
                }
                return commonSubscriber;
            }
        }

        private BindingCollection<T> Bindings { get; }

        private bool IsDisposed { get; set; }

        private bool IsTargetSuppressed { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataBinderCore<T>.<>c <>9;
            public static Action<DataBinderCore<T>.Binding> <>9__33_0;
            public static Action<DataBinderCore<T>.Binding> <>9__34_0;
            public static Action<DataBinderCore<T>.Binding> <>9__42_0;
            public static Action<DataBinderCore<T>.Binding> <>9__42_1;
            public static Action<DataBinderCore<T>.Binding> <>9__42_2;
            public static Action<DataBinderCore<T>.Binding> <>9__43_0;
            public static Action<DataBinderCore<T>.Binding> <>9__44_0;

            static <>c()
            {
                DataBinderCore<T>.<>c.<>9 = new DataBinderCore<T>.<>c();
            }

            internal void <OnCommonPropertyChanged>b__42_0(DataBinderCore<T>.Binding x)
            {
                if (!x.Info.IsExplicit)
                {
                    x.FromSourceToTarget();
                    x.FromTargetToSource();
                }
            }

            internal void <OnCommonPropertyChanged>b__42_1(DataBinderCore<T>.Binding x)
            {
                if (!x.Info.IsExplicit)
                {
                    x.FromSourceToTarget();
                }
            }

            internal void <OnCommonPropertyChanged>b__42_2(DataBinderCore<T>.Binding x)
            {
                if (!x.Info.IsExplicit)
                {
                    x.FromTargetToSource();
                }
            }

            internal void <OnSourcePropertyChanged>b__44_0(DataBinderCore<T>.Binding x)
            {
                if (!x.Info.IsExplicit)
                {
                    x.FromSourceToTarget();
                }
            }

            internal void <OnTargetPropertyChanged>b__43_0(DataBinderCore<T>.Binding x)
            {
                if (!x.Info.IsExplicit)
                {
                    x.FromTargetToSource();
                }
            }

            internal void <UpdateSource>b__34_0(DataBinderCore<T>.Binding x)
            {
                x.FromTargetToSource();
            }

            internal void <UpdateTarget>b__33_0(DataBinderCore<T>.Binding x)
            {
                x.FromSourceToTarget();
            }
        }

        public class Binding : IDisposable
        {
            private object localSource;
            private WeakReference weakLocalSource;
            private bool lockOnTargetPropertyChanged;
            private bool lockOnSourcePropertyChanged;
            private bool isFirstSyncTime;
            private DataBinder.PropertyCache.IFastProperty targetProperty;
            private DataBinder.PropertyCache.IFastProperty sourceProperty;

            private Binding(DataBinderCore<T> owner, DataBinderCore<T>.BindingInfo info, object localSource, int level)
            {
                this.isFirstSyncTime = true;
                this.<Owner>k__BackingField = owner;
                this.<Info>k__BackingField = info;
                if (this.Owner.isWeakSubSource)
                {
                    this.weakLocalSource = new WeakReference(localSource);
                }
                else
                {
                    this.localSource = localSource;
                }
                this.<Level>k__BackingField = level;
                this.<SourceProperty>k__BackingField = this.Info.SourcePath[this.Level];
                this.<SourcePropertyHashCode>k__BackingField = this.SourceProperty.GetHashCode();
                this.<HasSubBinding>k__BackingField = this.Level < (this.Info.SourcePath.Length - 1);
                switch (this.Info.Mode)
                {
                    case DataBinderCore<T>.BindingMode.TwoWay:
                        this.<CanUpdateTarget>k__BackingField = true;
                        this.<CanUpdateSource>k__BackingField = true;
                        break;

                    case DataBinderCore<T>.BindingMode.OneWay:
                    case DataBinderCore<T>.BindingMode.OneTime:
                        this.<CanUpdateTarget>k__BackingField = true;
                        break;

                    case DataBinderCore<T>.BindingMode.OneWayToSource:
                    case DataBinderCore<T>.BindingMode.OneTimeToSource:
                        this.<CanUpdateSource>k__BackingField = true;
                        break;

                    default:
                        break;
                }
                this.<IsOneTime>k__BackingField = (this.Info.Mode == DataBinderCore<T>.BindingMode.OneTime) || (this.Info.Mode == DataBinderCore<T>.BindingMode.OneTimeToSource);
            }

            public static DataBinderCore<T>.Binding Create(DataBinderCore<T> owner, DataBinderCore<T>.BindingInfo info, bool syncFirstTime)
            {
                DataBinderCore<T>.Binding binding = new DataBinderCore<T>.Binding(owner, info, null, 0);
                if (syncFirstTime)
                {
                    binding.FirstTimeSync();
                }
                return binding;
            }

            private static DataBinderCore<T>.Binding CreateSubBinding(DataBinderCore<T>.Binding parentBinding, object localSource) => 
                new DataBinderCore<T>.Binding(parentBinding.Owner, parentBinding.Info, localSource, parentBinding.Level + 1);

            public void Dispose()
            {
                this.Unsubscribe();
                this.LocalSourceSubscriber = null;
                DataBinderCore<T>.Binding subBinding = this.SubBinding;
                if (subBinding == null)
                {
                    DataBinderCore<T>.Binding local1 = subBinding;
                }
                else
                {
                    subBinding.Dispose();
                }
                this.SubBinding = null;
                this.localSource = null;
                this.weakLocalSource = null;
                this.IsDisposed = true;
            }

            private void FirstTimeSync()
            {
                switch (this.Info.Mode)
                {
                    case DataBinderCore<T>.BindingMode.TwoWay:
                    case DataBinderCore<T>.BindingMode.OneWay:
                    case DataBinderCore<T>.BindingMode.OneTime:
                        this.FromSourceToTarget();
                        return;
                }
                this.FromTargetToSource();
            }

            public void FromSourceToTarget()
            {
                if (!this.lockOnSourcePropertyChanged)
                {
                    this.isFirstSyncTime = false;
                    if (!this.TryDispose())
                    {
                        object sourcePropertyValue = this.GetSourcePropertyValue();
                        if (!this.HasSubBinding)
                        {
                            this.lockOnTargetPropertyChanged = true;
                            try
                            {
                                this.SetTargetPropertyValue(sourcePropertyValue);
                            }
                            finally
                            {
                                this.lockOnTargetPropertyChanged = false;
                            }
                        }
                        else if (sourcePropertyValue != null)
                        {
                            DataBinderCore<T>.Binding subBinding = this.SubBinding;
                            if (subBinding == null)
                            {
                                DataBinderCore<T>.Binding local2 = subBinding;
                            }
                            else
                            {
                                subBinding.Dispose();
                            }
                            this.SubBinding = DataBinderCore<T>.Binding.CreateSubBinding((DataBinderCore<T>.Binding) this, sourcePropertyValue);
                            this.SubBinding.FromSourceToTarget();
                            if (!this.Info.IsExplicit)
                            {
                                this.SubBinding.Subscribe();
                            }
                            else
                            {
                                this.SubBinding.Dispose();
                                this.SubBinding = null;
                            }
                        }
                        else
                        {
                            DataBinderCore<T>.Binding subBinding = this.SubBinding;
                            if (subBinding == null)
                            {
                                DataBinderCore<T>.Binding local1 = subBinding;
                            }
                            else
                            {
                                subBinding.Dispose();
                            }
                            this.SubBinding = null;
                            this.lockOnTargetPropertyChanged = true;
                            try
                            {
                                this.SetTargetPropertyValue(this.Info.FallbackValue);
                            }
                            finally
                            {
                                this.lockOnTargetPropertyChanged = false;
                            }
                        }
                    }
                }
            }

            public void FromTargetToSource()
            {
                if (!this.lockOnTargetPropertyChanged)
                {
                    if (this.TryDispose())
                    {
                        this.isFirstSyncTime = false;
                    }
                    else if (!this.HasSubBinding)
                    {
                        this.isFirstSyncTime = false;
                        object targetPropertyValue = this.GetTargetPropertyValue();
                        this.lockOnSourcePropertyChanged = true;
                        try
                        {
                            this.SetSourcePropertyValue(targetPropertyValue);
                        }
                        finally
                        {
                            this.lockOnSourcePropertyChanged = false;
                        }
                    }
                    else if (this.SubBinding != null)
                    {
                        this.isFirstSyncTime = false;
                        this.SubBinding.FromTargetToSource();
                    }
                    else if (this.isFirstSyncTime)
                    {
                        this.isFirstSyncTime = false;
                        object sourcePropertyValue = this.GetSourcePropertyValue();
                        if (sourcePropertyValue != null)
                        {
                            this.SubBinding = DataBinderCore<T>.Binding.CreateSubBinding((DataBinderCore<T>.Binding) this, sourcePropertyValue);
                            this.SubBinding.FromTargetToSource();
                            if (this.Info.IsExplicit)
                            {
                                this.SubBinding.Dispose();
                                this.SubBinding = null;
                            }
                            else
                            {
                                this.SubBinding.Subscribe();
                            }
                        }
                    }
                }
            }

            private object GetSource() => 
                (this.Level == 0) ? this.Owner.Source : this.LocalSource;

            private DataBinder.PropertyCache.IFastProperty GetSourceProperty(object source)
            {
                DataBinder.PropertyCache.IFastProperty sourceProperty = this.sourceProperty;
                if (this.sourceProperty == null)
                {
                    DataBinder.PropertyCache.IFastProperty local1 = this.sourceProperty;
                    sourceProperty = this.sourceProperty = DataBinder.PropertyCache.GetProperty(source, this.SourceProperty, true);
                }
                return sourceProperty;
            }

            private Type GetSourcePropertyType()
            {
                object source = this.GetSource();
                return this.GetSourceProperty(source).GetPropertyType(source);
            }

            private object GetSourcePropertyValue()
            {
                object source = this.GetSource();
                return this.GetSourceProperty(source).GetValue(source);
            }

            private DataBinder.PropertyCache.IFastProperty GetTargetProperty()
            {
                DataBinder.PropertyCache.IFastProperty targetProperty = this.targetProperty;
                if (this.targetProperty == null)
                {
                    DataBinder.PropertyCache.IFastProperty local1 = this.targetProperty;
                    targetProperty = this.targetProperty = DataBinder.PropertyCache.GetProperty(this.Owner.Target, this.Info.Property, true);
                }
                return targetProperty;
            }

            private object GetTargetPropertyValue() => 
                (this.Info.Get == null) ? this.GetTargetProperty().GetValue(this.Owner.Target) : this.Info.Get(this.Owner.Target, this.GetSourcePropertyType());

            private void OnLocalSourcePropertyChanged(string prop)
            {
                if (!this.lockOnSourcePropertyChanged && (string.IsNullOrEmpty(prop) || ((this.SourcePropertyHashCode == prop.GetHashCode()) && (this.SourceProperty == prop))))
                {
                    this.FromSourceToTarget();
                }
            }

            private void SetSourcePropertyValue(object value)
            {
                if (this.Level != 0)
                {
                    this.lockOnSourcePropertyChanged = true;
                    this.GetSourceProperty(this.LocalSource).SetValue(this.LocalSource, value);
                    this.lockOnSourcePropertyChanged = false;
                }
                else
                {
                    this.Owner.BeginSourcePropertyChanging(this.SourceProperty);
                    this.lockOnSourcePropertyChanged = true;
                    this.GetSourceProperty(this.Owner.Source).SetValue(this.Owner.Source, value);
                    this.lockOnSourcePropertyChanged = false;
                    this.Owner.EndSourcePropertyChanging();
                }
            }

            private void SetTargetPropertyValue(object value)
            {
                this.Owner.BeginTargetPropertyChanging(this.Info.Property);
                this.lockOnTargetPropertyChanged = true;
                if (this.Info.Set != null)
                {
                    this.Info.Set(this.Owner.Target, value);
                }
                else
                {
                    this.GetTargetProperty().SetValue(this.Owner.Target, value);
                }
                this.lockOnTargetPropertyChanged = false;
                this.Owner.EndTargetPropertyChanging();
            }

            private void Subscribe()
            {
                this.LocalSourceSubscriber ??= new DataBinderCore<T>.Subscriber(x => base.OnLocalSourcePropertyChanged(x), this.Owner.isWeakSubSource);
                this.LocalSourceSubscriber.Subscribe(this.LocalSource);
            }

            private bool TryDispose()
            {
                if (!this.IsDisposed)
                {
                    if ((this.Level == 0) && (this.Owner.Source == null))
                    {
                        this.Dispose();
                        return true;
                    }
                    if ((this.Level <= 0) || (this.LocalSource != null))
                    {
                        return false;
                    }
                    this.Dispose();
                }
                return true;
            }

            private void Unsubscribe()
            {
                DataBinderCore<T>.Subscriber localSourceSubscriber = this.LocalSourceSubscriber;
                if (localSourceSubscriber == null)
                {
                    DataBinderCore<T>.Subscriber local1 = localSourceSubscriber;
                }
                else
                {
                    localSourceSubscriber.Unsubscribe(this.LocalSource);
                }
            }

            public bool CanUpdateTarget { get; }

            public bool CanUpdateSource { get; }

            public bool IsOneTime { get; }

            public DataBinderCore<T> Owner { get; }

            public DataBinderCore<T>.BindingInfo Info { get; }

            public object LocalSource =>
                this.Owner.isWeakSubSource ? this.weakLocalSource.Target : this.localSource;

            public int Level { get; }

            public string SourceProperty { get; }

            private int SourcePropertyHashCode { get; }

            private bool HasSubBinding { get; }

            private DataBinderCore<T>.Subscriber LocalSourceSubscriber { get; set; }

            private DataBinderCore<T>.Binding SubBinding { get; set; }

            private bool IsDisposed { get; set; }
        }

        public class BindingCollection
        {
            private List<DataBinderCore<T>.Binding> allBindings;
            private Dictionary<string, List<DataBinderCore<T>.Binding>> bindingsByTargetProperty;
            private Dictionary<string, List<DataBinderCore<T>.Binding>> bindingsBySourceProperty;

            public BindingCollection()
            {
                this.allBindings = new List<DataBinderCore<T>.Binding>(1);
            }

            public void AddBinding(DataBinderCore<T>.Binding binding)
            {
                this.allBindings.Add(binding);
                if ((this.bindingsByTargetProperty != null) && !string.IsNullOrEmpty(binding.Info.Property))
                {
                    this.GetBindingsByTargetPropertyCore(binding.Info.Property, true).Add(binding);
                }
                if ((this.bindingsBySourceProperty != null) && binding.CanUpdateTarget)
                {
                    this.GetBindingsBySourcePropertyCore(binding.SourceProperty, true).Add(binding);
                }
            }

            public void Dispose(string targetProperty)
            {
                List<DataBinderCore<T>.Binding> bindingsByTargetProperty = this.GetBindingsByTargetProperty(targetProperty);
                if (bindingsByTargetProperty != null)
                {
                    bindingsByTargetProperty.ForEach(delegate (DataBinderCore<T>.Binding x) {
                        List<DataBinderCore<T>.Binding> bindingsBySourceProperty = base.GetBindingsBySourceProperty(x.SourceProperty);
                        if (bindingsBySourceProperty == null)
                        {
                            List<DataBinderCore<T>.Binding> local1 = bindingsBySourceProperty;
                        }
                        else
                        {
                            bindingsBySourceProperty.Remove(x);
                        }
                        base.allBindings.Remove(x);
                        x.Dispose();
                    });
                    bindingsByTargetProperty.Clear();
                }
            }

            public void DisposeAll()
            {
                Action<DataBinderCore<T>.Binding> action = <>c<T>.<>9__6_0;
                if (<>c<T>.<>9__6_0 == null)
                {
                    Action<DataBinderCore<T>.Binding> local1 = <>c<T>.<>9__6_0;
                    action = <>c<T>.<>9__6_0 = x => x.Dispose();
                }
                this.allBindings.ForEach(action);
                if (this.bindingsByTargetProperty == null)
                {
                    Dictionary<string, List<DataBinderCore<T>.Binding>> bindingsByTargetProperty = this.bindingsByTargetProperty;
                }
                else
                {
                    this.bindingsByTargetProperty.Clear();
                }
                if (this.bindingsBySourceProperty == null)
                {
                    Dictionary<string, List<DataBinderCore<T>.Binding>> bindingsBySourceProperty = this.bindingsBySourceProperty;
                }
                else
                {
                    this.bindingsBySourceProperty.Clear();
                }
                this.allBindings.Clear();
            }

            public List<DataBinderCore<T>.Binding> GetAllBindings() => 
                this.allBindings;

            public List<DataBinderCore<T>.Binding> GetBindingsBySourceProperty(string sourceProperty)
            {
                if (this.bindingsBySourceProperty == null)
                {
                    this.bindingsBySourceProperty = new Dictionary<string, List<DataBinderCore<T>.Binding>>(this.allBindings.Count);
                    this.allBindings.ForEach(delegate (DataBinderCore<T>.Binding x) {
                        base.GetBindingsBySourcePropertyCore(x.SourceProperty, true).Add(x);
                    });
                }
                return this.GetBindingsBySourcePropertyCore(sourceProperty, false);
            }

            private List<DataBinderCore<T>.Binding> GetBindingsBySourcePropertyCore(string sourceProperty, bool createIfNotExist)
            {
                List<DataBinderCore<T>.Binding> list;
                if (!this.bindingsBySourceProperty.TryGetValue(sourceProperty, out list) & createIfNotExist)
                {
                    list = new List<DataBinderCore<T>.Binding>(1);
                    this.bindingsBySourceProperty[sourceProperty] = list;
                }
                return list;
            }

            public List<DataBinderCore<T>.Binding> GetBindingsByTargetProperty(string targetProperty)
            {
                if (this.bindingsByTargetProperty == null)
                {
                    this.bindingsByTargetProperty = new Dictionary<string, List<DataBinderCore<T>.Binding>>(this.allBindings.Count);
                    this.allBindings.ForEach(delegate (DataBinderCore<T>.Binding x) {
                        base.GetBindingsByTargetPropertyCore(x.Info.Property, true).Add(x);
                    });
                }
                return this.GetBindingsByTargetPropertyCore(targetProperty, false);
            }

            private List<DataBinderCore<T>.Binding> GetBindingsByTargetPropertyCore(string targetProperty, bool createIfNotExist)
            {
                List<DataBinderCore<T>.Binding> list;
                if (!this.bindingsByTargetProperty.TryGetValue(targetProperty, out list) & createIfNotExist)
                {
                    list = new List<DataBinderCore<T>.Binding>(1);
                    this.bindingsByTargetProperty[targetProperty] = list;
                }
                return list;
            }

            public bool HasAnyBinding(string targetProperty)
            {
                List<DataBinderCore<T>.Binding> bindingsByTargetProperty = this.GetBindingsByTargetProperty(targetProperty);
                return ((bindingsByTargetProperty != null) && (bindingsByTargetProperty.Count > 0));
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DataBinderCore<T>.BindingCollection.<>c <>9;
                public static Action<DataBinderCore<T>.Binding> <>9__6_0;

                static <>c()
                {
                    DataBinderCore<T>.BindingCollection.<>c.<>9 = new DataBinderCore<T>.BindingCollection.<>c();
                }

                internal void <DisposeAll>b__6_0(DataBinderCore<T>.Binding x)
                {
                    x.Dispose();
                }
            }
        }

        public class BindingInfo
        {
            public BindingInfo(string property, string[] sourcePath, Func<T, Type, object> get, Action<T, object> set, object fallbackValue, DataBinderCore<T>.BindingMode mode, bool isExplicit)
            {
                this.<Property>k__BackingField = property;
                this.<SourcePath>k__BackingField = sourcePath;
                this.<Set>k__BackingField = set;
                this.<Get>k__BackingField = get;
                this.<FallbackValue>k__BackingField = fallbackValue;
                this.<Mode>k__BackingField = mode;
                this.<IsExplicit>k__BackingField = isExplicit;
            }

            public string Property { get; }

            public string[] SourcePath { get; }

            public Func<T, Type, object> Get { get; }

            public Action<T, object> Set { get; }

            public object FallbackValue { get; }

            public DataBinderCore<T>.BindingMode Mode { get; }

            public bool IsExplicit { get; }
        }

        public enum BindingMode
        {
            public const DataBinderCore<T>.BindingMode TwoWay = DataBinderCore<T>.BindingMode.TwoWay;,
            public const DataBinderCore<T>.BindingMode OneWay = DataBinderCore<T>.BindingMode.OneWay;,
            public const DataBinderCore<T>.BindingMode OneWayToSource = DataBinderCore<T>.BindingMode.OneWayToSource;,
            public const DataBinderCore<T>.BindingMode OneTime = DataBinderCore<T>.BindingMode.OneTime;,
            public const DataBinderCore<T>.BindingMode OneTimeToSource = DataBinderCore<T>.BindingMode.OneTimeToSource;
        }

        private class Subscriber
        {
            private readonly Action<string> onPropertyChanged;
            private readonly bool weak;
            private WeakEventHandler<DataBinderCore<T>.Subscriber, PropertyChangedEventArgs, PropertyChangedEventHandler> handler1;
            private WeakEventHandler<DataBinderCore<T>.Subscriber, DependencyPropertyChangedEventArgs, DependencyPropertyChangedEventHandler> handler2;

            public Subscriber(Action<string> onPropertyChanged, bool weak)
            {
                this.onPropertyChanged = onPropertyChanged;
                this.weak = weak;
            }

            private void OnDependencyPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
            {
                this.onPropertyChanged(e.Property.Name);
            }

            private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                this.onPropertyChanged(e.PropertyName);
            }

            public void Subscribe(object obj)
            {
                if (obj == null)
                {
                    this.IsSubscribed = false;
                }
                else if (!this.IsSubscribed)
                {
                    this.IsSubscribed = true;
                    INotifyPropertyChanged changed = obj as INotifyPropertyChanged;
                    INotifyDependencyPropertyChanged changed2 = obj as INotifyDependencyPropertyChanged;
                    if (this.weak)
                    {
                        if (changed != null)
                        {
                            changed.PropertyChanged += this.Handler1.Handler;
                        }
                        if (changed2 != null)
                        {
                            changed2.DependencyPropertyChanged += this.Handler2.Handler;
                        }
                    }
                    else
                    {
                        if (changed != null)
                        {
                            changed.PropertyChanged += new PropertyChangedEventHandler(this.OnPropertyChanged);
                        }
                        if (changed2 != null)
                        {
                            changed2.DependencyPropertyChanged += new DependencyPropertyChangedEventHandler(this.OnDependencyPropertyChanged);
                        }
                    }
                }
            }

            public void Unsubscribe(object obj)
            {
                if (obj == null)
                {
                    this.IsSubscribed = false;
                }
                else if (this.IsSubscribed)
                {
                    this.IsSubscribed = false;
                    INotifyPropertyChanged changed = obj as INotifyPropertyChanged;
                    INotifyDependencyPropertyChanged changed2 = obj as INotifyDependencyPropertyChanged;
                    if (this.weak)
                    {
                        if (changed != null)
                        {
                            changed.PropertyChanged -= this.Handler1.Handler;
                        }
                        if (changed2 != null)
                        {
                            changed2.DependencyPropertyChanged -= this.Handler2.Handler;
                        }
                    }
                    else
                    {
                        if (changed != null)
                        {
                            changed.PropertyChanged -= new PropertyChangedEventHandler(this.OnPropertyChanged);
                        }
                        if (changed2 != null)
                        {
                            changed2.DependencyPropertyChanged -= new DependencyPropertyChangedEventHandler(this.OnDependencyPropertyChanged);
                        }
                    }
                }
            }

            private WeakEventHandler<DataBinderCore<T>.Subscriber, PropertyChangedEventArgs, PropertyChangedEventHandler> Handler1
            {
                get
                {
                    if (this.handler1 == null)
                    {
                        Action<DataBinderCore<T>.Subscriber, object, PropertyChangedEventArgs> onEventAction = <>c<T>.<>9__5_0;
                        if (<>c<T>.<>9__5_0 == null)
                        {
                            Action<DataBinderCore<T>.Subscriber, object, PropertyChangedEventArgs> local1 = <>c<T>.<>9__5_0;
                            onEventAction = <>c<T>.<>9__5_0 = delegate (DataBinderCore<T>.Subscriber x, object s, PropertyChangedEventArgs e) {
                                x.OnPropertyChanged(s, e);
                            };
                        }
                        this.handler1 = new WeakEventHandler<DataBinderCore<T>.Subscriber, PropertyChangedEventArgs, PropertyChangedEventHandler>((DataBinderCore<T>.Subscriber) this, onEventAction, <>c<T>.<>9__5_1 ??= delegate (WeakEventHandler<DataBinderCore<T>.Subscriber, PropertyChangedEventArgs, PropertyChangedEventHandler> wh, object o) {
                            ((INotifyPropertyChanged) o).PropertyChanged -= wh.Handler;
                        }, <>c<T>.<>9__5_2 ??= wh => new PropertyChangedEventHandler(wh.OnEvent));
                    }
                    return this.handler1;
                }
            }

            private WeakEventHandler<DataBinderCore<T>.Subscriber, DependencyPropertyChangedEventArgs, DependencyPropertyChangedEventHandler> Handler2
            {
                get
                {
                    if (this.handler2 == null)
                    {
                        Action<DataBinderCore<T>.Subscriber, object, DependencyPropertyChangedEventArgs> onEventAction = <>c<T>.<>9__7_0;
                        if (<>c<T>.<>9__7_0 == null)
                        {
                            Action<DataBinderCore<T>.Subscriber, object, DependencyPropertyChangedEventArgs> local1 = <>c<T>.<>9__7_0;
                            onEventAction = <>c<T>.<>9__7_0 = delegate (DataBinderCore<T>.Subscriber x, object s, DependencyPropertyChangedEventArgs e) {
                                x.OnDependencyPropertyChanged(s, e);
                            };
                        }
                        this.handler2 = new WeakEventHandler<DataBinderCore<T>.Subscriber, DependencyPropertyChangedEventArgs, DependencyPropertyChangedEventHandler>((DataBinderCore<T>.Subscriber) this, onEventAction, <>c<T>.<>9__7_1 ??= delegate (WeakEventHandler<DataBinderCore<T>.Subscriber, DependencyPropertyChangedEventArgs, DependencyPropertyChangedEventHandler> wh, object o) {
                            ((INotifyDependencyPropertyChanged) o).DependencyPropertyChanged -= wh.Handler;
                        }, <>c<T>.<>9__7_2 ??= wh => new DependencyPropertyChangedEventHandler(wh.OnEvent));
                    }
                    return this.handler2;
                }
            }

            public bool IsSubscribed { get; private set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DataBinderCore<T>.Subscriber.<>c <>9;
                public static Action<DataBinderCore<T>.Subscriber, object, PropertyChangedEventArgs> <>9__5_0;
                public static Action<WeakEventHandler<DataBinderCore<T>.Subscriber, PropertyChangedEventArgs, PropertyChangedEventHandler>, object> <>9__5_1;
                public static Func<WeakEventHandler<DataBinderCore<T>.Subscriber, PropertyChangedEventArgs, PropertyChangedEventHandler>, PropertyChangedEventHandler> <>9__5_2;
                public static Action<DataBinderCore<T>.Subscriber, object, DependencyPropertyChangedEventArgs> <>9__7_0;
                public static Action<WeakEventHandler<DataBinderCore<T>.Subscriber, DependencyPropertyChangedEventArgs, DependencyPropertyChangedEventHandler>, object> <>9__7_1;
                public static Func<WeakEventHandler<DataBinderCore<T>.Subscriber, DependencyPropertyChangedEventArgs, DependencyPropertyChangedEventHandler>, DependencyPropertyChangedEventHandler> <>9__7_2;

                static <>c()
                {
                    DataBinderCore<T>.Subscriber.<>c.<>9 = new DataBinderCore<T>.Subscriber.<>c();
                }

                internal void <get_Handler1>b__5_0(DataBinderCore<T>.Subscriber x, object s, PropertyChangedEventArgs e)
                {
                    x.OnPropertyChanged(s, e);
                }

                internal void <get_Handler1>b__5_1(WeakEventHandler<DataBinderCore<T>.Subscriber, PropertyChangedEventArgs, PropertyChangedEventHandler> wh, object o)
                {
                    ((INotifyPropertyChanged) o).PropertyChanged -= wh.Handler;
                }

                internal PropertyChangedEventHandler <get_Handler1>b__5_2(WeakEventHandler<DataBinderCore<T>.Subscriber, PropertyChangedEventArgs, PropertyChangedEventHandler> wh) => 
                    new PropertyChangedEventHandler(wh.OnEvent);

                internal void <get_Handler2>b__7_0(DataBinderCore<T>.Subscriber x, object s, DependencyPropertyChangedEventArgs e)
                {
                    x.OnDependencyPropertyChanged(s, e);
                }

                internal void <get_Handler2>b__7_1(WeakEventHandler<DataBinderCore<T>.Subscriber, DependencyPropertyChangedEventArgs, DependencyPropertyChangedEventHandler> wh, object o)
                {
                    ((INotifyDependencyPropertyChanged) o).DependencyPropertyChanged -= wh.Handler;
                }

                internal DependencyPropertyChangedEventHandler <get_Handler2>b__7_2(WeakEventHandler<DataBinderCore<T>.Subscriber, DependencyPropertyChangedEventArgs, DependencyPropertyChangedEventHandler> wh) => 
                    new DependencyPropertyChangedEventHandler(wh.OnEvent);
            }
        }
    }
}

