namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Data;

    public class Document : BaseDocument
    {
        static Document()
        {
            new DependencyPropertyRegistrator<Document>().OverrideDefaultStyleKey(FrameworkElement.DefaultStyleKeyProperty);
        }

        protected override bool GetIsChildMenuVisible() => 
            this.GetIsChildMenuVisibleForFloatingElement((base.DocumentPanel == null) || base.DocumentPanel.IsFloating);

        protected override void ProcessMergeActions()
        {
            if (base.LayoutItem != null)
            {
                bool isMergingTarget;
                if (!base.CanMerge)
                {
                    isMergingTarget = false;
                }
                else
                {
                    switch (MDIControllerHelper.GetActualMDIMergeStyle(base.LayoutItem.GetDockLayoutManager(), base.LayoutItem))
                    {
                        case MDIMergeStyle.Default:
                        case MDIMergeStyle.Always:
                            isMergingTarget = true;
                            break;

                        case MDIMergeStyle.Never:
                            isMergingTarget = false;
                            break;

                        case MDIMergeStyle.WhenLoadedOrChildActivated:
                            isMergingTarget = base.IsMergingTarget;
                            break;

                        default:
                            isMergingTarget = base.IsActive;
                            break;
                    }
                }
                if (isMergingTarget)
                {
                    base.BeginMerge();
                }
                else
                {
                    base.UnMerge();
                }
            }
        }

        protected override void Subscribe(BaseLayoutItem item)
        {
            base.Subscribe(item);
            BindingHelper.SetBinding(this, DocumentPanel.MDIMergeStyleProperty, item, DocumentPanel.MDIMergeStyleProperty, BindingMode.OneWay);
        }
    }
}

