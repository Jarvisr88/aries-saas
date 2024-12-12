namespace DevExpress.Xpf.DXBinding
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public static class DXTriggerHelper
    {
        public static Style CreateStyle<T>(IEnumerable<T> items, DependencyProperty dependencyProperty, Func<T, object> getValue, object defaultValue) where T: DXTriggerBase
        {
            Style style = new Style();
            if (defaultValue != null)
            {
                style.Setters.Add(new Setter(dependencyProperty, defaultValue));
            }
            foreach (T local in items)
            {
                Setter item = new Setter(dependencyProperty, getValue(local));
                if (local.Binding == null)
                {
                    style.Setters.Add(item);
                    continue;
                }
                DataTrigger trigger1 = new DataTrigger();
                trigger1.Binding = local.Binding;
                trigger1.Value = local.Value;
                DataTrigger trigger = trigger1;
                trigger.Setters.Add(item);
                style.Triggers.Add(trigger);
            }
            style.Seal();
            return style;
        }
    }
}

