namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Data;

    public abstract class FunctionBindingBehaviorBase : Behavior<DependencyObject>
    {
        protected const string Error_PropertyNotFound = "Cannot find property with name {1} in the {0} class.";
        protected const string Error_SourceMethodNotFound = "FunctionBindingBehaviorBase error: Cannot find function with name {1} in the {0} class.";
        protected const string Error_SourceMethodReturnVoid = "The return value of the '{0}.{1}' function can't be void.";
        public static readonly DependencyProperty SourceProperty;
        public static readonly DependencyProperty TargetProperty;
        public static readonly DependencyProperty Arg1Property;
        public static readonly DependencyProperty Arg2Property;
        public static readonly DependencyProperty Arg3Property;
        public static readonly DependencyProperty Arg4Property;
        public static readonly DependencyProperty Arg5Property;
        public static readonly DependencyProperty Arg6Property;
        public static readonly DependencyProperty Arg7Property;
        public static readonly DependencyProperty Arg8Property;
        public static readonly DependencyProperty Arg9Property;
        public static readonly DependencyProperty Arg10Property;
        public static readonly DependencyProperty Arg11Property;
        public static readonly DependencyProperty Arg12Property;
        public static readonly DependencyProperty Arg13Property;
        public static readonly DependencyProperty Arg14Property;
        public static readonly DependencyProperty Arg15Property;
        private static readonly Regex argNameRegExp;
        private static readonly Regex argOrderRegExp;

        static FunctionBindingBehaviorBase()
        {
            SourceProperty = DependencyProperty.Register("Source", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnSourceChanged()));
            TargetProperty = DependencyProperty.Register("Target", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnTargetChanged(e)));
            Arg1Property = DependencyProperty.Register("Arg1", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg2Property = DependencyProperty.Register("Arg2", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg3Property = DependencyProperty.Register("Arg3", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg4Property = DependencyProperty.Register("Arg4", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg5Property = DependencyProperty.Register("Arg5", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg6Property = DependencyProperty.Register("Arg6", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg7Property = DependencyProperty.Register("Arg7", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg8Property = DependencyProperty.Register("Arg8", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg9Property = DependencyProperty.Register("Arg9", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg10Property = DependencyProperty.Register("Arg10", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg11Property = DependencyProperty.Register("Arg11", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg12Property = DependencyProperty.Register("Arg12", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg13Property = DependencyProperty.Register("Arg13", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg14Property = DependencyProperty.Register("Arg14", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            Arg15Property = DependencyProperty.Register("Arg15", typeof(object), typeof(FunctionBindingBehaviorBase), new PropertyMetadata(null, (d, e) => ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged()));
            argNameRegExp = new Regex(@"^Arg\d", RegexOptions.Compiled);
            argOrderRegExp = new Regex(@"\d+", RegexOptions.Compiled);
        }

        protected FunctionBindingBehaviorBase()
        {
        }

        protected static bool DefaultMethodInfoChecker(MethodInfo info, Type targetType, string functionName)
        {
            if (info == null)
            {
                Trace.WriteLine(string.Format("FunctionBindingBehaviorBase error: Cannot find function with name {1} in the {0} class.", targetType.Name, functionName));
                return false;
            }
            if (info.ReturnType == typeof(void))
            {
                throw new ArgumentException($"The return value of the '{targetType.Name}.{info.Name}' function can't be void.");
            }
            return true;
        }

        protected static List<ArgInfo> GetArgsInfo(FunctionBindingBehaviorBase instance)
        {
            Func<FieldInfo, bool> predicate = <>c.<>9__91_0;
            if (<>c.<>9__91_0 == null)
            {
                Func<FieldInfo, bool> local1 = <>c.<>9__91_0;
                predicate = <>c.<>9__91_0 = x => argNameRegExp.IsMatch(x.Name);
            }
            Func<FieldInfo, int> keySelector = <>c.<>9__91_1;
            if (<>c.<>9__91_1 == null)
            {
                Func<FieldInfo, int> local2 = <>c.<>9__91_1;
                keySelector = <>c.<>9__91_1 = x => int.Parse(argOrderRegExp.Match(x.Name).Value);
            }
            return (from x in typeof(FunctionBindingBehaviorBase).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).Where<FieldInfo>(predicate).OrderBy<FieldInfo, int>(keySelector) select new ArgInfo(instance, (DependencyProperty) x.GetValue(instance))).ToList<ArgInfo>();
        }

        private object GetAssociatedObjectDataContext()
        {
            Func<FrameworkElement, object> evaluator = <>c.<>9__88_0;
            if (<>c.<>9__88_0 == null)
            {
                Func<FrameworkElement, object> local1 = <>c.<>9__88_0;
                evaluator = <>c.<>9__88_0 = x => x.DataContext;
            }
            object local2 = (base.AssociatedObject as FrameworkElement).With<FrameworkElement, object>(evaluator);
            object local5 = local2;
            if (local2 == null)
            {
                object local3 = local2;
                local5 = (base.AssociatedObject as FrameworkContentElement).With<FrameworkContentElement, object>(<>c.<>9__88_1 ??= x => x.DataContext);
            }
            return local5;
        }

        private static MethodInfo GetMethodInfo(Type objType, string methodName, int argsCount = -1, Type[] argsType = null)
        {
            if (string.IsNullOrWhiteSpace(methodName) || (objType == null))
            {
                return null;
            }
            MethodInfo info = null;
            System.Reflection.BindingFlags bindingAttr = System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Instance;
            Func<Type[], bool> evaluator = <>c.<>9__95_0;
            if (<>c.<>9__95_0 == null)
            {
                Func<Type[], bool> local1 = <>c.<>9__95_0;
                evaluator = <>c.<>9__95_0 = delegate (Type[] x) {
                    if (x.Length == 0)
                    {
                        return true;
                    }
                    Func<Type, bool> predicate = <>c.<>9__95_1;
                    if (<>c.<>9__95_1 == null)
                    {
                        Func<Type, bool> local1 = <>c.<>9__95_1;
                        predicate = <>c.<>9__95_1 = e => e == null;
                    }
                    return x.Any<Type>(predicate);
                };
            }
            if (!argsType.Return<Type[], bool>(evaluator, (<>c.<>9__95_2 ??= () => false)))
            {
                info = objType.GetMethod(methodName, bindingAttr, Type.DefaultBinder, argsType, null);
            }
            return (info ?? TryFindAmongMethods(objType, methodName, argsCount, argsType));
        }

        protected static Action<object> GetObjectPropertySetter(object target, string property, bool? throwExcepctionOnNotFound = new bool?())
        {
            PropertyInfo targetPropertyInfo = ObjectPropertyHelper.GetPropertyInfoSetter(target, property);
            PropertyInfo info1 = targetPropertyInfo;
            if (targetPropertyInfo == null)
            {
                PropertyInfo local1 = targetPropertyInfo;
                info1 = ObjectPropertyHelper.GetPropertyInfo(target, property, System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
            }
            targetPropertyInfo = info1;
            if (targetPropertyInfo != null)
            {
                return delegate (object x) {
                    targetPropertyInfo.SetValue(target, x, null);
                };
            }
            DependencyProperty targetDependencyProperty = ObjectPropertyHelper.GetDependencyProperty(target, property);
            if (targetDependencyProperty != null)
            {
                return delegate (object x) {
                    ((DependencyObject) target).SetValue(targetDependencyProperty, x);
                };
            }
            if (throwExcepctionOnNotFound != null)
            {
                string message = string.Format("Cannot find property with name {1} in the {0} class.", target.GetType().Name, property);
                if (throwExcepctionOnNotFound.Value)
                {
                    throw new ArgumentException(message);
                }
                Trace.WriteLine($"MethodBindingBehaviorBase error: {message}");
            }
            return null;
        }

        protected static object InvokeSourceFunction(object source, string functionName, List<ArgInfo> argsInfo, Func<MethodInfo, Type, string, bool> functionChecker)
        {
            // Unresolved stack state at '000000F2'
        }

        private void OnAssociatedObjectDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Source == null)
            {
                this.UpdateActualSource();
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            (base.AssociatedObject as FrameworkElement).Do<FrameworkElement>(delegate (FrameworkElement x) {
                x.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnAssociatedObjectDataContextChanged);
            });
            (base.AssociatedObject as FrameworkContentElement).Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                x.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnAssociatedObjectDataContextChanged);
            });
            this.UpdateActualSource();
        }

        protected override void OnDetaching()
        {
            (base.AssociatedObject as FrameworkElement).Do<FrameworkElement>(delegate (FrameworkElement x) {
                x.DataContextChanged -= new DependencyPropertyChangedEventHandler(this.OnAssociatedObjectDataContextChanged);
            });
            (base.AssociatedObject as FrameworkContentElement).Do<FrameworkContentElement>(delegate (FrameworkContentElement x) {
                x.DataContextChanged -= new DependencyPropertyChangedEventHandler(this.OnAssociatedObjectDataContextChanged);
            });
            (this.ActualSource as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged x) {
                x.PropertyChanged -= new PropertyChangedEventHandler(this.OnSourceObjectPropertyChanged);
            });
            this.ActualSource = null;
            base.OnDetaching();
        }

        protected virtual void OnResultAffectedPropertyChanged()
        {
        }

        private void OnSourceChanged()
        {
            this.UpdateActualSource();
        }

        private void OnSourceObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == this.ActualFunction)
            {
                this.OnResultAffectedPropertyChanged();
            }
        }

        protected virtual void OnTargetChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
            {
                this.OnResultAffectedPropertyChanged();
            }
        }

        private static MethodInfo TryFindAmongMethods(Type objType, string methodName, int argsCount, Type[] argsType)
        {
            Func<MethodInfo, int> keySelector = <>c.<>9__96_1;
            if (<>c.<>9__96_1 == null)
            {
                Func<MethodInfo, int> local1 = <>c.<>9__96_1;
                keySelector = <>c.<>9__96_1 = o => o.GetParameters().Length;
            }
            IOrderedEnumerable<MethodInfo> source = (from e in objType.GetMethods(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Instance)
                where e.Name == methodName
                select e).OrderBy<MethodInfo, int>(keySelector);
            if ((argsType != null) && ((argsType.Length != 0) && (source.Count<MethodInfo>() > 1)))
            {
                using (IEnumerator<MethodInfo> enumerator = source.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        MethodInfo info = enumerator.Current;
                        if ((argsCount <= -1) || (info.GetParameters().Length >= argsCount))
                        {
                            int count = 0;
                            if (argsType.All<Type>(delegate (Type x) {
                                if (x == null)
                                {
                                    return true;
                                }
                                int index = count;
                                count = index + 1;
                                return info.GetParameters()[index].ParameterType.Equals(x);
                            }))
                            {
                                return info;
                            }
                        }
                    }
                }
            }
            MethodInfo local2 = (argsCount > -1) ? source.FirstOrDefault<MethodInfo>(x => (x.GetParameters().Length >= argsCount)) : null;
            MethodInfo local4 = local2;
            if (local2 == null)
            {
                MethodInfo local3 = local2;
                local4 = source.FirstOrDefault<MethodInfo>();
            }
            return local4;
        }

        protected virtual void UpdateActualSource()
        {
            (this.ActualSource as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged x) {
                x.PropertyChanged -= new PropertyChangedEventHandler(this.OnSourceObjectPropertyChanged);
            });
            object source = this.Source;
            object associatedObjectDataContext = source;
            if (source == null)
            {
                object local1 = source;
                associatedObjectDataContext = this.GetAssociatedObjectDataContext();
            }
            this.ActualSource = associatedObjectDataContext;
            (this.ActualSource as INotifyPropertyChanged).Do<INotifyPropertyChanged>(delegate (INotifyPropertyChanged x) {
                x.PropertyChanged += new PropertyChangedEventHandler(this.OnSourceObjectPropertyChanged);
            });
            this.OnResultAffectedPropertyChanged();
        }

        public object Source
        {
            get => 
                base.GetValue(SourceProperty);
            set => 
                base.SetValue(SourceProperty, value);
        }

        public object Target
        {
            get => 
                base.GetValue(TargetProperty);
            set => 
                base.SetValue(TargetProperty, value);
        }

        public object Arg1
        {
            get => 
                base.GetValue(Arg1Property);
            set => 
                base.SetValue(Arg1Property, value);
        }

        public object Arg2
        {
            get => 
                base.GetValue(Arg2Property);
            set => 
                base.SetValue(Arg2Property, value);
        }

        public object Arg3
        {
            get => 
                base.GetValue(Arg3Property);
            set => 
                base.SetValue(Arg3Property, value);
        }

        public object Arg4
        {
            get => 
                base.GetValue(Arg4Property);
            set => 
                base.SetValue(Arg4Property, value);
        }

        public object Arg5
        {
            get => 
                base.GetValue(Arg5Property);
            set => 
                base.SetValue(Arg5Property, value);
        }

        public object Arg6
        {
            get => 
                base.GetValue(Arg6Property);
            set => 
                base.SetValue(Arg6Property, value);
        }

        public object Arg7
        {
            get => 
                base.GetValue(Arg7Property);
            set => 
                base.SetValue(Arg7Property, value);
        }

        public object Arg8
        {
            get => 
                base.GetValue(Arg8Property);
            set => 
                base.SetValue(Arg8Property, value);
        }

        public object Arg9
        {
            get => 
                base.GetValue(Arg9Property);
            set => 
                base.SetValue(Arg9Property, value);
        }

        public object Arg10
        {
            get => 
                base.GetValue(Arg10Property);
            set => 
                base.SetValue(Arg10Property, value);
        }

        public object Arg11
        {
            get => 
                base.GetValue(Arg11Property);
            set => 
                base.SetValue(Arg11Property, value);
        }

        public object Arg12
        {
            get => 
                base.GetValue(Arg12Property);
            set => 
                base.SetValue(Arg12Property, value);
        }

        public object Arg13
        {
            get => 
                base.GetValue(Arg13Property);
            set => 
                base.SetValue(Arg13Property, value);
        }

        public object Arg14
        {
            get => 
                base.GetValue(Arg14Property);
            set => 
                base.SetValue(Arg14Property, value);
        }

        public object Arg15
        {
            get => 
                base.GetValue(Arg15Property);
            set => 
                base.SetValue(Arg15Property, value);
        }

        protected object ActualSource { get; private set; }

        protected abstract string ActualFunction { get; }

        protected object ActualTarget =>
            this.Target ?? base.AssociatedObject;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FunctionBindingBehaviorBase.<>c <>9 = new FunctionBindingBehaviorBase.<>c();
            public static Func<FrameworkElement, object> <>9__88_0;
            public static Func<FrameworkContentElement, object> <>9__88_1;
            public static Func<FieldInfo, bool> <>9__91_0;
            public static Func<FieldInfo, int> <>9__91_1;
            public static Predicate<FunctionBindingBehaviorBase.ArgInfo> <>9__92_0;
            public static Func<Type, bool> <>9__92_1;
            public static Func<bool> <>9__92_2;
            public static Func<FunctionBindingBehaviorBase.ArgInfo, Type> <>9__92_3;
            public static Func<Type, bool> <>9__95_1;
            public static Func<Type[], bool> <>9__95_0;
            public static Func<bool> <>9__95_2;
            public static Func<MethodInfo, int> <>9__96_1;

            internal void <.cctor>b__98_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnSourceChanged();
            }

            internal void <.cctor>b__98_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnTargetChanged(e);
            }

            internal void <.cctor>b__98_10(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_11(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_12(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_13(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_14(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_15(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_16(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_6(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_7(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_8(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal void <.cctor>b__98_9(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FunctionBindingBehaviorBase) d).OnResultAffectedPropertyChanged();
            }

            internal bool <GetArgsInfo>b__91_0(FieldInfo x) => 
                FunctionBindingBehaviorBase.argNameRegExp.IsMatch(x.Name);

            internal int <GetArgsInfo>b__91_1(FieldInfo x) => 
                int.Parse(FunctionBindingBehaviorBase.argOrderRegExp.Match(x.Name).Value);

            internal object <GetAssociatedObjectDataContext>b__88_0(FrameworkElement x) => 
                x.DataContext;

            internal object <GetAssociatedObjectDataContext>b__88_1(FrameworkContentElement x) => 
                x.DataContext;

            internal bool <GetMethodInfo>b__95_0(Type[] x)
            {
                if (x.Length == 0)
                {
                    return true;
                }
                Func<Type, bool> predicate = <>9__95_1;
                if (<>9__95_1 == null)
                {
                    Func<Type, bool> local1 = <>9__95_1;
                    predicate = <>9__95_1 = e => e == null;
                }
                return x.Any<Type>(predicate);
            }

            internal bool <GetMethodInfo>b__95_1(Type e) => 
                e == null;

            internal bool <GetMethodInfo>b__95_2() => 
                false;

            internal bool <InvokeSourceFunction>b__92_0(FunctionBindingBehaviorBase.ArgInfo x) => 
                !x.IsUnsetValue;

            internal bool <InvokeSourceFunction>b__92_1(Type x) => 
                x.IsAbstract && x.IsSealed;

            internal bool <InvokeSourceFunction>b__92_2() => 
                false;

            internal Type <InvokeSourceFunction>b__92_3(FunctionBindingBehaviorBase.ArgInfo x) => 
                x.Type;

            internal int <TryFindAmongMethods>b__96_1(MethodInfo o) => 
                o.GetParameters().Length;
        }

        protected class ArgInfo
        {
            public ArgInfo(object value)
            {
                this.IsUnsetValue = value == DependencyProperty.UnsetValue;
                if (!this.IsUnsetValue)
                {
                    this.Value = value;
                    Func<object, System.Type> evaluator = <>c.<>9__13_0;
                    if (<>c.<>9__13_0 == null)
                    {
                        Func<object, System.Type> local1 = <>c.<>9__13_0;
                        evaluator = <>c.<>9__13_0 = x => x.GetType();
                    }
                    this.Type = this.Value.With<object, System.Type>(evaluator);
                }
            }

            public ArgInfo(DependencyObject obj, DependencyProperty prop)
            {
                object obj2 = obj.ReadLocalValue(prop);
                this.IsUnsetValue = obj2 == DependencyProperty.UnsetValue;
                if (!this.IsUnsetValue)
                {
                    this.Value = (obj2 is BindingExpression) ? obj.GetValue(prop) : obj2;
                    this.Type = this.Value.With<object, System.Type>(<>c.<>9__12_0 ??= x => x.GetType());
                }
            }

            public object Value { get; private set; }

            public System.Type Type { get; private set; }

            public bool IsUnsetValue { get; private set; }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly FunctionBindingBehaviorBase.ArgInfo.<>c <>9 = new FunctionBindingBehaviorBase.ArgInfo.<>c();
                public static Func<object, Type> <>9__12_0;
                public static Func<object, Type> <>9__13_0;

                internal Type <.ctor>b__12_0(object x) => 
                    x.GetType();

                internal Type <.ctor>b__13_0(object x) => 
                    x.GetType();
            }
        }
    }
}

