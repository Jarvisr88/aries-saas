namespace DevExpress.Office.Drawing
{
    using System;

    public class ConnectionShapeLayoutInfo : ShapeLayoutInfo
    {
        public ConnectionShapeLayoutInfo(bool useForGroupEffects) : base(useForGroupEffects)
        {
        }

        protected override ShapeEffectBuildHelper GetEffectBuilder() => 
            new ConnectionShapeEffectBuildHelper(this);
    }
}

