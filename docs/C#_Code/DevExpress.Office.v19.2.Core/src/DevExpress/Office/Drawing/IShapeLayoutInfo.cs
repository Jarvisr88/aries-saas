namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public interface IShapeLayoutInfo
    {
        Rectangle CalculateReflectionTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize);
        Rectangle CalculateShadowTransformBounds(bool rotateWithShape, EffectAdditionalSize additionalSize);
        HitTestInfoCollection CreateReflectionHitTestInfos(Matrix shadowTransform, bool rotateWithShape);
        HitTestInfoCollection CreateShadowHitTestInfos(Matrix shadowTransform, bool rotateWithShape);
        void Dispose();
        void Draw(Graphics graphics);
        void DrawBlurEffect(Graphics graphics);
        void DrawGlow(Graphics graphics, Color color, EffectAdditionalSize additionalSize, Matrix shapeTransform);
        void DrawReflection(Graphics graphics, bool applyTransform);
        void DrawShadow(Graphics graphics, bool applyTransform);
        Rectangle GetBlurEffectBounds(Matrix parentTransform, bool applyTransform, EffectAdditionalSize additionalSize);
        Rectangle GetBounds();
        Rectangle GetGlowBounds(EffectAdditionalSize additionalSize, Matrix parentTransform);
        GraphicsPath GetPreparedForReflectionFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize);
        GraphicsPath GetPreparedForShadowFigure(Matrix shadowTransform, bool rotateWithShape, EffectAdditionalSize additionalSize);
        bool HitTest(Point logicalPoint);
        bool SelectionHitTest(Point logicalPoint);
        void SetSelectionMargin(int value);
        void SetupHitTestPen(float extendedWidth);

        bool UseForGroupEffects { get; }

        Rectangle InitialBounds { get; }

        Matrix ShapeTransform { get; }

        DevExpress.Office.Drawing.PenInfo PenInfo { get; }

        float PenWidth { get; }

        float ScaleFactor { get; set; }

        PathInfoCollection Paths { get; }

        float GlowSize { get; }

        float BlurSize { get; }

        float SoftEdgesSize { get; }
    }
}

