﻿namespace XamlGeneratedNamespace
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Windows.Markup;

    [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public sealed class GeneratedInternalTypeHelper : InternalTypeHelper
    {
        protected override void AddEventHandler(EventInfo eventInfo, object target, Delegate handler)
        {
            eventInfo.AddEventHandler(target, handler);
        }

        protected override Delegate CreateDelegate(Type delegateType, object target, string handler)
        {
            object[] args = new object[] { delegateType, handler };
            return (Delegate) target.GetType().InvokeMember("_CreateDelegate", BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance, null, target, args, null);
        }

        protected override object CreateInstance(Type type, CultureInfo culture) => 
            Activator.CreateInstance(type, BindingFlags.CreateInstance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, null, culture);

        protected override object GetPropertyValue(PropertyInfo propertyInfo, object target, CultureInfo culture) => 
            propertyInfo.GetValue(target, BindingFlags.Default, null, null, culture);

        protected override void SetPropertyValue(PropertyInfo propertyInfo, object target, object value, CultureInfo culture)
        {
            propertyInfo.SetValue(target, value, BindingFlags.Default, null, null, culture);
        }
    }
}

