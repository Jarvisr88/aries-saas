namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    public abstract class RenderBindingBase : RenderTriggerBase
    {
        protected RenderBindingBase();

        public IValueConverter Converter { get; set; }

        public object ConverterParamenter { get; set; }

        public string Property { get; set; }

        public System.Windows.DependencyProperty DependencyProperty { get; set; }

        public RenderValueSource ValueSource { get; set; }

        public string SourceName { get; set; }

        public string TargetName { get; set; }

        public string TargetProperty { get; set; }

        private RenderPropertyChangedListener Condition { get; set; }

        protected internal RenderConditionGroup ConditionGroup { get; private set; }
    }
}

