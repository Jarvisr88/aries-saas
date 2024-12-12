namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;

    public class LoadingIndicatorButtonInfo : ButtonInfoBase
    {
        static LoadingIndicatorButtonInfo()
        {
            Type forType = typeof(LoadingIndicatorButtonInfo);
            FrameworkContentElement.DefaultStyleKeyProperty.OverrideMetadata(forType, new FrameworkPropertyMetadata(forType));
        }

        protected override ButtonInfoBase CreateClone() => 
            new LoadingIndicatorButtonInfo();

        protected override List<DependencyProperty> CreateCloneProperties()
        {
            List<DependencyProperty> list = base.CreateCloneProperties();
            list.Add(ToolTipService.ShowDurationProperty);
            list.Add(ToolTipService.InitialShowDelayProperty);
            return list;
        }

        protected internal override AutomationPeer GetRenderAutomationPeer() => 
            null;
    }
}

