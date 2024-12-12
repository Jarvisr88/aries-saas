namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing.Drawing2D;

    public class PictureImageLayoutInfo : ShapeLayoutInfo
    {
        protected override HitTestInfoCollection CreateHitTestInfosCore(Matrix shadowTransform, bool rotateWithShape)
        {
            HitTestInfoCollection infos = new HitTestInfoCollection();
            PictureImagePathInfo pathInfo = base.Paths.GetPathInfo<PictureImagePathInfo>();
            if (pathInfo != null)
            {
                foreach (HitTestInfo info2 in pathInfo.HitTestInfos)
                {
                    GraphicsPath graphicsPath = (GraphicsPath) info2.GraphicsPath.Clone();
                    if (!rotateWithShape && (base.ShapeTransform != null))
                    {
                        graphicsPath.Transform(base.ShapeTransform);
                    }
                    graphicsPath.Transform(shadowTransform);
                    infos.Add(new HitTestInfo(graphicsPath, info2.HasFill));
                }
            }
            return infos;
        }

        protected internal override PathInfoBase[] GetPathInfos() => 
            base.Paths.GetPathInfos<PictureImagePathInfo>();
    }
}

