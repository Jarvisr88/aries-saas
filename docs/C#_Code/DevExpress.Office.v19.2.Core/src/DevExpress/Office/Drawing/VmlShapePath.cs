namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Runtime.CompilerServices;

    public class VmlShapePath : ISupportsCopyFrom<VmlShapePath>, ICloneable<VmlShapePath>
    {
        public VmlShapePath()
        {
            this.ConnectType = VmlConnectType.None;
            this.GradientShapeOk = false;
            this.ExtrusionOk = true;
            this.FillOk = true;
            this.StrokeOk = true;
            this.ShadowOk = true;
            this.ConnectionSites = string.Empty;
            this.ConnectionAngles = string.Empty;
            this.TextboxRect = string.Empty;
        }

        public VmlShapePath Clone()
        {
            VmlShapePath path = new VmlShapePath();
            path.CopyFrom(this);
            return path;
        }

        public void CopyFrom(VmlShapePath source)
        {
            this.ConnectType = source.ConnectType;
            this.GradientShapeOk = source.GradientShapeOk;
            this.ExtrusionOk = source.ExtrusionOk;
            this.ShadowOk = source.ShadowOk;
            this.FillOk = source.FillOk;
            this.StrokeOk = source.StrokeOk;
            this.ConnectionSites = source.ConnectionSites;
            this.ConnectionAngles = source.ConnectionAngles;
            this.TextboxRect = source.TextboxRect;
            this.Path = source.Path;
            this.LimoX = source.LimoX;
            this.LimoY = source.LimoY;
        }

        public VmlConnectType ConnectType { get; set; }

        public bool GradientShapeOk { get; set; }

        public bool ExtrusionOk { get; set; }

        public bool FillOk { get; set; }

        public bool StrokeOk { get; set; }

        public bool ShadowOk { get; set; }

        public string ConnectionSites { get; set; }

        public string ConnectionAngles { get; set; }

        public string TextboxRect { get; set; }

        public string Path { get; set; }

        public int LimoX { get; set; }

        public int LimoY { get; set; }
    }
}

