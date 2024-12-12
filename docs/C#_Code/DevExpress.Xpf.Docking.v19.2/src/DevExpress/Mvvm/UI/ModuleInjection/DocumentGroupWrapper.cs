namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Xpf.Docking;
    using System;

    public class DocumentGroupWrapper : LayoutGroupBaseWrapper<DocumentGroup, DocumentPanel>
    {
        protected override DocumentPanel CreateChild(object viewModel)
        {
            DocumentPanel child = base.Manager.CreateDocumentPanel();
            base.ConfigureChild(child, viewModel);
            base.Target.PrepareContainerForItemCore(child);
            base.Target.Add(child);
            return child;
        }
    }
}

