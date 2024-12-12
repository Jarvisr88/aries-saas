namespace DevExpress.Office.Drawing
{
    using System;

    [Flags]
    public enum GraphicsPathCollectFlags
    {
        None = 0,
        ExcludeInvisiblePaths = 1,
        ExcludeOuterShadow = 4,
        ExcludeInnerShadow = 8,
        ExcludeGlow = 0x10,
        ExcludeReflection = 0x20,
        ExcludeConnectors = 0x40,
        ExcludeTextRectangle = 0x80,
        ExcludeSoftEdges = 0x100,
        ExcludeBlur = 0x200,
        FigureWithoutEffects = 60
    }
}

