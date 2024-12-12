namespace DevExpress.Office.DrawingML
{
    public interface IScene3DProperties : IScene3DCamera, IScene3DLightRig
    {
        IScene3DCamera Camera { get; }

        IScene3DLightRig LightRig { get; }

        DevExpress.Office.DrawingML.BackdropPlane BackdropPlane { get; }
    }
}

