namespace DevExpress.Office.DrawingML
{
    using System;

    public interface IScene3DLightRig
    {
        LightRigDirection Direction { get; set; }

        LightRigPreset Preset { get; set; }

        int Lat { get; set; }

        int Lon { get; set; }

        int Rev { get; set; }

        bool HasRotation { get; }
    }
}

