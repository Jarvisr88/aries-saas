namespace DevExpress.Office.DrawingML
{
    using System;

    public interface IScene3DCamera
    {
        PresetCameraType Preset { get; set; }

        int Fov { get; set; }

        int Zoom { get; set; }

        int Lat { get; set; }

        int Lon { get; set; }

        int Rev { get; set; }

        bool HasRotation { get; }
    }
}

