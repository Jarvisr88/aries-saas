namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;

    internal class DXTriggerBySetter : DXTrigger
    {
        private Collection<object> PreviousValues;

        public DXTriggerBySetter(UIElement owner, DXConditionCollection conditions, TargetPropertyUpdater resolver, DXSetterCollection setters);
        private DependencyProperty GetProperty(string strProperty);
        protected internal override void PerformAction();
        private bool SetOrResetValue(bool isSetValue, int i);
    }
}

