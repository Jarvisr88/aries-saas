namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Core.Native;
    using System.Windows;

    public class RenderButtonContainer : RenderRealContentPresenter
    {
        protected override FrameworkElement CreateFrameworkElement(FrameworkRenderElementContext context) => 
            new ButtonContainer();
    }
}

