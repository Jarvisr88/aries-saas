namespace DevExpress.Xpf.Editors
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class UITypeEditorValue : BindableBase
    {
        private object originalValue;
        private object value;
        private object content;
        private bool isModified;
        private bool? forcePost;

        public UITypeEditorValue(DependencyObject owner, IEditorSource source, object editValue, object content)
        {
            this.Owner = owner;
            this.Source = source;
            this.originalValue = editValue;
            this.value = editValue;
            this.content = content;
        }

        protected internal virtual bool ShouldPost() => 
            (this.ForcePost != null) ? this.ForcePost.Value : this.isModified;

        public override string ToString()
        {
            if (this.Content != null)
            {
                Func<object, string> func1 = <>c.<>9__29_0;
                if (<>c.<>9__29_0 == null)
                {
                    Func<object, string> local1 = <>c.<>9__29_0;
                    func1 = <>c.<>9__29_0 = x => x.ToString();
                }
                return this.Content.With<object, string>(func1);
            }
            if (!this.IsModified)
            {
                Func<object, string> func2 = <>c.<>9__29_2;
                if (<>c.<>9__29_2 == null)
                {
                    Func<object, string> local2 = <>c.<>9__29_2;
                    func2 = <>c.<>9__29_2 = x => x.ToString();
                }
                return this.OriginalValue.With<object, string>(func2);
            }
            Func<object, string> evaluator = <>c.<>9__29_1;
            if (<>c.<>9__29_1 == null)
            {
                Func<object, string> local3 = <>c.<>9__29_1;
                evaluator = <>c.<>9__29_1 = x => x.ToString();
            }
            return this.Value.With<object, string>(evaluator);
        }

        protected internal DependencyObject Owner { get; private set; }

        protected internal IEditorSource Source { get; private set; }

        public object OriginalValue
        {
            get => 
                this.originalValue;
            private set => 
                base.SetProperty<object>(ref this.originalValue, value, System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UITypeEditorValue)), (MethodInfo) methodof(UITypeEditorValue.get_OriginalValue)), new ParameterExpression[0]));
        }

        public object Value
        {
            get => 
                this.value;
            set => 
                base.SetProperty<object>(ref this.value, value, System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UITypeEditorValue)), (MethodInfo) methodof(UITypeEditorValue.get_Value)), new ParameterExpression[0]), () => this.isModified = true);
        }

        public object Content
        {
            get => 
                this.content;
            set => 
                base.SetProperty<object>(ref this.content, value, System.Linq.Expressions.Expression.Lambda<Func<object>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UITypeEditorValue)), (MethodInfo) methodof(UITypeEditorValue.get_Content)), new ParameterExpression[0]));
        }

        public bool IsModified =>
            this.isModified;

        public bool? ForcePost
        {
            get => 
                this.forcePost;
            set => 
                base.SetProperty<bool?>(ref this.forcePost, value, System.Linq.Expressions.Expression.Lambda<Func<bool?>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(UITypeEditorValue)), (MethodInfo) methodof(UITypeEditorValue.get_ForcePost)), new ParameterExpression[0]));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UITypeEditorValue.<>c <>9 = new UITypeEditorValue.<>c();
            public static Func<object, string> <>9__29_0;
            public static Func<object, string> <>9__29_1;
            public static Func<object, string> <>9__29_2;

            internal string <ToString>b__29_0(object x) => 
                x.ToString();

            internal string <ToString>b__29_1(object x) => 
                x.ToString();

            internal string <ToString>b__29_2(object x) => 
                x.ToString();
        }
    }
}

