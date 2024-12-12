namespace DevExpress.Xpf.Docking
{
    using System;

    public class SaveLayoutCommand : SerializationControllerCommand
    {
        protected override bool CanExecuteCore(object path) => 
            path != null;

        protected override void ExecuteCore(ISerializationController controller, object path)
        {
            controller.SaveLayout(path);
        }
    }
}

