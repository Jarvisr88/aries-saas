namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Utils;
    using System;

    public class PresetShadowEffect : IDrawingEffect
    {
        private readonly DrawingColor color;
        private readonly PresetShadowType type;
        private readonly OffsetShadowInfo info;

        public PresetShadowEffect(DrawingColor color, PresetShadowType type, OffsetShadowInfo info)
        {
            Guard.ArgumentNotNull(color, "PresetShadowColor");
            Guard.ArgumentNotNull(info, "OffsetShadowInfo");
            this.color = color;
            this.type = type;
            this.info = info;
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel) => 
            new PresetShadowEffect(this.color.CloneTo(documentModel), this.type, this.info.Clone());

        public override bool Equals(object obj)
        {
            PresetShadowEffect effect = obj as PresetShadowEffect;
            return ((effect != null) ? (this.color.Equals(effect.color) && ((this.type == effect.type) && this.info.Equals(effect.info))) : false);
        }

        public override int GetHashCode() => 
            ((base.GetType().GetHashCode() ^ this.color.GetHashCode()) ^ this.type.GetHashCode()) ^ this.info.GetHashCode();

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingColor Color =>
            this.color;

        public PresetShadowType Type =>
            this.type;

        public OffsetShadowInfo OffsetShadow =>
            this.info;
    }
}

