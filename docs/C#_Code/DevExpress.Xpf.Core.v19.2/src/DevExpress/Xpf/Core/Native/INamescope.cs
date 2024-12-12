namespace DevExpress.Xpf.Core.Native
{
    using System;

    public interface INamescope
    {
        void AddChild(FrameworkRenderElementContext context);
        FrameworkRenderElementContext GetElement(string name);
        void GoToState(string state);
        void RegisterElement(FrameworkRenderElementContext context);
        void ReleaseElement(FrameworkRenderElementContext context);
        void RemoveChild(FrameworkRenderElementContext context);

        RenderTriggerContextCollection Triggers { get; set; }

        FrameworkRenderElementContext RootElement { get; }
    }
}

